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
    /// Interaction logic for FrmKupac.xaml
    /// </summary>
    public partial class FrmKupac : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmKupac()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtIme.Focus();
        }
        public FrmKupac(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtIme.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
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
                cmd.Parameters.Add("@jmbg", System.Data.SqlDbType.NVarChar).Value = txtJMBG.Text;
                cmd.Parameters.Add("@adresa", System.Data.SqlDbType.NVarChar).Value = txtAdresa.Text;
                cmd.Parameters.Add("@telefon", System.Data.SqlDbType.NVarChar).Value = txtBrojTelefona.Text;
                if (azuriraj) {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocniRed["ID"];
                    cmd.CommandText = @"UPDATE tblKupac SET Ime=@ime,Prezime=@prezime,JMBG=@jmbg,Adresa=@adresa,BrojTelefona=@telefon
                                        WHERE KupacID=@id";
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO tblKupac (Ime,Prezime,JMBG,Adresa,BrojTelefona)
                                    VALUES (@ime,@prezime,@jmbg,@adresa,@telefon)";
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

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtJMBG_TextChanged(object sender, TextChangedEventArgs e)
        {
            for(int i = 0; i < txtJMBG.Text.Length; i++)
            {
                if (!char.IsNumber(txtJMBG.Text[i])){
                    MessageBox.Show("JMBG se mora sastojati samo od cifara!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtJMBG.Text = "";
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

        private void txtBrojTelefona_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < txtBrojTelefona.Text.Length; i++)
            {
                if (!char.IsNumber(txtBrojTelefona.Text[i]))
                {
                    MessageBox.Show("Broj telefona se mora sastojati samo od cifara!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtBrojTelefona.Text = "";
                }
            }
        }
    }
}
