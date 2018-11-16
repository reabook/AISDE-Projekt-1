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

            //graph.DrawPath(graph.minimumSpanningTree());
            graph.DrawPath(graph.shortestPath(1, 5));

        }
    }
}
