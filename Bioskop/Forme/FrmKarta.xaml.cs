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
    /// Interaction logic for FrmKarta.xaml
    /// </summary>
    public partial class FrmKarta : Window
    {
        Konekcija kon=new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmKarta()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtCena.Focus();
            try
            {
                konekcija.Open();
                string vratiTransakcije = @"SELECT TransakcijaID FROM tblTransakcija";
                SqlDataAdapter daTransakcija=new SqlDataAdapter(vratiTransakcije,konekcija);
                DataTable dtTransakcija = new DataTable();
                daTransakcija.Fill(dtTransakcija);
                cbxTransakcijaID.ItemsSource = dtTransakcija.DefaultView;
                daTransakcija.Dispose();
                dtTransakcija.Dispose();

                string vratiFilmove = @"SELECT FilmID,Naziv FROM tblFilm";
                SqlDataAdapter daFilm = new SqlDataAdapter(vratiFilmove,konekcija);
                DataTable dtFilm=new DataTable();
                daFilm.Fill(dtFilm);
                cbxFilm.ItemsSource = dtFilm.DefaultView;
                daFilm.Dispose();
                dtFilm.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popunjene!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null) { 
                    konekcija.Close();
                }
            }
        }
        public FrmKarta(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtCena.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            try
            {
                konekcija.Open();
                string vratiTransakcije = @"SELECT TransakcijaID FROM tblTransakcija";
                SqlDataAdapter daTransakcija = new SqlDataAdapter(vratiTransakcije, konekcija);
                DataTable dtTransakcija = new DataTable();
                daTransakcija.Fill(dtTransakcija);
                cbxTransakcijaID.ItemsSource = dtTransakcija.DefaultView;
                daTransakcija.Dispose();
                dtTransakcija.Dispose();

                string vratiFilmove = @"SELECT FilmID,Naziv FROM tblFilm";
                SqlDataAdapter daFilm = new SqlDataAdapter(vratiFilmove, konekcija);
                DataTable dtFilm = new DataTable();
                daFilm.Fill(dtFilm);
                cbxFilm.ItemsSource = dtFilm.DefaultView;
                daFilm.Dispose();
                dtFilm.Dispose();

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
                DateTime dt = (DateTime)dpDatumProjekcije.SelectedDate;
                string datum = dt.ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@cena", System.Data.SqlDbType.Int).Value = int.Parse(txtCena.Text);
                cmd.Parameters.Add("@datump", System.Data.SqlDbType.Date).Value = datum;
                cmd.Parameters.Add("@transakcijaid", System.Data.SqlDbType.Int).Value = cbxTransakcijaID.SelectedValue;
                cmd.Parameters.Add("@filmid", System.Data.SqlDbType.Int).Value = cbxFilm.SelectedValue;
                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocniRed["ID"];
                    cmd.CommandText = @"UPDATE tblKarta SET Cena=@cena,DatumProjekcije=@datump,TransakcijaID=@transakcijaiD,FilmID=@filmid
                                    WHERE KartaID=@id";
                }
                else { 
                    cmd.CommandText = @"INSERT INTO tblKarta (Cena,DatumProjekcije,TransakcijaID,FilmID)
                                    VALUES (@cena,@datump,@transakcijaid,@filmid)";
                }
                
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();

            }
            catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih podataka nije validan!","Greska!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null) {
                    konekcija.Close();
                }  
            }
        }

        private void txtCena_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < txtCena.Text.Length; i++)
            {
                if (!char.IsNumber(txtCena.Text[i]))
                {
                    MessageBox.Show("Cena se mora sastojati samo od brojeva!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtCena.Text = "";
                }
            }
        }
    }
}
