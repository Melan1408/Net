using HealthHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthHelper.Views
{
    /// <summary>
    /// Логика взаимодействия для FamilyMemberView.xaml
    /// </summary>
    public partial class FamilyMemberView : Window
    {
        //public FamilyMember FamilyMember { get; set; }

        public FamilyMemberView(/*FamilyMember familyMember*/)
        {
            InitializeComponent();

            /*FamilyMember = familyMember;
            DataContext = FamilyMember;*/
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
