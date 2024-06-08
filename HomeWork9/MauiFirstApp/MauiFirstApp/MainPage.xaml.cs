namespace MauiFirstApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ShowResume(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            string workingDirectory = Directory.GetCurrentDirectory();

            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.Parent.FullName;

            try
            {
                ResumeLabel.Text = File.ReadAllText($"{projectDirectory}/resume.txt");
            }
            catch(Exception ex) 
            {
                ResumeLabel.Text = ex.Message;
            }

            SemanticScreenReader.Announce(ResumeLabel.Text);
        }
            
    }

}
