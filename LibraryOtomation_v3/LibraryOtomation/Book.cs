using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Data;
using System.Windows;

namespace LibraryOtomation
{
    internal class Book
    {
        public int id;
        public string name;
        public string autor;
        public int publication_year;
        public int owner_id;
        public string icon_url;
        public Book(int ıd,string Name,string Autor ,int pb_year ,int Own_id , string icon_path)
        {

            id = ıd;
            owner_id = Own_id;
            name = Name;
            autor = Autor;
            publication_year = pb_year;
            icon_url =  icon_path;  
        }


        public static string Search_owner(int owner_id)  // benzer fonksiyon yazalım   kitap idsinden kitap name döndürsün
        {
            string conn_Str = "Data Source= Library.db;Version=3;";
            string query = "select name from members where ID=@owner_id";
            using (SQLiteConnection conn = new SQLiteConnection(conn_Str))
            {
                conn.Open();
                //string query = "Select İmage_url from Books";   //SQLiteCommand cmd = conn.CreateCommand()  diğer seçeneck query vremene gerke yok şuanlık

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@owner_id", owner_id);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            return reader.GetString(0); 

                        }
                    }

                }

            }
            return null;
        }
        public static string search_book_owned(int book_id) // owner_id alıp book tablosunda eğer sahibi varsa bu kitabın ismi return ediliyor
        {
            
            string conn_str = "Data Source= Library.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(conn_str))
            {
                conn.Open() ;   
                string cmd_txt = $"select Name from Books where Owner_ID ='{book_id}'";
                using(SQLiteCommand cmd = new SQLiteCommand(cmd_txt, conn))
                {
                    using(SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader["Name"].ToString();
                        }
                        return "No Has Book !"; // sahipi yoktur kitabın !!  // null yaparsam bir şey dönmeyecek
                    }
                }

            }



        }

    }
}
