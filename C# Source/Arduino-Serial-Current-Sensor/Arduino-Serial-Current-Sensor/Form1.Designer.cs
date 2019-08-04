namespace Arduino_Serial_Current_Sensor
{
    partial class CurrentSensor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.voltage = new System.Windows.Forms.Label();
            this.current = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // voltage
            // 
            this.voltage.AutoSize = true;
            this.voltage.BackColor = System.Drawing.Color.Transparent;
            this.voltage.Dock = System.Windows.Forms.DockStyle.Top;
            this.voltage.Font = new System.Drawing.Font("Roboto Medium", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.voltage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.voltage.Location = new System.Drawing.Point(0, 0);
            this.voltage.Name = "voltage";
            this.voltage.Size = new System.Drawing.Size(0, 115);
            this.voltage.TabIndex = 0;
            // 
            // current
            // 
            this.current.AutoSize = true;
            this.current.BackColor = System.Drawing.Color.Transparent;
            this.current.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.current.Font = new System.Drawing.Font("Roboto Medium", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.current.Location = new System.Drawing.Point(0, 94);
            this.current.Name = "current";
            this.current.Size = new System.Drawing.Size(0, 115);
            this.current.TabIndex = 1;
            // 
            // CurrentSensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(581, 209);
            this.Controls.Add(this.current);
            this.Controls.Add(this.voltage);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CurrentSensor";
            this.Text = "INA219 Current Sensor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label voltage;
        private System.Windows.Forms.Label current;
    }
}

