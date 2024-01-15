namespace NEA_CPU_Model
{
    partial class Model
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
            instructionsTextBox = new TextBox();
            executeBtn = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            loadBtn = new Button();
            stepBtn = new Button();
            pauseBtn = new Button();
            SuspendLayout();
            // 
            // instructionsTextBox
            // 
            instructionsTextBox.Location = new Point(12, 92);
            instructionsTextBox.Multiline = true;
            instructionsTextBox.Name = "instructionsTextBox";
            instructionsTextBox.Size = new Size(150, 117);
            instructionsTextBox.TabIndex = 1;
            instructionsTextBox.Text = "ADD 1,0,3\r\nSUB 1,0,3\r\nHALT";
            // 
            // executeBtn
            // 
            executeBtn.Location = new Point(12, 227);
            executeBtn.Name = "executeBtn";
            executeBtn.Size = new Size(79, 34);
            executeBtn.TabIndex = 2;
            executeBtn.Text = "Execute";
            executeBtn.UseVisualStyleBackColor = true;
            executeBtn.Click += executeBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 49);
            label1.Name = "label1";
            label1.Size = new Size(79, 25);
            label1.TabIndex = 3;
            label1.Text = "Controls";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(186, 49);
            label2.Name = "label2";
            label2.Size = new Size(45, 25);
            label2.TabIndex = 4;
            label2.Text = "CPU";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(572, 49);
            label3.Name = "label3";
            label3.Size = new Size(51, 25);
            label3.TabIndex = 5;
            label3.Text = "RAM";
            // 
            // loadBtn
            // 
            loadBtn.Location = new Point(92, 227);
            loadBtn.Name = "loadBtn";
            loadBtn.Size = new Size(70, 34);
            loadBtn.TabIndex = 6;
            loadBtn.Text = "Load";
            loadBtn.UseVisualStyleBackColor = true;
            // 
            // stepBtn
            // 
            stepBtn.Location = new Point(12, 267);
            stepBtn.Name = "stepBtn";
            stepBtn.Size = new Size(79, 34);
            stepBtn.TabIndex = 7;
            stepBtn.Text = "Step";
            stepBtn.UseVisualStyleBackColor = true;
            // 
            // pauseBtn
            // 
            pauseBtn.Location = new Point(92, 267);
            pauseBtn.Name = "pauseBtn";
            pauseBtn.Size = new Size(70, 34);
            pauseBtn.TabIndex = 8;
            pauseBtn.Text = "Pause";
            pauseBtn.UseVisualStyleBackColor = true;
            // 
            // Model
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pauseBtn);
            Controls.Add(stepBtn);
            Controls.Add(loadBtn);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(executeBtn);
            Controls.Add(instructionsTextBox);
            Name = "Model";
            Text = "Model";
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion
        private TextBox textBox1;
        private Button executeBtn;
        private TextBox instructionsTextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button loadBtn;
        private Button stepBtn;
        private Button pauseBtn;
    }
}
