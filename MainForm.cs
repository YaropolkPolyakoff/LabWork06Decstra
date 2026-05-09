using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DijkstraApp
{
    public partial class MainForm : Form
    {
        private Graph graph;
        private Dictionary<int, Point> vertexPositions;
        private int selectedVertex = -1;
        private List<int> shortestPath;
        
        public MainForm()
        {
            InitializeComponent();
            graph = new Graph();
            vertexPositions = new Dictionary<int, Point>();
            shortestPath = new List<int>();
            LoadDefaultGraph();
            UpdateVertexComboBoxes();
        }
        
        private void LoadDefaultGraph()
        {
            // Create default graph with 6 vertices and 10+ edges
            int centerX = graphPanel.Width / 2;
            int centerY = graphPanel.Height / 2;
            int radius = 150;
            
            for (int i = 0; i < 6; i++)
            {
                graph.AddVertex(i);
                double angle = i * Math.PI * 2 / 6 - Math.PI / 2;
                int x = centerX + (int)(Math.Cos(angle) * radius);
                int y = centerY + (int)(Math.Sin(angle) * radius);
                vertexPositions[i] = new Point(x, y);
            }
            
            // Add edges to form a connected graph with multiple paths
            graph.AddEdge(0, 1, 4);
            graph.AddEdge(0, 2, 2);
            graph.AddEdge(1, 2, 1);
            graph.AddEdge(1, 3, 5);
            graph.AddEdge(2, 3, 8);
            graph.AddEdge(2, 4, 10);
            graph.AddEdge(3, 4, 2);
            graph.AddEdge(3, 5, 6);
            graph.AddEdge(4, 5, 3);
            graph.AddEdge(0, 5, 7);
            graph.AddEdge(1, 4, 9);
        }
        
        private void UpdateVertexComboBoxes()
        {
            var vertices = graph.GetAllVertices();
            vertices.Sort();
            
            cmbSourceVertex.Items.Clear();
            cmbDestVertex.Items.Clear();
            
            foreach (var v in vertices)
            {
                cmbSourceVertex.Items.Add(v);
                cmbDestVertex.Items.Add(v);
            }
            
            if (vertices.Count > 0)
            {
                cmbSourceVertex.SelectedIndex = 0;
                if (vertices.Count > 1)
                    cmbDestVertex.SelectedIndex = vertices.Count - 1;
            }
        }
        
        private void btnAddVertex_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtVertexId.Text, out int vertexId))
            {
                if (graph.GetAllVertices().Contains(vertexId))
                {
                    MessageBox.Show("Вершина с таким ID уже существует!", "Ошибка");
                    return;
                }
                
                graph.AddVertex(vertexId);
                
                // Position new vertex randomly
                Random rand = new Random();
                int x = rand.Next(50, graphPanel.Width - 50);
                int y = rand.Next(50, graphPanel.Height - 50);
                vertexPositions[vertexId] = new Point(x, y);
                
                UpdateVertexComboBoxes();
                graphPanel.Invalidate();
                txtVertexId.Clear();
            }
            else
            {
                MessageBox.Show("Введите корректный ID вершины!", "Ошибка");
            }
        }
        
        private void btnRemoveVertex_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtVertexId.Text, out int vertexId))
            {
                graph.RemoveVertex(vertexId);
                vertexPositions.Remove(vertexId);
                UpdateVertexComboBoxes();
                graphPanel.Invalidate();
                txtVertexId.Clear();
            }
        }
        
        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtEdgeFrom.Text, out int from) &&
                int.TryParse(txtEdgeTo.Text, out int to) &&
                int.TryParse(txtEdgeWeight.Text, out int weight))
            {
                if (!graph.GetAllVertices().Contains(from) || !graph.GetAllVertices().Contains(to))
                {
                    MessageBox.Show("Обе вершины должны существовать!", "Ошибка");
                    return;
                }
                
                if (weight <= 0)
                {
                    MessageBox.Show("Вес ребра должен быть положительным!", "Ошибка");
                    return;
                }
                
                graph.AddEdge(from, to, weight);
                graphPanel.Invalidate();
                txtEdgeFrom.Clear();
                txtEdgeTo.Clear();
                txtEdgeWeight.Clear();
            }
            else
            {
                MessageBox.Show("Введите корректные значения!", "Ошибка");
            }
        }
        
        private void btnRemoveEdge_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtEdgeFrom.Text, out int from) &&
                int.TryParse(txtEdgeTo.Text, out int to))
            {
                graph.RemoveEdge(from, to);
                graphPanel.Invalidate();
                txtEdgeFrom.Clear();
                txtEdgeTo.Clear();
            }
        }
        
        private void btnFindPath_Click(object sender, EventArgs e)
        {
            if (cmbSourceVertex.SelectedItem == null || cmbDestVertex.SelectedItem == null)
            {
                MessageBox.Show("Выберите начальную и конечную вершины!", "Ошибка");
                return;
            }
            
            int source = (int)cmbSourceVertex.SelectedItem;
            int dest = (int)cmbDestVertex.SelectedItem;
            
            var result = graph.FindShortestPath(source);
            
            if (result.Distances[dest] == int.MaxValue)
            {
                txtResult.Text = "Путь не найден! Вершины не связаны.";
                shortestPath.Clear();
            }
            else
            {
                shortestPath = ReconstructPath(result.Previous, source, dest);
                txtResult.Text = $"Кратчайший путь: {string.Join(" → ", shortestPath)}\r\n";
                txtResult.Text += $"Длина пути: {result.Distances[dest]}";
            }
            
            graphPanel.Invalidate();
        }
        
        private List<int> ReconstructPath(Dictionary<int, int> previous, int source, int dest)
        {
            var path = new List<int>();
            int current = dest;
            
            while (current != -1)
            {
                path.Insert(0, current);
                if (current == source)
                    break;
                current = previous[current];
            }
            
            return path;
        }
        
        private void btnClearGraph_Click(object sender, EventArgs e)
        {
            graph.ClearGraph();
            vertexPositions.Clear();
            shortestPath.Clear();
            UpdateVertexComboBoxes();
            graphPanel.Invalidate();
            txtResult.Clear();
        }
        
        private void btnResetDefault_Click(object sender, EventArgs e)
        {
            graph.ClearGraph();
            vertexPositions.Clear();
            shortestPath.Clear();
            LoadDefaultGraph();
            UpdateVertexComboBoxes();
            graphPanel.Invalidate();
            txtResult.Clear();
        }
        
        private void graphPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            // Draw edges
            var vertices = graph.GetAllVertices();
            var drawnEdges = new HashSet<string>();
            
            foreach (var vertex in vertices)
            {
                if (!vertexPositions.ContainsKey(vertex))
                    continue;
                
                Point p1 = vertexPositions[vertex];
                var edges = graph.GetEdgesFrom(vertex);
                
                foreach (var edge in edges)
                {
                    if (!vertexPositions.ContainsKey(edge.Target))
                        continue;
                    
                    string edgeKey = vertex < edge.Target ? $"{vertex}-{edge.Target}" : $"{edge.Target}-{vertex}";
                    if (drawnEdges.Contains(edgeKey))
                        continue;
                    
                    drawnEdges.Add(edgeKey);
                    Point p2 = vertexPositions[edge.Target];
                    
                    bool isInPath = false;
                    for (int i = 0; i < shortestPath.Count - 1; i++)
                    {
                        if ((shortestPath[i] == vertex && shortestPath[i + 1] == edge.Target) ||
                            (shortestPath[i] == edge.Target && shortestPath[i + 1] == vertex))
                        {
                            isInPath = true;
                            break;
                        }
                    }
                    
                    Pen edgePen = isInPath ? new Pen(Color.Red, 3) : new Pen(Color.Black, 2);
                    g.DrawLine(edgePen, p1, p2);
                    
                    // Draw weight
                    int midX = (p1.X + p2.X) / 2;
                    int midY = (p1.Y + p2.Y) / 2;
                    g.FillEllipse(Brushes.White, midX - 12, midY - 12, 24, 24);
                    g.DrawString(edge.Weight.ToString(), Font, Brushes.Blue, midX - 8, midY - 8);
                }
            }
            
            // Draw vertices
            foreach (var vertex in vertices)
            {
                if (!vertexPositions.ContainsKey(vertex))
                    continue;
                
                Point pos = vertexPositions[vertex];
                bool isInPath = shortestPath.Contains(vertex);
                
                Brush brush = isInPath ? Brushes.LightGreen : Brushes.LightBlue;
                g.FillEllipse(brush, pos.X - 20, pos.Y - 20, 40, 40);
                g.DrawEllipse(Pens.Black, pos.X - 20, pos.Y - 20, 40, 40);
                
                string label = vertex.ToString();
                SizeF labelSize = g.MeasureString(label, Font);
                g.DrawString(label, Font, Brushes.Black, pos.X - labelSize.Width / 2, pos.Y - labelSize.Height / 2);
            }
        }
        
        private void graphPanel_MouseDown(object sender, MouseEventArgs e)
        {
            selectedVertex = -1;
            
            foreach (var kvp in vertexPositions)
            {
                Point pos = kvp.Value;
                int dist = (int)Math.Sqrt(Math.Pow(e.X - pos.X, 2) + Math.Pow(e.Y - pos.Y, 2));
                
                if (dist <= 20)
                {
                    selectedVertex = kvp.Key;
                    break;
                }
            }
        }
        
        private void graphPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedVertex != -1 && e.Button == MouseButtons.Left)
            {
                vertexPositions[selectedVertex] = new Point(e.X, e.Y);
                graphPanel.Invalidate();
            }
        }
        
        private void graphPanel_MouseUp(object sender, MouseEventArgs e)
        {
            selectedVertex = -1;
        }
    }
}
