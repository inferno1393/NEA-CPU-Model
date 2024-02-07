namespace NEA_CPU_Model
{
    public partial class Model : Form
    {
        // attributes
        Processor processor = new Processor();
        RAM RAM = new RAM();

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

            // yeah its not complete though
            MessageBox.Show("This button does nothing");

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
            }
            else
            {
                MessageBox.Show(parsingOutput);
            }

        }
    }
}