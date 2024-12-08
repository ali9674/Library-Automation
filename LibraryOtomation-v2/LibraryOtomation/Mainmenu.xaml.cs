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

namespace LibraryOtomation
{
    /// <summary>
    /// Mainmenu.xaml etkileşim mantığı
    /// </summary>
    public partial class Mainmenu : Window
    {
        public Mainmenu()
        {
            InitializeComponent();
        }


        public void Remove_button_style()
        {
            Member_btn.Style = (Style)Application.Current.FindResource("not_Selected_btn");
            Member_settings_btn.Style = (Style)Application.Current.FindResource("not_Selected_btn");
            Books_btn.Style = (Style)Application.Current.FindResource("not_Selected_btn");
            Book_Settings_btn.Style = (Style)Application.Current.FindResource("not_Selected_btn");
            Give_book_btn.Style = (Style)Application.Current.FindResource("not_Selected_btn");


        }// mouse click ile tıkkalayınca ve üzerinden ayrılınca style resetleniyor
        bool isStyledMember = false;
        bool isStyledMemberSettings = true;
        bool isStyledBook = true;                      
        bool isStyledBookSettings = true;
        bool isStyledGiveBook = true;


        private void Books_btn_MouseMove(object sender, MouseEventArgs e)
        {
             if(isStyledBook)       // CLİCK OLAYINDA BURAYI GİRİŞLERİ ENGELLİYORUM
            Books_btn.Style = (Style)Application.Current.FindResource("on_hover_btn");
        }

        private void Books_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            if(isStyledBook)// CLİCK OLAYINDA BURAYI GİRİŞLERİ ENGELLİYORUM
                Books_btn.Style = (Style)Application.Current.FindResource("not_Selected_btn");
        }

        private void Member_settings_btn_MouseMove(object sender, MouseEventArgs e)
        {
            if (isStyledMemberSettings)// CLİCK OLAYINDA BURAYI GİRİŞLERİ ENGELLİYORUM
                Member_settings_btn.Style = (Style)Application.Current.FindResource("on_hover_btn");
        }

        private void Member_settings_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isStyledMemberSettings)// CLİCK OLAYINDA BURAYI GİRİŞLERİ ENGELLİYORUM
                Member_settings_btn.Style = (Style)Application.Current.FindResource("not_Selected_btn");
        }

        private void Member_settings_btn_Click(object sender, RoutedEventArgs e)
        {
            Remove_button_style();   //mebvcut styleleri kaldıralım tüm butonlardan

            Style Required_Style = (Style)Application.Current.FindResource("Selected_btn");
            Member_settings_btn.Style = Required_Style;

             isStyledMember = true;
             isStyledMemberSettings = false;
             isStyledBook = true;
             isStyledBookSettings = true;
             isStyledGiveBook = true;
        }

        private void Books_btn_Click(object sender, RoutedEventArgs e)
        {
            Remove_button_style();   //mebvcut styleleri kaldıralım tüm butonlardan

            Style Required_Style = (Style)Application.Current.FindResource("Selected_btn");
            Books_btn.Style = Required_Style;

             isStyledMember = true;
             isStyledMemberSettings = true;
             isStyledBook = false;
             isStyledBookSettings = true;
             isStyledGiveBook = true;
        }

        private void Member_btn_MouseMove(object sender, MouseEventArgs e)
        {
            if (isStyledMember)// CLİCK OLAYINDA BURAYI GİRİŞLERİ ENGELLİYORUM
                Member_btn.Style = (Style)Application.Current.FindResource("on_hover_btn");
        }

        private void Member_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isStyledMember)// CLİCK OLAYINDA BURAYI GİRİŞLERİ ENGELLİYORUM
                Member_btn.Style = (Style)Application.Current.FindResource("not_Selected_btn");
        }

        private void Member_btn_Click(object sender, RoutedEventArgs e)
        {
            Remove_button_style();   //mebvcut styleleri kaldıralım tüm butonlardan

            Style Required_Style = (Style)Application.Current.FindResource("Selected_btn");
            Member_btn.Style = Required_Style;

            isStyledMember = false;
            isStyledMemberSettings = true;
            isStyledBook = true;
            isStyledBookSettings = true;
            isStyledGiveBook = true;
        }

        private void Book_Settings_btn_Click(object sender, RoutedEventArgs e)
        {
            Remove_button_style();   //mebvcut styleleri kaldıralım tüm butonlardan

            Style Required_Style = (Style)Application.Current.FindResource("Selected_btn");
            Book_Settings_btn.Style = Required_Style;

            isStyledMember = true;
            isStyledMemberSettings = true;
            isStyledBook = true;
            isStyledBookSettings = false;
            isStyledGiveBook = true;

        }

        private void Book_Settings_btn_MouseMove(object sender, MouseEventArgs e)
        {
            if (isStyledBookSettings)// CLİCK OLAYINDA BURAYI GİRİŞLERİ ENGELLİYORUM
                Book_Settings_btn.Style = (Style)Application.Current.FindResource("on_hover_btn");
        }

        private void Book_Settings_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isStyledBookSettings)// CLİCK OLAYINDA BURAYI GİRİŞLERİ ENGELLİYORUM
                Book_Settings_btn.Style = (Style)Application.Current.FindResource("not_Selected_btn");
        }

        private void Give_book_btn_Click(object sender, RoutedEventArgs e)
        {
            Remove_button_style();   //mebvcut styleleri kaldıralım tüm butonlardan

            Style Required_Style = (Style)Application.Current.FindResource("Selected_btn");
            Give_book_btn.Style = Required_Style;

            isStyledMember = true;
            isStyledMemberSettings = true;
            isStyledBook = true;
            isStyledBookSettings = true;
            isStyledGiveBook = false;

        }

        private void Give_book_btn_MouseMove(object sender, MouseEventArgs e)
        {
            if (isStyledGiveBook)// CLİCK OLAYINDA BURAYI GİRİŞLERİ ENGELLİYORUM
                Give_book_btn.Style = (Style)Application.Current.FindResource("on_hover_btn");
        }

        private void Give_book_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isStyledGiveBook)// CLİCK OLAYINDA BURAYI GİRİŞLERİ ENGELLİYORUM
                Give_book_btn.Style = (Style)Application.Current.FindResource("not_Selected_btn");
        }
    }
}
