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

namespace LibraryOtomation
{
    /// <summary>
    /// Members.xaml etkileşim mantığı
    /// </summary>
    public partial class Members : Page
    {
        public Members()
        {
            InitializeComponent();
        }

        private void uye_ara_txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(uye_ara_txtbox.Text))
            {
                txtblock_uyearama.Visibility = Visibility.Visible;
            }
            else
            {
                txtblock_uyearama.Visibility = Visibility.Hidden;
            }
        }

        private void search_member_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(uye_ara_txtbox.Text);
        }

        private void uye_ara_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (uye_ara_txtbox.Text == "") return;
            else if (e.Key == Key.Enter) //trigerler ile buton enterla işlev yapmama çünkü trigerler UI üzerinde tasarım değişikiklerini mümkün kılar 
            {
                search_member_btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                // MessageBox.Show("lan ");
            }
        }
    }
}
