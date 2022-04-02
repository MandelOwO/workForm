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

namespace workForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyContext context = new MyContext();    
        Tables.User CurrentUser { get; set; }

        public MainWindow(Tables.User u)
        {
            InitializeComponent();
            CurrentUser = u;
            lab_user.Content = u.Name;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProjects(CurrentUser);
            
        }

        public void LoadProjects(Tables.User usr)
        {
           // List<Tables.Project> projects = new List<Tables.Project>();
           // IQueryable<Tables.Project> rtn = context.tbProjects.Where(x => x.idUser == usr.IDuser).AsQueryable<Tables.Project>().ToList();

            var projects = context.tbProjects.Where(u => u.idUser == usr.IDuser).AsQueryable().ToList<Tables.Project>();

            foreach (var project in projects)
            {
                cb_projectsList.Items.Add(project.Name);
            }
        }
    }
}
