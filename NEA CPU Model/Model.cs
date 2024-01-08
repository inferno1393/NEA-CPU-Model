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
        private void Execute()
        {
            // creates the Queue and Stack necessary for parsing
            List<string> instructions = new List<string>();
            StackArray<string> splitInstructions = new StackArray<string>();

            string text = textBox1.Text;
            instructions.Append(text);

            // creates an instance of the Parser class and uses it to check the instructions are valid
            Parser parser = new Parser(instructions, splitInstructions);
            MessageBox.Show(parser.ParseInstructions(instructions, splitInstructions));
        }

        // Executes the code when the appropriate button is clicked by the user
        private void button1_Click(object sender, EventArgs e)
        {
            Execute();
        }
    }
}
