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
using System.Windows.Navigation;
using System.Windows.Shapes;
using workForm.Tables;

namespace workForm.Windows.Main
{
    /// <summary>
    /// Interaction logic for pgViewProject.xaml
    /// </summary>
    public partial class pgViewProject : Page
    {
        public User CurrentUser { get; set; }
        public pgViewProject(User usr)
        {
            InitializeComponent();
            CurrentUser = usr;

            Frame1.Content = new pgProjectList(CurrentUser);
        }

    }
}
