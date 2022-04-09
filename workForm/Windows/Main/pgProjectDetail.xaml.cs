using System;
using System.Collections.Generic;
using System.Data;
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
        public Windows.Main.pgDatagrid pageDatagrid { get; set; } 

        //   public dataBindings.WorksDataModel Model { get; set; }



        public pgProjectDetail(Tables.Project p)
        {
            InitializeComponent();
            selProject = p;
            // Model = new dataBindings.WorksDataModel(selProject);
            pageDatagrid = new Windows.Main.pgDatagrid(selProject);

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            selCustomer = FindCustomer(selProject);
            lab_projectName.Content = selProject.Name;
            lab_rate.Content = selProject.Rate;
            lab_customer.Content = selCustomer.Name;

            frame1.Content = pageDatagrid;
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

        private void btn_addWork_Click(object sender, RoutedEventArgs e)
        {
            Windows.Main.pgEditWork pg = new Windows.Main.pgEditWork(selProject, new Tables.Work());
            frame1.Content = pg;
        }

        public void DisableButtons()
        {
            btn_addWork.IsEnabled = false;
            btn_ediWtork.IsEnabled = false;
            btn_deleteWork.IsEnabled = false;
        }
    }
}
