using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Using_Async_and_Await2
{
    public partial class Form1 : Form
    {
        public async Task<int> CalculateValueAsync()
        {
            await Task.Delay(5000);
            return 123;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnCalculate_Click(object sender, EventArgs e)
        {
            int value = await CalculateValueAsync();
            lblResult.Text = value.ToString();

            await Task.Delay(5000);
            using (var wc = new WebClient())
            {
                string data = await wc.DownloadStringTaskAsync(@"https://google.com/robots.txt");
                lblResult.Text = data.Split('\n')[0].Trim();
            }
        }
    }
}
