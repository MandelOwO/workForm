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
    /// Interaction logic for pgDatagrid.xaml
    /// </summary>
    public partial class pgDatagrid : Page
    {
        public DataTable listSource { get; set; }
        MyContext context = new MyContext();
        public Windows.Main.pgProjectDetail pageProjectDetail { get; set; }
        public Tables.Project selProject { get; set; }
        public Tables.Work selWork { get; set; }

        public pgDatagrid(Tables.Project p )
        {
            InitializeComponent();
            selProject = p;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            FillData();
            dg_works.ItemsSource = listSource.DefaultView;

            /* hide colums */
            dg_works.Columns[0].Visibility = Visibility.Collapsed;
            dg_works.Columns[2].Visibility = Visibility.Collapsed;
            dg_works.Columns[3].Visibility = Visibility.Collapsed;
            dg_works.Columns[4].Visibility = Visibility.Collapsed;
        }
        public void FillData()
        {
            var data = context.tbWorks.Where(w => w.idProject == selProject.IDproject).ToList();
            listSource = tools.DataLoader.ToDataTable(data);
        }

        private void dg_works_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var row = (Tables.Work)dg_works.SelectedItem;

            DataRowView r = dg_works.SelectedItem as DataRowView;
            int workid = Convert.ToInt32(r.Row.ItemArray[0].ToString());

            selWork = context.tbWorks.SingleOrDefault(x => x.IDwork == workid);
            
        }
    }
}
