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

            var usr = context.tbUsers.SingleOrDefault(u => u.Username == usernameInput);
            var pass = context.tbUsers.SingleOrDefault(p => p.Password == passwordInput);

            if (usr != null && pass != null)
            {
                if (usernameInput == usr.Username && passwordInput == usr.Password)
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
