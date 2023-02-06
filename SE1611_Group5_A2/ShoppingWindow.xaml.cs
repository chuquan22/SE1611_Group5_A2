
using Microsoft.EntityFrameworkCore;
using SE1611_Group5_A2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class ShoppingWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        public int index = 0;
        public string title_1 = "";
        public string title_2 = "";
        public string title_3 = "";
        public string title_4 = "";
        
        public ShoppingWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            LoadData(index, 0);
        }


        private void LoadData(int index, int? genreId)
        {
            int count = 0;
            MusicStoreContext context = new MusicStoreContext();
            cbGenre.ItemsSource = context.Genres.ToList();
            if (genreId.Value == 0)
            {
                foreach (Album album in context.Albums.ToList())
                {
                    if (count == index) { Title1 = album.Title + ": " + album.Price + " USD"; }
                    if (count == index + 1) { Title2 = album.Title + ": " + album.Price + " USD"; }
                    if (count == index + 2) { Title3 = album.Title + ": " + album.Price + " USD"; }
                    if (count == index + 3) { Title4 = album.Title + ": " + album.Price + " USD"; }
                    count++;
                }
            }
            else
            {
                count = 0;
                foreach (Album album in context.Albums.Where(x => x.GenreId == genreId.Value).ToList())
                {
                        if (count == index) { Title1 = album.Title + ": " + album.Price + " USD"; }
                        if (count == index + 1) { Title2 = album.Title + ": " + album.Price + " USD"; }
                        if (count == index + 2) { Title3 = album.Title + ": " + album.Price + " USD"; }
                        if (count == index + 3) { Title4 = album.Title + ": " + album.Price + " USD"; }
                        count++;
                }
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int? genreId = 0;
            string? genre = cbGenre.SelectedValue?.ToString();
            if (string.IsNullOrWhiteSpace(genre)) { } else
            {
                 genreId = int.Parse(genre);
            }
            LoadData(0, genreId);
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

        public string? Title1
        {
            get { return title_1; }
            set { title_1 = value; OnPropertyChanged("Title1"); }
        }
        public string? Title2
        {
            get { return title_2; }
            set { title_2 = value; OnPropertyChanged("Title2"); }
        }
        public string? Title3
        {
            get { return title_3; }
            set { title_3 = value; OnPropertyChanged("Title3"); }
        }
        public string? Title4
        {
            get { return title_4; }
            set { title_4 = value ;  OnPropertyChanged("Title4"); }
        }

        
    }
}
