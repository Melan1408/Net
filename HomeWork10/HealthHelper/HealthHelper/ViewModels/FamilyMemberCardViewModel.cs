using HealthHelper.Commands;
using HealthHelper.Views;
using System.Windows;
using System.Windows.Input;

namespace HealthHelper.ViewModels
{
    public class FamilyMemberCardViewModel : ViewModelBase
    {
        private string _name;
        private string _surname;
        private int _age;

        private readonly ApplicationContext _context;
        private Window _mainView;
        public int Id { get; set; }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Surname { get => _surname; set { _surname = value; OnPropertyChanged(); } }
        public int Age { get => _age; set { _age = value; OnPropertyChanged(); } }

        public ICommand ShowCourseOfTablets { get; set; }
        #region Constructor
        public FamilyMemberCardViewModel(ApplicationContext context)
        {    
            _context = context;
            ShowCourseOfTablets = new RelayCommand(OnShowCourseOfTablets, CanShowCourseOfTablets);
        }
        #endregion
        #region Commands
        #region Commands ShowCourseOfTablets
        public void OnShowCourseOfTablets(object p)
        {
            _mainView = Application.Current.MainWindow;
            var courseOfTabletsViewModel = new CourseOfTabletsViewModel(_context, this);
            var courseOfTabletsView = new CourseOfTabletsView();
            courseOfTabletsView.DataContext = courseOfTabletsViewModel;
            courseOfTabletsView.Owner = _mainView;
            courseOfTabletsView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            courseOfTabletsView.ShowDialog();
        }
        public bool CanShowCourseOfTablets(object p)
        {
            return true;
        }
        #endregion
        #endregion
    }
}
