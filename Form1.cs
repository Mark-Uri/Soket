using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Text;
using System.IO;
using WinFormsApp1;



namespace Client3
{

    public partial class Form1: Form
    {
        private readonly ClientService clientService = new ClientService();

        public Form1()
        {
            InitializeComponent();
            _ = InitConnection();
        }


        private async Task InitConnection()
        {
            bool connected = await clientService.ConnectToServerAsync();
            if (!connected)
                txtOutput.Text = "Не удалось подключиться к серверу.";
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnDate_Click(object sender, EventArgs e)
        {
            string response = await clientService.SendRequestAsync("1");
            txtOutput.AppendText(response + Environment.NewLine);
        }

        private async void btnTime_Click(object sender, EventArgs e)
        {
            string response = await clientService.SendRequestAsync("2");
            txtOutput.AppendText(response + Environment.NewLine);
        }

        private async void btnEuro_Click(object sender, EventArgs e)
        {
            string response = await clientService.SendRequestAsync("3");
            txtOutput.AppendText(response + Environment.NewLine);
        }

        private async void btnBitcoin_Click(object sender, EventArgs e)
        {
            string response = await clientService.SendRequestAsync("4");
            txtOutput.AppendText(response + Environment.NewLine);
        }

        private async void btnExit_Click(object sender, EventArgs e)
        {
            clientService.Close();
            Application.Exit();
        }
    }
}
