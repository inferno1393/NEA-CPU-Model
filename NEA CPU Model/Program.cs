namespace NEA_CPU_Model
{
    internal static class Program
    {
        // The main entry point for the application
        // creates an instance of the model form class	
        public static Model model = new Model();

        [STAThread]

        static void Main()
        {
            // configures and runs the model form
            ApplicationConfiguration.Initialize();
            Application.Run(model);
        }
    }
}