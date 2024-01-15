namespace NEA_CPU_Model
{
    public partial class Model : Form
    {
        // Attributes


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
            List<string> parsingOutput = parser.ParseInstructions(instructions, splitInstructions);

            // takes the output of the parsing and either sends the instructions to be executed
            // or shows the error to the user
            if (parsingOutput != instructions)
            {
                // code has compiled correctly, execute
                Processor processor = new Processor(instructions);
                processor.Execute(instructions);
            }
            else
            {
                // error in compiling (already shown to user)
                // so do nothing
            }
        }
    }
}
