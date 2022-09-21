using Bioskop.Forme;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bioskop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ucitanaTabela;
        bool azuriraj;
        Konekcija kon=new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        #region SelectUpiti
        static string radniciBlagajneSelect = @"SELECT RadnikID AS ID,Ime,Prezime FROM tblRadnikBlagajne";
        static string kupciSelect = @"SELECT KupacID AS ID,Ime,Prezime,JMBG,Adresa,BrojTelefona FROM tblKupac";
        static string transakcijeSelect = @"SELECT TransakcijaID AS ID,NacinPlacanja,RadnikBlagajneID AS 'Radnik ID',KupacID AS 'Kupac ID' FROM tblTransakcija";
        static string karteSelect = @"SELECT KartaID AS ID,Cena,DatumProjekcije,TransakcijaID AS 'Transakcija ID',FilmID AS 'Film ID' FROM tblKarta";
        static string filmoviSelect = @"SELECT FilmID AS ID,Naziv,Godina,ZanrID AS 'Zanr ID',RediteljID AS 'Reditelj ID',RepertoarID AS 'Repertoar ID' FROM tblFilm";
        static string rediteljiSelect = @"SELECT RediteljID AS ID,Ime,Prezime FROM tblReditelj";
        static string zanroviSelect = @"SELECT ZanrID AS ID,NazivZanra AS 'Naziv Zanra' FROM tblZanrFilma";
        static string repertoariSelect = @"SELECT RepertoarID AS ID,Kapacitet FROM tblRepertoar";
        static string adminiSelect = @"SELECT AdminID AS ID,Ime,Prezime,RepertoarID AS 'Repertoar ID' FROM tblAdmin";
        #endregion
        #region SelectUpiti sa uslovom
        static string radniciUslov = @"SELECT * FROM tblRadnikBlagajne WHERE RadnikID=";
        static string kupciUslov = @"SELECT * FROM tblKupac WHERE KupacID=";
        static string transakcijeUslov = @"SELECT * FROM tblTransakcija WHERE TransakcijaID=";
        static string karteUslov = @"SELECT * FROM tblKarta WHERE KartaID=";
        static string filmoviUslov = @"SELECT * FROM tblFilm WHERE FilmID=";
        static string rediteljiUslov = @"SELECT * FROM tblReditelj WHERE RediteljID=";
        static string zanroviUslov = @"SELECT * FROM tblZanrFilma WHERE ZanrID=";
        static string repertoariUslov = @"SELECT * FROM tblRepertoar WHERE RepertoarID=";
        static string adminiUslov = @"SELECT * FROM tblAdmin WHERE AdminID=";
        #endregion
        #region DeleteUpiti
        static string radniciDelete = @"DELETE FROM tblRadnikBlagajne WHERE RadnikID=";
        static string kupciDelete = @"DELETE FROM tblKupac WHERE KupacID=";
        static string transakcijeDelete = @"DELETE FROM tblTransakcija WHERE TransakcijaID=";
        static string karteDelete = @"DELETE FROM tblKarta WHERE KartaID=";
        static string filmoviDelete = @"DELETE FROM tblFilm WHERE FilmID=";
        static string rediteljiDelete = @"DELETE FROM tblReditelj WHERE RediteljID=";
        static string zanroviDelete = @"DELETE FROM tblZanrFilma WHERE ZanrID=";
        static string repertoariDelete = @"DELETE FROM tblRepertoar WHERE RepertoarID=";
        static string adminiDelete = @"DELETE FROM tblAdmin WHERE AdminID=";
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            dataGridLevo.Visibility = Visibility.Collapsed;
            konekcija =kon.KreirajKonekciju();
            btnDodaj.IsEnabled = false;
            btnIzmeni.IsEnabled = false;
            btnObrisi.IsEnabled = false;
            btnRadnikBlagajne.IsEnabled = false;
            btnKupac.IsEnabled = false;
            btnTransakcija.IsEnabled=false;
            btnKarta.IsEnabled = false;
            btnFilm.IsEnabled = false;
            btnReditelj.IsEnabled = false;
            btnZanrFilma.IsEnabled = false;
            btnAdmin.IsEnabled = false;
            btnRepertoar.IsEnabled = false;
            btnLogOut.IsEnabled = false;
        }
        public void UcitajPodatke(DataGrid grid, string selectUpit)
        {
            try
            {
                konekcija.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(selectUpit,konekcija);
                DataTable dt =new DataTable();
                adapter.Fill(dt);
                if (grid != null) {
                    grid.ItemsSource = dt.DefaultView;
                }
                ucitanaTabela = selectUpit;
                dt.Dispose();
                adapter.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Neuspesno ucitani podaci!","Greska",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null) { 
                    konekcija.Close();
                }
                
            }
            
        }

        private void btnRadnikBlagajne_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridLevo, radniciBlagajneSelect);
            dataGridLevo.Visibility = Visibility.Visible;
        }

        private void btnKupac_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridLevo, kupciSelect);
            dataGridLevo.Visibility = Visibility.Visible;
        }

        private void btnTransakcija_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridLevo, transakcijeSelect);
            dataGridLevo.Visibility = Visibility.Visible;
        }

        private void btnKarta_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridLevo, karteSelect);
            dataGridLevo.Visibility = Visibility.Visible;
        }

        private void btnFilm_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridLevo, filmoviSelect);
            dataGridLevo.Visibility = Visibility.Visible;
        }

        private void btnReditelj_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridLevo, rediteljiSelect);
            dataGridLevo.Visibility = Visibility.Visible;
        }

        private void btnZanrFilma_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridLevo, zanroviSelect);
            dataGridLevo.Visibility = Visibility.Visible;
        }

        private void btnRepertoar_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridLevo, repertoariSelect);
            dataGridLevo.Visibility = Visibility.Visible;
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridLevo, adminiSelect);
            dataGridLevo.Visibility = Visibility.Visible;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;
            if (ucitanaTabela.Equals(radniciBlagajneSelect))
            {
                prozor = new FrmRadnikBlagajne();
                prozor.ShowDialog();
                UcitajPodatke(dataGridLevo, radniciBlagajneSelect);
            }
            else if (ucitanaTabela.Equals(kupciSelect))
            {
                prozor = new FrmKupac();
                prozor.ShowDialog();
                UcitajPodatke(dataGridLevo, kupciSelect);
            }
            else if (ucitanaTabela.Equals(transakcijeSelect))
            {
                prozor = new FrmTransakcija();
                prozor.ShowDialog();
                UcitajPodatke(dataGridLevo, transakcijeSelect);
            }
            else if (ucitanaTabela.Equals(karteSelect))
            {
                prozor = new FrmKarta();
                prozor.ShowDialog();
                UcitajPodatke(dataGridLevo, karteSelect);
            }
            else if (ucitanaTabela.Equals(filmoviSelect))
            {
                prozor = new FrmFilm();
                prozor.ShowDialog();
                UcitajPodatke(dataGridLevo, filmoviSelect);
            }
            else if (ucitanaTabela.Equals(rediteljiSelect))
            {
                prozor = new FrmReditelj();
                prozor.ShowDialog();
                UcitajPodatke(dataGridLevo, rediteljiSelect);
            }
            else if (ucitanaTabela.Equals(zanroviSelect))
            {
                prozor = new FrmZanrFilma();
                prozor.ShowDialog();
                UcitajPodatke(dataGridLevo, zanroviSelect);
            }
            else if (ucitanaTabela.Equals(repertoariSelect))
            {
                prozor = new FrmRepertoar();
                prozor.ShowDialog();
                UcitajPodatke(dataGridLevo, repertoariSelect);
            }
            else if (ucitanaTabela.Equals(adminiSelect))
            {
                prozor = new FrmAdmin();
                prozor.ShowDialog();
                UcitajPodatke(dataGridLevo, adminiSelect);
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(radniciBlagajneSelect))
            {
                PopuniFormu(dataGridLevo,radniciUslov);
                UcitajPodatke(dataGridLevo, radniciBlagajneSelect);
            }
            else if (ucitanaTabela.Equals(kupciSelect))
            {
                PopuniFormu(dataGridLevo, kupciUslov);
                UcitajPodatke(dataGridLevo, kupciSelect);
            }
            else if (ucitanaTabela.Equals(transakcijeSelect))
            {
                PopuniFormu(dataGridLevo, transakcijeUslov);
                UcitajPodatke(dataGridLevo, transakcijeSelect);
            }
            else if (ucitanaTabela.Equals(karteSelect))
            {
                PopuniFormu(dataGridLevo, karteUslov);
                UcitajPodatke(dataGridLevo, karteSelect);
            }
            else if (ucitanaTabela.Equals(filmoviSelect))
            {
                PopuniFormu(dataGridLevo, filmoviUslov);
                UcitajPodatke(dataGridLevo, filmoviSelect);
            }
            else if (ucitanaTabela.Equals(rediteljiSelect))
            {
                PopuniFormu(dataGridLevo, rediteljiUslov);
                UcitajPodatke(dataGridLevo, rediteljiSelect);
            }
            else if (ucitanaTabela.Equals(zanroviSelect))
            {
                PopuniFormu(dataGridLevo, zanroviUslov);
                UcitajPodatke(dataGridLevo, zanroviSelect);
            }
            else if (ucitanaTabela.Equals(repertoariSelect))
            {
                PopuniFormu(dataGridLevo, repertoariUslov);
                UcitajPodatke(dataGridLevo, repertoariSelect);
            }
            else if (ucitanaTabela.Equals(adminiSelect))
            {
                PopuniFormu(dataGridLevo, adminiUslov);
                UcitajPodatke(dataGridLevo, adminiSelect);
            }
        }
        void PopuniFormu(DataGrid grid, string selectUslov)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                azuriraj = true;
                SqlCommand komanda = new SqlCommand
                { 
                    Connection=konekcija
                };
                komanda.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                komanda.CommandText = selectUslov + "@id";
                SqlDataReader citac=komanda.ExecuteReader();
                komanda.Dispose();
                while (citac.Read()) 
                {
                    if (ucitanaTabela.Equals(radniciBlagajneSelect))
                    {
                        FrmRadnikBlagajne prozorRadnikBlagajne = new FrmRadnikBlagajne(azuriraj, red);
                        prozorRadnikBlagajne.txtIme.Text = citac["Ime"].ToString();
                        prozorRadnikBlagajne.txtPrezime.Text=citac["Prezime"].ToString();
                        prozorRadnikBlagajne.ShowDialog();                    
                    }
                    else if (ucitanaTabela.Equals(kupciSelect))
                    {
                        FrmKupac prozorKupac = new FrmKupac(azuriraj, red);
                        prozorKupac.txtIme.Text = citac["Ime"].ToString();
                        prozorKupac.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorKupac.txtJMBG.Text = citac["JMBG"].ToString();
                        prozorKupac.txtAdresa.Text = citac["Adresa"].ToString();
                        prozorKupac.txtBrojTelefona.Text = citac["BrojTelefona"].ToString();
                        prozorKupac.ShowDialog();                   
                    }
                    else if (ucitanaTabela.Equals(transakcijeSelect))
                    {
                        FrmTransakcija prozorTransakcija = new FrmTransakcija(azuriraj, red);
                        prozorTransakcija.txtNacinPlacanja.Text = citac["NacinPlacanja"].ToString();
                        prozorTransakcija.cbxRadnik.SelectedValue = citac["RadnikBlagajneID"].ToString();
                        prozorTransakcija.cbxKupac.SelectedValue = citac["KupacID"].ToString();
                        prozorTransakcija.ShowDialog();                    
                    }
                    else if (ucitanaTabela.Equals(karteSelect))
                    {
                        FrmKarta prozorKarta = new FrmKarta(azuriraj, red);
                        prozorKarta.txtCena.Text = citac["Cena"].ToString();
                        prozorKarta.dpDatumProjekcije.SelectedDate =(DateTime)citac["DatumProjekcije"];
                        prozorKarta.cbxTransakcijaID.SelectedValue = citac["TransakcijaID"].ToString();
                        prozorKarta.cbxFilm.SelectedValue = citac["FilmID"].ToString();
                        prozorKarta.ShowDialog();                   
                    }
                    else if (ucitanaTabela.Equals(filmoviSelect))
                    {
                        FrmFilm prozorFilm = new FrmFilm(azuriraj, red);
                        prozorFilm.txtNaziv.Text = citac["Naziv"].ToString();
                        prozorFilm.dpGodina.SelectedDate = (DateTime)citac["Godina"];
                        prozorFilm.cbxZanr.SelectedValue=citac["ZanrID"].ToString();
                        prozorFilm.cbxReditelj.SelectedValue=citac["RediteljID"].ToString();
                        prozorFilm.cbxRepertoarID.SelectedValue = citac["RepertoarID"].ToString();
                        prozorFilm.ShowDialog();                   
                    }
                    else if (ucitanaTabela.Equals(rediteljiSelect))
                    {
                        FrmReditelj prozorReditelj = new FrmReditelj(azuriraj, red);
                        prozorReditelj.txtIme.Text = citac["Ime"].ToString();
                        prozorReditelj.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorReditelj.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(zanroviSelect))
                    {
                        FrmZanrFilma prozorZanr = new FrmZanrFilma(azuriraj, red);
                        prozorZanr.txtNazivZanra.Text = citac["NazivZanra"].ToString();
                        prozorZanr.ShowDialog();            
                    }
                    else if (ucitanaTabela.Equals(repertoariSelect))
                    {
                        FrmRepertoar prozorRepertoar = new FrmRepertoar(azuriraj, red);
                        prozorRepertoar.txtKapacitet.Text = citac["Kapacitet"].ToString();
                        prozorRepertoar.ShowDialog();                  
                    }
                    else if (ucitanaTabela.Equals(adminiSelect))
                    {
                        FrmAdmin prozorAdmin = new FrmAdmin(azuriraj, red);
                        prozorAdmin.txtIme.Text = citac["Ime"].ToString();
                        prozorAdmin.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorAdmin.cbxRepertoarID.SelectedValue = citac["RepertoarID"].ToString();
                        prozorAdmin.ShowDialog();
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!","Greska!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null) { 
                    konekcija.Close();
                }
                azuriraj = false;
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(radniciBlagajneSelect))
            {
                ObrisiZapis(dataGridLevo, radniciDelete);
                UcitajPodatke(dataGridLevo, radniciBlagajneSelect);
            }
            else if (ucitanaTabela.Equals(kupciSelect))
            {
                ObrisiZapis(dataGridLevo, kupciDelete);
                UcitajPodatke(dataGridLevo, kupciSelect);
            }
            else if (ucitanaTabela.Equals(transakcijeSelect))
            {
                ObrisiZapis(dataGridLevo, transakcijeDelete);
                UcitajPodatke(dataGridLevo, transakcijeSelect);
            }
            else if (ucitanaTabela.Equals(karteSelect))
            {
                ObrisiZapis(dataGridLevo, karteDelete);
                UcitajPodatke(dataGridLevo, karteSelect);
            }
            else if (ucitanaTabela.Equals(filmoviSelect))
            {
                ObrisiZapis(dataGridLevo, filmoviDelete);
                UcitajPodatke(dataGridLevo, filmoviSelect);
            }
            else if (ucitanaTabela.Equals(rediteljiSelect))
            {
                ObrisiZapis(dataGridLevo, rediteljiDelete);
                UcitajPodatke(dataGridLevo, rediteljiSelect);
            }
            else if (ucitanaTabela.Equals(zanroviSelect))
            {
                ObrisiZapis(dataGridLevo, zanroviDelete);
                UcitajPodatke(dataGridLevo, zanroviSelect);
            }
            else if (ucitanaTabela.Equals(repertoariSelect))
            {
                ObrisiZapis(dataGridLevo, repertoariDelete);
                UcitajPodatke(dataGridLevo, repertoariSelect);
            }
            else if (ucitanaTabela.Equals(adminiSelect))
            {
                ObrisiZapis(dataGridLevo, adminiDelete);
                UcitajPodatke(dataGridLevo, adminiSelect);
            }
        }
        void ObrisiZapis(DataGrid grid, string deleteUpit)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni da zelite da obrisete?", "Upozorenje!", MessageBoxButton.YesNo, MessageBoxImage.Stop);
                if(rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand komanda = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    komanda.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    komanda.CommandText = deleteUpit + "@id";
                    komanda.ExecuteNonQuery();
                    komanda.Dispose();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Neuspesno brisanje!","Greska!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null) {
                    konekcija.Close();
                }
            }
        }
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                string ulogovani = FrmLogIn.vratiUlogovanog();
                if (ulogovani.Equals("radnik"))
                {
                    btnAdmin.IsEnabled = false;
                    btnFilm.IsEnabled = false;
                    btnReditelj.IsEnabled = false;
                    btnZanrFilma.IsEnabled = false;
                    btnRepertoar.IsEnabled = false;
                    btnRadnikBlagajne.IsEnabled=false;
                    btnKupac.IsEnabled = true;
                    btnTransakcija.IsEnabled = true;
                    btnKarta.IsEnabled = true;
                    btnDodaj.IsEnabled = true;
                    btnIzmeni.IsEnabled = true;
                    btnObrisi.IsEnabled = true;
                    btnLogOut.IsEnabled = true;
                    btnLogIn.IsEnabled = false;
                    lblLogovanje.Content = "Ulogovani ste kao radnik!";
                }
                else if (ulogovani.Equals("admin"))
                {
                    btnRadnikBlagajne.IsEnabled = true;
                    btnKupac.IsEnabled = true;
                    btnTransakcija.IsEnabled = true;
                    btnKarta.IsEnabled = true;
                    btnFilm.IsEnabled=true;
                    btnReditelj.IsEnabled=true;
                    btnZanrFilma.IsEnabled=true;
                    btnRepertoar.IsEnabled=true;
                    btnAdmin.IsEnabled=true;
                    btnDodaj.IsEnabled=true;
                    btnIzmeni.IsEnabled=true;
                    btnObrisi.IsEnabled=true;
                    btnLogOut.IsEnabled=true;
                    btnLogIn.IsEnabled=false;
                    lblLogovanje.Content = "Ulogovani ste kao admin!";
                }
                else if (ulogovani.Equals(null))
                {
                    MessageBox.Show("Morate uneti podatke!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Pogresni podaci!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            btnDodaj.IsEnabled = false;
            btnIzmeni.IsEnabled = false;
            btnObrisi.IsEnabled = false;
            btnRadnikBlagajne.IsEnabled = false;
            btnKupac.IsEnabled = false;
            btnTransakcija.IsEnabled = false;
            btnKarta.IsEnabled = false;
            btnFilm.IsEnabled = false;
            btnReditelj.IsEnabled = false;
            btnZanrFilma.IsEnabled = false;
            btnAdmin.IsEnabled = false;
            btnRepertoar.IsEnabled = false;
            btnLogOut.IsEnabled = false;
            btnLogIn.IsEnabled = true;
            dataGridLevo.Visibility=Visibility.Hidden;
            lblLogovanje.Content = "";
        }
    }
}
