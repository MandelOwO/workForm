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
    /// Interaction logic for pgViewCustomer.xaml
    /// </summary>
    public partial class pgViewCustomer : Page
    {
        private MyContext Context { get; set; } = new MyContext();
        private pgCustomerList pageCustomerList { get; set; } = new pgCustomerList();
        public pgViewCustomer()
        {
            InitializeComponent();

            Frame1.Content = pageCustomerList;
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = pageCustomerList.GetSelectedCustomer();
            if (customer == null) return;

            pgEditCustomer pg = new pgEditCustomer(customer, "viewing"); ;
            Frame1.Content = pg;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            pgEditCustomer pg = new pgEditCustomer(new Customer(), "");
            Frame1.Content = pg;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = pageCustomerList.GetSelectedCustomer();
            if (customer == null) return;

            pgEditCustomer pg = new pgEditCustomer(customer, "editing"); ;
            Frame1.Content = pg;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            pageCustomerList.DeleteCustomer();
        }
    }
}