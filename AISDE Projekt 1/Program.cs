using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISDE_Projekt_1 {
    class Program {
        static void Main(string[] args) {

            Graph graph = new Graph("network.txt");
            //Dijkstra dij = new Dijkstra(graph, 1, 9);
            Prim prim = new Prim(graph);
            //dij.ProcessPath();

            //graph.DrawPath(dij.ProcessPath());
            graph.DrawPath(prim.ProcessPath());
        }
    }
}
