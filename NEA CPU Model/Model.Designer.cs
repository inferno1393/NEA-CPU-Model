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
            Data0 = new TextBox();
            Data1 = new TextBox();
            Data2 = new TextBox();
            Data3 = new TextBox();
            Data4 = new TextBox();
            Data5 = new TextBox();
            Data6 = new TextBox();
            Data7 = new TextBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label10 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            mbrText = new TextBox();
            marText = new TextBox();
            accumulatorText = new TextBox();
            RData0 = new TextBox();
            RData1 = new TextBox();
            RData2 = new TextBox();
            RData3 = new TextBox();
            label11 = new Label();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();
            label18 = new Label();
            label19 = new Label();
            label20 = new Label();
            label21 = new Label();
            label22 = new Label();
            label23 = new Label();
            label24 = new Label();
            label25 = new Label();
            programCounterText = new TextBox();
            label26 = new Label();
            cirText = new TextBox();
            label9 = new Label();
            SuspendLayout();
            // 
            // instructionsTextBox
            // 
            instructionsTextBox.Location = new Point(12, 92);
            instructionsTextBox.Multiline = true;
            instructionsTextBox.Name = "instructionsTextBox";
            instructionsTextBox.Size = new Size(269, 446);
            instructionsTextBox.TabIndex = 1;
            instructionsTextBox.Text = "STR #1, 3\r\nLDR 0, 3\r\nADD 1, 0, 3\r\nHALT";
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
            label4.Location = new Point(1486, 105);
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
            // Data0
            // 
            Data0.Location = new Point(1635, 133);
            Data0.Name = "Data0";
            Data0.ReadOnly = true;
            Data0.Size = new Size(150, 31);
            Data0.TabIndex = 19;
            // 
            // Data1
            // 
            Data1.Location = new Point(1635, 187);
            Data1.Name = "Data1";
            Data1.ReadOnly = true;
            Data1.Size = new Size(150, 31);
            Data1.TabIndex = 20;
            // 
            // Data2
            // 
            Data2.Location = new Point(1635, 245);
            Data2.Name = "Data2";
            Data2.ReadOnly = true;
            Data2.Size = new Size(150, 31);
            Data2.TabIndex = 21;
            // 
            // Data3
            // 
            Data3.Location = new Point(1635, 298);
            Data3.Name = "Data3";
            Data3.ReadOnly = true;
            Data3.Size = new Size(150, 31);
            Data3.TabIndex = 22;
            // 
            // Data4
            // 
            Data4.Location = new Point(1635, 354);
            Data4.Name = "Data4";
            Data4.ReadOnly = true;
            Data4.Size = new Size(150, 31);
            Data4.TabIndex = 23;
            // 
            // Data5
            // 
            Data5.Location = new Point(1635, 410);
            Data5.Name = "Data5";
            Data5.ReadOnly = true;
            Data5.Size = new Size(150, 31);
            Data5.TabIndex = 24;
            // 
            // Data6
            // 
            Data6.Location = new Point(1635, 461);
            Data6.Name = "Data6";
            Data6.ReadOnly = true;
            Data6.Size = new Size(150, 31);
            Data6.TabIndex = 25;
            // 
            // Data7
            // 
            Data7.Location = new Point(1635, 507);
            Data7.Name = "Data7";
            Data7.ReadOnly = true;
            Data7.Size = new Size(150, 31);
            Data7.TabIndex = 26;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(710, 285);
            label6.Name = "label6";
            label6.Size = new Size(83, 25);
            label6.TabIndex = 27;
            label6.Text = "Registers";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(710, 187);
            label7.Name = "label7";
            label7.Size = new Size(112, 25);
            label7.TabIndex = 28;
            label7.Text = "Accumulator";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(888, 190);
            label8.Name = "label8";
            label8.Size = new Size(44, 25);
            label8.TabIndex = 29;
            label8.Text = "ALU";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(1068, 124);
            label10.Name = "label10";
            label10.Size = new Size(35, 25);
            label10.TabIndex = 31;
            label10.Text = "CU";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(1161, 389);
            label12.Name = "label12";
            label12.Size = new Size(59, 25);
            label12.TabIndex = 33;
            label12.Text = "Cache";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(1214, 298);
            label13.Name = "label13";
            label13.Size = new Size(51, 25);
            label13.TabIndex = 34;
            label13.Text = "MAR";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(1216, 193);
            label14.Name = "label14";
            label14.Size = new Size(49, 25);
            label14.TabIndex = 35;
            label14.Text = "MBR";
            // 
            // mbrText
            // 
            mbrText.Location = new Point(1207, 236);
            mbrText.Name = "mbrText";
            mbrText.ReadOnly = true;
            mbrText.Size = new Size(150, 31);
            mbrText.TabIndex = 36;
            // 
            // marText
            // 
            marText.Location = new Point(1207, 334);
            marText.Name = "marText";
            marText.ReadOnly = true;
            marText.Size = new Size(150, 31);
            marText.TabIndex = 37;
            // 
            // accumulatorText
            // 
            accumulatorText.Location = new Point(689, 215);
            accumulatorText.Name = "accumulatorText";
            accumulatorText.ReadOnly = true;
            accumulatorText.Size = new Size(150, 31);
            accumulatorText.TabIndex = 39;
            // 
            // RData0
            // 
            RData0.Location = new Point(753, 323);
            RData0.Name = "RData0";
            RData0.ReadOnly = true;
            RData0.Size = new Size(150, 31);
            RData0.TabIndex = 44;
            // 
            // RData1
            // 
            RData1.Location = new Point(753, 383);
            RData1.Name = "RData1";
            RData1.ReadOnly = true;
            RData1.Size = new Size(150, 31);
            RData1.TabIndex = 45;
            // 
            // RData2
            // 
            RData2.Location = new Point(753, 444);
            RData2.Name = "RData2";
            RData2.ReadOnly = true;
            RData2.Size = new Size(150, 31);
            RData2.TabIndex = 46;
            // 
            // RData3
            // 
            RData3.Location = new Point(753, 507);
            RData3.Name = "RData3";
            RData3.ReadOnly = true;
            RData3.Size = new Size(150, 31);
            RData3.TabIndex = 47;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(689, 337);
            label11.Name = "label11";
            label11.Size = new Size(22, 25);
            label11.TabIndex = 48;
            label11.Text = "0";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(689, 383);
            label15.Name = "label15";
            label15.RightToLeft = RightToLeft.No;
            label15.Size = new Size(22, 25);
            label15.TabIndex = 49;
            label15.Text = "1";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(689, 444);
            label16.Name = "label16";
            label16.Size = new Size(22, 25);
            label16.TabIndex = 50;
            label16.Text = "2";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(689, 510);
            label17.Name = "label17";
            label17.Size = new Size(22, 25);
            label17.TabIndex = 51;
            label17.Text = "3";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(1494, 139);
            label18.Name = "label18";
            label18.Size = new Size(22, 25);
            label18.TabIndex = 52;
            label18.Text = "0";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(1494, 190);
            label19.Name = "label19";
            label19.Size = new Size(22, 25);
            label19.TabIndex = 53;
            label19.Text = "1";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(1494, 251);
            label20.Name = "label20";
            label20.Size = new Size(22, 25);
            label20.TabIndex = 54;
            label20.Text = "2";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(1494, 301);
            label21.Name = "label21";
            label21.Size = new Size(22, 25);
            label21.TabIndex = 55;
            label21.Text = "3";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(1494, 357);
            label22.Name = "label22";
            label22.Size = new Size(22, 25);
            label22.TabIndex = 56;
            label22.Text = "4";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(1494, 416);
            label23.Name = "label23";
            label23.Size = new Size(22, 25);
            label23.TabIndex = 57;
            label23.Text = "5";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(1494, 461);
            label24.Name = "label24";
            label24.Size = new Size(22, 25);
            label24.TabIndex = 58;
            label24.Text = "6";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(1494, 507);
            label25.Name = "label25";
            label25.Size = new Size(22, 25);
            label25.TabIndex = 59;
            label25.Text = "7";
            // 
            // programCounterText
            // 
            programCounterText.Location = new Point(689, 124);
            programCounterText.Name = "programCounterText";
            programCounterText.ReadOnly = true;
            programCounterText.Size = new Size(150, 31);
            programCounterText.TabIndex = 60;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(689, 82);
            label26.Name = "label26";
            label26.Size = new Size(149, 25);
            label26.TabIndex = 61;
            label26.Text = "Program Counter";
            // 
            // cirText
            // 
            cirText.Location = new Point(1023, 236);
            cirText.Name = "cirText";
            cirText.ReadOnly = true;
            cirText.Size = new Size(150, 31);
            cirText.TabIndex = 38;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(1068, 193);
            label9.Name = "label9";
            label9.Size = new Size(39, 25);
            label9.TabIndex = 30;
            label9.Text = "CIR";
            // 
            // Model
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 1144);
            Controls.Add(label26);
            Controls.Add(programCounterText);
            Controls.Add(label25);
            Controls.Add(label24);
            Controls.Add(label23);
            Controls.Add(label22);
            Controls.Add(label21);
            Controls.Add(label20);
            Controls.Add(label19);
            Controls.Add(label18);
            Controls.Add(label17);
            Controls.Add(label16);
            Controls.Add(label15);
            Controls.Add(label11);
            Controls.Add(RData3);
            Controls.Add(RData2);
            Controls.Add(RData1);
            Controls.Add(RData0);
            Controls.Add(accumulatorText);
            Controls.Add(cirText);
            Controls.Add(marText);
            Controls.Add(mbrText);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(Data7);
            Controls.Add(Data6);
            Controls.Add(Data5);
            Controls.Add(Data4);
            Controls.Add(Data3);
            Controls.Add(Data2);
            Controls.Add(Data1);
            Controls.Add(Data0);
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
        public TextBox Data0;
        public TextBox Data1;
        public TextBox Data2;
        public TextBox Data3;
        public TextBox Data4;
        public TextBox Data5;
        public TextBox Data6;
        public TextBox Data7;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label10;
        private Label label12;
        private Label label13;
        private Label label14;
        private TextBox textBox6;
        private TextBox textBox8;
        private TextBox textBox10;
        private TextBox textBox12;
        public TextBox RData0;
        public TextBox RData1;
        public TextBox RData2;
        public TextBox RData3;
        private Label label11;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        public TextBox programCounterText;
        public TextBox accumulatorText;
        public TextBox cirText;
        private Label label9;
        public TextBox mbrText;
        public TextBox marText;
    }
}
