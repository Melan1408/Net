using HealthHelper.ViewModels;
using Microsoft.Extensions.Hosting;

namespace HealthHelper.Models
{
    public class FamilyMember : ViewModelBase
    {
        public int Id { get; set; }
   
        string? name;
        string? surname;
        int age;

        public string Name
        {
            get { return name; }
            set
            {
               name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        public ICollection<CourseOfTablet> CourseOfTablet { get; } = new List<CourseOfTablet>();
    }
}
