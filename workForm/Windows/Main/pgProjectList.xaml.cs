using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for pgProjectList.xaml
    /// </summary>
    public partial class pgProjectList : Page
    {
        public MyContext Context { get; set; } = new MyContext();
        public User CurrentUser { get; set; }
        private ObservableCollection<ProjectWithCustomerName> _projects { get; set; } = new ObservableCollection<ProjectWithCustomerName>();

        public pgProjectList(User usr)
        {
            InitializeComponent();
            CurrentUser = usr;
            LoadProjectsIntoList();
            lvProjects.ItemsSource = _projects;
        }

        public void LoadProjectsIntoList()
        {
            var p = Context.tbProjects.Where(x => x.idUser == CurrentUser.IDuser).ToList<Project>();
            foreach (var item in p)
            {
                var customer = Context.tbCustomers.SingleOrDefault(x => x.IDcustomer == item.idCustomer);
                ProjectWithCustomerName project = new ProjectWithCustomerName(item.IDproject, item.Name, item.Rate, customer.Name, item.Completed);
                _projects.Add(project);
            }


        }

        public Project GetSelectedProject()
        {
            ProjectWithCustomerName p = lvProjects.SelectedItem as ProjectWithCustomerName;
            Project project = new Project();
            if (p == null)
                return null;
            project = Context.tbProjects.SingleOrDefault(x => x.IDproject == p.IDproject);
            return project;
        }
    }
}
