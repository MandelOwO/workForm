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
using System.Windows.Shapes;

namespace workForm.Windows.Login
{
    /// <summary>
    /// Interaction logic for WinLogin.xaml
    /// </summary>
    public partial class WinLogin : Window
    {

        MyContext context = new MyContext();

        public WinLogin()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {

            CloseWindow();
        }

        public void CloseWindow()
        {

            this.Close();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            lab_incorrect.Content = "";
            string usernameInput = this.tb_username.Text;
            string passwordInput = this.tb_password.Password;
            var usr = new Tables.User();
            var pass = new Tables.User();

            try
            {
                usr = context.tbUsers.SingleOrDefault(u => u.Username == usernameInput) as Tables.User;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not connect to the database\nError:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (usr != null && pass != null)
            {
                if (usernameInput == usr.Username && usr.Password == passwordInput)
                {
                    MainWindow w = new MainWindow(usr);
                    w.Show();
                    this.Close();
                }
            }
            else
            {
                lab_incorrect.Content = "Please check your login information.";
            }
        }
    }
}
