using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagermanagementsystem
{
    public class Stellplatz
    {
        public string Artikel { get; set; } = "";
        public bool IstBelegt { get; set; } = false;
    }

    public class Regal
    {
        public List<Stellplatz> Stellplaetze { get; set; }
       
        // Initialisiert das Regal mit einer bestimmten Anzahl an Stellplätzen
        public Regal(int anzahlStellplaetze)
        {
            Stellplaetze = new List<Stellplatz>();
            for (int i = 0; i < anzahlStellplaetze; i++)
            {
                Stellplaetze.Add(new Stellplatz());
            }
        }
    }

    public class Lager
    {
        public List<Regal> Regale { get; set; }

        // Initialisiert das Lager mit einer bestimmten Anzahl an Regalen und Stellplätzen pro Regal
        public Lager(int anzahlRegale, int stellplaetzeProRegal)
        {
            Regale = new List<Regal>();
            for (int i = 0; i < anzahlRegale; i++)
            {
                Regale.Add(new Regal(stellplaetzeProRegal));
            }
        }
    }
}
