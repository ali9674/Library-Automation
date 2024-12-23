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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using System.Data.SqlClient;

namespace LibraryOtomation
{
    /// <summary>
    /// Member_Settings.xaml etkileşim mantığı
    /// </summary>
    public partial class Member_Settings : Page
    { 
        public Member_Settings()
        {
            InitializeComponent();
        }

        private void uye_ara_memberstts_txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(uye_ara_memberstts_txtbox.Text))
            {
                txtblock_uyearama_memberstts.Visibility = Visibility.Visible;
            }
            else
            {
                txtblock_uyearama_memberstts.Visibility = Visibility.Hidden;
            }
        }

        private void search_member_memberstts_btn_Click(object sender, RoutedEventArgs e) // üye getiecek
        {

            if (uye_ara_memberstts_txtbox.Text == "")
            {
                Members_Settings_textboxes_clear();
                return;
            }

            string conn_Str = "Data Source= Library.db;Version=3;";
            using (SQLiteConnection cnn = new SQLiteConnection(conn_Str))
            {
                cnn.Open();
                string fullname = uye_ara_memberstts_txtbox.Text.Trim();
                string[] parts = fullname.Split(' ');
              


                string cmd_text = $"SELECT * FROM Members WHERE Name LIKE '%{parts[0]}%' and Surname LIKE '%{parts[parts.Length-1]}%' ";
                using (SQLiteCommand cmd = new SQLiteCommand(cmd_text,cnn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            memberid_txtbox.Text = reader["ID"].ToString();
                            string name, surname;
                            name = reader["Name"].ToString();
                            surname = reader["Surname"].ToString();
                            name_surname_txtbox.Text = name +" " +surname;
                            phonenumber_txtbox.Text = reader["Phone_number"].ToString();
                            books_txtbox.Text = Book.search_book_owned(Convert.ToInt16(reader["ID"])); // kitabın id sini verip adını bulan fonksiyon
                            tc_txtbox.Text = reader["TC"].ToString();
                        }
                    }
                }

            }
            
        

          //  MessageBox.Show(parts[0]); // ad verir her zaman çünkü trimle siliyorum baş ve sonu
          //  MessageBox.Show(parts[parts.Length-1]); // ad verir her zaman çünkü trimle siliyorum baş ve sonu


        }

        private void search_member_memberstts_btn_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtblock_uyearama_memberstts.Text == "") return;
            else if (e.Key == Key.Enter) //trigerler ile buton enterla işlev yapmama çünkü trigerler UI üzerinde tasarım değişikiklerini mümkün kılar 
            {
                search_member_memberstts_btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                
            }
        }

        private void add_member_btn_Click(object sender, RoutedEventArgs e)
        {    
            if(name_surname_txtbox.Text =="") { MessageBox.Show("Lütfen Ad Soyad Bilgilerini Giriniz !"); return; }
            else if(phonenumber_txtbox.Text=="") { MessageBox.Show("Lütfen Telefon Numarası Giriniz !"); return; }
            else if (tc_txtbox.Text == "") { MessageBox.Show("Lütfen TC Bilgisini Giriniz !"); return; }



            string conn_Str = "Data Source= Library.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(conn_Str))
            {
                conn.Open();
                string cmd_Str = "INSERT INTO Members (Name,Surname,TC,Phone_number) values (@name,@surname,@tc,@phn_nmb)";
                using(SQLiteCommand cmd = new SQLiteCommand(cmd_Str, conn))
                {
                    string fullname = name_surname_txtbox.Text.Trim();
                    string[] parts = fullname.Split(' ');

                    cmd.Parameters.AddWithValue("@name", parts[0]); // adı gönderiyorum
                    cmd.Parameters.AddWithValue("@surname", parts[parts.Length - 1]);
                    cmd.Parameters.AddWithValue("@tc", tc_txtbox.Text); // adı gönderiyorum
                    cmd.Parameters.AddWithValue("@phn_nmb", phonenumber_txtbox.Text); // adı gönderiyorum

                    try
                    {
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show($" {parts[0]} {parts[parts.Length - 1]} Kullanıcısı Başarıyla eklendi");
                        }
                    }
                    catch (Exception ex) {
                        MessageBox.Show("Kullanıcı  Sistemde Mevcut ,Lütfen Bilgileri Kontrol Ediniz !","İnvalid",MessageBoxButton.OK,MessageBoxImage.Stop);
                    }


                }

            }




        }

        private void uye_ara_memberstts_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) //trigerler ile buton enterla işlev yapmama çünkü trigerler UI üzerinde tasarım değişikiklerini mümkün kılar 
            {
                search_member_memberstts_btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
         
            }
        }
        public void Members_Settings_textboxes_clear()
        {
            memberid_txtbox.Clear();
            name_surname_txtbox.Clear();
            phonenumber_txtbox.Clear();
            books_txtbox.Clear();
            tc_txtbox.Clear();
        }


        private void delete_member_btn_Click(object sender, RoutedEventArgs e)
        {
            if (memberid_txtbox.Text == "")
            {
                MessageBox.Show("Lütfen Önce Üye Seçiniz !","İnvalid Member",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }
            string cnn_str = "Data Source= Library.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(cnn_str))
            {
                conn.Open();
                string cmd_Str = $"DELETE FROM Members WHERE ID ={memberid_txtbox.Text}";
                using (SQLiteCommand cmd = new SQLiteCommand(cmd_Str, conn))
                {
                    string deleted_memb_name=name_surname_txtbox.Text;
                   int rows_affected  =  cmd.ExecuteNonQuery();  
                    if (rows_affected > 0)
                    {
                         MessageBox.Show($"{deleted_memb_name} Başarıyla Silindi","Succesful",MessageBoxButton.OK,MessageBoxImage.Information);
                    }
                }
            }
            // şimdi textboxları temizleyelim
           Members_Settings_textboxes_clear();  
        }

        private void update_member_btn_Click(object sender, RoutedEventArgs e)
        {

            if (memberid_txtbox.Text == "")
            {
                MessageBox.Show("Lütfen Önce Üye Seçiniz !", "İnvalid Member", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;  // üye seçilmediyse fonksiyon durduruluyor
            }

            string cnn_Str = "Data Source= Library.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(cnn_Str))
            {
                conn.Open();
                string cmd_Str = "UPDATE Members  SET Name =@new_name,Surname=@new_surname,TC=@new_tc,Phone_number=@new_phnum  WHERE ID=@target_id";
                using (SQLiteCommand cmd = new SQLiteCommand(cmd_Str, conn))
                {
                    string fullname = name_surname_txtbox.Text.Trim();
                    string[] parts = fullname.Split(' ');
                    cmd.Parameters.AddWithValue("@new_name", parts[0]);
                    cmd.Parameters.AddWithValue("@new_surname", parts[parts.Length-1]); // ad ve soyad güncellendi !!
                    cmd.Parameters.AddWithValue("@target_id", memberid_txtbox.Text);
                    cmd.Parameters.AddWithValue("@new_tc",tc_txtbox.Text);
                    cmd.Parameters.AddWithValue("@new_phnum", phonenumber_txtbox.Text);

                    int affected_count = cmd.ExecuteNonQuery();     
                    if (affected_count > 0)
                    {
                        MessageBox.Show("Değişiklikler Başarıyla Uygulandı !");
                    }
                    else
                    {
                        MessageBox.Show("Değiştirilen Alan bulunamadı !!");
                    }
                }

            }

        }
    }
}
