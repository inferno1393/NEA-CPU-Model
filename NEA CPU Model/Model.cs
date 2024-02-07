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
            Program.model.RData7
        };

        // creates array of avaiable labels for registers
        private Label[] registerAddress =
        {
            Program.model.registerAddress0,
            Program.model.registerAddress1,
            Program.model.registerAddress2,
            Program.model.registerAddress3,
            Program.model.registerAddress4,
            Program.model.registerAddress5,
            Program.model.registerAddress6,
            Program.model.registerAddress7
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
            Program.model.Data15
        };

        // creates array of avaiable labels for RAM
        private Label[] ramAddress = 
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
            Program.model.ramAddress15
        };

        // initializes indexes for ram and register addresses
        static public int ramIndex = 0;
        static public int registerIndex = 0;

        // creates dictionary of accepted colours
        private Dictionary<string, Color> colours = new Dictionary<string, Color>
        {
            {"white", Color.White },
            {"black", Color.Black },
            {"blue", Color.Blue },
            {"red", Color.Red },
            {"green", Color.Green }
        };

        // constructor
        public Model()
        {
            InitializeComponent();
        }

        // parses the instructions and then executes them
        private void executeBtn_Click(object sender, EventArgs e)
        {
            executeBtn.Enabled = false; // disables the button to prevent spamming

            Process(true); // calls to execute with looping
            
            executeBtn.Enabled = true; // renables the button ready for next use
        }

        // allows the user to step through the code instruction by instruction
        private void stepBtn_Click(object sender, EventArgs e)
        {
            stepBtn.Enabled = false; // disables the button to prevent spamming

            Process(false); // calls to execute without looping

            stepBtn.Enabled = true; // renables the button ready for next use
        }

        // allows the user to load instructions from a text file
        private void loadBtn_Click(object sender, EventArgs e)
        {
            loadBtn.Enabled = false; // disables the button to prevent spamming

            List<string> instructions = ReadFromFile();
            for (int i = 0; i < instructions.Count; i++)
            {
                instructionsTextBox.Text += instructions[i];
                if (i < instructions.Count - 2)
                {
                    instructionsTextBox.Text += Environment.NewLine;
                }
            }

            loadBtn.Enabled = true; // renables the button ready for next use
        }

        // creates the instructions from the textbox in the interface, parses and then executes them (if valid)
        private void Process(bool loop)
        {
            IndexRead(); // call for reading then writing the index start points for RAM and register addresses

            // sets background colour to user input
            if (colours.ContainsKey(backColour.Text))
            {
                Program.model.BackColor = colours[backColour.Text];
            }

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
            Parser parser = new Parser(instructions, splitInstructions);
            string parsingOutput = parser.ParseInstructions(instructions, splitInstructions);

            // takes the output of the parsing and either sends the instructions to be executed
            // or shows the error to the user
            if (parsingOutput == "Valid")
            {
                // code has compiled correctly, execute
                processor.Flow(instructions, RAM, loop);
                WriteToFile(instructions); // writes the instructions into a textfile
            }
            else
            {
                MessageBox.Show(parsingOutput);
            }

        }

        // uses the inputted indexes for RAM and Register start points
        // to make the range of addresses changeable
        private void IndexRead()
        {
            // tries to convert inputted indexes to integers for use
            bool converted = false;
            converted = int.TryParse(ramIndexText.Text, out ramIndex);
            if (converted)
            {
                // successfully converted
            }
            else // not successfully converted
            {
                MessageBox.Show("RAM Start Index value invalid");
            }

            converted = int.TryParse(registerIndexText.Text, out registerIndex);
            if (converted)
            {
                // successfully converted
            }
            else // not successfully converted
            {
                MessageBox.Show("Register Start Index value invalid");
            }

            IndexWrite();
        }

        // updates the addresses in the interface to update the new indexes
        private void IndexWrite()
        {
            // updates RAM
            for (int i = 0; i < ramAddress.Count(); i++)
            {
                ramAddress[i].Text = (ramIndex + i).ToString();
            }

            // updates registers
            for (int i = 0; i < registerAddress.Count(); i++)
            {
                registerAddress[i].Text = (registerIndex + i).ToString();
            }
        }


        // writes the instructions to a textfile
        private void WriteToFile(List<string> instructions)
        {
            StreamWriter writer = new StreamWriter("Instructions.txt"); // creates instance of streamwriter for the given file
            for (int i = 0; i < instructions.Count; i++)
            {
                writer.WriteLine(instructions[i]); // adds each instruction to the textfile in turn
            }
            writer.Close(); // closes the file to avoid errors
        }

        // reads the instructions from a textfile
        private List<string> ReadFromFile()
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