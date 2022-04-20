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

            if (CurrentProject.Name != null)
            {
                Editing = true;
                tbName.Text = CurrentProject.Name;
                tbRate.Text = CurrentProject.Rate.ToString();
                var customer = Context.tbCustomers.SingleOrDefault(x => x.IDcustomer == CurrentProject.idCustomer);
                cbCustomer.Text = customer.Name;
                if (CurrentProject.Completed == true)
                    chkCompeted.IsChecked = true;
            }



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

            try
            {
                CurrentProject.Name = tbName.Text;
                CurrentProject.Rate = Convert.ToInt32(tbRate.Text);
                var customer = Context.tbCustomers.SingleOrDefault(x => x.Name == cbCustomer.Text);
                if (customer != null)
                    CurrentProject.idCustomer = customer.IDcustomer;
                if (chkCompeted.IsChecked == true)
                    CurrentProject.Completed = true;
            }
            catch (Exception ex)
            {

                return;
            }




            if (Editing)
            {
                var project = Context.tbProjects.SingleOrDefault(x => x.IDproject == CurrentProject.IDproject);
                if (project != null)
                {
                    project.Name = CurrentProject.Name;
                    project.Rate = CurrentProject.Rate;
                    project.idCustomer = CurrentProject.idCustomer;
                    project.Completed = CurrentProject.Completed;
                }
            }
            else
            {
                Context.Add(CurrentProject);

            }

            Context.SaveChanges();
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

            var project = Context.tbProjects.SingleOrDefault(x => x.Name == tbName.Text);
            if (project != null && !Editing)
            {
                labMessage.Content = "This project already exists ";
                isValid = false;
            }
            if (tbRate.Text == "")
            {
                labMessage.Content = "Plese enter a rate ";
                isValid = false;
            }
            int num = -1;
            if (!int.TryParse(tbRate.Text, out num))
            {
                labMessage.Content = "Rate must be a number ";
                isValid = false;
            }
            if (cbCustomer.Text == "")
            {
                labMessage.Content = "Plese select a customer ";
                isValid = false;
            }
            if (tbName.Text == "")
            {
                labMessage.Content = "Plese enter a project name ";
                isValid = false;
            }


            return isValid;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }
    }
}
