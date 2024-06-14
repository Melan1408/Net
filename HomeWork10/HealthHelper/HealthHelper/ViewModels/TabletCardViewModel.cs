namespace HealthHelper.ViewModels
{
    public class TabletCardViewModel : ViewModelBase
    {
        private string _name;
        private string _discription;

        private readonly ApplicationContext _context;

        public int Id { get; set; }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Discription { get => _discription; set { _discription = value; OnPropertyChanged(); } }
      
        public TabletCardViewModel(ApplicationContext context)
        {
            _context = context;           
        }

    }
}
