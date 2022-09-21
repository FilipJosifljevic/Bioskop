using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for FrmRadnikBlagajne.xaml
    /// </summary>
    public partial class FrmRadnikBlagajne : Window
    {   Konekcija kon=new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmRadnikBlagajne()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtIme.Focus();
        }
        public FrmRadnikBlagajne(bool azuriraj,DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtIme.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }
        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@ime", System.Data.SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@prezime", System.Data.SqlDbType.NVarChar).Value = txtPrezime.Text;
                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocniRed["ID"];
                    cmd.CommandText = @"UPDATE tblRadnikBlagajne SET Ime=@ime,Prezime=@Prezime WHERE RadnikID=@id";
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO tblRadnikBlagajne (Ime,Prezime)
                                    VALUES (@ime,@prezime)";
                } 
                
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();

            }
            catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih podataka nije validan!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void txtIme_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < txtIme.Text.Length; i++)
            {
                if (!char.IsLetter(txtIme.Text[i]))
                {
                    MessageBox.Show("Ime se mora sastojati samo od slova!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtIme.Text = "";
                }
            }
        }

        private void txtPrezime_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < txtPrezime.Text.Length; i++)
            {
                if (!char.IsLetter(txtPrezime.Text[i]))
                {
                    MessageBox.Show("Prezime se mora sastojati samo od slova!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtPrezime.Text = "";
                }
            }
        }
    }
}
