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
    /// Interaction logic for FrmFilm.xaml
    /// </summary>
    public partial class FrmFilm : Window
    {
        Konekcija kon=new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmFilm()
        {
            InitializeComponent();
            konekcija=kon.KreirajKonekciju();
            txtNaziv.Focus();
            try
            {
                konekcija.Open();
                string vratiZanrove = "SELECT ZanrID,NazivZanra FROM tblZanrFilma";
                SqlDataAdapter daZanr=new SqlDataAdapter(vratiZanrove,konekcija);
                DataTable dtZanr = new DataTable();
                daZanr.Fill(dtZanr);
                cbxZanr.ItemsSource = dtZanr.DefaultView;
                dtZanr.Dispose();
                daZanr.Dispose();

                string vratiReditelje = "SELECT RediteljID,Ime + ' ' + Prezime AS 'Ime i prezime' from tblReditelj";
                SqlDataAdapter daReditelj = new SqlDataAdapter(vratiReditelje,konekcija);
                DataTable dtReditelj = new DataTable();
                daReditelj.Fill(dtReditelj);
                cbxReditelj.ItemsSource = dtReditelj.DefaultView;
                daReditelj.Dispose();
                dtReditelj.Dispose();

                string vratiRepertoare = "SELECT RepertoarID FROM tblRepertoar";
                SqlDataAdapter daRepertoar = new SqlDataAdapter(vratiRepertoare,konekcija);
                DataTable dtRepertoar = new DataTable();
                daRepertoar.Fill(dtRepertoar);
                cbxRepertoarID.ItemsSource = dtRepertoar.DefaultView;
                daRepertoar.Dispose();
                dtRepertoar.Dispose();

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
        public FrmFilm(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNaziv.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            try
            {
                konekcija.Open();
                string vratiZanrove = "SELECT ZanrID,NazivZanra FROM tblZanrFilma";
                SqlDataAdapter daZanr = new SqlDataAdapter(vratiZanrove, konekcija);
                DataTable dtZanr = new DataTable();
                daZanr.Fill(dtZanr);
                cbxZanr.ItemsSource = dtZanr.DefaultView;
                dtZanr.Dispose();
                daZanr.Dispose();

                string vratiReditelje = "SELECT RediteljID,Ime + ' ' + Prezime AS 'Ime i prezime' from tblReditelj";
                SqlDataAdapter daReditelj = new SqlDataAdapter(vratiReditelje, konekcija);
                DataTable dtReditelj = new DataTable();
                daReditelj.Fill(dtReditelj);
                cbxReditelj.ItemsSource = dtReditelj.DefaultView;
                daReditelj.Dispose();
                dtReditelj.Dispose();

                string vratiRepertoare = "SELECT RepertoarID FROM tblRepertoar";
                SqlDataAdapter daRepertoar = new SqlDataAdapter(vratiRepertoare, konekcija);
                DataTable dtRepertoar = new DataTable();
                daRepertoar.Fill(dtRepertoar);
                cbxRepertoarID.ItemsSource = dtRepertoar.DefaultView;
                daRepertoar.Dispose();
                dtRepertoar.Dispose();

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
                DateTime dt = (DateTime)dpGodina.SelectedDate;
                string datum = dt.ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@naziv", System.Data.SqlDbType.NVarChar).Value = txtNaziv.Text;
                cmd.Parameters.Add("@godina", System.Data.SqlDbType.Date).Value = datum;
                cmd.Parameters.Add("@zanr", System.Data.SqlDbType.Int).Value = cbxZanr.SelectedValue;
                cmd.Parameters.Add("@reditelj", System.Data.SqlDbType.Int).Value = cbxReditelj.SelectedValue;
                cmd.Parameters.Add("@repertoar", System.Data.SqlDbType.Int).Value = cbxRepertoarID.SelectedValue;
                if (azuriraj) {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pomocniRed["ID"];
                    cmd.CommandText = @"UPDATE tblFilm SET Naziv=@Naziv,Godina=@godina,ZanrID=@zanr,RediteljID=@reditelj,RepertoarID=@repertoar
                                    WHERE FilmID=@id";
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO tblFilm (Naziv,Godina,ZanrID,RediteljID,RepertoarID)
                                    VALUES (@naziv,@godina,@zanr,@reditelj,@repertoar)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();

            }
            catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih podataka nije validan!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Morate izabrati datum!","Greska!",MessageBoxButton.OK,MessageBoxImage.Error);
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

    }
}
