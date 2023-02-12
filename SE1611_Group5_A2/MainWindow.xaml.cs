using Assignment2;
using SE1611_Group5_A2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace SE1611_Group5_A2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        internal static bool isLogin = false;
        internal static bool isAdmin = false;
        internal static string username = "";
        private void LoginLogout_Click(object sender, RoutedEventArgs e)
        {
            // if login
            if (isLogin)
            {
                // if login as admin
                if (isAdmin)
                {
                    menuAlbum.IsEnabled = false;
                }
                menuLogin.Header = "Login";
                isLogin = false;
                username = "";
            }
            else
            {
                LoginWindow login = new LoginWindow();
                login.ShowDialog();
            }

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            // if login
            if (isLogin)
            {

                menuLogin.Header = "Logout(" + username + ")";
                // if admin
                if (isAdmin)
                {
                    menuAlbum.IsEnabled = true;
                }

            }
        }

        private void Shopping_Click(object sender, RoutedEventArgs e)
        {
            ShoppingWindow shoppingWindow = new ShoppingWindow();
            shoppingWindow.ShowDialog();
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuAlbum_Click(object sender, RoutedEventArgs e)
        {
            AlbumWindow albumWindow = new AlbumWindow();
            albumWindow.ShowDialog();
        }
    }
}
