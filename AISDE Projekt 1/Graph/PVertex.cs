using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_Projekt_1 {
    public class PVertex : Vertex {

        public List<(PVertex ver, Edge edg)> Neighbours { get; set; } = new List<(PVertex ver, Edge edg)>();
        public PVertex Previous { get; set; }

        public PVertex(Vertex vertex) {
            this.ID = vertex.ID;
            this.X = vertex.X;
            this.Y = vertex.Y;
        }

        public bool Equals(Vertex v) {
            return (v.ID == this.ID && v.X == this.X && v.Y == this.Y);
        }

        public bool Equals(PVertex v) {
            return v.ID == this.ID && v.X == this.X && v.Y == this.Y;
        }

        public void AddNeighbours(PVertex vertex, Edge edg) => Neighbours.Add((vertex, edg));
    }
}

