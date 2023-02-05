using SE1611_Group5_A2.Models;
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

namespace SE1611_Group5_A2
{
    /// <summary>
    /// Interaction logic for ShoppingWindow.xaml
    /// </summary>
    public partial class ShoppingWindow : Window
    {
        public ShoppingWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            MusicStoreContext context = new MusicStoreContext();
            cbGenre.ItemsSource = context.Genres.ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_AddToCart(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Previous_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
