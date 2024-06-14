namespace HealthHelper.ViewModels
{
    public class CourseOfTabletCardViewModel : ViewModelBase
    {
        private string _name;
        private string _discription;
        private string _time;

        private readonly ApplicationContext _context;

        public int Id { get; set; }
        public int TabletId { get; set; }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Discription { get => _discription; set { _discription = value; OnPropertyChanged(); } }
        public string Time { get => _time; set { _time = value; OnPropertyChanged(); } }

        public CourseOfTabletCardViewModel(ApplicationContext context)
        {
            _context = context;
        }
    }
}
