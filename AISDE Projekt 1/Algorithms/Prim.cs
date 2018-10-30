using AISDE_Projekt_1.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_Projekt_1 {
    public class Prim : IAlgorithm {
        private Graph graph;

        private List<PVertex> vertices = new List<PVertex>();
        private List<Edge> edges = new List<Edge>();
        public Prim(Graph graph) {
            this.graph = graph;

            // dodanie krawedzi
            this.edges = graph.Edges;

            foreach (var vertex in graph.Vertices) {
                this.vertices.Add(new PVertex(null, vertex.Value));
            }

            // dodanie wszystkim wierzcholkom sasiadow
            foreach (var edge in edges) {
                var fv = this.vertices.Find(v => v.Equals(edge.Begin));
                var sv = this.vertices.Find(v => v.Equals(edge.End));

                fv.AddNeighbours(sv, edge);
                sv.AddNeighbours(fv, edge);
            }
            foreach (var vertex in this.vertices) {
                vertex.Neighbours.Sort((v1, v2) => v1.edg.Cost.CompareTo(v2.edg.Cost));
            }
        }
        public List<Edge> ProcessPath() {
            var tree = new List<PVertex>();
            var queue = new List<(PVertex ver, Edge edg)>();
            var MSTedges = new List<Edge>();

            var begin = vertices[0];
            tree.Add(begin);

            foreach (var neighbour in begin.Neighbours) {
                queue.Add(neighbour);
            }

            while(tree.Count != vertices.Count) {
                int i = 0;

                var nextVertex = queue[0];
                while (tree.Contains(nextVertex.ver) && i < queue.Count) {
                    nextVertex = queue[i];
                    i++;
                }

                tree.Add(nextVertex.ver);
                MSTedges.Add(nextVertex.edg);

                queue.Remove(nextVertex);

                foreach (var neighbour in nextVertex.ver.Neighbours) {
                    queue.Add(neighbour);
                }

                queue.Sort((v1, v2) => v1.edg.Cost.CompareTo(v2.edg.Cost));
            }

            foreach (var edge in MSTedges) {
                Console.WriteLine(edge.Begin.ToString() + " " + edge.End.ToString());
            }

            return MSTedges;
        }
    }
}
