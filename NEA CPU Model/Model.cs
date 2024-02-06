namespace NEA_CPU_Model
{
    public partial class Model : Form
    {
        // constructor
        public Model()
        {
            InitializeComponent();
        }

        // parses the instructions and then executes them
        private void executeBtn_Click(object sender, EventArgs e)
        {
            // creates the List (and puts the values in the text box into it) and Stack necessary for parsing
            List<string> instructions = instructionsTextBox.Text.Split('\n').ToList<string>();
            StackArray<string> splitInstructions = new StackArray<string>();

            // creates an instance of the Parser class and uses it to check the instructions are valid
            Parser parser = new Parser(instructions, splitInstructions);
            string parsingOutput = parser.ParseInstructions(instructions, splitInstructions);

            // takes the output of the parsing and either sends the instructions to be executed
            // or shows the error to the user
            if (parsingOutput == "Valid")
            {
                // code has compiled correctly, execute
                Processor processor = new Processor();
                RAM RAM = new RAM();

                // passes the instructions into the processor
                processor.Flow(instructions, RAM, true);

                MessageBox.Show("Execution Complete"); // REMOVE AFTER IT WORKS PLEASE THANKS
            }
            else
            {
                MessageBox.Show(parsingOutput);
            }
        }

        // allows the user to step through the code instruction by instruction
        private void stepBtn_Click(object sender, EventArgs e)
        {
            List<string> instructions = instructionsTextBox.Text.Split('\n').ToList<string>();
            StackArray<string> splitInstructions = new StackArray<string>();

            // creates an instance of the Parser class and uses it to check the instructions are valid
            Parser parser = new Parser(instructions, splitInstructions);
            string parsingOutput = parser.ParseInstructions(instructions, splitInstructions);

            // takes the output of the parsing and either sends the instructions to be executed
            // or shows the error to the user
            if (parsingOutput == "Valid")
            {
                // code has compiled correctly, execute
                Processor processor = new Processor();
                RAM RAM = new RAM();

                // passes the instructions into the processor
                processor.Flow(instructions, RAM, false);

                MessageBox.Show("Executed 1 instruction"); // REMOVE AFTER IT WORKS PLEASE THANKS
            }
            else
            {
                MessageBox.Show(parsingOutput);
            }
        }

        // allows the user to load instructions from a text file
        private void loadBtn_Click(object sender, EventArgs e)
        {
            // yeah its not complete though
            MessageBox.Show("This button does nothing");
        }
    }
}
