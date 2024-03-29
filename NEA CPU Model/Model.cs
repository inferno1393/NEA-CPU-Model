namespace NEA_CPU_Model
{
    public partial class Model : Form
    {
        // Interface Class, models the calculations for the user to see

        // dictionaries
        // creates dictionary of accepted colours
        private Dictionary<string, Color> colours = new Dictionary<string, Color>
        {
            {"white", Color.White },
            {"blue", Color.Aqua },
            {"red", Color.Crimson },
            {"yellow", Color.LightYellow },
            {"green", Color.Green },
            {"orange", Color.DarkGoldenrod },
            {"violet", Color.Violet },
        };

        // arrays
        // creates array of available text boxes for registers
        public static TextBox[] registersData =
        {
            Program.model.RData0,
            Program.model.RData1,
            Program.model.RData2,
            Program.model.RData3,
            Program.model.RData4,
            Program.model.RData5,
            Program.model.RData6,
            Program.model.RData7,
        };

        // creates array of available labels for registers
        private static Label[] registerAddress =
        {
            Program.model.registerAddress0,
            Program.model.registerAddress1,
            Program.model.registerAddress2,
            Program.model.registerAddress3,
            Program.model.registerAddress4,
            Program.model.registerAddress5,
            Program.model.registerAddress6,
            Program.model.registerAddress7,
        };

        // creates array of available text boxes for RAM
        public static TextBox[] ramData =
        {
            Program.model.Data0,
            Program.model.Data1,
            Program.model.Data2,
            Program.model.Data3,
            Program.model.Data4,
            Program.model.Data5,
            Program.model.Data6,
            Program.model.Data7,
            Program.model.Data8,
            Program.model.Data9,
            Program.model.Data10,
            Program.model.Data11,
            Program.model.Data12,
            Program.model.Data13,
            Program.model.Data14,
            Program.model.Data15,
        };

        // creates array of available labels for RAM
        private static Label[] ramAddress =
        {
            Program.model.ramAddress0,
            Program.model.ramAddress1,
            Program.model.ramAddress2,
            Program.model.ramAddress3,
            Program.model.ramAddress4,
            Program.model.ramAddress5,
            Program.model.ramAddress6,
            Program.model.ramAddress7,
            Program.model.ramAddress8,
            Program.model.ramAddress9,
            Program.model.ramAddress10,
            Program.model.ramAddress11,
            Program.model.ramAddress12,
            Program.model.ramAddress13,
            Program.model.ramAddress14,
            Program.model.ramAddress15,
        };

        // creates array of available text boxes for cache
        public static TextBox[] cacheData =
        {
            Program.model.CData0,
            Program.model.CData1,
            Program.model.CData2,
            Program.model.CData3,
            Program.model.CData4,
            Program.model.CData5,
            Program.model.CData6,
            Program.model.CData7,
        };

        // creates array of available labels for cache
        public static Label[] cacheAddress =
        {
            Program.model.cacheAddress0,
            Program.model.cacheAddress1,
            Program.model.cacheAddress2,
            Program.model.cacheAddress3,
            Program.model.cacheAddress4,
            Program.model.cacheAddress5,
            Program.model.cacheAddress6,
            Program.model.cacheAddress7,
        };

        // attributes
        private Processor processor = new Processor();
        private RAM RAM = new RAM();

        // initializes indexes for ram and register addresses
        public static int ramIndex = 0;
        public static int registerIndex = 0;

        // sets the filename to store the last used colour
        private string ColourFileName = "Colours.txt";

        // sets the default filename to load/write instructions to (although this can be changed by the user in the interface)
        private string InstructionsFileName = "Instructions.txt";


        // constructor
        public Model()
        {
            InitializeComponent();
        }

        // called at runtime on form load
        public void ModelLoad(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size; // matches the screen size to the resolution

            RestoreColour(); // restores the last used colour

            CreateForm(); // creates the form components at run time
        }

        // adds the buttons to the form
        private void CreateForm()
        {
            // initializes constants to be used to add buttons to the interface
            const int xSize = 180;
            const int ySize = 100;
            const int xParam = 24;
            const int yParam = 70;
            const int topRowYPoint = 570;
            const int bottomRowYPoint = 700;
            const int extraRowYPoint = 950;
            
            // sets parameters for and adds each button in turn
            // adds the execute button
            Button executeBtn = new Button();
            executeBtn.Size = new Size(xSize, ySize);
            executeBtn.Location = new Point(24, topRowYPoint);
            executeBtn.BackColor = Color.White;
            executeBtn.Click += new EventHandler(executeBtnClick);
            executeBtn.Text = "Execute";
            Controls.Add(executeBtn);

            // adds the step button
            Button stepBtn = new Button();
            stepBtn.Size = new Size(xSize, ySize);
            stepBtn.Location = new Point(224, topRowYPoint);
            stepBtn.BackColor = Color.White;
            stepBtn.Click += new EventHandler(stepBtnClick);
            stepBtn.Text = "Step";
            Controls.Add(stepBtn);

            // adds the reset button
            Button resetBtn = new Button();
            resetBtn.Size = new Size(xSize, ySize);
            resetBtn.Location = new Point(424, topRowYPoint);
            resetBtn.BackColor = Color.White;
            resetBtn.Click += new EventHandler(resetBtnClick);
            resetBtn.Text = "Reset";
            Controls.Add(resetBtn);

            // adds the load instructions button
            Button loadBtn = new Button();
            loadBtn.Size = new Size(xSize, ySize);
            loadBtn.Location = new Point(100, bottomRowYPoint);
            loadBtn.BackColor = Color.White;
            loadBtn.Click += new EventHandler(loadBtnClick);
            loadBtn.Text = "Load Instructions From File";
            Controls.Add(loadBtn);

            // adds the write instructions button
            Button writeBtn = new Button();
            writeBtn.Size = new Size(xSize, ySize);
            writeBtn.Location = new Point(330, bottomRowYPoint);
            writeBtn.BackColor = Color.White;
            writeBtn.Click += new EventHandler(writeBtnClick);
            writeBtn.Text = "Write Instructions To File";
            Controls.Add(writeBtn);

            // adds the update colour button
            Button updateColourBtn = new Button();
            updateColourBtn.Size = new Size(xSize + 2, ySize);
            updateColourBtn.Location = new Point(12, extraRowYPoint);
            updateColourBtn.BackColor = Color.White;
            updateColourBtn.Click += new EventHandler(updateColourBtnClick);
            updateColourBtn.Text = "Update Colour";
            Controls.Add(updateColourBtn);

            // adds the update RAM index button
            Button updateRamBtn = new Button();
            updateRamBtn.Size = new Size(xSize - 30, ySize);
            updateRamBtn.Location = new Point(232, extraRowYPoint);
            updateRamBtn.BackColor = Color.White;
            updateRamBtn.Click += new EventHandler(updateRamBtnClick);
            updateRamBtn.Text = "Update RAM Index";
            Controls.Add(updateRamBtn);

            // adds the update register index button
            Button updateRegisterBtn = new Button();
            updateRegisterBtn.Size = new Size(xSize - 30, ySize);
            updateRegisterBtn.Location = new Point(422, extraRowYPoint);
            updateRegisterBtn.BackColor = Color.White;
            updateRegisterBtn.Click += new EventHandler(updateRegisterBtnClick);
            updateRegisterBtn.Text = "Update Register Index";
            Controls.Add(updateRegisterBtn);

            // adds the update cache capacity button
            Button updateCacheCapacityBtn = new Button();
            updateCacheCapacityBtn.Size = new Size(xSize - 30, ySize);
            updateCacheCapacityBtn.Location = new Point(617, extraRowYPoint);
            updateCacheCapacityBtn.BackColor = Color.White;
            updateCacheCapacityBtn.Click += new EventHandler(updateCacheCapacityBtnClick);
            updateCacheCapacityBtn.Text = "Update Cache Capacity";
            Controls.Add(updateCacheCapacityBtn);
        }

        // parses the instructions and then executes them on appropriate button click
        private void executeBtnClick(object sender, EventArgs e)
        {
            Process(true); // calls to execute with looping
        }

        // allows the user to step through the code instruction by instruction on appropriate button click
        private void stepBtnClick(object sender, EventArgs e)
        {
            Process(false); // calls to execute without looping
        }

        // allows the user to load the last set of instructions from a text file on appropriate button click
        private void loadBtnClick(object sender, EventArgs e)
        {
            InstructionsFileName = fileNameText.Text; // sets the filename to read from to the inputted file name

            List<string> instructions = ReadInstructionsFromFile(); // calls a method to load the instructions

            for (int i = 0; i < instructions.Count; i++) // updates the instructions text box to show the loaded instructions
            {
                instructionsTextBox.Text += instructions[i];
                if (i < instructions.Count - 2)
                {
                    instructionsTextBox.Text += Environment.NewLine; // adds a new line character to the end of each instruction
                }
            }
        }

        private void writeBtnClick(object sender, EventArgs e)
        {
            // creates the list of instructions and puts the values in the text box into it
            List<string> instructions = instructionsTextBox.Text.Split('\n').ToList<string>();

            InstructionsFileName = fileNameText.Text; // sets the filename to write to to the inputted file name

            WriteInstructionsToFile(instructions); // writes the instructions into a text file
        }

        // resets the system back to beginning to be empty on appropriate button click
        private void resetBtnClick(object sender, EventArgs e)
        {
            RAM.Clear(); // calls a method to clear RAM
            processor.Clear(); // calls a method to clear registers and cache

            // updates the interface to show that the registers have been cleared
            for (int i = 0; i < registersData.Count(); i++)
            {
                registersData[i].Text = "";
            }

            // updates the interface to show that the RAM has been cleared
            for (int i = 0; i < ramData.Count(); i++)
            {
                ramData[i].Text = "";
            }

            // updates the interface to show that the cache has been cleared
            for (int i = 0; i < cacheData.Count(); i++)
            {
                cacheData[i].Text = "";
                cacheAddress[i].Text = "null";
            }

            // resest the program counter to 0
            programCounterText.Text = 0.ToString();
        }

        // updates the background colours to the value in the colourBox menu on appropriate button click
        private void updateColourBtnClick(object sender, EventArgs e)
        {
            // sets background colour to user input
            string text = colourBox.Text.ToLower(); // changes the case to be all lower to avoid being case sensitive
            if (colours.ContainsKey(text)) // checks colour is valid using a dictionary
            {
                Program.model.BackColor = colours[text];
            }
            else // colour is not valid
            {
                MessageBox.Show("Invalid colour");
            }

            // writes the colour to a text file so it can be restored on next use
            StreamWriter writer = new StreamWriter("Colours.txt");
            writer.WriteLine(text); // writes the colour inputted to the file
            writer.Close(); // closes the file to avoid errors
        }

        // updates RAM index on appropriate button click
        private void updateRamBtnClick(object sender, EventArgs e)
        {
            // tries to convert the input into an integer for use
            bool converted = int.TryParse(ramIndexText.Text, out ramIndex);

            // updates the RAM Addresses in the interface to update to the new indexes
            if (converted) // index was successfully converted so continue to update interface
            {
                for (int i = 0; i < ramAddress.Count(); i++)
                {
                    ramAddress[i].Text = (ramIndex + i).ToString();
                }
            }
            else // not successfully converted
            {
                MessageBox.Show("RAM Start Index value invalid");
            }
        }

        // updates Register index on appropriate button click
        private void updateRegisterBtnClick(object sender, EventArgs e)
        {
            // tries to convert the input into an integer for use
            bool converted = int.TryParse(registerIndexText.Text, out registerIndex);

            // updates the registers in the interface to update to the new indexes
            if (converted) // index was successfully converted so continue to update interface
            {
                for (int i = 0; i < registerAddress.Count(); i++)
                {
                    registerAddress[i].Text = (registerIndex + i).ToString();
                }
            }
            else // not successfully converted
            {
                MessageBox.Show("Register Start Index value invalid");
            }
        }

        private void updateCacheCapacityBtnClick(object sender, EventArgs e)
        {
            // tries to convert the input into an integer for use
            bool converted = int.TryParse(cacheCapacity.Text, out Processor.cache.capacity);

            if (!converted)
            {
                MessageBox.Show("Cache Capacity value invalid");
            }
        }

        // creates the instructions from the text box in the interface, parses and then executes them (if valid)
        private void Process(bool loop)
        {
            // creates the list of instructions and puts the values in the text box into it
            List<string> instructions = instructionsTextBox.Text.Split('\n').ToList<string>();

            // removes white space from the instruction
            for (int i = 0; i < instructions.Count; i++)
            {
                instructions[i] = instructions[i].Replace(" ", "");
                instructions[i] = instructions[i].Replace("\r", "");
                instructions[i] = instructions[i].Replace("\n", "");
            }

            // creates an instance of the Parser class and uses it to check the instructions are valid
            Parser parser = new Parser(instructions);
            string parsingOutput = parser.ParseInstructions(instructions);

            // takes the output of the parsing and either sends the instructions to be executed
            // or shows the error to the user
            if (parsingOutput == "Valid")
            {
                // code has compiled correctly, execute
                processor.FlowOfInstructions(instructions, RAM, loop);
            }
            else
            {
                MessageBox.Show(parsingOutput);
            }
        }

        // writes the instructions to a text file
        private void WriteInstructionsToFile(List<string> instructions)
        {
            StreamWriter writer = new StreamWriter(InstructionsFileName); // creates instance of streamwriter for the given file
            for (int i = 0; i < instructions.Count; i++)
            {
                writer.WriteLine(instructions[i]); // adds each instruction to the text file in turn
            }
            writer.Close(); // closes the file to avoid errors
        }

        // reads the instructions from a text file
        private List<string> ReadInstructionsFromFile()
        {
            List<string> instructions = new List<string>();
            string line = string.Empty;

            if (File.Exists(InstructionsFileName))
            {
                StreamReader reader = new StreamReader(InstructionsFileName); // creates instance of streamreader for the given file

                while (line != null) // while not reached the end of the file
                {
                    line = reader.ReadLine(); // reads each line in turn
                    instructions.Add(line); // adds the line to the list of instructions
                }
                reader.Close(); // closes the file to avoid errors
            }
            else
            {
                MessageBox.Show("File Name not valid");
            }
            return instructions; // returns the list of instructions
        }

        private void RestoreColour()
        {
            // updates the colour to be the last used colour
            StreamReader reader = new StreamReader(ColourFileName);
            string line = reader.ReadLine();
            string colour = string.Empty;

            if (line != null) // verifies the file contains a value
            {
                colour = line.ToLower();
            }
            reader.Close(); // closes the file to avoid errors

            BackColor = colours[colour]; // updates the interface to have the correct colour

            colourBox.Text = colour; // updates the interface to show the colour selected
        }
    }
}