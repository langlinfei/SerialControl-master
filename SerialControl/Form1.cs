using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SerialControl
{
    public partial class Form1 : Form
    {
        //第一个继电器
        const byte DeviceOpen1 = 0x01;
        const byte DeviceClose1 = 0x81;
        //第二个继电器
        const byte DeviceOpen2 = 0x02;
        const byte DeviceClose2 = 0x82;
        //第三个继电器
        const byte DeviceOpen3 = 0x03;
        const byte DeviceClose3 = 0x83;

        //byte[] SerialPortDataBuffer = new byte[1];

        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Close();
                }
                catch { }
                button1.Text = "打开串口";
            }
            else
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    button1.Text = "关闭串口";
                }
                catch
                {
                    MessageBox.Show("串口打开失败！","提示：");
                }  
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SearchAndAddSerialToComboBox(serialPort1,comboBox1);
        }

        private void SearchAndAddSerialToComboBox(SerialPort MyPort, ComboBox MyBox)  //串口扫描函数
        {
            string Buffer;
            string[] Ports = new string[100];
            MyBox.Items.Clear();
            for (int i = 1; i < 100; i++) 
            {
                try
                {
                    Buffer = "COM" + i.ToString();
                    MyPort.PortName = Buffer;
                    MyPort.Open();
                    MyBox.Items.Add(Buffer);
                    MyPort.Close();
                    Ports[i - 1] = Buffer;
                }
                catch { }

                MyBox.Text = Ports[0];
            }
        }

        private void button2_Click(object sender, EventArgs e)   //串口扫描按钮
        {
            SearchAndAddSerialToComboBox(serialPort1, comboBox1);
        }


        private void WriteByteToSerialPort(byte data) //串口数据发送函数
        {
            byte[] Buffer = new byte[2] {0x00, data};
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Write(Buffer, 0, 2);
                }
                catch
                {
                    MessageBox.Show("串口数据发送错误！", "提示");
                }

            }
            else
            {
                MessageBox.Show("串口未打开","提示");
            }
        }

        private void button3_Click(object sender, EventArgs e)  //第一个继电器开
        {
            WriteByteToSerialPort(DeviceOpen1);
        }

        private void button4_Click(object sender, EventArgs e) //第一个继电器关
        {
            WriteByteToSerialPort(DeviceClose1);
        }

        private void button5_Click(object sender, EventArgs e) //第二个继电器开
        {
            WriteByteToSerialPort(DeviceOpen2);
        }

        private void button6_Click(object sender, EventArgs e) //第二个继电器关
        {
            WriteByteToSerialPort(DeviceClose2);
        }

        private void button7_Click(object sender, EventArgs e) //第三个继电器开
        {
            WriteByteToSerialPort(DeviceOpen3);
        }

        private void button8_Click(object sender, EventArgs e) //第三个继电器关
        {
            WriteByteToSerialPort(DeviceClose3);
        }
    }
}



