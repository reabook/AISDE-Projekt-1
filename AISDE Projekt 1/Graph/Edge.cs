using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_Projekt_1 {
    public class Edge {
        public int ID { get; }
        public Vertex Begin { get; }
        public Vertex End { get; }
        public double Cost { get; private set; }
        public Edge() {
            this.ID = 0;
            this.Begin = null;
            this.End = null;
        }
        public Edge(int id, Vertex begin, Vertex end) {
            this.ID = id;
            this.Begin = begin;
            this.End = end;
            CalculateCost(begin, end);
        }

        public void CalculateCost(Vertex a, Vertex b) { 

            Cost = Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.X, 2));

        }
    }
}
