namespace NEA_CPU_Model
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 

        public static Model model = new Model();

        [STAThread]

        static void Main()
        {
            
            ApplicationConfiguration.Initialize();
            Application.Run(model);
        }
    }
}