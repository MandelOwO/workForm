using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for pgDatagrid.xaml
    /// </summary>
    public partial class pgDatagrid : Page
    {
        public DataTable listSource { get; set; }
        MyContext context = new MyContext();
        public Windows.Main.pgProjectDetail pageProjectDetail { get; set; }
        public Project SelProject { get; set; }
        public Work SelWork { get; set; } = new Work();


        private ObservableCollection<Work> _works { get; set; } = new ObservableCollection<Work>();
        public pgDatagrid(Project p)
        {
            InitializeComponent();
            SelProject = p;
            FillData();
            lvWorks.ItemsSource = _works;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            // FillData();

        }

        public void FillData()
        {
            var w = context.tbWorks.Where(x => x.idProject == SelProject.IDproject).ToList<Work>();
            foreach (var item in w)
            {
                _works.Add(item);
            }

        }
        public void DeleteWork()
        {
            Work wrk = GetSelectedWork();
            if (wrk != null)
            {
                var w = context.tbWorks.SingleOrDefault(x => x.IDwork == wrk.IDwork);
                if (w != null)
                {
                    context.tbWorks.Remove(w);
                    _works.Remove(w);
                    lvWorks.Items.Remove(w);
                }

                context.SaveChanges();
                lvWorks.Items.Refresh();
            }
        }
        public Work GetSelectedWork()
        {
            Work work = (Work)lvWorks.SelectedItem;
            return work;
        }
    }
}
