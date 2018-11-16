using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Layout;


namespace AISDE_Projekt_1 {
    public class Graph {

        Microsoft.Msagl.Drawing.Graph graph;
        public Dictionary<int, Vertex> Vertices { get; set; } = new Dictionary<int, Vertex>();
        public List<Edge> Edges { get; set; } = new List<Edge>();

        public TextBox Txt { get; set; } = new TextBox();
        public System.Windows.Forms.Label Lbl { get; set; } = new System.Windows.Forms.Label();


        public Graph(string filePath) {
            LoadFromFile(filePath);
        }

        public Graph(Dictionary<int, Vertex> vertices, List<Edge> edges) {
            this.Vertices = vertices;
            this.Edges = edges;
        }

        private void LoadFromFile(string filePath) {
            try {
                using (StreamReader reader = new StreamReader(filePath)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        string[] parts = line.Split(' ');

                        if (parts[0][0] != '#') {
                            if (parts[0] == "WEZLY") {
                                int NumberOfVertices = int.Parse(parts[2]);

                                for (int i = 0; i < NumberOfVertices; i++) {
                                    line = reader.ReadLine();
                                    parts = line.Split(' ');
                                    if (parts[0][0] == '#') {
                                        i--;
                                        continue;
                                    }

                                    Vertices.Add(int.Parse(parts[0]), new Vertex(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])));
                                }
                            } else if (parts[0] == "LACZA") {
                                int NumberOfEdges = int.Parse(parts[2]);

                                for (int i = 0; i < NumberOfEdges; i++) {
                                    line = reader.ReadLine();
                                    parts = line.Split(' ');
                                    if (parts[0][0] == '#') {
                                        i--;
                                        continue;
                                    }

                                    Edges.Add(new Edge(int.Parse(parts[0]), Vertices[int.Parse(parts[1])], Vertices[int.Parse(parts[2])]));
                                }
                            }
                        }

                    }
                }
            }catch(FileNotFoundException e) {
                Console.WriteLine(e.Message);
                Console.Read();
                return;
            }
        }

        public void Draw() {
            var form = new Form();

            var viewer = new GViewer();

            graph = new Microsoft.Msagl.Drawing.Graph("graph");
            graph.Directed = false;


            foreach (var edge in Edges) {
                var ed = graph.AddEdge(edge.Begin.ID.ToString(), edge.End.ID.ToString());

                ed.Attr.ArrowheadAtTarget = ArrowStyle.None;
                ed.LabelText = Math.Round(edge.Cost, 2).ToString();
                ed.Label.FontColor = Color.DarkOrange;
                ed.Label.FontSize = 4;

                graph.FindNode(edge.Begin.ID.ToString()).Attr.FillColor = Color.Orange;
                graph.FindNode(edge.Begin.ID.ToString()).Attr.Shape = Shape.Circle;
                graph.FindNode(edge.End.ID.ToString()).Attr.FillColor = Color.Orange;
                graph.FindNode(edge.End.ID.ToString()).Attr.Shape = Shape.Circle;

            }
                ShowGraph(form, viewer, graph);
            
        }

        public void DrawPath(List<Edge> path) {
            //graph.FindNode(Path.Begin.ID.ToString()).Attr.FillColor = Color.Green; // poczatkek sciezki na zielono
            //graph.FindNode(Path.End.ID.ToString()).Attr.FillColor = Color.Red; // koniec sciezki na niebiesko

            var form = new Form();

            var viewer = new GViewer();

            graph = new Microsoft.Msagl.Drawing.Graph("graph");
            graph.Directed = false;


            foreach (var edge in Edges) {
                var ed = graph.AddEdge(edge.Begin.ID.ToString(), edge.End.ID.ToString());

                if (path.Contains(edge)) {
                    ed.Attr.Color = Color.Red;
                }
                ed.Attr.ArrowheadAtTarget = ArrowStyle.None;
                ed.LabelText = edge.ID.ToString() + ": " + Math.Round(edge.Cost, 2).ToString();
                ed.Label.FontColor = Color.DarkOrange;
                ed.Label.FontSize = 4;

                graph.FindNode(edge.Begin.ID.ToString()).Attr.FillColor = Color.Orange;
                graph.FindNode(edge.Begin.ID.ToString()).Attr.Shape = Shape.Circle;
                graph.FindNode(edge.End.ID.ToString()).Attr.FillColor = Color.Orange;
                graph.FindNode(edge.End.ID.ToString()).Attr.Shape = Shape.Circle;

            }
            ShowGraph(form, viewer, graph);
        }

            private void SetCost(Form form, double cost) {
            int formHeight = form.Height / 6;
            Lbl.BackColor = System.Drawing.Color.FromArgb(211, 211, 211);
            Lbl.Top = formHeight - (int)Txt.Font.Size*2;
            Lbl.Text = "Koszt";
            Lbl.Enabled = false;
            Txt.Top = formHeight;
            Txt.Text = Math.Round(cost, 2).ToString();
            Txt.Enabled = false;
        }


        private void ShowGraph(Form form, GViewer viewer, Microsoft.Msagl.Drawing.Graph graph) {

            viewer.LayoutEditingEnabled = false;
            viewer.CurrentLayoutMethod = LayoutMethod.MDS;
            viewer.Graph = graph;
            viewer.Dock = DockStyle.Fill;
            
            form.Height = 1000;
            form.Width = 1000;
            form.Controls.Add(Txt);
            form.Controls.Add(Lbl);
            form.SuspendLayout();
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();
        }

        public List<Edge> minimumSpanningTree() {
            Prim prim = new Prim(this);
            return prim.ProcessPath();
        }

        public List<Edge> shortestPath(int a, int b) {
            Dijkstra dij = new Dijkstra(this, a, b);
            return dij.ProcessPath();
        }
    }
}
