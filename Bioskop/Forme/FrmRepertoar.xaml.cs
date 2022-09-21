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
    /// Interaction logic for FrmRepertoar.xaml
    /// </summary>
    public partial class FrmRepertoar : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmRepertoar()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtKapacitet.Focus();
        }
        public FrmRepertoar(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtKapacitet.Focus();
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
                cmd.Parameters.Add("@kapacitet", System.Data.SqlDbType.Int).Value = int.Parse(txtKapacitet.Text);
                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocniRed["ID"];
                    cmd.CommandText = @"UPDATE tblRepertoar SET Kapacitet=@kapacitet WHERE RepertoarID=@id";
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO tblRepertoar (Kapacitet)
                                     VALUES (@kapacitet)";
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

        private void txtKapacitet_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < txtKapacitet.Text.Length; i++)
            {
                if (!char.IsNumber(txtKapacitet.Text[i]))
                {
                    MessageBox.Show("Kapacitet se mora sastojati samo od cifara!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtKapacitet.Text = "";
                }
            }
        }
    }
}
