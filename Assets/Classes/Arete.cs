using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Arete
    {
        private Sommet depart;
        private Sommet fin;
        private int distance;

        public Sommet Depart {
            get { return depart; }
            set { depart = value; }
        }
        public Sommet Fin {
            get { return fin; }
            set { fin = value; }
        }
        public int Distance {
            get { return distance; }
            set { distance = value; }
        }

        // Constructeur
        public Arete(Sommet depart, Sommet fin) {
            this.depart = depart;
            this.fin = fin;
        }
    }

