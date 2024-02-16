namespace NEA_CPU_Model
{
    public partial class Model : Form
    {
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
        // creates array of avaiable text boxes for registers
        static public TextBox[] registersData =
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

        // creates array of avaiable labels for registers
        static private Label[] registerAddress =
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

        // creates array of avaiable text boxes for RAM
        static public TextBox[] ramData =
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

        // creates array of avaiable labels for RAM
        static private Label[] ramAddress =
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

        // creates array of avaiable text boxes for cache
        static public TextBox[] cacheData =
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

        // creates array of avaiable labels for cache
        static public Label[] cacheAddress =
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
        Processor processor = new Processor();
        RAM RAM = new RAM();

        // initializes indexes for ram and register addresses
        static public int ramIndex = 0;
        static public int registerIndex = 0;

        private string ColourFileName = "Colours.txt";
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
            // sets parameters for and adds each button in turn
            // adds the execute button
            Button executeBtn = new Button();
            executeBtn.Size = new Size(180, 100);
            executeBtn.Location = new Point(24, 570);
            executeBtn.BackColor = Color.White;
            executeBtn.Click += new EventHandler(executeBtn_Click);
            executeBtn.Text = "Execute";
            Controls.Add(executeBtn);

            // adds the step button
            Button stepBtn = new Button();
            stepBtn.Size = new Size(180, 100);
            stepBtn.Location = new Point(224, 570);
            stepBtn.BackColor = Color.White;
            stepBtn.Click += new EventHandler(stepBtn_Click);
            stepBtn.Text = "Step";
            Controls.Add(stepBtn);

            // adds the reset button
            Button resetBtn = new Button();
            resetBtn.Size = new Size(180, 100);
            resetBtn.Location = new Point(424, 570);
            resetBtn.BackColor = Color.White;
            resetBtn.Click += new EventHandler(resetBtn_Click);
            resetBtn.Text = "Reset";
            Controls.Add(resetBtn);

            // adds the load instructions button
            Button loadBtn = new Button();
            loadBtn.Size = new Size(180, 100);
            loadBtn.Location = new Point(100, 700);
            loadBtn.BackColor = Color.White;
            loadBtn.Click += new EventHandler(loadBtn_Click);
            loadBtn.Text = "Load Instructions From File";
            Controls.Add(loadBtn);

            // adds the write instructions button
            Button writeBtn = new Button();
            writeBtn.Size = new Size(180, 100);
            writeBtn.Location = new Point(330, 700);
            writeBtn.BackColor = Color.White;
            writeBtn.Click += new EventHandler(writeBtn_Click);
            writeBtn.Text = "Write Instructions To File";
            Controls.Add(writeBtn);

            // adds the update colour button
            Button updateColourBtn = new Button();
            updateColourBtn.Size = new Size(182, 100);
            updateColourBtn.Location = new Point(12, 950);
            updateColourBtn.BackColor = Color.White;
            updateColourBtn.Click += new EventHandler(updateColourBtn_Click);
            updateColourBtn.Text = "Update Colour";
            Controls.Add(updateColourBtn);

            // adds the update RAM index button
            Button updateRamBtn = new Button();
            updateRamBtn.Size = new Size(150, 100);
            updateRamBtn.Location = new Point(232, 950);
            updateRamBtn.BackColor = Color.White;
            updateRamBtn.Click += new EventHandler(updateRamBtn_Click);
            updateRamBtn.Text = "Update RAM Index";
            Controls.Add(updateRamBtn);

            // adds the update register index button
            Button updateRegisterBtn = new Button();
            updateRegisterBtn.Size = new Size(150, 100);
            updateRegisterBtn.Location = new Point(422, 950);
            updateRegisterBtn.BackColor = Color.White;
            updateRegisterBtn.Click += new EventHandler(updateRegisterBtn_Click);
            updateRegisterBtn.Text = "Update Register Index";
            Controls.Add(updateRegisterBtn);

            //adds the update cache capacity button
            Button updateCacheCapacityBtn = new Button();
            updateCacheCapacityBtn.Size = new Size(150, 100);
            updateCacheCapacityBtn.Location = new Point(617, 950);
            updateCacheCapacityBtn.BackColor = Color.White;
            updateCacheCapacityBtn.Click += new EventHandler(updateCacheCapacityBtn_Click);
            updateCacheCapacityBtn.Text = "Update Cache Capacity";
            Controls.Add(updateCacheCapacityBtn);
        }

        // parses the instructions and then executes them on appropriate button click
        private void executeBtn_Click(object sender, EventArgs e)
        {
            Process(true); // calls to execute with looping
        }

        // allows the user to step through the code instruction by instruction on appropriate button click
        private void stepBtn_Click(object sender, EventArgs e)
        {
            Process(false); // calls to execute without looping
        }

        // allows the user to load the last set of instructions from a text file on appropriate button click
        private void loadBtn_Click(object sender, EventArgs e)
        {
            InstructionsFileName = fileNameText.Text; // sets the filename to read from to the inputted file name

            List<string> instructions = ReadInstructionsFromFile(); // calls a method to load the instructions

            for (int i = 0; i < instructions.Count; i++) // updates the instructions textbox to show the loaded instructions
            {
                instructionsTextBox.Text += instructions[i];
                if (i < instructions.Count - 2)
                {
                    instructionsTextBox.Text += Environment.NewLine; // adds a new line character to the end of each instruction
                }
            }
        }

        private void writeBtn_Click(object sender, EventArgs e)
        {
            // creates the list of instructions and puts the values in the text box into it
            List<string> instructions = instructionsTextBox.Text.Split('\n').ToList<string>();

            InstructionsFileName = fileNameText.Text; // sets the filename to write to, to the inputted file name

            WriteInstructionsToFile(instructions);// writes the instructions into a textfile
        }

        // resets the system back to beginning to be empty on appropriate button click
        private void resetBtn_Click(object sender, EventArgs e)
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
        private void updateColourBtn_Click(object sender, EventArgs e)
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
        private void updateRamBtn_Click(object sender, EventArgs e)
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
        private void updateRegisterBtn_Click(object sender, EventArgs e)
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

        private void updateCacheCapacityBtn_Click(object sender, EventArgs e)
        {
            // tries to convert the input into an integer for use
            bool converted = int.TryParse(cacheCapacity.Text, out Processor.cache.capacity);
            if (!converted)
            {
                MessageBox.Show("Cache Capacity value invalid");
            }
        }

        // creates the instructions from the textbox in the interface, parses and then executes them (if valid)
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

        // writes the instructions to a textfile
        private void WriteInstructionsToFile(List<string> instructions)
        {
            StreamWriter writer = new StreamWriter(InstructionsFileName); // creates instance of streamwriter for the given file
            for (int i = 0; i < instructions.Count; i++)
            {
                writer.WriteLine(instructions[i]); // adds each instruction to the textfile in turn
            }
            writer.Close(); // closes the file to avoid errors
        }

        // reads the instructions from a textfile
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