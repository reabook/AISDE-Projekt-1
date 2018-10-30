using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_Projekt_1 {
    public class DVertex : Vertex {

        public List<(DVertex ver, Edge edg)> Neighbours { get; set; } = new List<(DVertex ver, Edge edg)>();
        public double Distance { get; set; }
        public (DVertex ver, Edge edg) Previous { get; set; }

        public DVertex((DVertex ver, Edge edg) previous, Vertex vertex) {
            this.Previous = previous;
            this.ID = vertex.ID;
            this.X = vertex.X;
            this.Y = vertex.Y;
        }
        public DVertex((DVertex ver, Edge edg) previous, Vertex vertex, double dist) : this(previous, vertex) {
            this.Distance = dist;
        }

        public bool Equals(Vertex v) {
            return (v.ID == this.ID && v.X == this.X && v.Y == this.Y);
        }

        public bool Equals(DVertex v) {
            return v.ID == this.ID && v.X == this.X && v.Y == this.Y;
        }

        public void AddNeighbours(DVertex vertex, Edge edg) => Neighbours.Add((vertex, edg));
    }
}

