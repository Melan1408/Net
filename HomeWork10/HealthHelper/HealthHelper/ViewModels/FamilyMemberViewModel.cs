using HealthHelper.Commands;
using HealthHelper.Models;
using System.Windows.Input;

namespace HealthHelper.ViewModels
{
    public class FamilyMemberViewModel : ViewModelBase
    {
        private string _name;
        private string _surname;
        private int _age;
        private readonly ApplicationContext _context;
        private MainViewModel _mainViewModel { get; set; }
        private bool _editMode { get; set; }
        public int Id { get; set; }

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Surname { get => _surname; set { _surname = value; OnPropertyChanged(); } }
        public int Age { get => _age; set { _age = value; OnPropertyChanged(); } }

        public ICommand UpsertFamilyMemberCommand { get; set; }

        #region Constructor

        public FamilyMemberViewModel(ApplicationContext applicationDbContext, MainViewModel mainViewModel, bool editMode=false)
        {
            _mainViewModel = mainViewModel;
            _context = applicationDbContext;
            _editMode = editMode;
            UpsertFamilyMemberCommand = new RelayCommand(OnUpsertFamilyMemberCommand, CanUpsertFamilyMemberCommand);
        }

        #endregion
        #region Commands
        #region Commands UpsertFamilyMember


        private void OnUpsertFamilyMemberCommand(object p)
        {
            
            if (_editMode)
            {
                var familyMember = _context.FamilyMembers
                    .Where(x => x.Id == Id)
                    .FirstOrDefault();
                familyMember.Name = _name;
                familyMember.Surname = _surname;
                familyMember.Age = _age;
                _context.SaveChanges();
                var item = _mainViewModel.FamilyMemberList.FirstOrDefault(x => x.Id == Id);
                if (item != null)
                {
                    item.Name = _name;
                    item.Surname = _surname;
                    item.Age = _age;
                }
            }
            else
            {
                var familyMember = new FamilyMember { Name = _name, Surname = _surname, Age = _age };
                _context.FamilyMembers.Add(familyMember);
                _context.SaveChanges();
                _mainViewModel.FamilyMemberList.Add(new FamilyMemberCardViewModel(_context)
                { Name = familyMember.Name, Surname = familyMember.Surname, Age = familyMember.Age, Id = familyMember.Id });
            }          
        }

        private bool CanUpsertFamilyMemberCommand(object p) => String.IsNullOrEmpty(Name) ? false : true;
        #endregion
        #endregion
    }
}
