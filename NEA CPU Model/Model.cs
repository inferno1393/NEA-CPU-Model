namespace NEA_CPU_Model
{
    public partial class Model : Form
    {
        // attributes
        Processor processor = new Processor();
        RAM RAM = new RAM();

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

        // initializes indexes for ram and register addresses
        static public int ramIndex = 0;
        static public int registerIndex = 0;
  

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

        // constructor
        public Model()
        {
            InitializeComponent();
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
        }

        // parses the instructions and then executes them on appropriate button click
        private void executeBtn_Click(object sender, EventArgs e)
        {
            executeBtn.Enabled = false; // disables the button to prevent spamming

            Process(true); // calls to execute with looping

            executeBtn.Enabled = true; // renables the button ready for next use
        }

        // allows the user to step through the code instruction by instruction on appropriate button click
        private void stepBtn_Click(object sender, EventArgs e)
        {
            stepBtn.Enabled = false; // disables the button to prevent spamming

            Process(false); // calls to execute without looping

            stepBtn.Enabled = true; // renables the button ready for next use
        }

        // allows the user to load the last set of instructions from a text file on appropriate button click
        private void loadBtn_Click(object sender, EventArgs e)
        {
            loadBtn.Enabled = false; // disables the button to prevent spamming

            List<string> instructions = ReadInstructionsFromFile(); // calls a method to load the instructions

            for (int i = 0; i < instructions.Count; i++) // updates the instructions textbox to show the loaded instructions
            {
                instructionsTextBox.Text += instructions[i];
                if (i < instructions.Count - 2)
                {
                    instructionsTextBox.Text += Environment.NewLine; // adds a new line character to the end of each instruction
                }
            }

            loadBtn.Enabled = true; // renables the button ready for next use
        }


        // resets the system back to beginning to be empty on appropriate button click
        private void resetBtn_Click(object sender, EventArgs e)
        {
            resetBtn.Enabled = false; // disables the button to prevent spamming

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

            resetBtn.Enabled = true; // renables the button ready for next use
        }

        // updates the background colours to the value in the colourBox menu on appropriate button click
        private void updateColourBtn_Click(object sender, EventArgs e)
        {
            updateColourBtn.Enabled = false; // disables the button to prevent spamming

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

            updateColourBtn.Enabled = true; // renables the button ready for next use
        }

        // updates RAM index on appropriate button click
        private void updateRamBtn_Click(object sender, EventArgs e)
        {
            updateRamBtn.Enabled = false; // disables the button to prevent spamming

            // tries to convert the input into an integer for use
            bool converted = int.TryParse(ramIndexText.Text, out ramIndex);
            if (!converted) // was not successfully converted
            {
                MessageBox.Show("RAM Start Index value invalid");
            }

            // updates the RAM Addresses in the interface to update to the new indexes
            if (converted) // index was successfully converted so continue to update interface
            {
                for (int i = 0; i < ramAddress.Count(); i++)
                {
                    ramAddress[i].Text = (ramIndex + i).ToString();
                }
            }

            updateRamBtn.Enabled = true; // renables the button ready for next use
        }

        // updates Register index on appropriate button click
        private void updateRegisterBtn_Click(object sender, EventArgs e)
        {
            updateRegisterBtn.Enabled = false; // disables the button to prevent spamming

            // tries to convert the input into an integer for use
            bool converted = int.TryParse(registerIndexText.Text, out registerIndex);
            if (!converted) // not successfully converted
            {
                MessageBox.Show("Register Start Index value invalid");
            }

            // updates the registers in the interface to update to the new indexes
            if (converted) // index was successfully converted so continue to update interface
            {
                for (int i = 0; i < registerAddress.Count(); i++)
                {
                    registerAddress[i].Text = (registerIndex + i).ToString();
                }
            }

            updateRegisterBtn.Enabled = true; // renables the button ready for next use
        }

        private void updateCacheCapacityBtn_Click(object sender, EventArgs e)
        {
            updateCacheCapacityBtn.Enabled = false; // disables the button to prevent spamming

            // tries to convert the input into an integer for use
            bool converted = int.TryParse(cacheCapacity.Text, out Processor.cache.capacity);
            if (!converted)
            {
                MessageBox.Show("Cache Capacity value invalid");
            }
            MessageBox.Show($"{Processor.cache.capacity}");

            updateCacheCapacityBtn.Enabled = true; // renables the button ready for next use
        }

        // creates the instructions from the textbox in the interface, parses and then executes them (if valid)
        private void Process(bool loop)
        {
            // creates the List (and puts the values in the text box into it) and Stack necessary for parsing
            List<string> instructions = instructionsTextBox.Text.Split('\n').ToList<string>();
            StackArray<string> splitInstructions = new StackArray<string>();

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
                WriteInstructionsToFile(instructions); // writes the instructions into a textfile
            }
            else
            {
                MessageBox.Show(parsingOutput);
            }

        }

        // writes the instructions to a textfile
        private void WriteInstructionsToFile(List<string> instructions)
        {
            StreamWriter writer = new StreamWriter("Instructions.txt"); // creates instance of streamwriter for the given file
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

            StreamReader reader = new StreamReader("Instructions.txt"); // creates instance of streamreader for the given file

            while (line != null) // while not reached the end of the file
            {
                line = reader.ReadLine(); // reads each line in turn
                instructions.Add(line); // adds the line to the list of instructions
            }
            reader.Close(); // closes the file to avoid errors
            return instructions; // returns the list of instructions

        }
    }
}