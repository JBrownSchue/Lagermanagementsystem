using System.Windows;
using System.Windows.Controls;


namespace Lagermanagementsystem
{
    /// <summary>
    /// Interaction logic for LagerWindow.xaml
    /// </summary>
    public partial class LagerWindow : Window
    {
        private readonly Lager lager;

        // Konstruktor, der das Lager an die UI bindet und die Regalansicht erstellt
        public LagerWindow(Lager lager)
        {
            InitializeComponent();
            this.lager = lager;
            CreateRegalAnsicht();
        }
        public void UpdateRegalAnsicht()
        {
            gridRegale.Children.Clear();
            CreateRegalAnsicht();
        }

        private static TextBlock CreateTextBlock(string text)
        {
            return CreateTextBlock(text, FontWeights.Normal);
        }

        // Überladene Methode, um einen Textblock mit festgelegter Schriftstärke zu erstellen
        private static TextBlock CreateTextBlock(string text, FontWeight fontWeight)
        {
            return new TextBlock
            {
                Text = text,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = fontWeight
            };
        }

        private void CreateRegalAnsicht()
        {
            gridRegale.ColumnDefinitions.Clear();
            gridRegale.RowDefinitions.Clear();

            gridRegale.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Erstellt Spalten für jedes Regal und zeigt die Header an
            for (int i = 0; i < lager.Regale.Count; i++)
            {
                gridRegale.ColumnDefinitions.Add(new ColumnDefinition());

                var regalHeader = CreateTextBlock($"Regal {i + 1}", FontWeights.Bold);

                Grid.SetColumn(regalHeader, i);
                Grid.SetRow(regalHeader, 0);
                gridRegale.Children.Add(regalHeader);
            }

            // Füllt die Ansicht mit den Stellplätzen und deren Belegungsstatus
            for (int i = 0; i < lager.Regale.Count; i++)
            {
                for (int j = 0; j < lager.Regale[i].Stellplaetze.Count; j++)
                {
                    var textBlock = CreateTextBlock($"{j + 1}: " + (lager.Regale[i].Stellplaetze[j].IstBelegt
                            ? lager.Regale[i].Stellplaetze[j].Artikel
                            : "-"));

                    Grid.SetColumn(textBlock, i);
                    Grid.SetRow(textBlock, j + 1);

                    gridRegale.Children.Add(textBlock);
                }

                // Stellt sicher, dass ausreichend Zeilen für die Stellplätze existieren
                if (gridRegale.RowDefinitions.Count < lager.Regale[i].Stellplaetze.Count)
                {
                    for (int r = 0; r < lager.Regale[i].Stellplaetze.Count; r++)
                    {
                        gridRegale.RowDefinitions.Add(new RowDefinition());
                    }
                }
            }
        }
    }
}
