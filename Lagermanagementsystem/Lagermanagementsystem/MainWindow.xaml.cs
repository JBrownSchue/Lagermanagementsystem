using System.Windows;

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
        }

        private void BtnEinlagern_Click(object sender, RoutedEventArgs e)
        {
            string artikelName = txtArtikelName.Text;

            // Validierung der Eingaben für Artikelname und benötigte Stellplätze
            if (!ValidateArtikelName(artikelName) || !ValidateStellplaetze(txtStellplaetze.Text, out int benoetigteStellplaetze))
            {
                return;
            }

            bool erfolgreichEingelagert = Einlagern(artikelName, benoetigteStellplaetze);

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

            var entfernteWare = Entnehmen(regalIndex, stellplatzIndex);

            txtOutput.Text = entfernteWare != null ? $"{entfernteWare} erfolgreich entnommen." : "An diesem Platz ist keine Ware.";
            UpdateLagerWindow();
        }

        private void BtnAufraeumen_Click(object sender, RoutedEventArgs e)
        {
            Aufraeumen();
            BtnAnzeigen_Click(sender, e);
            txtOutput.Text = "\nDas Lager wurde aufgeräumt.";
        }

        private void BtnAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            if (showWindow == null || !showWindow.IsVisible)
            {
                showWindow = new LagerWindow(lager);
                showWindow.Left = this.Left + this.Width;
                showWindow.Top = this.Top;
                showWindow.Show();
            }
            else
            {
                showWindow.UpdateRegalAnsicht();
            }
        }

        public bool Einlagern(string artikel, int benoetigteStellsplaetze)
        {
            foreach (var regal in lager.Regale)
            {
                int freierPlatz = 0;
                int startIndex = -1;

                // Suche nach zusammenhängenden freien Stellplätzen
                for (int i = 0; i < regal.Stellplaetze.Count; i++)
                {
                    if (!regal.Stellplaetze[i].IstBelegt)
                    {
                        if (freierPlatz == 0)
                        {
                            startIndex = i;
                        }
                        freierPlatz++;

                        // Wenn genügend Platz gefunden wurde, Artikel einlagern
                        if (freierPlatz == benoetigteStellsplaetze)
                        {
                            for (int j = startIndex; j < startIndex + benoetigteStellsplaetze; j++)
                            {
                                regal.Stellplaetze[j].Artikel = artikel;
                                regal.Stellplaetze[j].IstBelegt = true;
                            }
                            return true;
                        }
                    }
                    else
                    {
                        freierPlatz = 0;
                        startIndex = -1;
                    }
                }
            }
            return false; // Kein Platz gefunden
        }
        public string Entnehmen(int regalIndex, int stellplatzIndex)
        {
            var regal = lager.Regale[regalIndex];

            if (regal.Stellplaetze[stellplatzIndex].IstBelegt)
            {
                string artikel = regal.Stellplaetze[stellplatzIndex].Artikel;

                regal.Stellplaetze[stellplatzIndex].Artikel = string.Empty;
                regal.Stellplaetze[stellplatzIndex].IstBelegt = false;

                return artikel;
            }
            return null; // Kein Artikel gefunden
        }

        public void Aufraeumen()
        {
            foreach (var regal in lager.Regale)
            {
                var artikelListe = regal.Stellplaetze.Where(s => s.IstBelegt).ToList();
                int index = 0;

                // Alle belegten Stellplätze nach vorne verschieben
                foreach (var stellplatz in regal.Stellplaetze)
                {
                    if (index < artikelListe.Count)
                    {
                        stellplatz.Artikel = artikelListe[index].Artikel;
                        stellplatz.IstBelegt = true;
                        index++;
                    }
                    else
                    {
                        stellplatz.Artikel = string.Empty;
                        stellplatz.IstBelegt = false;
                    }
                }
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