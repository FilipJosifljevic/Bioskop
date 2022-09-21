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
namespace Bioskop.Forme
{
    /// <summary>
    /// Interaction logic for FrmLogIn.xaml
    /// </summary>
    public partial class FrmLogIn : Window
    {
        string radnikUser = "radnikblagajne";
        string radnikPass = "radnikblagajne123";
        string adminUser = "admin";
        string adminPass = "admin123";
        static string ulogovani;
        public FrmLogIn()
        {
            InitializeComponent();
            txtKorisnickoIme.Focus();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public string passwordCheck()
        {
                    if(txtKorisnickoIme.Text.Equals(radnikUser) && pbpassword.Password.Equals(radnikPass))
                    {
                        ulogovani = "radnik";
                        return ulogovani;
                        
                    }
                    else if(txtKorisnickoIme.Text.Equals(adminUser) && pbpassword.Password.Equals(adminPass))
                    {
                        ulogovani = "admin";
                        return ulogovani;
                    }
                    else
                    {
                        return null;
                    }
        }
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public static string vratiUlogovanog()
        {
            FrmLogIn login = new FrmLogIn();
            login.ShowDialog();
            string ulogovani = login.passwordCheck();
            return ulogovani;
        }
    }
}
