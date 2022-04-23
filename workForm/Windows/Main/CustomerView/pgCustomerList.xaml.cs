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

namespace workForm.Windows.Main.CustomerView
{
    /// <summary>
    /// Interaction logic for pgCustomerList.xaml
    /// </summary>
    public partial class pgCustomerList : Page
    {
        public MyContext Context { get; set; } = new MyContext();
        private ObservableCollection<Customer> _customers { get; set; } = new ObservableCollection<Customer>();

        public pgCustomerList()
        {
            InitializeComponent();
            LoadCustomersIntoList();
            lvCustomers.ItemsSource = _customers;
        }

        public void LoadCustomersIntoList()
        {
            var p = Context.tbCustomers.ToList<Customer>();
            foreach (var item in p)
            {
                _customers.Add(item);
            }
        }

        public Customer GetSelectedCustomer()
        {
            Customer customer = lvCustomers.SelectedItem as Customer;

            return customer;
        }

        public void DeleteCustomer()
        {
            Customer c = GetSelectedCustomer();
            if (c == null) return;

            var customer = Context.tbCustomers.SingleOrDefault(x => x.IDcustomer == c.IDcustomer);
            if (customer == null) return;
            var projects = Context.tbProjects.Where(x => x.idCustomer == c.IDcustomer).ToList<Project>();

            string projectsString = GetStringOfProjects(projects);


            if (MessageBox.Show("Are you sure that you want to delete " + c.Name + " and all projects bound to this customer?\n" + projectsString, "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                pgProjectList pgp = new pgProjectList(new User());
                foreach (var project in projects)
                {
                    pgp.DeleteProject(project);
                }

                Context.tbCustomers.Remove(customer);
                try
                {
                    Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.ToString());

                }
            }
            lvCustomers.Items.Refresh();

        }

        public string GetStringOfProjects(List<Project> projects)
        {
            string projectsString = "";
            foreach (var project in projects)
            {
                projectsString += project.Name + "\n";
            }
            return projectsString;
        }
    }
}
