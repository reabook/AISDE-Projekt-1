using AISDE_Projekt_1.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_Projekt_1 {
    public class Prim : IAlgorithm {

        private List<PVertex> vertices = new List<PVertex>();
        private List<Edge> edges = new List<Edge>();

        public Prim(Graph graph) : this(graph.Vertices.Values.ToList(), graph.Edges) {}

        public Prim(List<Vertex> vertices, List<Edge> edges) {
            foreach (var vertex in vertices) {
                this.vertices.Add(new PVertex(vertex));
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
            var MSTedges = new List<Edge>();
            var queue = new List<(PVertex ver, Edge edg)>();

            var begin = vertices[0];
            tree.Add(begin);

            foreach (var neighbour in begin.Neighbours) { // dodanie do kolejki sasiadow pierwszego wierzcholka
                AddWithoutReplies(queue, neighbour);
            }

            while(tree.Count != vertices.Count) {
                var current = queue[0]; // wybranie najblizszego wierzcholka 
              
                tree.Add(current.ver); // dodanie do drzewa MST 
                MSTedges.Add(current.edg); // dodanie do drzewa MST


                foreach (var neighbour in current.ver.Neighbours) { // dodanie sasiadow aktualnie rozpatrywanego wierzcholka
                    AddWithoutReplies(queue, neighbour);
                }

                RemoveUsedFromQueue(tree, queue); // usuniecie niepotrzebnych (wykorzystanych) wierzcholkow
                queue.Sort((v1, v2) => v1.edg.Cost.CompareTo(v2.edg.Cost)); // przesortowanie wzgledem kosztu
            }

            return MSTedges;
        }

        private void RemoveUsedFromQueue(List<PVertex> tree, List<(PVertex ver, Edge edg)> queue) {
            foreach (var vertex in tree) {
                queue.RemoveAll(v => v.ver.ID.Equals(vertex.ID));
            }
        }

        private void AddWithoutReplies(List<(PVertex ver, Edge edg)> queue, (PVertex ver, Edge edg) neighbour) {
            if (!queue.Contains(neighbour))
                queue.Add(neighbour);
        }
    }
}


