using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Data.SQLite;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LibraryOtomation
{
    /// <summary>
    /// Books.xaml etkileşim mantığı
    /// </summary>
    public partial class Books : Page
    {
        public Books()
        {
            InitializeComponent();
             string query = "select * from books where İmage_url !='' ";
             AddBooksToPanel(query);
     


        }


        private List<Book> Get_url_from_Db(string query)
        {
            List<Book> Allbook = new List<Book>();
            string conn_Str = "Data Source= Library.db;Version=3;";
            
            using (SQLiteConnection conn = new SQLiteConnection(conn_Str))
            {
                conn.Open();
                //string query = "Select İmage_url from Books";   //SQLiteCommand cmd = conn.CreateCommand()  diğer seçeneck query vremene gerke yok şuanlık
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : Convert.ToInt32(reader["ID"]);

                            // 'Name' sütununu alırken 'NULL' kontrolü yapıyoruz
                            string name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : (string)reader["Name"];

                            // 'Writer' sütununu alırken 'NULL' kontrolü yapıyoruz
                            string writer = reader.IsDBNull(reader.GetOrdinal("Writer")) ? string.Empty : (string)reader["Writer"];

                            // 'Publication_Year' sütununu alırken 'NULL' kontrolü yapıyoruz
                            int publicationYear = reader.IsDBNull(reader.GetOrdinal("Publication_Year")) ? 0 : Convert.ToInt32(reader["Publication_Year"]);

                            // 'Category_ID' sütununu alırken 'NULL' kontrolü yapıyoruz
                            int categoryId = reader.IsDBNull(reader.GetOrdinal("Category_ID")) ? 0 : Convert.ToInt32(reader["Category_ID"]);

                            // 'Cabinet_NO' sütununu alırken 'NULL' kontrolü yapıyoruz
                            int cabinetNo = reader.IsDBNull(reader.GetOrdinal("Cabinet_NO")) ? 0 : Convert.ToInt32(reader["Cabinet_NO"]);

                            // 'Owner_ID' sütununu alırken 'NULL' kontrolü yapıyoruz
                            int ownerId = reader.IsDBNull(reader.GetOrdinal("Owner_ID")) ? -1 : Convert.ToInt32(reader["Owner_ID"]);

                            // 'İmage_url' sütununu alırken 'NULL' kontrolü yapıyoruz
                            string imageUrl = reader.IsDBNull(reader.GetOrdinal("İmage_url")) ? string.Empty : (string)reader["İmage_url"];

                            
                            // Veritabanından aldığımız verilerle yeni bir Book nesnesi oluşturuyoruz
                            Allbook.Add(new Book(id, name, writer, publicationYear, ownerId, imageUrl));



                        }
                    }

                }

            }
            return Allbook;  
        }


        public void AddBooksToPanel(string query)
        {
            Bookpanel.Children.Clear();
            foreach (var book in Get_url_from_Db(query))
            {
                // Yeni bir Border kontrolü oluşturuyoruz
                var border = new Border
                {
                    Width = 150,
                    Height = 190,
                    Margin = new System.Windows.Thickness(10),
                    CornerRadius = new CornerRadius(10),
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(3),
                    ClipToBounds = true,
                    Background = Brushes.Transparent
                };

                try
                {
                    if(book.icon_url == "") return;  // uygulama gereği   fotoğraf yoksa gelmesin hiç !

                    // Yeni bir Image kontrolü oluşturuyoruz
                    var bookImage = new Image
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 180,
                        Height = 190,
                        Source = new BitmapImage(new Uri(book.icon_url, UriKind.Absolute)),
                        Stretch = Stretch.Fill,
                    };

                    // Popup oluşturma
                    var popup = new Popup
                    {
                        AllowsTransparency = true,
                        PlacementTarget = border,
                        Placement = PlacementMode.Mouse,
                        StaysOpen = true,
                        Width =280,
                        Height = 170,
                        Child = new Border
                        {
                            Background = new SolidColorBrush(Color.FromRgb(209,99,2)),
                            BorderBrush = Brushes.Transparent,
                            BorderThickness = new Thickness(1),
                            Padding = new Thickness(12),
                            CornerRadius = new CornerRadius(11),
                            Child = new TextBlock
                            {
                               FontSize=15,
                               HorizontalAlignment= HorizontalAlignment.Left,
                               VerticalAlignment = VerticalAlignment.Top,
                                Text = $"\nName : {book.name}\n Writer : {book.autor}\n Publication Year : {book.publication_year}\n Owner:{Book.Search_owner(book.owner_id)}",
                                TextWrapping = TextWrapping.Wrap,
                                Width = 280
                            }
                        }
                    };

                    // Tıklama olayı için handler ekleme
                    border.MouseLeftButtonDown += (sender, e) =>
                    {
                        popup.IsOpen = !popup.IsOpen; // Aç/Kapat
                    };

                    // Image'i Border'ın içine ekliyoruz
                    border.Child = bookImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Resim yüklenemedi: {book.icon_url}\nHata: {ex.Message}");
                    return;
                }

                // Border'ı panele ekliyoruz
                Panel.SetZIndex(border, 2);
                Bookpanel.Children.Add(border);
            }
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(book_search_box is null))
            {
                Bookpanel.Children.Clear();

                //string query = $"SELECT İmage_url FROM Books WHERE Name LIKE '%{book_search_box.Text}%'";
                string query = $"SELECT * FROM Books WHERE Name LIKE '%{book_search_box.Text}%'";

                AddBooksToPanel(query);
            }

          
        }

        private void book_search_box_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
            //if (book_search_box.Text == "") return;    // boşken entere müsaade ediyorumm!!
             if (e.Key == Key.Enter) //trigerler ile buton enterla işlev yapmama çünkü trigerler UI üzerinde tasarım değişikiklerini mümkün kılar 
            {
                search_button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                // MessageBox.Show("lan ");
            }
            
           // search_button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));   YAZMAYA BAŞLADIKTAN SONRA ANLIK ARAR !!

        }
    }

    

}
