using HealthHelper.Commands;
using System.Windows.Input;
using Tablet = HealthHelper.Models.Tablet;

namespace HealthHelper.ViewModels
{
    public class TabletViewModel : ViewModelBase
    {
        private string _name;
        private string _discription;
        private readonly ApplicationContext _context;
        private CourseOfTabletsViewModel _courseOfTabletsViewModel { get; set; }
        private bool _editMode { get; set; }
        public int Id { get; set; }

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Discription { get => _discription; set { _discription = value; OnPropertyChanged(); } }

        public ICommand UpsertTabletCommand { get; set; }

        #region Constructor

        public TabletViewModel(ApplicationContext applicationDbContext, CourseOfTabletsViewModel courseOfTabletsViewModel, bool editMode = false)
        {
            _courseOfTabletsViewModel = courseOfTabletsViewModel;
            _context = applicationDbContext;
            _editMode = editMode;
            UpsertTabletCommand = new RelayCommand(OnUpsertTabletCommand, CanUpsertTabletCommand);
        }

        #endregion
        #region Commands
        #region Commands UpsertTabletCommand


        private void OnUpsertTabletCommand(object p)
        {

            if (_editMode)
            {
                var tablet = _context.Tablets
                    .Where(x => x.Id == Id)
                    .FirstOrDefault();
                tablet.Name = _name;
                tablet.Discription = _discription;
                _context.SaveChanges();
                var item = _courseOfTabletsViewModel.TabletList.FirstOrDefault(x => x.Id == Id);
                if (item != null)
                {
                    item.Name = _name;
                    item.Discription = _discription;
                }
            }
            else
            {
                var tablet = new Tablet { Name = _name, Discription = _discription };
                _context.Tablets.Add(tablet);
                _context.SaveChanges();
                _courseOfTabletsViewModel.TabletList.Add(new TabletCardViewModel(_context)
                { Name = tablet.Name, Discription = tablet.Discription, Id = tablet.Id });
            }
        }

        private bool CanUpsertTabletCommand(object p) => String.IsNullOrEmpty(Name) ? false : true;
        #endregion
        #endregion
    }
}
