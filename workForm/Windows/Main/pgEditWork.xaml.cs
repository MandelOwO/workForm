using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using workForm.Tables;

namespace workForm.Windows.Main
{
    /// <summary>
    /// Interaction logic for pgEditWork.xaml
    /// </summary>
    public partial class pgEditWork : Page
    {
        public MyContext context { get; set; } = new MyContext();
        public Project selProject { get; set; } = new Project();
        public Work Work { get; set; } = new Work();
        public pgProjectDetail NamingContainer { get; private set; }
        private bool automaticTimerStarted;
        private bool editing = false;
        private DateTime autoStartingTime;
        private DateTime autoEndingTime;
        private Action EnableButtons;

        public pgEditWork(Project p, Work w, Action enableButtons)
        {
            InitializeComponent();
            selProject = p;

            Work.idProject = selProject.IDproject;
            Work = w;
            EnableButtons = enableButtons;
            lodaWorkData();

        }
        private void lodaWorkData()
        {
            if (Work.Descripton != null)
            {
                try
                {
                    tbName.Text = Work.Descripton;
                    if (Work.Completed)
                    {
                        chkCompleted.IsChecked = true;
                    }
                    dtpStart.Text = Work.Start.ToString().Substring(0, 10);
                    cbStart.Text = Work.Start.ToString().Substring(11, 5);
                    dtpEnd.Text = Work.End.ToString().Substring(0, 10);
                    cbEnd.Text = Work.End.ToString().Substring(11, 5);
                    rbManual.IsChecked = true;
                    if (Work.Descripton != null)
                    {
                        editing = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

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

            string t = cb.Text.ToString();
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
            if (automaticTimerStarted)
            {
                if (MessageBox.Show("Do you want to clear timer and enter time manually instead?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    enableManual();
                    disableAuto();
                    automaticTimerStarted = false;
                    labAutoTime.Content = "00:00:00";
                }
            }
            else
            {
                enableManual();
                disableAuto();
                automaticTimerStarted = false;
                labAutoTime.Content = "00:00:00";
            }

        }
        private void rbAuto_Checked(object sender, RoutedEventArgs e)
        {
            Timer = new DispatcherTimer();
            InitializeTimer();
            increment = 0;
            enableAuto();
            disableManual();
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
            btnStart.IsEnabled = false;
            btnEnd.IsEnabled = false;
        }
        private void enableAuto()
        {
            btnStart.IsEnabled = true;
            btnEnd.IsEnabled = true;
        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Work.Descripton = tbName.Text;
            if (automaticTimerStarted)
            {
                Work.Start = autoStartingTime;
                Work.End = autoEndingTime;
            }
            else
            {
                Work.Start = getTime(cbStart, dtpStart);
                Work.End = getTime(cbEnd, dtpEnd);
            }
            Work.idProject = selProject.IDproject;
            if (chkCompleted.IsChecked == true)
            {
                Work.Completed = true;
            }
            else
            {
                Work.Completed = false;
            }
            Work.Duration = Work.End.Subtract(Work.Start);

            if (editing)
            {
                var dr = context.tbWorks.FirstOrDefault(x => x.IDwork == Work.IDwork);
                if (dr != null)
                {
                    dr.Descripton = Work.Descripton;
                    dr.Start = Work.Start;
                    dr.End = Work.End;
                    dr.Completed = Work.Completed;
                    dr.Duration = Work.Duration;
                }
            }
            else
            {
                context.tbWorks.Add(Work);
            }

            context.SaveChanges();
            pgDatagrid p = new pgDatagrid(selProject);
            p.FillData();
            ClosePage();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        public void ClosePage()
        {
            pgDatagrid secPage = new pgDatagrid(selProject);
            EnableButtons();
            NavigationService.Navigate(secPage);
        }


        /* TIMER */
        public DispatcherTimer Timer { get; set; } = new DispatcherTimer();

        public void InitializeTimer()
        {
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
        }

        private int increment = 0;
        private TimeSpan incrementInterval;
        private void Timer_Tick(object? sender, EventArgs e)
        {
            increment++;
            incrementInterval = TimeSpan.FromSeconds(increment);
            string h = incrementInterval.Hours.ToString().PadLeft(2, '0');
            string m = incrementInterval.Minutes.ToString().PadLeft(2, '0');
            string s = incrementInterval.Seconds.ToString().PadLeft(2, '0');
            labAutoTime.Content = $"{h}:{m}:{s}";
        }
        /*
        private string Padder(string x)
        {
            x.PadLeft(2, '0');
            return x;
        }
        */
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (automaticTimerStarted)
            {
                if (MessageBox.Show("Do you want to start again?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    autoStartingTime = DateTime.Now;
                    Timer.Start();
                    increment = 0;
                    rbManual.IsEnabled = false;
                    btnOk.IsEnabled = false;
                    btnStart.IsEnabled = false;
                    automaticTimerStarted = true;

                }

            }
            else
            {
                autoStartingTime = DateTime.Now;
                Timer.Start();
                rbManual.IsEnabled = false;
                btnOk.IsEnabled = false;
                btnStart.IsEnabled = false;
                automaticTimerStarted = true;
            }

        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            autoEndingTime = DateTime.Now;
            Timer.Stop();
            rbManual.IsEnabled = true;
            btnOk.IsEnabled = true;
            btnStart.IsEnabled = true;
        }


        /*VALIDATION*/


    }
}
