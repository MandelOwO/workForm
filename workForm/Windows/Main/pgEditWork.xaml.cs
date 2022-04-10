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
    /// Interaction logic for pgEditWork.xaml
    /// </summary>
    public partial class pgEditWork : Page
    {
           public MyContext context { get; set; } = new MyContext();
           public Tables.Project selProject { get; set; } = new Tables.Project();
           public Tables.Work Work { get; set; } = new Tables.Work();
        public pgProjectDetail NamingContainer { get; private set; }

        public pgEditWork(Tables.Project p, Tables.Work w)
        {
            InitializeComponent();
            selProject = p;

            Work.idProject = selProject.IDproject;
            Work = w;



        }
        private void lodaWorkData()
        {
            tbName.Text = Work.Descripton;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dtpStart.SelectedDate = DateTime.Today;
            dtpEnd.SelectedDate = DateTime.Today;

            cbStart.ItemsSource = cbSource();
            cbEnd.ItemsSource = cbSource();

            disableAuto();
            disableManual();


        }


        private DateTime getTime(ComboBox cb, DatePicker dp)
        {
            DateTime dt = DateTime.Now;

            string t =  cb.Text.ToString();
            string d = dp.ToString();
            d = d.Substring(0, d.Length - 8);

            dt = DateTime.Parse(d + " " + t);

            return dt;
        }
        private List<string> cbSource()
        {
            List<string> source = new List<string> { "00:00", "00:30", "01:00", "01:30", "02:00", "02:30", "03:00", "03:30", "04:00", "04:30", "05:00", "05:30", "06:00", "06:30", "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00", "20:30", "21:00", "21:30", "22:00", "22:30", "23:00", "23:30" };
            return source;
        }

        private void rbManual_Checked(object sender, RoutedEventArgs e)
        {
            enableManual();
        }

        private void disableManual()
        {
            cbStart.IsEnabled = false;
            cbEnd.IsEnabled = false;
            dtpStart.IsEnabled = false;
            dtpEnd.IsEnabled = false;
        }
        private void enableManual()
        {
            cbStart.IsEnabled = true;
            cbEnd.IsEnabled = true;
            dtpStart.IsEnabled = true;
            dtpEnd.IsEnabled = true;
        }
        private void disableAuto()
        {

        }
        private void enableAuto()
        {

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Work.Descripton = tbName.Text;
            Work.Start = getTime(cbStart, dtpStart);
            Work.End = getTime(cbEnd, dtpEnd);
            Work.idProject = selProject.IDproject;

            context.tbWorks.Add(Work);
            context.SaveChanges();
            ClosePage();


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }
        
        public void ClosePage()
        {
            pgDatagrid secPage = new pgDatagrid(selProject);
            NavigationService.Navigate(secPage);
        }
    }
}
