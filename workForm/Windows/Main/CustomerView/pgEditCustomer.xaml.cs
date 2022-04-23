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

namespace workForm.Windows.Main.CustomerView
{
    /// <summary>
    /// Interaction logic for pgEditCustomer.xaml
    /// </summary>
    public partial class pgEditCustomer : Page
    {
        private MyContext Context { get; set; } = new MyContext();
        public Customer CurrentCustomer { get; set; }
        private bool Editing = false;
        private bool Viewing = false;

        public pgEditCustomer(Customer customer, string action)
        {
            InitializeComponent();
            CurrentCustomer = customer;

            if (action == "viewing")
            {
                SetViewing();
            }
            else if (action == "editing")
            {
                SetEditing();
            }
        }

        public void SetViewing()
        {
            Viewing = true;
            tbName.IsEnabled = false;
            tbCity.IsEnabled = false;
            tbStreet.IsEnabled = false;
            tbZip.IsEnabled = false;
            tbCin.IsEnabled = false;
            tbTin.IsEnabled = false;
            btnCancel.Visibility = Visibility.Hidden;
            LoadDataIntoBoxes();
        }

        public void SetEditing()
        {
            Editing = true;
            LoadDataIntoBoxes();
        }

        public void LoadDataIntoBoxes()
        {
            tbName.Text = CurrentCustomer.Name;
            tbCity.Text = CurrentCustomer.City;
            tbStreet.Text = CurrentCustomer.Street;
            tbZip.Text = CurrentCustomer.PSC;
            tbCin.Text = CurrentCustomer.ICO;
            tbTin.Text = CurrentCustomer.DIC;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentCustomer.Name = tbName.Text;
                CurrentCustomer.City = tbCity.Text;
                CurrentCustomer.Street = tbStreet.Text;
                CurrentCustomer.PSC = tbZip.Text;
                CurrentCustomer.ICO = tbCin.Text;
                CurrentCustomer.DIC = tbTin.Text;
            }
            catch (Exception ex)
            {

                return;
            }

            if (!IsValid()) return;

            if (Editing)
            {
                var customer = Context.tbCustomers.SingleOrDefault(x => x.IDcustomer == CurrentCustomer.IDcustomer);
                if (customer != null)
                {
                    customer.Name = CurrentCustomer.Name;
                    customer.City = CurrentCustomer.City;
                    customer.Street = CurrentCustomer.Street;
                    customer.PSC = CurrentCustomer.PSC;
                    customer.ICO = CurrentCustomer.ICO;
                    customer.DIC = CurrentCustomer.DIC;
                }
            }
            else if (Viewing)
            {
                ClosePage();
                return;
            }
            else
            {
                Context.Add(CurrentCustomer);
            }

            try
            {
                Context.SaveChanges();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            ClosePage();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        private void ClosePage()
        {
            pgCustomerList pg = new pgCustomerList();
            NavigationService.Navigate(pg);
        }


        private bool IsValid()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(CurrentCustomer.Name))
            {
                labMessage.Content = "Please fill all boxes first";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(CurrentCustomer.City))
            {
                labMessage.Content = "Please fill all boxes first";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(CurrentCustomer.Street))
            {
                labMessage.Content = "Please fill all boxes first";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(CurrentCustomer.PSC))
            {
                labMessage.Content = "Please fill all boxes first";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(CurrentCustomer.ICO))
            {
                labMessage.Content = "Please fill all boxes first";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(CurrentCustomer.DIC))
            {
                labMessage.Content = "Please fill all boxes first";
                isValid = false;
            }

            return isValid;
        }
    }
}
