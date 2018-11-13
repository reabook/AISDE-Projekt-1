using AISDE_Projekt_1.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_Projekt_1 {
    public class Dijkstra : IAlgorithm{

        private Graph graph;
        const double INFINITY = Double.MaxValue;

        private DVertex begin;
        private DVertex end;

        private List<DVertex> vertices = new List<DVertex>();
        private List<Edge> edges = new List<Edge>();

        public Dijkstra(Graph graph, int start, int end) {
            this.graph = graph;

            // przepisanie Vertex na DVertex i ustawianie dystansu na nieskonczonosc
            foreach (var vertex in graph.Vertices) {
                this.vertices.Add(new DVertex((null, null), vertex.Value, INFINITY));
            }

            // przepisanie poczatku i konca na DVertex
            this.begin = this.vertices.Find(v => v.ID.Equals(start));
            this.end = this.vertices.Find(v => v.ID.Equals(end));

            // dodanie krawedzi
            this.edges = graph.Edges;

            // dodanie wszystkim wierzcholkom sasiadow
            foreach (var edge in edges) {
                var fv = this.vertices.Find(v => v.Equals(edge.Begin));
                var sv = this.vertices.Find(v => v.Equals(edge.End));

                fv.AddNeighbours(sv, edge);
                sv.AddNeighbours(fv, edge);
            }


            //foreach (var vertex in this.vertices) {
            //    vertex.Neighbours.Sort((v1, v2) => v1.edg.Cost.CompareTo(v2.edg.Cost));
            //}

        }

        public List<Edge> ProcessPath() {

            begin.Distance = 0; // ustawienie dystansu poczatkowego wierzcholka od zrodla na 0

            var Q = vertices; // dodanie wszystkich wierzcholkow do kolejki

            var current = this.begin; // ustawienie aktualnego na poczatkowy

            while (Q.Count > 1) {
                Q.Remove(current); // usuniecie wierzcholka z Q

                // sprawdzenie wszystkich sasiadow wierzcholka
                foreach (var v in current.Neighbours) {
                    if (v.ver.Distance > current.Distance + v.edg.Cost) {
                        v.ver.Distance = current.Distance + v.edg.Cost;
                        v.ver.Previous = (current, v.edg); // dodanie poprzednika
                    }
                }

                // posuniecie sie dalej
                // wziecie najblizszego sasiada, ktory wciaz jest w Q
                Q.Sort((v1, v2) => v1.Distance.CompareTo(v2.Distance)); // przesortowanie kolejki
                current = Q[0];
            }
            Console.WriteLine("KONIEC");

            // wyswietlenie trasy i kosztu

            List<Edge> edglist = new List<Edge>();
            DVertex temp = this.end;

            if (end.Previous.ver == null) {
                Console.WriteLine("Brak polaczenia z koncem");
                return edglist;
            }
            double cost = temp.Distance;

            // stworzenie sciezki
            while (temp.Previous.edg != null) {
                edglist.Add(temp.Previous.edg);
                temp = temp.Previous.ver;
            }

            edglist.Reverse();

            return edglist;
        }
    }
}

