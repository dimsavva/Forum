namespace BackgroundWorkerExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // You can use a try-catch block to handle an exception
            //try
            //{
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(100);
                backgroundWorker.ReportProgress(i);

                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    backgroundWorker.ReportProgress(0);
                    return;
                }

                // If an exception is thrown it will be added to the RunWorkerCompletedEventArgs.Error
                // and can be accessed in the RunWorkerCompleted event handler.
                // Uncomment the next line to see what happens when a exception is thrown.
                // throw new InvalidOperationException("Random exception.");
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.StackTrace, ex.Message);
            //    If the exception is not thrown, RunWorkerCompletedEventArgs.Error will be null
            //    throw;
            //}

            e.Result = "Completed";
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            progressLabel.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                progressLabel.Text = "Processing cancelled";
            }
            else if (e.Error is not null)
            {
                progressLabel.Text = e.Error.Message;
            }
            else
            {
                progressLabel.Text = e.Result?.ToString();
            }
        }

        private void processButton_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            randomNumberLabel.Text = new Random().Next(0, 100).ToString();
        }
    }
}