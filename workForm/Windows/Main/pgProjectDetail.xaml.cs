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
using workForm.Tables;

namespace workForm.Windows.Main
{
    /// <summary>
    /// Interaction logic for pgProjectDetail.xaml
    /// </summary>
    public partial class pgProjectDetail : Page
    {

        MyContext context = new MyContext();
        public Project selProject { get; set; }
        public Customer selCustomer { get; set; }
        public pgDatagrid pageDatagrid { get; set; }

        //   public dataBindings.WorksDataModel Model { get; set; }



        public pgProjectDetail(Project p)
        {
            InitializeComponent();
            selProject = p;
            // Model = new dataBindings.WorksDataModel(selProject);


        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            pageDatagrid = new pgDatagrid(selProject);
            selCustomer = FindCustomer(selProject);
            lab_projectName.Content = selProject.Name;
            lab_rate.Content = selProject.Rate;
            lab_customer.Content = selCustomer.Name;

            frame1.Content = pageDatagrid;
        }



        public Customer FindCustomer(Project project)
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
            pgEditWork pg = new pgEditWork(selProject, new Work(), EnableButtons);
            frame1.Content = pg;

            DisableButtons();       //  <-- JE POTŘEBA DOŘEŠIT
        }
        private void btn_ediWtork_Click(object sender, RoutedEventArgs e)
        {
            Work work = pageDatagrid.GetSelectedWork();
            if (work != null)
            {
                pgEditWork pg = new pgEditWork(selProject, work, EnableButtons);
                frame1.Content = pg;
                DisableButtons();
            }
        }

        public void DisableButtons()
        {
            btn_addWork.IsEnabled = false;
            btn_ediWtork.IsEnabled = false;
            btn_deleteWork.IsEnabled = false;
        }

        public void EnableButtons()
        {
            btn_addWork.IsEnabled = true;
            btn_ediWtork.IsEnabled = true;
            btn_deleteWork.IsEnabled = true;
        }

        private void btn_deleteWork_Click(object sender, RoutedEventArgs e)
        {
            pageDatagrid.DeleteWork();
        }
    }
}
