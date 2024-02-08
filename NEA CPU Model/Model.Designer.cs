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
            registerAddress0 = new Label();
            registerAddress1 = new Label();
            registerAddress2 = new Label();
            registerAddress3 = new Label();
            ramAddress0 = new Label();
            ramAddress1 = new Label();
            ramAddress2 = new Label();
            ramAddress3 = new Label();
            ramAddress4 = new Label();
            ramAddress5 = new Label();
            ramAddress6 = new Label();
            ramAddress7 = new Label();
            programCounterText = new TextBox();
            label26 = new Label();
            cirText = new TextBox();
            label9 = new Label();
            registerAddress4 = new Label();
            registerAddress5 = new Label();
            registerAddress6 = new Label();
            registerAddress7 = new Label();
            RData4 = new TextBox();
            RData5 = new TextBox();
            RData6 = new TextBox();
            RData7 = new TextBox();
            ramAddress8 = new Label();
            ramAddress9 = new Label();
            ramAddress10 = new Label();
            ramAddress11 = new Label();
            ramAddress12 = new Label();
            ramAddress13 = new Label();
            ramAddress14 = new Label();
            ramAddress15 = new Label();
            Data8 = new TextBox();
            Data9 = new TextBox();
            Data10 = new TextBox();
            Data11 = new TextBox();
            Data12 = new TextBox();
            Data13 = new TextBox();
            Data14 = new TextBox();
            Data15 = new TextBox();
            label39 = new Label();
            label40 = new Label();
            ramIndexText = new TextBox();
            registerIndexText = new TextBox();
            label11 = new Label();
            colourBox = new ComboBox();
            updateColour = new Button();
            updateRam = new Button();
            updateRegister = new Button();
            resetBtn = new Button();
            SuspendLayout();
            // 
            // instructionsTextBox
            // 
            instructionsTextBox.Location = new Point(12, 92);
            instructionsTextBox.Multiline = true;
            instructionsTextBox.Name = "instructionsTextBox";
            instructionsTextBox.Size = new Size(269, 446);
            instructionsTextBox.TabIndex = 1;
            instructionsTextBox.Text = "Enter Instructions Here";
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
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(71, 49);
            label1.Name = "label1";
            label1.Size = new Size(83, 25);
            label1.TabIndex = 3;
            label1.Text = "Controls";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(905, 49);
            label2.Name = "label2";
            label2.Size = new Size(47, 25);
            label2.TabIndex = 4;
            label2.Text = "CPU";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(1581, 49);
            label3.Name = "label3";
            label3.Size = new Size(54, 25);
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
            loadBtn.Location = new Point(12, 651);
            loadBtn.Name = "loadBtn";
            loadBtn.Size = new Size(127, 73);
            loadBtn.TabIndex = 8;
            loadBtn.Text = "Restore Last Instructions";
            loadBtn.UseVisualStyleBackColor = true;
            loadBtn.Click += loadBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.Location = new Point(1486, 105);
            label4.Name = "label4";
            label4.Size = new Size(72, 25);
            label4.TabIndex = 9;
            label4.Text = "Address";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label5.Location = new Point(1667, 105);
            label5.Name = "label5";
            label5.Size = new Size(51, 25);
            label5.TabIndex = 10;
            label5.Text = "Data";
            // 
            // Data0
            // 
            Data0.Location = new Point(1618, 133);
            Data0.Name = "Data0";
            Data0.ReadOnly = true;
            Data0.Size = new Size(150, 31);
            Data0.TabIndex = 19;
            // 
            // Data1
            // 
            Data1.Location = new Point(1618, 184);
            Data1.Name = "Data1";
            Data1.ReadOnly = true;
            Data1.Size = new Size(150, 31);
            Data1.TabIndex = 20;
            // 
            // Data2
            // 
            Data2.Location = new Point(1618, 239);
            Data2.Name = "Data2";
            Data2.ReadOnly = true;
            Data2.Size = new Size(150, 31);
            Data2.TabIndex = 21;
            // 
            // Data3
            // 
            Data3.Location = new Point(1618, 292);
            Data3.Name = "Data3";
            Data3.ReadOnly = true;
            Data3.Size = new Size(150, 31);
            Data3.TabIndex = 22;
            // 
            // Data4
            // 
            Data4.Location = new Point(1618, 351);
            Data4.Name = "Data4";
            Data4.ReadOnly = true;
            Data4.Size = new Size(150, 31);
            Data4.TabIndex = 23;
            // 
            // Data5
            // 
            Data5.Location = new Point(1618, 410);
            Data5.Name = "Data5";
            Data5.ReadOnly = true;
            Data5.Size = new Size(150, 31);
            Data5.TabIndex = 24;
            // 
            // Data6
            // 
            Data6.Location = new Point(1618, 461);
            Data6.Name = "Data6";
            Data6.ReadOnly = true;
            Data6.Size = new Size(150, 31);
            Data6.TabIndex = 25;
            // 
            // Data7
            // 
            Data7.Location = new Point(1618, 529);
            Data7.Name = "Data7";
            Data7.ReadOnly = true;
            Data7.Size = new Size(150, 31);
            Data7.TabIndex = 26;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.Location = new Point(710, 285);
            label6.Name = "label6";
            label6.Size = new Size(81, 25);
            label6.TabIndex = 27;
            label6.Text = "Registers";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.Location = new Point(710, 187);
            label7.Name = "label7";
            label7.Size = new Size(111, 25);
            label7.TabIndex = 28;
            label7.Text = "Accumulator";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label8.Location = new Point(888, 190);
            label8.Name = "label8";
            label8.Size = new Size(43, 25);
            label8.TabIndex = 29;
            label8.Text = "ALU";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label10.Location = new Point(1068, 124);
            label10.Name = "label10";
            label10.Size = new Size(36, 25);
            label10.TabIndex = 31;
            label10.Text = "CU";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label12.Location = new Point(1161, 389);
            label12.Name = "label12";
            label12.Size = new Size(61, 25);
            label12.TabIndex = 33;
            label12.Text = "Cache";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label13.Location = new Point(1214, 298);
            label13.Name = "label13";
            label13.Size = new Size(48, 25);
            label13.TabIndex = 34;
            label13.Text = "MAR";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label14.Location = new Point(1216, 193);
            label14.Name = "label14";
            label14.Size = new Size(47, 25);
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
            RData0.Location = new Point(753, 331);
            RData0.Name = "RData0";
            RData0.ReadOnly = true;
            RData0.Size = new Size(150, 31);
            RData0.TabIndex = 44;
            // 
            // RData1
            // 
            RData1.Location = new Point(753, 386);
            RData1.Name = "RData1";
            RData1.ReadOnly = true;
            RData1.Size = new Size(150, 31);
            RData1.TabIndex = 45;
            // 
            // RData2
            // 
            RData2.Location = new Point(753, 441);
            RData2.Name = "RData2";
            RData2.ReadOnly = true;
            RData2.Size = new Size(150, 31);
            RData2.TabIndex = 46;
            // 
            // RData3
            // 
            RData3.Location = new Point(753, 501);
            RData3.Name = "RData3";
            RData3.ReadOnly = true;
            RData3.Size = new Size(150, 31);
            RData3.TabIndex = 47;
            // 
            // registerAddress0
            // 
            registerAddress0.AutoSize = true;
            registerAddress0.Location = new Point(689, 337);
            registerAddress0.Name = "registerAddress0";
            registerAddress0.Size = new Size(22, 25);
            registerAddress0.TabIndex = 48;
            registerAddress0.Text = "0";
            // 
            // registerAddress1
            // 
            registerAddress1.AutoSize = true;
            registerAddress1.Location = new Point(689, 389);
            registerAddress1.Name = "registerAddress1";
            registerAddress1.RightToLeft = RightToLeft.No;
            registerAddress1.Size = new Size(22, 25);
            registerAddress1.TabIndex = 49;
            registerAddress1.Text = "1";
            // 
            // registerAddress2
            // 
            registerAddress2.AutoSize = true;
            registerAddress2.Location = new Point(689, 447);
            registerAddress2.Name = "registerAddress2";
            registerAddress2.Size = new Size(22, 25);
            registerAddress2.TabIndex = 50;
            registerAddress2.Text = "2";
            // 
            // registerAddress3
            // 
            registerAddress3.AutoSize = true;
            registerAddress3.Location = new Point(689, 507);
            registerAddress3.Name = "registerAddress3";
            registerAddress3.Size = new Size(22, 25);
            registerAddress3.TabIndex = 51;
            registerAddress3.Text = "3";
            // 
            // ramAddress0
            // 
            ramAddress0.AutoSize = true;
            ramAddress0.Location = new Point(1494, 139);
            ramAddress0.Name = "ramAddress0";
            ramAddress0.Size = new Size(22, 25);
            ramAddress0.TabIndex = 52;
            ramAddress0.Text = "0";
            // 
            // ramAddress1
            // 
            ramAddress1.AutoSize = true;
            ramAddress1.Location = new Point(1494, 187);
            ramAddress1.Name = "ramAddress1";
            ramAddress1.Size = new Size(22, 25);
            ramAddress1.TabIndex = 53;
            ramAddress1.Text = "1";
            // 
            // ramAddress2
            // 
            ramAddress2.AutoSize = true;
            ramAddress2.Location = new Point(1494, 242);
            ramAddress2.Name = "ramAddress2";
            ramAddress2.Size = new Size(22, 25);
            ramAddress2.TabIndex = 54;
            ramAddress2.Text = "2";
            // 
            // ramAddress3
            // 
            ramAddress3.AutoSize = true;
            ramAddress3.Location = new Point(1494, 298);
            ramAddress3.Name = "ramAddress3";
            ramAddress3.Size = new Size(22, 25);
            ramAddress3.TabIndex = 55;
            ramAddress3.Text = "3";
            // 
            // ramAddress4
            // 
            ramAddress4.AutoSize = true;
            ramAddress4.Location = new Point(1494, 357);
            ramAddress4.Name = "ramAddress4";
            ramAddress4.Size = new Size(22, 25);
            ramAddress4.TabIndex = 56;
            ramAddress4.Text = "4";
            // 
            // ramAddress5
            // 
            ramAddress5.AutoSize = true;
            ramAddress5.Location = new Point(1494, 416);
            ramAddress5.Name = "ramAddress5";
            ramAddress5.Size = new Size(22, 25);
            ramAddress5.TabIndex = 57;
            ramAddress5.Text = "5";
            // 
            // ramAddress6
            // 
            ramAddress6.AutoSize = true;
            ramAddress6.Location = new Point(1494, 467);
            ramAddress6.Name = "ramAddress6";
            ramAddress6.Size = new Size(22, 25);
            ramAddress6.TabIndex = 58;
            ramAddress6.Text = "6";
            // 
            // ramAddress7
            // 
            ramAddress7.AutoSize = true;
            ramAddress7.Location = new Point(1494, 529);
            ramAddress7.Name = "ramAddress7";
            ramAddress7.Size = new Size(22, 25);
            ramAddress7.TabIndex = 59;
            ramAddress7.Text = "7";
            // 
            // programCounterText
            // 
            programCounterText.Location = new Point(689, 124);
            programCounterText.Name = "programCounterText";
            programCounterText.ReadOnly = true;
            programCounterText.Size = new Size(150, 31);
            programCounterText.TabIndex = 60;
            programCounterText.Text = "0";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label26.Location = new Point(689, 82);
            label26.Name = "label26";
            label26.Size = new Size(147, 25);
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
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label9.Location = new Point(1068, 193);
            label9.Name = "label9";
            label9.Size = new Size(39, 25);
            label9.TabIndex = 30;
            label9.Text = "CIR";
            // 
            // registerAddress4
            // 
            registerAddress4.AutoSize = true;
            registerAddress4.Location = new Point(689, 560);
            registerAddress4.Name = "registerAddress4";
            registerAddress4.Size = new Size(22, 25);
            registerAddress4.TabIndex = 62;
            registerAddress4.Text = "4";
            // 
            // registerAddress5
            // 
            registerAddress5.AutoSize = true;
            registerAddress5.Location = new Point(689, 605);
            registerAddress5.Name = "registerAddress5";
            registerAddress5.Size = new Size(22, 25);
            registerAddress5.TabIndex = 63;
            registerAddress5.Text = "5";
            // 
            // registerAddress6
            // 
            registerAddress6.AutoSize = true;
            registerAddress6.Location = new Point(689, 660);
            registerAddress6.Name = "registerAddress6";
            registerAddress6.Size = new Size(22, 25);
            registerAddress6.TabIndex = 64;
            registerAddress6.Text = "6";
            // 
            // registerAddress7
            // 
            registerAddress7.AutoSize = true;
            registerAddress7.Location = new Point(689, 711);
            registerAddress7.Name = "registerAddress7";
            registerAddress7.Size = new Size(22, 25);
            registerAddress7.TabIndex = 65;
            registerAddress7.Text = "7";
            // 
            // RData4
            // 
            RData4.Location = new Point(753, 554);
            RData4.Name = "RData4";
            RData4.ReadOnly = true;
            RData4.Size = new Size(150, 31);
            RData4.TabIndex = 66;
            // 
            // RData5
            // 
            RData5.Location = new Point(753, 599);
            RData5.Name = "RData5";
            RData5.ReadOnly = true;
            RData5.Size = new Size(150, 31);
            RData5.TabIndex = 67;
            // 
            // RData6
            // 
            RData6.Location = new Point(753, 654);
            RData6.Name = "RData6";
            RData6.ReadOnly = true;
            RData6.Size = new Size(150, 31);
            RData6.TabIndex = 68;
            // 
            // RData7
            // 
            RData7.Location = new Point(753, 711);
            RData7.Name = "RData7";
            RData7.ReadOnly = true;
            RData7.Size = new Size(150, 31);
            RData7.TabIndex = 69;
            // 
            // ramAddress8
            // 
            ramAddress8.AutoSize = true;
            ramAddress8.Location = new Point(1494, 593);
            ramAddress8.Name = "ramAddress8";
            ramAddress8.Size = new Size(22, 25);
            ramAddress8.TabIndex = 70;
            ramAddress8.Text = "8";
            // 
            // ramAddress9
            // 
            ramAddress9.AutoSize = true;
            ramAddress9.Location = new Point(1494, 657);
            ramAddress9.Name = "ramAddress9";
            ramAddress9.Size = new Size(22, 25);
            ramAddress9.TabIndex = 71;
            ramAddress9.Text = "9";
            // 
            // ramAddress10
            // 
            ramAddress10.AutoSize = true;
            ramAddress10.Location = new Point(1494, 708);
            ramAddress10.Name = "ramAddress10";
            ramAddress10.Size = new Size(32, 25);
            ramAddress10.TabIndex = 72;
            ramAddress10.Text = "10";
            // 
            // ramAddress11
            // 
            ramAddress11.AutoSize = true;
            ramAddress11.Location = new Point(1494, 760);
            ramAddress11.Name = "ramAddress11";
            ramAddress11.Size = new Size(32, 25);
            ramAddress11.TabIndex = 73;
            ramAddress11.Text = "11";
            // 
            // ramAddress12
            // 
            ramAddress12.AutoSize = true;
            ramAddress12.Location = new Point(1494, 825);
            ramAddress12.Name = "ramAddress12";
            ramAddress12.Size = new Size(32, 25);
            ramAddress12.TabIndex = 74;
            ramAddress12.Text = "12";
            // 
            // ramAddress13
            // 
            ramAddress13.AutoSize = true;
            ramAddress13.Location = new Point(1494, 894);
            ramAddress13.Name = "ramAddress13";
            ramAddress13.Size = new Size(32, 25);
            ramAddress13.TabIndex = 75;
            ramAddress13.Text = "13";
            // 
            // ramAddress14
            // 
            ramAddress14.AutoSize = true;
            ramAddress14.Location = new Point(1494, 953);
            ramAddress14.Name = "ramAddress14";
            ramAddress14.Size = new Size(32, 25);
            ramAddress14.TabIndex = 76;
            ramAddress14.Text = "14";
            // 
            // ramAddress15
            // 
            ramAddress15.AutoSize = true;
            ramAddress15.Location = new Point(1494, 1009);
            ramAddress15.Name = "ramAddress15";
            ramAddress15.Size = new Size(32, 25);
            ramAddress15.TabIndex = 77;
            ramAddress15.Text = "15";
            // 
            // Data8
            // 
            Data8.Location = new Point(1618, 596);
            Data8.Name = "Data8";
            Data8.ReadOnly = true;
            Data8.Size = new Size(150, 31);
            Data8.TabIndex = 78;
            // 
            // Data9
            // 
            Data9.Location = new Point(1618, 651);
            Data9.Name = "Data9";
            Data9.ReadOnly = true;
            Data9.Size = new Size(150, 31);
            Data9.TabIndex = 79;
            // 
            // Data10
            // 
            Data10.Location = new Point(1618, 705);
            Data10.Name = "Data10";
            Data10.ReadOnly = true;
            Data10.Size = new Size(150, 31);
            Data10.TabIndex = 80;
            // 
            // Data11
            // 
            Data11.Location = new Point(1618, 757);
            Data11.Name = "Data11";
            Data11.ReadOnly = true;
            Data11.Size = new Size(150, 31);
            Data11.TabIndex = 81;
            // 
            // Data12
            // 
            Data12.Location = new Point(1618, 822);
            Data12.Name = "Data12";
            Data12.ReadOnly = true;
            Data12.Size = new Size(150, 31);
            Data12.TabIndex = 82;
            // 
            // Data13
            // 
            Data13.Location = new Point(1618, 891);
            Data13.Name = "Data13";
            Data13.ReadOnly = true;
            Data13.Size = new Size(150, 31);
            Data13.TabIndex = 83;
            // 
            // Data14
            // 
            Data14.Location = new Point(1618, 947);
            Data14.Name = "Data14";
            Data14.ReadOnly = true;
            Data14.Size = new Size(150, 31);
            Data14.TabIndex = 84;
            // 
            // Data15
            // 
            Data15.Location = new Point(1618, 1006);
            Data15.Name = "Data15";
            Data15.ReadOnly = true;
            Data15.Size = new Size(150, 31);
            Data15.TabIndex = 85;
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label39.Location = new Point(210, 866);
            label39.Name = "label39";
            label39.Size = new Size(206, 25);
            label39.TabIndex = 86;
            label39.Text = "RAM Address Start Index:";
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label40.Location = new Point(447, 866);
            label40.Name = "label40";
            label40.Size = new Size(167, 25);
            label40.TabIndex = 87;
            label40.Text = "Register Start Index:";
            // 
            // ramIndexText
            // 
            ramIndexText.Location = new Point(210, 916);
            ramIndexText.Name = "ramIndexText";
            ramIndexText.Size = new Size(150, 31);
            ramIndexText.TabIndex = 88;
            ramIndexText.Text = "0";
            // 
            // registerIndexText
            // 
            registerIndexText.Location = new Point(447, 916);
            registerIndexText.Name = "registerIndexText";
            registerIndexText.Size = new Size(150, 31);
            registerIndexText.TabIndex = 89;
            registerIndexText.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label11.Location = new Point(12, 866);
            label11.Name = "label11";
            label11.Size = new Size(167, 25);
            label11.TabIndex = 90;
            label11.Text = "Background Colour:";
            // 
            // colourBox
            // 
            colourBox.FormattingEnabled = true;
            colourBox.Items.AddRange(new object[] { "White", "Blue", "Red", "Yellow", "Green", "Orange", "Violet" });
            colourBox.Location = new Point(12, 914);
            colourBox.Name = "colourBox";
            colourBox.Size = new Size(182, 33);
            colourBox.TabIndex = 92;
            colourBox.Text = "Select Colour";
            // 
            // updateColour
            // 
            updateColour.Location = new Point(12, 962);
            updateColour.Name = "updateColour";
            updateColour.Size = new Size(182, 75);
            updateColour.TabIndex = 93;
            updateColour.Text = "Update Colour";
            updateColour.UseVisualStyleBackColor = true;
            updateColour.Click += updateColour_Click;
            // 
            // updateRam
            // 
            updateRam.Location = new Point(210, 959);
            updateRam.Name = "updateRam";
            updateRam.Size = new Size(150, 78);
            updateRam.TabIndex = 94;
            updateRam.Text = "Update RAM Index";
            updateRam.UseVisualStyleBackColor = true;
            updateRam.Click += updateRam_Click;
            // 
            // updateRegister
            // 
            updateRegister.Location = new Point(439, 961);
            updateRegister.Name = "updateRegister";
            updateRegister.Size = new Size(158, 75);
            updateRegister.TabIndex = 95;
            updateRegister.Text = "Update Register Index";
            updateRegister.UseVisualStyleBackColor = true;
            updateRegister.Click += updateRegister_Click;
            // 
            // resetBtn
            // 
            resetBtn.Location = new Point(156, 651);
            resetBtn.Name = "resetBtn";
            resetBtn.RightToLeft = RightToLeft.No;
            resetBtn.Size = new Size(125, 73);
            resetBtn.TabIndex = 96;
            resetBtn.Text = "Reset";
            resetBtn.UseVisualStyleBackColor = true;
            resetBtn.Click += resetBtn_Click;
            // 
            // Model
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 1144);
            Controls.Add(resetBtn);
            Controls.Add(updateRegister);
            Controls.Add(updateRam);
            Controls.Add(updateColour);
            Controls.Add(colourBox);
            Controls.Add(label11);
            Controls.Add(registerIndexText);
            Controls.Add(ramIndexText);
            Controls.Add(label40);
            Controls.Add(label39);
            Controls.Add(Data15);
            Controls.Add(Data14);
            Controls.Add(Data13);
            Controls.Add(Data12);
            Controls.Add(Data11);
            Controls.Add(Data10);
            Controls.Add(Data9);
            Controls.Add(Data8);
            Controls.Add(ramAddress15);
            Controls.Add(ramAddress14);
            Controls.Add(ramAddress13);
            Controls.Add(ramAddress12);
            Controls.Add(ramAddress11);
            Controls.Add(ramAddress10);
            Controls.Add(ramAddress9);
            Controls.Add(ramAddress8);
            Controls.Add(RData7);
            Controls.Add(RData6);
            Controls.Add(RData5);
            Controls.Add(RData4);
            Controls.Add(registerAddress7);
            Controls.Add(registerAddress6);
            Controls.Add(registerAddress5);
            Controls.Add(registerAddress4);
            Controls.Add(label26);
            Controls.Add(programCounterText);
            Controls.Add(ramAddress7);
            Controls.Add(ramAddress6);
            Controls.Add(ramAddress5);
            Controls.Add(ramAddress4);
            Controls.Add(ramAddress3);
            Controls.Add(ramAddress2);
            Controls.Add(ramAddress1);
            Controls.Add(ramAddress0);
            Controls.Add(registerAddress3);
            Controls.Add(registerAddress2);
            Controls.Add(registerAddress1);
            Controls.Add(registerAddress0);
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
        private TextBox textBox8;
        private TextBox textBox10;
        private TextBox textBox12;
        public TextBox RData0;
        public TextBox RData1;
        public TextBox RData2;
        public TextBox RData3;
        private Label registerAddress0;
        private Label registerAddress1;
        private Label registerAddress2;
        private Label registerAddress3;
        private Label ramAddress0;
        private Label ramAddress1;
        private Label ramAddress2;
        private Label ramAddress3;
        private Label ramAddress4;
        private Label ramAddress5;
        private Label ramAddress6;
        private Label ramAddress7;
        private Label label26;
        public TextBox programCounterText;
        public TextBox accumulatorText;
        public TextBox cirText;
        private Label label9;
        public TextBox mbrText;
        public TextBox marText;
        private Label registerAddress4;
        private Label registerAddress5;
        private Label registerAddress6;
        private Label registerAddress7;
        public TextBox RData4;
        public TextBox RData5;
        public TextBox RData6;
        public TextBox RData7;
        private Label ramAddress8;
        private Label ramAddress9;
        private Label ramAddress10;
        private Label ramAddress11;
        private Label ramAddress12;
        private Label ramAddress13;
        private Label ramAddress14;
        private Label ramAddress15;
        private TextBox registerIndexText;
        private TextBox textBox4;
        private TextBox textBox7;
        public TextBox Data8;
        public TextBox Data9;
        public TextBox Data10;
        public TextBox Data11;
        public TextBox Data12;
        public TextBox Data13;
        public TextBox Data14;
        public TextBox Data15;
        private Label label39;
        private Label label40;
        private TextBox ramIndexText;
        private Label label11;
        private ComboBox colourBox;
        private Button updateColour;
        private Button updateRam;
        private Button updateRegister;
        private Button resetBtn;
    }
}
