using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace Arduino_Serial_Current_Sensor
{
    public partial class CurrentSensor : Form
    {
        // Default settings
        public string comPort = "4";
        public int baudRate = 115200;
        public string fontName = "Roboto";
        public int fontSize = 72;
        public bool truncateCurrent = false;
        public enum VTYPE {
            AMPERAGE,
            VOLTAGE,
            INVALID,
        }

        public class displayType
        {
            public VTYPE vType { get; set; }
            public string vValue { get; set; }
        }

        public CurrentSensor()
        {
            InitializeComponent();
        }

        private SerialPort p;

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();

            for( int i = 0; i < args.Length; i++)
            {
                if( args[i] == "-h")
                {
                    MessageBox.Show(
                        "\t---=== INA219 Meter for OBS ===---\n"
                        + "\t-h\tInformation/Help Page\n"
                        + "\t-p\tCOM Port Number\n"
                        + "\t-b\tBaud rate\n"
                        + "\t\tDefault: 115200\n"
                        + "\t-f\tFont\n"
                        + "\t\tDefault: Roboto\n"
                        + "\t-fs\tFont Size\n"
                        + "\t\tDefault: 72\n"
                        + "\t-vc\tColor of voltage text (Hexadecimal)\n"
                        + "\t\tDefault: #3F51B5\n"
                        + "\t-cc\tColor of current text (Hexadecimal)\n"
                        + "\t\tDefault: #3F51B5\n"
                        + "\t-ck\tColor of background for chroma-keying\n\t\t(Hexadecimal)\n"
                        + "\t\tDefault: #000000\n"
                        + "\t-tc\tTruncate decimal points in mA current range\n"
                        + "\t\tDefault: false\n"
                    );

                    Application.Exit();
                }
                if( args[i] == "-p")
                {
                    comPort = args[i + 1];
                    if ( !UInt16.TryParse(comPort, out UInt16 cp) )
                    {
                        // Non-number entered
                        MessageBox.Show("Invalid COM port argument.\nCheck that you have entered a valid number!");
                        Application.Exit();
                    }
                }
                if (args[i] == "-b")
                {
                    string baud = args[i + 1];
                    if (!Int32.TryParse(baud, out baudRate))
                    {
                        // Non-number entered
                        MessageBox.Show("Invalid baudrate argument.\nCheck that you have entered a valid number!");
                        Application.Exit();
                    }
                }
                if (args[i] == "-f")
                {
                    fontName = args[i + 1];
                }
                if( args[i] == "-fs")
                {
                    fontSize = Int32.Parse(args[i + 1]);
                }
                if ( args[i] == "-vc")
                {
                    // Voltage color (Default: #3F51B5)
                    string vc = args[i + 1];
                    vc = vc.Replace("#", "");
                    voltage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#" + vc);

                }
                if ( args[i] == "-cc")
                {
                    // Current color (default: #3F51B5)
                    string cc = args[i + 1];
                    cc = cc.Replace("#", "");
                    current.ForeColor = System.Drawing.ColorTranslator.FromHtml("#" + cc);
                }
                if ( args[i] == "-ck")
                {
                    // Chroma-key color (Default: #000000)
                    string ck = args[i + 1];
                    ck = ck.Replace("#", "");
                    this.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + ck);
                }
                if( args[i] == "-tc")
                {
                    string cv = args[i + 1].ToLower();
                    if( cv == "false" )
                    {
                        truncateCurrent = false;
                    }
                }
                voltage.Font = new System.Drawing.Font(fontName, fontSize, System.Drawing.FontStyle.Regular);
                current.Font = new System.Drawing.Font(fontName, fontSize, System.Drawing.FontStyle.Regular);
            }

            // Instantiate new SerialPort
            p = new SerialPort(
                "COM" + comPort,
                baudRate,
                Parity.None,
                8,
                StopBits.One
            );

            // Call our port_DataReceived function when event triggered
            p.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            try
            {
                p.Open();
            }
            catch(Exception ex)
            {
                if( ex is System.IO.IOException)
                {
                    MessageBox.Show("Invalid COM port configuration!");
                }
                if( ex is System.UnauthorizedAccessException)
                {
                    MessageBox.Show(
                        "Unable to access COM port."
                        + Environment.NewLine
                        + "Is it in use by something else?"
                   );
                }

                Application.Exit();
            }
        }

        private displayType checkMeasurementType(string m)
        {
            displayType d = new displayType();

            if (m.Length < 2)
            {
                d.vType = VTYPE.INVALID;
                d.vValue = null;

                return d;
            }

            string t = m.Substring(1, m.Length - 2);

            if( m[0] == 'V')
            {
                t += "V";
                d.vType = VTYPE.VOLTAGE;
                d.vValue = t;

                return d;
            }

            if( m[0] == 'C')
            {
                double current = Double.Parse(t);
                string appendix = "mA";

                if (current >= 1000.00)
                {
                    t = (current / 1000.00).ToString();
                    appendix = "A";
                }
                else
                {
                    if(truncateCurrent)
                    {
                        t = Math.Floor(Double.Parse(t)).ToString();
                    }
                }
              
                t += appendix;
                d.vValue = t;
                d.vType = VTYPE.AMPERAGE;

                return d;
            }

            d.vType = VTYPE.INVALID;
            return d;
        } 

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string measurement = p.ReadLine();
            displayType d = checkMeasurementType(measurement);

            if( d.vType != VTYPE.INVALID)
            {
                switch (d.vType)
                {
                    case (VTYPE.VOLTAGE):
                        if (this.IsDisposed || voltage.IsDisposed) { break; }
                        this.Invoke((MethodInvoker)delegate { voltage.Text = d.vValue; });
                        break;
                    case (VTYPE.AMPERAGE):
                        if(this.IsDisposed || current.IsDisposed) { break; }
                        this.Invoke((MethodInvoker)delegate { current.Text = d.vValue; });
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
