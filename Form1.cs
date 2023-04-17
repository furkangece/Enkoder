using EasyModbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        Thread thread;
        public void plc_connect()
        {
            modbusClient = new ModbusClient("10.0.0.41", 502);
            modbusClient.Connect();
            thread = new Thread(new ThreadStart(updscreen));

            thread.Start();
        }

        ModbusClient modbusClient;
        private void connectBtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                plc_connect();
                label1.Text = "Bağlantı Sağlandı.";
            }
            catch
            {
                modbusClient.Disconnect();
                label1.Text = "Bağlantı Sağlanamadı.";
                textBox1.Text = "";
            }

        }
        private void disconnectBtn_Click_1(object sender, EventArgs e)
        {
            thread.Suspend();
            modbusClient.Disconnect();
            label1.Text = "Bağlantı Kesildi.";
            textBox1.Text = "";


        }
        int[] readHoldingRegisters;

        public void updscreen()
        {
            while (true)
            {
                readHoldingRegisters = modbusClient.ReadHoldingRegisters(10, 1);
                double dreadHoldingRegisters = Convert.ToDouble(readHoldingRegisters[0]);
                textBox1.Text = (dreadHoldingRegisters / 1000).ToString();

            }
        }
        }
    }
