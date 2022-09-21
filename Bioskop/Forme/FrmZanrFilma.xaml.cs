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
    /// Interaction logic for FrmZanrFilma.xaml
    /// </summary>
    public partial class FrmZanrFilma : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmZanrFilma()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNazivZanra.Focus();
        }
        public FrmZanrFilma(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNazivZanra.Focus();
            this.azuriraj=azuriraj;
            this.pomocniRed=pomocniRed;
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
                cmd.Parameters.Add("@naziv", System.Data.SqlDbType.NVarChar).Value = txtNazivZanra.Text;
                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocniRed["ID"];
                    cmd.CommandText = @"UPDATE tblZanrFilma SET NazivZanra=@naziv WHERE ZanrID=@id";
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO tblZanrFilma (NazivZanra)
                                       VALUES (@naziv)";
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

        private void txtNazivZanra_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < txtNazivZanra.Text.Length; i++)
            {
                if (!char.IsLetter(txtNazivZanra.Text[i]))
                {
                    MessageBox.Show("Naziv zanra se mora sastojati samo od slova!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtNazivZanra.Text = "";
                }
            }
        }
    }
}
