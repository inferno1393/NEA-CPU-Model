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
            // creates the Queue and Stack necessary for parsing
            List<string> instructions = new List<string>();
            StackArray<string> splitInstructions = new StackArray<string>();

            // takes the text out of the textbox and adds it to the list of instructions
            string text = instructionsTextBox.Text;
            instructions = text.Split('\n').ToList<string>();
            
            
            // creates an instance of the Parser class and uses it to check the instructions are valid
            Parser parser = new Parser(instructions, splitInstructions);
            MessageBox.Show(parser.ParseInstructions(instructions, splitInstructions));
        }
    }
}
