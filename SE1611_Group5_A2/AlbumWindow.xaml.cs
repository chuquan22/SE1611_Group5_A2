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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment2
{
    /// <summary>
    /// Interaction logic for AlbumWindow.xaml
    /// </summary>
    public partial class AlbumWindow : Window
    {
        private MusicStoreContext context = new MusicStoreContext();
        private int? AlbumID = null;
        public AlbumWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetGridView();
            SetComboBoxGenre();
            SetComboBoxArtist();
            DisplayListFilm();
        }
        void SetGridView()
        {
            gridView.Columns[0].DisplayMemberBinding = new Binding("GenreId");
            gridView.Columns[1].DisplayMemberBinding = new Binding("ArtistId");
            gridView.Columns[2].DisplayMemberBinding = new Binding("Title");
            gridView.Columns[3].DisplayMemberBinding = new Binding("Price");
            gridView.Columns[4].DisplayMemberBinding = new Binding("AlbumUrl");
        }

        void SetComboBoxGenre()
        {
            List<Genre> list = context.Genres.ToList();
            cbGenre.ItemsSource= list;
            cbGenre.DisplayMemberPath = "Name";
            cbGenre.SelectedIndex = 0;
        }

        void SetComboBoxArtist()
        {
            List<Artist> list = context.Artists.ToList();
            cbArtist.ItemsSource= list;
            cbArtist.DisplayMemberPath = "Name";
            cbArtist.SelectedIndex = 0;
        }

        void DisplayListFilm()
        {
            List<Album> list = context.Albums.ToList();
            lvAlbum.ItemsSource = list;
        }

        private void lvAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView list = (ListView) sender;
            Album album = (Album) list.SelectedItem;
            // if selected
            if(album != null )
            {
                AlbumID = album.AlbumId;
                Genre? genre = context.Genres.SingleOrDefault(genre => genre.GenreId == album.GenreId);
                Artist? artist = context.Artists.SingleOrDefault(artist => artist.ArtistId== album.ArtistId);
                cbGenre.SelectedValue= genre;
                cbArtist.SelectedValue= artist;
                tbTitle.Text= album.Title;
                tbPrice.Text = album.Price.ToString();
                tbAlbum.Text= album.AlbumUrl;
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // if not select album to delete
            if(AlbumID == null)
            {
                MessageBox.Show("You have to choose album to delete","",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            else
            {
                Album? album = context.Albums.SingleOrDefault(album => album.AlbumId==AlbumID);
                MessageBoxResult result = MessageBox.Show("Do you want to delete","Confirm",MessageBoxButton.YesNo,MessageBoxImage.Question);
                // if choose yes
                if(result == MessageBoxResult.Yes)
                {
                   // get list cart
                   List<Cart> listCart = context.Carts.ToList();
                   // traverse through list cart
                   foreach(Cart cart in listCart)
                    {
                        // if find cart with value AlbumID
                        if(cart.AlbumId == AlbumID)
                        {
                            // delete cart
                            context.Carts.Remove(cart);
                        }
                    }
                    // get list order detail
                   List<OrderDetail> listOrderDetail = context.OrderDetails.ToList();
                    // traverse through list order detail
                    foreach(OrderDetail detail in listOrderDetail)
                    {
                        // if find order detail with album id
                        if(detail.AlbumId == AlbumID) { 
                        // delete order detail
                        context.OrderDetails.Remove(detail);}
                    }
                    context.Albums.Remove(album);
                   int number = context.SaveChanges();
                   // if delete successful
                   if(number > 0 )
                    {
                        MessageBox.Show("That album is deleted!");
                        DisplayListFilm();
                    }
                }
               
            }
        }

        private string CheckValuePrice(string price)
        {   
            // if price empty
            if (price.Length == 0) {
                return "You have to input price";
            }
            else
            {
                try
                {
                   decimal value = decimal.Parse(price);
                   return value > 0 ? "" : "Price must be greater than 0";
                }catch(Exception)
                {
                    return "Invalid price";
                }
                
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Genre genre = (Genre) cbGenre.SelectedValue ;
            Artist artist = (Artist) cbArtist.SelectedValue ;
            string title = tbTitle.Text.Trim();
            string Price = tbPrice.Text.Trim();
            string AlbumURL = tbAlbum.Text.Trim();
            string check = CheckValuePrice(Price);
            // if title empty
            if(title.Length == 0)
            {
                MessageBox.Show("You have to input title", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            // if invalid price
            } else if(check.Length != 0) {
                MessageBox.Show(check, "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                decimal price = decimal.Parse(Price);
                Album album = new Album()
                {
                    Title = title,
                    Price = price,
                    GenreId= genre.GenreId,
                    ArtistId = artist.ArtistId,
                    AlbumUrl = AlbumURL
                };
                // add album
                context.Albums.Add(album);
                int number = context.SaveChanges();
                // if add successful
                if(number > 0)
                {
                    MessageBox.Show("A new album is added");
                    DisplayListFilm();
                }

            }


        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Genre genre = (Genre)cbGenre.SelectedValue;
            Artist artist = (Artist)cbArtist.SelectedValue;
            string title = tbTitle.Text.Trim();
            string Price = tbPrice.Text.Trim();
            string AlbumURL = tbAlbum.Text.Trim();
            string check = CheckValuePrice(Price);
            // if not select album to update
            if (AlbumID == null)
            {
                MessageBox.Show("You have to choose album to update", "", MessageBoxButton.OK, MessageBoxImage.Warning);
               // if title empty
            } else if (title.Length == 0)
            {
                MessageBox.Show("You have to input title", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                // if invalid price
            } else if (check.Length != 0)
            {
                MessageBox.Show(check, "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                decimal price = decimal.Parse(Price);
                Album? album = context.Albums.FirstOrDefault(album => album.AlbumId == AlbumID);
                // if get album successful
                if (album != null)
                {
                    // set value
                    album.Title = title;
                    album.Price = price;
                    album.ArtistId = artist.ArtistId;
                    album.AlbumUrl = AlbumURL;
                    album.GenreId = genre.GenreId;
                    // update album
                    context.Albums.Update(album);
                    int number = context.SaveChanges();
                    // if update successful
                    if (number > 0)
                    {
                        MessageBox.Show("That album is updated");
                        DisplayListFilm();
                    }
                }
                
                
            }

        }
    }
}
