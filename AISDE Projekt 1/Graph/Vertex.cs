using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_Projekt_1 {
    public class Vertex {
        public int ID { get; protected set; }
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public Vertex() {
            this.ID = 0;
            this.X = 0;
            this.Y = 0;
        }
        public Vertex(int id, int x, int y) {
            this.ID = id;
            this.X = x;
            this.Y = y;
        }

    }
}
