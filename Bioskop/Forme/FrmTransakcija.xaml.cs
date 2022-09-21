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
    /// Interaction logic for FrmTransakcija.xaml
    /// </summary>
    public partial class FrmTransakcija : Window
    {
        Konekcija kon=new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;

        public FrmTransakcija()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNacinPlacanja.Focus();
            try
            {
                konekcija.Open();
                string vratiRadnike = @"SELECT RadnikID,Ime + ' ' + Prezime AS 'Ime i prezime' FROM tblRadnikBlagajne";
                SqlDataAdapter daRadnik=new SqlDataAdapter(vratiRadnike,konekcija);
                DataTable dtRadnik = new DataTable();
                daRadnik.Fill(dtRadnik);
                cbxRadnik.ItemsSource = dtRadnik.DefaultView;
                daRadnik.Dispose();
                dtRadnik.Dispose();

                string vratiKupce = @"SELECT KupacID,Ime + ' ' + Prezime AS 'Ime i prezime' FROM tblKupac";
                SqlDataAdapter daKupac=new SqlDataAdapter(vratiKupce,konekcija);
                DataTable dtKupac = new DataTable();
                daKupac.Fill(dtKupac);
                cbxKupac.ItemsSource= dtKupac.DefaultView;
                daKupac.Dispose();
                dtKupac.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popunjene!","Greska!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null) { 
                    konekcija.Close();
                }
            }
        }
        public FrmTransakcija(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNacinPlacanja.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            try
            {
                konekcija.Open();
                string vratiRadnike = @"SELECT RadnikID,Ime + ' ' + Prezime AS 'Ime i prezime' FROM tblRadnikBlagajne";
                SqlDataAdapter daRadnik = new SqlDataAdapter(vratiRadnike, konekcija);
                DataTable dtRadnik = new DataTable();
                daRadnik.Fill(dtRadnik);
                cbxRadnik.ItemsSource = dtRadnik.DefaultView;
                daRadnik.Dispose();
                dtRadnik.Dispose();

                string vratiKupce = @"SELECT KupacID,Ime + ' ' + Prezime AS 'Ime i prezime' FROM tblKupac";
                SqlDataAdapter daKupac = new SqlDataAdapter(vratiKupce, konekcija);
                DataTable dtKupac = new DataTable();
                daKupac.Fill(dtKupac);
                cbxKupac.ItemsSource = dtKupac.DefaultView;
                daKupac.Dispose();
                dtKupac.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popunjene!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
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
                cmd.Parameters.Add("@nacinplacanja", System.Data.SqlDbType.NVarChar).Value = txtNacinPlacanja.Text;
                cmd.Parameters.Add("@radnik", System.Data.SqlDbType.Int).Value = cbxRadnik.SelectedValue;
                cmd.Parameters.Add("@kupac", System.Data.SqlDbType.Int).Value = cbxKupac.SelectedValue;
                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocniRed["ID"];
                    cmd.CommandText = @"UPDATE tblTransakcija SET NacinPlacanja=@nacinplacanja,RadnikBlagajneID=@radnik,KupacID=@kupac
                                     WHERE TransakcijaID=@id";
                }
                else {
                    cmd.CommandText = @"INSERT INTO tblTransakcija (NacinPlacanja,RadnikBlagajneID,KupacID)
                                    VALUES (@nacinplacanja,@radnik,@kupac)";
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

        private void txtNacinPlacanja_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < txtNacinPlacanja.Text.Length; i++)
            {
                if (!char.IsLetter(txtNacinPlacanja.Text[i]))
                {
                    MessageBox.Show("Nacin placanja se mora sastojati samo od slova!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtNacinPlacanja.Text = "";
                }
            }
        }
    }
}
