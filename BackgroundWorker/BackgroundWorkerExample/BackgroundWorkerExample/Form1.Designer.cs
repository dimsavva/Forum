namespace BackgroundWorkerExample
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            processButton = new Button();
            cancelButton = new Button();
            progressBar = new ProgressBar();
            progressLabel = new Label();
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            randomNumberButton = new Button();
            randomNumberLabel = new Label();
            SuspendLayout();
            // 
            // processButton
            // 
            processButton.Location = new Point(12, 12);
            processButton.Name = "processButton";
            processButton.Size = new Size(138, 29);
            processButton.TabIndex = 0;
            processButton.Text = "Process";
            processButton.UseVisualStyleBackColor = true;
            processButton.Click += processButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(174, 12);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(139, 29);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 53);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(301, 29);
            progressBar.TabIndex = 2;
            // 
            // progressLabel
            // 
            progressLabel.AutoSize = true;
            progressLabel.Location = new Point(12, 96);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new Size(29, 20);
            progressLabel.TabIndex = 3;
            progressLabel.Text = "0%";
            // 
            // backgroundWorker
            // 
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            // 
            // randomNumberButton
            // 
            randomNumberButton.Location = new Point(12, 138);
            randomNumberButton.Name = "randomNumberButton";
            randomNumberButton.Size = new Size(138, 29);
            randomNumberButton.TabIndex = 4;
            randomNumberButton.Text = "Random Number";
            randomNumberButton.UseVisualStyleBackColor = true;
            randomNumberButton.Click += button1_Click;
            // 
            // randomNumberLabel
            // 
            randomNumberLabel.AutoSize = true;
            randomNumberLabel.Location = new Point(203, 142);
            randomNumberLabel.Name = "randomNumberLabel";
            randomNumberLabel.Size = new Size(17, 20);
            randomNumberLabel.TabIndex = 5;
            randomNumberLabel.Text = "0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(330, 187);
            Controls.Add(randomNumberLabel);
            Controls.Add(randomNumberButton);
            Controls.Add(progressLabel);
            Controls.Add(progressBar);
            Controls.Add(cancelButton);
            Controls.Add(processButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button processButton;
        private Button cancelButton;
        private ProgressBar progressBar;
        private Label progressLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private Button randomNumberButton;
        private Label randomNumberLabel;
    }
}