using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_Projekt_1 {
    public class Path {
        public Vertex Begin { get; }
        public Vertex End { get; }

        public double Cost { get; set; }

        public List<int> Nodes { get; set; } = new List<int>();

        public Path(Vertex begin, Vertex end) {
            this.Begin = begin;
            this.End = end;
        }
    }
}
