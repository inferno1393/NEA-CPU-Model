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
            string parsingOutput = parser.ParseInstructions(instructions, splitInstructions);

            // takes the output of the parsing and either shows the error to the user 
            // or sends the instructions to be executed
            if(parsingOutput == "Valid")
            {
                MessageBox.Show("WORKS");
            }
            else
            {

                MessageBox.Show(parsingOutput);
            }
        }
    }
}
