using HealthHelper.ViewModels;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHelper.Models
{
    public class Tablet : ViewModelBase
    {
        public int Id { get; set; }

        string? name;
        string? discription;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Discription
        {
            get { return discription; }
            set
            {
                discription = value;
                OnPropertyChanged("Discription");
            }
        }

    }
}
