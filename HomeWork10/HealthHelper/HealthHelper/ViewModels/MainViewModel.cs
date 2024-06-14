using HealthHelper.Commands;
using HealthHelper.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthHelper.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ApplicationContext _context;
        private readonly MainView _mainView;

        public ICommand DeleteCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }

        public ObservableCollection<FamilyMemberCardViewModel> FamilyMemberList { get; set; }

        #region Constructor

        public MainViewModel(ApplicationContext context, MainView mainView)
        {
            _context = context;
            _context.FamilyMembers.Load();
            FamilyMemberList = new();
            
            AddCommand = new RelayCommand(OnAddCommand, CanAddCommand);
            EditCommand = new RelayCommand(OnEditCommand, CanEditCommand);
            DeleteCommand = new RelayCommand(OnDeleteCommand, CanDeleteCommand);
            _mainView = mainView;
            LoadFamilyMemberList();
        }

        private void LoadFamilyMemberList()
        {
            var familyMemberListBD = _context.FamilyMembers.ToList();

            var familyMemberList = familyMemberListBD.Select(x => new FamilyMemberCardViewModel(_context)
            {
                Name = x.Name,
                Surname = x.Surname,
                Age = x.Age,
                Id = x.Id,
               
            }).ToList();

            familyMemberList.ForEach(x => FamilyMemberList.Add(x));
        }



        #endregion

        #region Commands

        #region Commands AddCommand

        private void OnAddCommand(object p)
        {
            var familyMemberViewModel = new FamilyMemberViewModel(_context, this);
            var familyMemberView = new FamilyMemberView();
            familyMemberView.DataContext = familyMemberViewModel;
            familyMemberView.Owner = _mainView;
            familyMemberView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            familyMemberView.ShowDialog();

        }

        private bool CanAddCommand(object p)
        {
            return true;
        }

        #endregion

        #region Commands EditCommand

        private void OnEditCommand(object selectedItem)
        {
            var familyMember = (FamilyMemberCardViewModel)selectedItem;
            var familyMemberViewModel = new FamilyMemberViewModel(_context, this, true);
            familyMemberViewModel.Name = familyMember.Name;
            familyMemberViewModel.Surname = familyMember.Surname;
            familyMemberViewModel.Age = familyMember.Age;
            familyMemberViewModel.Id = familyMember.Id;
            var familyMemberView = new FamilyMemberView();
            familyMemberView.DataContext = familyMemberViewModel;
            familyMemberView.Owner = _mainView;
            familyMemberView.WindowStartupLocation = WindowStartupLocation.CenterOwner;           
            familyMemberView.ShowDialog();
        }

        private bool CanEditCommand(object selectedItem)
        {
            if (selectedItem is not FamilyMemberCardViewModel familyMember) return false;
            return true;
        }

        #endregion

        #region Commands DeleteCommand

        private void OnDeleteCommand(object selectedItem)
        {
            var familyMemberCard = (FamilyMemberCardViewModel)selectedItem;
            var familyMember = _context.FamilyMembers.SingleOrDefault(x => x.Id == familyMemberCard.Id);
            _context.FamilyMembers.Remove(familyMember);
            _context.SaveChanges();
            FamilyMemberList.Remove(familyMemberCard);
        }

        private bool CanDeleteCommand(object selectedItem)
        {
            if (selectedItem is not FamilyMemberCardViewModel familyMember) return false;
            return true;
        }

        #endregion

        #endregion
    }
}
