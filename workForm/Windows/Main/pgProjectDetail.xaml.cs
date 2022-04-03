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

namespace workForm.Windows.Main
{
    /// <summary>
    /// Interaction logic for pgProjectDetail.xaml
    /// </summary>
    public partial class pgProjectDetail : Page
    {
        MyContext context = new MyContext();

        public Tables.Project selProject { get; set; }
        public Tables.Customer selCustomer { get; set; }

        public pgProjectDetail(Tables.Project p)
        {
            InitializeComponent();
            selProject = p; 
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            selCustomer = FindCustomer(selProject);
            lab_projectName.Content = selProject.Name;
            lab_rate.Content = selProject.Rate;
            lab_customer.Content = selCustomer.Name;
        }

        public Tables.Customer FindCustomer(Tables.Project project)
        {
            var cust = context.tbCustomers.SingleOrDefault(c => c.IDcustomer == project.idCustomer);
            return cust;
        }

        private void btn_customerDetails_Click(object sender, RoutedEventArgs e)
        {
            string customerInfo = $"Name: {selCustomer.Name}\nAdress: {selCustomer.Street}, {selCustomer.City}, {selCustomer.PSC}\nCIN: {selCustomer.ICO}\nVAT: {selCustomer.DIC}";
            MessageBox.Show(customerInfo);
        }
    }
}
