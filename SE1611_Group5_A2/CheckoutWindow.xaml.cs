using Microsoft.Data.SqlClient;
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
    /// Interaction logic for CheckoutWindow.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        MusicStoreContext _context;
        public CheckoutWindow()
        {
            InitializeComponent();
            _context= new MusicStoreContext();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstName.Text.Trim();
            string lastName = LastName.Text.Trim();
            string address = Address.Text.Trim();
            string city = City.Text.Trim();
            string state = State.Text.Trim();
            string country = Country.Text.Trim();
            string phone = Phone.Text.Trim();
            string email = Email.Text.Trim();
            decimal total ;
            if (!decimal.TryParse(Total.Text.Trim(),out total))
            {
                MessageBox.Show("Total phải không được bỏ trống và là số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Order order = new Order();
            order.FirstName = firstName;
            order.LastName = lastName;
            order.Address = address;
            order.City = city;
            order.State = state;
            order.Country = country;
            order.Phone = phone;
            order.Email = email;
            order.Total = total;
            order.OrderDate = DateTime.Now;
            order.UserName = order.FirstName + " " + order.LastName;
            _context.Orders.Add(order);
            _context.SaveChanges();
            int orderId = order.OrderId;
            MessageBox.Show("Order = "+orderId+" is saved", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //SaveOrder(order);
        }

        private void SaveOrder(Order order)
        {
            // Chuỗi kết nối tới SQL Server
            string connectionString = "Server=LAPTOP-1RCFEU2R;Database=MusicStore;User Id=sa;Password=123456;";

            // Tạo kết nối
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Câu lệnh SQL để chèn dữ liệu vào bảng Persons
                string sql = "INSERT INTO Orders " +
                    "(OrderDate, UserName, FirstName, LastName, Address, City, State, Country, Phone, Email, Total)" +
                    " VALUES (@OrderDate, @UserName, @FirstName, @LastName, @Address, @City, @State, @Country, @Phone, @Email, @Total)";

                // Tạo đối tượng SqlCommand
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Thêm các tham số vào câu lệnh SQL
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@UserName", order.UserName);
                    command.Parameters.AddWithValue("@FirstName", order.FirstName);
                    command.Parameters.AddWithValue("@LastName", order.LastName);
                    command.Parameters.AddWithValue("@Address", order.Address);
                    command.Parameters.AddWithValue("@City", order.City);
                    command.Parameters.AddWithValue("@State", order.State);
                    command.Parameters.AddWithValue("@Country", order.Country);
                    command.Parameters.AddWithValue("@Phone", order.Phone);
                    command.Parameters.AddWithValue("@Email", order.Email);
                    command.Parameters.AddWithValue("@Total", order.Total);

                    // Thực thi câu lệnh SQL
                    int saveSuccessful= command.ExecuteNonQuery();

                  
                    if (saveSuccessful > 0)
                    {
                        MessageBox.Show("Order đã được lưu thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Lưu Order thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }
            }
        }
    }
}
