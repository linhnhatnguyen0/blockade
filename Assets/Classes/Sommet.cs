using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockade
{
    public class Sommet
    {
        private int distance;
        private int x;
        private int y;
        private List<Arete> aretes;
     
        public int Distance {
            get { return distance; }
            set { distance = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public List<Arete> Aretes { 
            get { return aretes; }
        }
      
        public Sommet(int x,int y) {
            this.x = x;
            this.y = y;
            this.aretes = new List<Arete>();
        }
    }
}
