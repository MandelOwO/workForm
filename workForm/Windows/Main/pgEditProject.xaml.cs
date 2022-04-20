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
    /// Interaction logic for pgEditProject.xaml
    /// </summary>
    public partial class pgEditProject : Page
    {
        public MyContext Context { get; set; } = new MyContext();
        public Project CurrentProject { get; set; }
        public User CurrentUser { get; set; }
        private bool Editing = false;
        public pgEditProject(Project project, User user)
        {
            InitializeComponent();
            CurrentProject = project;
            CurrentUser = user;
            if (CurrentProject.IDproject == null)
                Editing = true;
            CurrentProject.idUser = user.IDuser;

            LoadCustomers();
        }

        private void LoadCustomers()
        {
            var data = Context.tbCustomers.ToList<Customer>();

            List<String> customerNames = new List<string>();
            foreach (var customer in data)
            {
                customerNames.Add(customer.Name);
            }

            cbCustomer.ItemsSource = customerNames;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValid())
                return;

            CurrentProject.Name = tbName.Text;
            CurrentProject.Rate = Convert.ToInt32(tbRate.Text);
            var customer = Context.tbCustomers.SingleOrDefault(x => x.Name == cbCustomer.Text);
            if (customer != null)
                CurrentProject.idCustomer = customer.IDcustomer;
            if (chkCompeted.IsChecked == true)
                CurrentProject.Completed = true;

            if (Editing)
            {

            }
            else
            {
                Context.Add(CurrentProject);
                Context.SaveChanges();
            }

            ClosePage();

        }

        private void ClosePage()
        {
            pgProjectList pg = new pgProjectList(CurrentUser);
            NavigationService.Navigate(pg);
        }

        private bool IsValid()
        {
            bool isValid = true;


            return isValid;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }
    }
}
