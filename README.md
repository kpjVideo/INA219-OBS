# INA219 Current Sensor for OBS

This C# program in conjunction with a simple Arduino sketch will allow you to display the voltage/current draw of any load (up to 26V-3.2A) in OBS or any other video editor with chroma key capabilities. This script utilizes the I2C interface on the INA219 breakout board sold by [AdaFruit](https://www.adafruit.com/product/904) and other various online sellers.

## How is this useful?

If you record electronics videos or livestream and would like to display these measurements without shelling out tons of money for a power supply with RS232 or similar, this is a pretty decent solution. (Up to the 26V-3.2A limitiation of the INA219 chip, of course)

### Command-Line Arguments

| Argument | Value | Default |
| ------ | ------ | ------ |
| -h | Information/Help Page | N/A
| -p | COM Port Number | 4
| -b | Serial baud rate | 115200
| -f | Text font | Roboto
| -fs | Font size | 72
| -vc | Color of voltage text (Hexadecimal) | #3F51B5
| -cc | Color of current text (Hexadecimal) | #3F51B5
| -ck | Color of background for chroma-keying (Hexadecimal) | #000000
| -tc | Truncate decimal points in mA current range | false

## Instructions
* Pin Layout
	* SCL pin of INA219 to SCL (A5 on Arduino Nano)
	* SDA pin of INA219 to SDA (A4 on Arduino Nano)
	* Vcc to 5V of Arduino
	* GND to GND of Arduino and PSU
	* Vin(-) to the positive of your load
	* Vin(+) to the positive of your PSU
* Compile & Upload `Arduino-Serial-Current-Sensor.ino` to Arduino
* Ensure Arduino is connected via USB to PC
* Refer to command line arguments to change your desired configuration (including COM port) and execute
* Done :)

## Hardware Requirements
* INA219 DC Current Sensor
* Arduino Nano or compatible

## Software Requirements
* Arduino Editor
* Adafruit_INA219 library
* Wire library

## Screenshot
![Screenshot](https://i.imgur.com/8E7CpFc.png)