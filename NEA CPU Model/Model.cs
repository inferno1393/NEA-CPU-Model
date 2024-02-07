namespace NEA_CPU_Model
{
    public partial class Model : Form
    {
        // attributes
        Processor processor = new Processor();
        RAM RAM = new RAM();

        // creates array of avaiable text boxes for registers
        static public TextBox[] registersData =
        {   Program.model.RData0,
            Program.model.RData1,
            Program.model.RData2,
            Program.model.RData3
        };

        // creates array of avaiable text boxes for RAM
        static public TextBox[] ramData =
        {   Program.model.Data0,
            Program.model.Data1,
            Program.model.Data2,
            Program.model.Data3
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
            }

            loadBtn.Enabled = true; // renables the button ready for next use
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
            Parser parser = new Parser(instructions, splitInstructions);
            string parsingOutput = parser.ParseInstructions(instructions, splitInstructions);

            // takes the output of the parsing and either sends the instructions to be executed
            // or shows the error to the user
            if (parsingOutput == "Valid")
            {
                // code has compiled correctly, execute
                processor.Flow(instructions, RAM, loop);
                WriteToFile(instructions);
            }
            else
            {
                MessageBox.Show(parsingOutput);
            }

        }

        // writes the instructions to a textfile
        private void WriteToFile(List<string> instructions)
        {
            StreamWriter writer = new StreamWriter("Instructions.txt");
            for (int i = 0; i < instructions.Count; i++)
            {
                writer.WriteLine(instructions[i] + '\n');
            }
            writer.Close();
        }

        // reads the instructions from a textfile
        private List<string> ReadFromFile()
        {
            List<string> instructions = new List<string>();
            string line = string.Empty;
            StreamReader reader = new StreamReader("Instructions.txt");

            while (line != null)
            {
                line = reader.ReadLine();
                instructions.Add(line);
            }
            reader.Close();
            return instructions;

        }
    }
}