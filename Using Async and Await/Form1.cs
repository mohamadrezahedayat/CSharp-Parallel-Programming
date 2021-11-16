using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Using_Async_and_Await
{
    public partial class Form1 : Form
    {
        public Task<int> CalculateValueAsync()
        {
            return Task.Factory.StartNew(() => {
                Thread.Sleep(5000);
                return 123;
            });
        }
        public async Task<int> CalculateValueAsync2()
        {
            await Task.Delay(5000);
            return 123;
        }
        public int CalculateValue()
        {
            Thread.Sleep(5000);
            return 123;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private async void  btnCalculate_Click(object sender, System.EventArgs e)
        {
            /* way 1: blocking code */
            //int n = CalculateValue();
            //lblResult.Text = n.ToString();

            /* way 2: creating task (not blocking)*/
            //var calculation = CalculateValueAsync();
            //calculation.ContinueWith(
            //    t => lblResult.Text = t.Result.ToString(),
            //    TaskScheduler.FromCurrentSynchronizationContext()
            //    );

            /* way 3: using async await */
            int value = await CalculateValueAsync2();
            lblResult.Text = value.ToString();
        }
    }
}
