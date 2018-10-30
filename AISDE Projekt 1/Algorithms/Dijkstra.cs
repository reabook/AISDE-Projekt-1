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
            //this.begin = this.vertices.Find(v => v.Equals(graph.Path.Begin));
            //this.end = this.vertices.Find(v => v.Equals(graph.Path.End));
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
            foreach (var vertex in this.vertices) {
                vertex.Neighbours.Sort((v1, v2) => v1.edg.Cost.CompareTo(v2.edg.Cost));
            }
        }

        public List<Edge> ProcessPath() {

            begin.Distance = 0; // ustawienie jego dystansu od zrodla na 0

            var Q = vertices; // dodanie wszystkich wierzcholkow do kolejki

            var current = this.begin;

            foreach (var v in current.Neighbours) {
                if (v.ver.Distance > current.Distance + v.edg.Cost) {
                    v.ver.Distance = current.Distance + v.edg.Cost;
                    v.ver.Previous = (current, v.edg);
                    Q.Remove(this.begin);
                    Console.WriteLine("Dodaje poprzednika do: " + v.ver.ID + " <- " + current.ID);
                }
            }

            while (Q.Count != 0) {
                Console.WriteLine("Znajduje najblizszego sasiada " + current.ID);
                // wziecie najblizszego sasiada, ktory wciaz jest w Q
                DVertex u = null;
                int i = 0;
                do {
                    u = current.Neighbours[i].ver; // sasiedzi sa posortowani w zaleznosci od odleglosci od wierzcholka
                    i++;
                } while (!Q.Contains(u));

                Console.WriteLine("Znalazlem " + u.ID);
                Q.Remove(u); // usuniecie wierzcholka z Q

                Console.WriteLine("Sprawdzam sasiadow " + u.ID);
                // sprawdzenie wszystkich sasiadow wierzcholka
                foreach (var v in u.Neighbours) {
                    if (v.ver.Distance > u.Distance + v.edg.Cost) {
                        v.ver.Distance = u.Distance + v.edg.Cost;
                        v.ver.Previous = (u, v.edg); // dodanie poprzednika
                        Q.Remove(this.begin);
                        Console.WriteLine("Dodaje poprzednika do: " + v.ver.ID + " <- " + u.ID);
                    }
                }
                // posuniecie sie dalej
                current = u;
            }
            Console.WriteLine("KONIEC");

            // wyswietlenie trasy i kosztu
            DVertex temp = this.end;
            //List<(DVertex ver, Edge edg)> path = new List<(DVertex ver, Edge edg)>();
            List<Edge> edglist = new List<Edge>();
            
            double cost = temp.Distance;

            // stworzenie sciezki
            while (temp.Previous.edg != null) {
                edglist.Add(temp.Previous.edg);
                temp = temp.Previous.ver;
            }

            edglist.Reverse();

            //this.graph.Path.Nodes = path;

            //foreach (var pair in path) {
            //    if (pair.ver.ID != end.ID)
            //        Console.Write(pair.ver.ID + " -> ");
            //    else
            //        Console.WriteLine(pair.ver.ID);
            //}

            return edglist;
        }
    }
}

