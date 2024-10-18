using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Lagermanagementsystem
{
    public class Stellplatz
    {
        public string Artikel { get; set; } = "";
        public bool IstBelegt { get; set; } = false;
    }

    public class Regal
    {
        public List<Stellplatz> Stellplaetze { get; }

        // Initialisiert das Regal mit einer bestimmten Anzahl an Stellplätzen
        public Regal(int anzahlStellplaetze)
        {
            Stellplaetze = new List<Stellplatz>();
            for (int i = 0; i < anzahlStellplaetze; i++)
            {
                Stellplaetze.Add(new Stellplatz());
            }
        }

        public bool Einlagern(string artikel, int benoetigteStellsplaetze)
        {
            int freierPlatz = 0;
            int startIndex = -1;

            // Suche nach zusammenhängenden freien Stellplätzen
            for (int i = 0; i < Stellplaetze.Count; i++)
            {
                if (!Stellplaetze[i].IstBelegt)
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
                            Stellplaetze[j].Artikel = artikel;
                            Stellplaetze[j].IstBelegt = true;
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
            return false; // Kein Platz gefunden
        }

        public string? Entnehmen(int stellplatzIndex)
        {
            if (Stellplaetze[stellplatzIndex].IstBelegt)
            {
                string artikel = Stellplaetze[stellplatzIndex].Artikel;

                Stellplaetze[stellplatzIndex].Artikel = string.Empty;
                Stellplaetze[stellplatzIndex].IstBelegt = false;

                return artikel;
            }
            return null; // Kein Artikel gefunden
        }

        public void Aufraeumen()
        {
            var artikelListe = Stellplaetze.Where(s => s.IstBelegt).ToList();
            int index = 0;

            // Alle belegten Stellplätze nach vorne verschieben
            foreach (var stellplatz in Stellplaetze)
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

    public class Lager
    {
        public List<Regal> Regale { get; }

        // Initialisiert das Lager mit einer bestimmten Anzahl an Regalen und Stellplätzen pro Regal
        public Lager(int anzahlRegale, int stellplaetzeProRegal)
        {
            Regale = new List<Regal>();
            for (int i = 0; i < anzahlRegale; i++)
            {
                Regale.Add(new Regal(stellplaetzeProRegal));
            }
        }

        public bool Einlagern(string artikel, int benoetigteStellplaetze)
        {
            foreach (var regal in Regale)
            {
                if (regal.Einlagern(artikel, benoetigteStellplaetze))
                {
                    return true;
                }
            }
            return false;
        }

        public string? Entnehmen(int regalIndex, int stellplatzIndex)
        {
            // Greift auf das angegebene Regal und den Stellplatz zu, um den Artikel zu entnehmen.
            return Regale[regalIndex].Entnehmen(stellplatzIndex);
        }

        public void Aufraeumen()
        {
            foreach (var regal in Regale)
            {
                regal.Aufraeumen();
            }
        }
    }
}
