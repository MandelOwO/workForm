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
        public pgProjectList pageProjectList { get; set; }
        public Action DisableMainButtons { get; set; }
        public Action EnableMainButtons { get; set; }

        public pgViewProject(User usr, Action disaButt, Action enaButt)
        {
            InitializeComponent();
            CurrentUser = usr;
            pageProjectList = new pgProjectList(CurrentUser);
            DisableMainButtons = disaButt;
            EnableMainButtons = enaButt;


            Frame1.Content = pageProjectList;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            Project project = pageProjectList.GetSelectedProject();
            if (project != null)
            {
                pgProjectDetail secPage = new pgProjectDetail(project, DisableMainButtons, EnableMainButtons);
                NavigationService.Navigate(secPage);
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            pgEditProject pg = new pgEditProject(new Project(), CurrentUser);
            Frame1.Content = pg;
        }
    }
}
