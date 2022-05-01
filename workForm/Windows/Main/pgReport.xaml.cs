using Microsoft.Win32;
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
using workForm.tools;

namespace workForm.Windows.Main
{
    /// <summary>
    /// Interaction logic for pgReport.xaml
    /// </summary>
    public partial class pgReport : Page
    {
        public bool DateFiltering { get; set; } = false;
        public User CurrentUser { get; set; }

        public pgReport(User usr)
        {
            InitializeComponent();
            CurrentUser = usr;
        }

        private void dtStart_LostFocus(object sender, RoutedEventArgs e)
        {
            DateFiltering = true;
        }

        private void dtEnd_LostFocus(object sender, RoutedEventArgs e)
        {
            DateFiltering = true;
        }

        private void btnDetailedReport_Click(object sender, RoutedEventArgs e)
        {
            string file = SavePrompt();
            if (string.IsNullOrWhiteSpace(file)) return;

            Reporter r = new Reporter(CurrentUser, file, DateTime.Now, DateTime.Now);
            if (DateFiltering)
            {
                r = new Reporter(CurrentUser, file, Convert.ToDateTime(dtStart.SelectedDate.Value.Date), Convert.ToDateTime(dtEnd.SelectedDate.Value.Date));
            }
            else
            {
                r = new Reporter(CurrentUser, file, DateTime.MinValue, DateTime.MaxValue);
            }

            r.GenerateDetailedReport();
            MessageBox.Show("Detailed report was created", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private string SavePrompt()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = @"C:\";
            save.Filter = "Html file (*.html)|*.html";
            save.RestoreDirectory = true;
            save.Title = "Select save location file name";
            save.DefaultExt = "html";
            save.AddExtension = true;

            save.ShowDialog();
            if (save.FileName != null)
            {
                return save.FileName;
            }
            else
                return null;
        }

        private void btnSummaryReport_Click(object sender, RoutedEventArgs e)
        {
            string file = SavePrompt();
            if (string.IsNullOrWhiteSpace(file)) return;

            Reporter r = new Reporter(CurrentUser, file, DateTime.Now, DateTime.Now);
            if (DateFiltering)
            {
                r = new Reporter(CurrentUser, file, Convert.ToDateTime(dtStart), Convert.ToDateTime(dtEnd));
            }
            else
            {
                r = new Reporter(CurrentUser, file, DateTime.MinValue, DateTime.MaxValue);
            }


            r.GenerateSummaryReport();
            MessageBox.Show("Summary report was created", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
