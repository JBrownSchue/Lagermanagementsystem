using System.Windows;
using System.Windows.Media;

namespace Lagermanagementsystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Lager lager;
        private LagerWindow? showWindow;
        public MainWindow()
        {
            InitializeComponent();
            lager = new Lager(10, 20);

            this.Closed += MainWindow_Closed!;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            // Überprüfen, ob das LagerWindow noch offen ist
            if (showWindow != null && showWindow.IsVisible)
            {
                showWindow.Close();
            }
        }

        private void BtnEinlagern_Click(object sender, RoutedEventArgs e)
        {
            string artikelName = txtArtikelName.Text;

            // Validierung der Eingaben für Artikelname und benötigte Stellplätze
            if (!ValidateArtikelName(artikelName) || !ValidateStellplaetze(txtStellplaetze.Text, out int benoetigteStellplaetze))
            {
                return;
            }

            bool erfolgreichEingelagert = lager.Einlagern(artikelName, benoetigteStellplaetze);

            txtOutput.Text = erfolgreichEingelagert ? $"{artikelName} erfolgreich eingelagert" : $"Kein Platz für die Ware.";
            UpdateLagerWindow();
        }

        private void BtnEntnehmen_Click(object sender, RoutedEventArgs e)
        {
            // Validierung der Eingaben für Regal und Stellplatz
            if (!ValidateRegalUndStellplatz(txtRegalIndex.Text, txtStellplaetzIndex.Text, out int regalIndex, out int stellplatzIndex))
            {
                return;
            }

            var entfernteWare = lager.Entnehmen(regalIndex, stellplatzIndex);

            txtOutput.Text = entfernteWare != null ? $"{entfernteWare} erfolgreich entnommen." : "An diesem Platz ist keine Ware.";
            UpdateLagerWindow();
        }

        private void BtnAufraeumen_Click(object sender, RoutedEventArgs e)
        {
            lager.Aufraeumen();
            BtnAnzeigen_Click(sender, e);
            txtOutput.Text = "\nDas Lager wurde aufgeräumt.";
        }

        private void BtnAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            if (showWindow == null || !showWindow.IsVisible)
            {
                showWindow = new LagerWindow(lager)
                {
                    Left = this.Left + this.Width,
                    Top = this.Top
                };
                showWindow.Show();
            }
            else
            {
                showWindow.UpdateRegalAnsicht();
            }
        }

        private void UpdateLagerWindow()
        {
            if (showWindow != null && showWindow.IsVisible)
            {
                showWindow.UpdateRegalAnsicht();
            }
        }

        private bool ValidateArtikelName(string artikelName)
        {
            if (string.IsNullOrEmpty(artikelName))
            {
                txtOutput.Text = "Artikelname darf nicht leer sein!";
                return false;
            }
            return true;
        }

        private bool ValidateStellplaetze(string stellplaetzeText, out int benoetigteStellplaetze)
        {
            // Versucht, die Eingabe in eine Zahl zu konvertieren und prüft, ob diese größer als 0 ist
            if (int.TryParse(stellplaetzeText, out benoetigteStellplaetze) && benoetigteStellplaetze > 0)
            {
                return true;
            }
            txtOutput.Text = "Ungültige Anzahl an Stellplätzen";
            return false;
        }

        private bool ValidateRegalUndStellplatz(string regalText, string stellplatzText, out int regalIndex, out int stellplatzIndex)
        {
            regalIndex = -1;
            stellplatzIndex = -1;

            if (int.TryParse(regalText, out int regalNummer) && int.TryParse(stellplatzText, out int stellplatzNummer))
            {
                regalIndex = regalNummer - 1;
                stellplatzIndex = stellplatzNummer - 1;

                // Überprüft, ob die Indexe im gültigen Bereich der Regale und Stellplätze liegen
                if (regalIndex >= 0 && regalIndex < lager.Regale.Count &&
                    stellplatzIndex >= 0 && stellplatzIndex < lager.Regale[regalIndex].Stellplaetze.Count)
                {
                    return true;
                }
                txtOutput.Text = "Ungültiger Regal- oder Stellplatz Index!";
                return false;
            }
            txtOutput.Text = "Ungültige Eingaben für Regal oder Stellplatz!";
            return false;
        }
    }
}