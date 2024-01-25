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
            stepBtn = new Button();
            loadBtn = new Button();
            label4 = new Label();
            label5 = new Label();
            Address0 = new TextBox();
            Address1 = new TextBox();
            Address2 = new TextBox();
            Address3 = new TextBox();
            Address4 = new TextBox();
            Address5 = new TextBox();
            Address6 = new TextBox();
            Address7 = new TextBox();
            SuspendLayout();
            // 
            // instructionsTextBox
            // 
            instructionsTextBox.Location = new Point(12, 92);
            instructionsTextBox.Multiline = true;
            instructionsTextBox.Name = "instructionsTextBox";
            instructionsTextBox.Size = new Size(269, 446);
            instructionsTextBox.TabIndex = 1;
            instructionsTextBox.Text = "ADD 1,0,3\r\nSUB 1,0,3\r\nHALT";
            // 
            // executeBtn
            // 
            executeBtn.Location = new Point(12, 554);
            executeBtn.Name = "executeBtn";
            executeBtn.Size = new Size(125, 73);
            executeBtn.TabIndex = 2;
            executeBtn.Text = "Execute";
            executeBtn.UseVisualStyleBackColor = true;
            executeBtn.Click += executeBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(71, 49);
            label1.Name = "label1";
            label1.Size = new Size(79, 25);
            label1.TabIndex = 3;
            label1.Text = "Controls";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(905, 49);
            label2.Name = "label2";
            label2.Size = new Size(45, 25);
            label2.TabIndex = 4;
            label2.Text = "CPU";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1581, 49);
            label3.Name = "label3";
            label3.Size = new Size(51, 25);
            label3.TabIndex = 5;
            label3.Text = "RAM";
            // 
            // stepBtn
            // 
            stepBtn.Location = new Point(156, 554);
            stepBtn.Name = "stepBtn";
            stepBtn.Size = new Size(125, 73);
            stepBtn.TabIndex = 7;
            stepBtn.Text = "Step";
            stepBtn.UseVisualStyleBackColor = true;
            stepBtn.Click += stepBtn_Click;
            // 
            // loadBtn
            // 
            loadBtn.Location = new Point(87, 646);
            loadBtn.Name = "loadBtn";
            loadBtn.Size = new Size(127, 73);
            loadBtn.TabIndex = 8;
            loadBtn.Text = "Load";
            loadBtn.UseVisualStyleBackColor = true;
            loadBtn.Click += loadBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1457, 105);
            label4.Name = "label4";
            label4.Size = new Size(77, 25);
            label4.TabIndex = 9;
            label4.Text = "Address";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1682, 105);
            label5.Name = "label5";
            label5.Size = new Size(49, 25);
            label5.TabIndex = 10;
            label5.Text = "Data";
            // 
            // Address0
            // 
            Address0.Location = new Point(1422, 133);
            Address0.Name = "Address0";
            Address0.Size = new Size(150, 31);
            Address0.TabIndex = 11;
            // 
            // Address1
            // 
            Address1.Location = new Point(1422, 187);
            Address1.Name = "Address1";
            Address1.Size = new Size(150, 31);
            Address1.TabIndex = 12;
            // 
            // Address2
            // 
            Address2.Location = new Point(1422, 245);
            Address2.Name = "Address2";
            Address2.Size = new Size(150, 31);
            Address2.TabIndex = 13;
            // 
            // Address3
            // 
            Address3.Location = new Point(1422, 298);
            Address3.Name = "Address3";
            Address3.Size = new Size(150, 31);
            Address3.TabIndex = 14;
            // 
            // Address4
            // 
            Address4.Location = new Point(1422, 354);
            Address4.Name = "Address4";
            Address4.Size = new Size(150, 31);
            Address4.TabIndex = 15;
            // 
            // Address5
            // 
            Address5.Location = new Point(1422, 410);
            Address5.Name = "Address5";
            Address5.Size = new Size(150, 31);
            Address5.TabIndex = 16;
            // 
            // Address6
            // 
            Address6.Location = new Point(1422, 461);
            Address6.Name = "Address6";
            Address6.Size = new Size(150, 31);
            Address6.TabIndex = 17;
            // 
            // Address7
            // 
            Address7.Location = new Point(1422, 507);
            Address7.Name = "Address7";
            Address7.Size = new Size(150, 31);
            Address7.TabIndex = 18;
            // 
            // Model
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 1144);
            Controls.Add(Address7);
            Controls.Add(Address6);
            Controls.Add(Address5);
            Controls.Add(Address4);
            Controls.Add(Address3);
            Controls.Add(Address2);
            Controls.Add(Address1);
            Controls.Add(Address0);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(loadBtn);
            Controls.Add(stepBtn);
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
        private Button executeBtn;
        private TextBox instructionsTextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button stepBtn;
        private Button loadBtn;
        private Label label4;
        private Label label5;
        private TextBox textBox7;
        private TextBox textBox9;
        public TextBox Address0;
        public TextBox Address1;
        public TextBox Address2;
        public TextBox Address3;
        public TextBox Address4;
        public TextBox Address5;
        public TextBox Address6;
        public TextBox Address7;
    }
}
