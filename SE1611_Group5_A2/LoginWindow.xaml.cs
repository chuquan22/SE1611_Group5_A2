

using SE1611_Group5_A2;
using SE1611_Group5_A2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace Assignment2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private MusicStoreContext context = new MusicStoreContext();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string userLogin = txtUsername.Text;
            string passLogin = txtPassword.Password;
            User? user = context.Users.SingleOrDefault(user => user.UserName == userLogin);
            // if user not exist
            if (user == null || string.Compare(passLogin, user.Password, false) != 0)
            {
                MessageBox.Show("Don't have that user");
            }
            else
            {
                MainWindow.isLogin = true;
                MainWindow.username = user.UserName;
                // if admin
                if (user.Role == 1)
                {
                    MainWindow.isAdmin = true;
                }
                MessageBox.Show("You are login successfull");
                Close();
                
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}
