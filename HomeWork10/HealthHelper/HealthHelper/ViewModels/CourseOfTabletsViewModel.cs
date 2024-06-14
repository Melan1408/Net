using HealthHelper.Commands;
using HealthHelper.Models;
using HealthHelper.Views;
using Microsoft.EntityFrameworkCore;
using RoyT.TimePicker;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthHelper.ViewModels
{
    public class CourseOfTabletsViewModel : ViewModelBase
    {
        private readonly ApplicationContext _context;
        private AnalogueTime _time;

        public AnalogueTime Time { get => _time; set { _time = value; OnPropertyChanged(); } }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public ICommand DeleteCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand AddTabletCommand { get; }
        public ICommand DeleteTabletCommand { get; }
        public ICommand AMPMButtonCommand { get; }

        public ObservableCollection<TabletCardViewModel> TabletList { get; set; }

        public ObservableCollection<CourseOfTabletCardViewModel> FamilyMemberTabletList { get; set; }

        public CourseOfTabletsViewModel(ApplicationContext applicationDbContext, FamilyMemberCardViewModel familyMemberCardViewModel)
        {
            Name = familyMemberCardViewModel.Name;
            Surname = familyMemberCardViewModel.Surname;
            Age = familyMemberCardViewModel.Age;
            Id = familyMemberCardViewModel.Id;
            _context = applicationDbContext;
            TabletList = new();
            FamilyMemberTabletList = new();
            AddCommand = new RelayCommand(OnAddCommand, CanAddCommand);
            EditCommand = new RelayCommand(OnEditCommand, CanEditCommand);
            DeleteCommand = new RelayCommand(OnDeleteCommand, CanDeleteCommand);
            DeleteTabletCommand = new RelayCommand(OnDeleteTabletCommand, CanDeleteTabletCommand);
            AddTabletCommand = new RelayCommand(OnAddTabletCommand, CanAddTabletCommand);
            AMPMButtonCommand = new RelayCommand(OnAMPMButtonCommand, CanAMPMButtonCommand);
            LoadTabletList();
            LoadFamilyMemberTabletList();
        }
        private void LoadTabletList()
        {
            var tabletListBD = _context.Tablets.ToList();

            var tabletList = tabletListBD.Select(x => new TabletCardViewModel(_context)
            {
                Name = x.Name,
                Discription = x.Discription,
                Id = x.Id,

            }).ToList();

            tabletList.ForEach(x => TabletList.Add(x));
        }

        private void LoadFamilyMemberTabletList()
        {
            var familyMembersBD = _context.FamilyMembers
                .Include(familyMembers =>  familyMembers.CourseOfTablet)
                .SingleOrDefault(x => x.Id == Id);
            
            var familyMembers = familyMembersBD.CourseOfTablet.Select(x => new CourseOfTabletCardViewModel(_context)
            {
                Name = x.Tablet.Name,
                Discription = x.Tablet.Discription,
                TabletId = x.Tablet.Id,
                Id = x.Id,
                Time = x.Time,

            }).ToList();

            familyMembers.ForEach(x => FamilyMemberTabletList.Add(x));
        }

        #region Commands

        #region Commands AddCommand

        private void OnAddCommand(object p)
        {
            var _mainView = Application.Current.MainWindow;
            var tabletViewModel = new TabletViewModel(_context, this);
            var tabletView= new TabletView();
            tabletView.DataContext = tabletViewModel;
            tabletView.Owner = _mainView;
            tabletView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            tabletView.ShowDialog();

        }

        private bool CanAddCommand(object p)
        {
            return true;
        }

        #endregion

        #region Commands EditCommand

        private void OnEditCommand(object selectedItem)
        {
            var _mainView = Application.Current.MainWindow;
            var tablet = (TabletCardViewModel)selectedItem;
            var tabletViewModel = new TabletViewModel(_context, this, true);
            tabletViewModel.Name = tablet.Name;
            tabletViewModel.Discription = tablet.Discription;
            tabletViewModel.Id = tablet.Id;
            var tabletView = new TabletView();
            tabletView.DataContext = tabletViewModel;
            tabletView.Owner = _mainView;
            tabletView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            tabletView.ShowDialog();
        }

        private bool CanEditCommand(object selectedItem)
        {
            if (selectedItem is not TabletCardViewModel tablet) return false;
            return true;
        }

        #endregion

        #region Commands DeleteCommand

        private void OnDeleteCommand(object selectedItem)
        {
            var tabletCard = (TabletCardViewModel)selectedItem;
            var tablet = _context.Tablets.SingleOrDefault(x => x.Id == tabletCard.Id);
            _context.Tablets.Remove(tablet);
            _context.SaveChanges();
            TabletList.Remove(tabletCard);
        }

        private bool CanDeleteCommand(object selectedItem)
        {
            if (selectedItem is not TabletCardViewModel tablet) return false;
            return true;
        }

        #endregion

        #region Commands AddTabletCommand

        private void OnAddTabletCommand(object selectedItem)
        {
            var tabletCard = (TabletCardViewModel)selectedItem;
            var tablet = _context.Tablets.SingleOrDefault(x => x.Id == tabletCard.Id);
            var familyMembers = _context.FamilyMembers.SingleOrDefault(x => x.Id == Id);
            var courseOfTablet = new CourseOfTablet { Time = _time.ToString(), Tablet = tablet, FamilyMember = familyMembers };          
            _context.CourseOfTablets.Add(courseOfTablet);
            _context.SaveChanges();
            var courseOfTabletCard = new CourseOfTabletCardViewModel(_context)
            {
                Name = courseOfTablet.Tablet.Name,
                Discription = courseOfTablet.Tablet.Discription,
                Time = _time.ToString(),
                Id = courseOfTablet.Id,
                TabletId = courseOfTablet.Tablet.Id
            };
            FamilyMemberTabletList.Add(courseOfTabletCard);
        }

        private bool CanAddTabletCommand(object selectedItem)
        {
            if (selectedItem is not TabletCardViewModel tablet) return false;
            else if (FamilyMemberTabletList.Any(x => x.TabletId == tablet.Id)) return false;
            return true;
     
        }

        #endregion

        #endregion

        #region Commands DeleteCommand

        private void OnDeleteTabletCommand(object selectedItem)
        {
            var courseOfTabletCard = (CourseOfTabletCardViewModel)selectedItem;
            var courseOfTablet = _context.CourseOfTablets.SingleOrDefault(x => x.Id == courseOfTabletCard.Id);
            var familyMembers = _context.FamilyMembers.SingleOrDefault(x => x.Id == Id);
            _context.CourseOfTablets.Remove(courseOfTablet);
            _context.SaveChanges();
            FamilyMemberTabletList.Remove(courseOfTabletCard);
        }

        private bool CanDeleteTabletCommand(object selectedItem)
        {
            if (selectedItem is not CourseOfTabletCardViewModel courseOfTablet) return false;
            return true;
        }

        #endregion

        #region Commands AMPMButtonCommand

        private void OnAMPMButtonCommand(object selectedItem)
        {

            if (Time.Meridiem == Meridiem.AM)
            {
                Time = new AnalogueTime(Time.Hour, Time.Minute, Meridiem.PM);
            }
            else
            {
                Time = new AnalogueTime(Time.Hour, Time.Minute, Meridiem.AM);
            }
        }

        private bool CanAMPMButtonCommand(object selectedItem)
        {           
            return true;
        }

        #endregion
    }
}
