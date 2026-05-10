using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShortestPathApp
{
    public class WeightedEdge
    {
        public string SourceVertex;
        public string DestinationVertex;
        public int EdgeWeight;
    }

    public class GraphVertex
    {
        public string VertexName;
        public int PositionX;
        public int PositionY;
    }

    public class WeightedGraph
    {
        private Dictionary<string, List<WeightedEdge>> adjacencyMap = new Dictionary<string, List<WeightedEdge>>();
        private Dictionary<string, GraphVertex> vertexMap = new Dictionary<string, GraphVertex>();

        public void CreateVertex(string name, int x, int y)
        {
            if (!vertexMap.ContainsKey(name))
            {
                vertexMap[name] = new GraphVertex { VertexName = name, PositionX = x, PositionY = y };
                adjacencyMap[name] = new List<WeightedEdge>();
            }
        }

        public void CreateEdge(string from, string to, int weight)
        {
            if (vertexMap.ContainsKey(from) && vertexMap.ContainsKey(to))
            {
                adjacencyMap[from].Add(new WeightedEdge 
                { 
                    SourceVertex = from, 
                    DestinationVertex = to, 
                    EdgeWeight = weight 
                });
            }
        }

        public void RemoveVertex(string name)
        {
            if (vertexMap.ContainsKey(name))
            {
                vertexMap.Remove(name);
                adjacencyMap.Remove(name);
                foreach (var edgeList in adjacencyMap.Values)
                {
                    edgeList.RemoveAll(edge => edge.DestinationVertex == name);
                }
            }
        }

        public void RemoveEdge(string from, string to)
        {
            if (adjacencyMap.ContainsKey(from))
            {
                adjacencyMap[from].RemoveAll(edge => edge.DestinationVertex == to);
            }
        }

        public void ClearGraph()
        {
            vertexMap.Clear();
            adjacencyMap.Clear();
        }

        public Dictionary<string, GraphVertex> GetVertices() => vertexMap;
        public Dictionary<string, List<WeightedEdge>> GetEdges() => adjacencyMap;

        private double CalculateHeuristic(string from, string to)
        {
            var fromVertex = vertexMap[from];
            var toVertex = vertexMap[to];
            int dx = toVertex.PositionX - fromVertex.PositionX;
            int dy = toVertex.PositionY - fromVertex.PositionY;
            return Math.Sqrt(dx * dx + dy * dy) / 50.0; // Normalize by approximate edge length
        }

        public ShortestPathInfo AStarAlgorithm(string startVertex, string endVertex)
        {
            if (!vertexMap.ContainsKey(startVertex) || !vertexMap.ContainsKey(endVertex))
                return new ShortestPathInfo { IsPathFound = false, AlgorithmName = "A*" };

            var gScore = new Dictionary<string, double>();
            var fScore = new Dictionary<string, double>();
            var predecessorVertex = new Dictionary<string, string>();
            var openSet = new HashSet<string>();
            var closedSet = new HashSet<string>();
            int relaxationCount = 0;
            int visitedCount = 0;

            foreach (var v in vertexMap.Keys)
            {
                gScore[v] = double.MaxValue;
                fScore[v] = double.MaxValue;
                predecessorVertex[v] = null;
            }

            gScore[startVertex] = 0;
            fScore[startVertex] = CalculateHeuristic(startVertex, endVertex);
            openSet.Add(startVertex);

            while (openSet.Count > 0)
            {
                // Find vertex with minimum fScore
                string current = null;
                double minFScore = double.MaxValue;
                foreach (var v in openSet)
                {
                    if (fScore[v] < minFScore)
                    {
                        minFScore = fScore[v];
                        current = v;
                    }
                }

                if (current == null)
                    break;

                visitedCount++;

                if (current == endVertex)
                {
                    var reconstructedPath = new List<string>();
                    string node = endVertex;
                    while (node != null)
                    {
                        reconstructedPath.Insert(0, node);
                        node = predecessorVertex[node];
                    }

                    return new ShortestPathInfo
                    {
                        IsPathFound = true,
                        VertexSequence = reconstructedPath,
                        TotalCost = (int)gScore[endVertex],
                        RelaxationCount = relaxationCount,
                        VisitedNodesCount = visitedCount,
                        AlgorithmName = "A*"
                    };
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var edge in adjacencyMap[current])
                {
                    if (closedSet.Contains(edge.DestinationVertex))
                        continue;

                    double tentativeGScore = gScore[current] + edge.EdgeWeight;
                    relaxationCount++;

                    if (!openSet.Contains(edge.DestinationVertex))
                    {
                        openSet.Add(edge.DestinationVertex);
                    }
                    else if (tentativeGScore >= gScore[edge.DestinationVertex])
                    {
                        continue;
                    }

                    predecessorVertex[edge.DestinationVertex] = current;
                    gScore[edge.DestinationVertex] = tentativeGScore;
                    fScore[edge.DestinationVertex] = gScore[edge.DestinationVertex] + CalculateHeuristic(edge.DestinationVertex, endVertex);
                }
            }

            return new ShortestPathInfo 
            { 
                IsPathFound = false, 
                RelaxationCount = relaxationCount, 
                VisitedNodesCount = visitedCount,
                AlgorithmName = "A*"
            };
        }

        public ShortestPathInfo NaiveShortestPath(string startVertex, string endVertex)
        {
            if (!vertexMap.ContainsKey(startVertex) || !vertexMap.ContainsKey(endVertex))
                return new ShortestPathInfo { IsPathFound = false, AlgorithmName = "Naive O(n²+m)" };

            var costToVertex = new Dictionary<string, int>();
            var predecessorVertex = new Dictionary<string, string>();
            int relaxationCount = 0;
            int visitedCount = 0;

            foreach (var v in vertexMap.Keys)
            {
                costToVertex[v] = int.MaxValue;
                predecessorVertex[v] = null;
            }

            costToVertex[startVertex] = 0;
            int n = vertexMap.Count;

            // Naive approach: relax all edges n-1 times
            for (int i = 0; i < n - 1; i++)
            {
                bool updated = false;
                visitedCount++;

                foreach (var kvp in adjacencyMap)
                {
                    string u = kvp.Key;
                    if (costToVertex[u] == int.MaxValue)
                        continue;

                    foreach (var edge in kvp.Value)
                    {
                        relaxationCount++;
                        int newCost = costToVertex[u] + edge.EdgeWeight;
                        if (newCost < costToVertex[edge.DestinationVertex])
                        {
                            costToVertex[edge.DestinationVertex] = newCost;
                            predecessorVertex[edge.DestinationVertex] = u;
                            updated = true;
                        }
                    }
                }

                if (!updated)
                    break;
            }

            if (costToVertex[endVertex] == int.MaxValue)
                return new ShortestPathInfo 
                { 
                    IsPathFound = false, 
                    RelaxationCount = relaxationCount, 
                    VisitedNodesCount = visitedCount,
                    AlgorithmName = "Naive O(n²+m)"
                };

            var reconstructedPath = new List<string>();
            string current = endVertex;
            while (current != null)
            {
                reconstructedPath.Insert(0, current);
                current = predecessorVertex[current];
            }

            return new ShortestPathInfo
            {
                IsPathFound = true,
                VertexSequence = reconstructedPath,
                TotalCost = costToVertex[endVertex],
                RelaxationCount = relaxationCount,
                VisitedNodesCount = visitedCount,
                AlgorithmName = "Naive O(n²+m)"
            };
        }

        public ShortestPathInfo DijkstraAlgorithm(string startVertex, string endVertex)
        {
            if (!vertexMap.ContainsKey(startVertex) || !vertexMap.ContainsKey(endVertex))
                return new ShortestPathInfo { IsPathFound = false, AlgorithmName = "Dijkstra" };

            var costToVertex = new Dictionary<string, int>();
            var predecessorVertex = new Dictionary<string, string>();
            var remainingVertices = new HashSet<string>();
            int relaxationCount = 0;
            int visitedCount = 0;

            foreach (var v in vertexMap.Keys)
            {
                costToVertex[v] = int.MaxValue;
                predecessorVertex[v] = null;
                remainingVertices.Add(v);
            }

            costToVertex[startVertex] = 0;

            while (remainingVertices.Count > 0)
            {
                string minVertex = null;
                int minCost = int.MaxValue;

                foreach (var v in remainingVertices)
                {
                    if (costToVertex[v] < minCost)
                    {
                        minCost = costToVertex[v];
                        minVertex = v;
                    }
                }

                if (minVertex == null || costToVertex[minVertex] == int.MaxValue)
                    break;

                remainingVertices.Remove(minVertex);
                visitedCount++;

                if (minVertex == endVertex)
                    break;

                foreach (var edge in adjacencyMap[minVertex])
                {
                    if (remainingVertices.Contains(edge.DestinationVertex))
                    {
                        relaxationCount++;
                        int newCost = costToVertex[minVertex] + edge.EdgeWeight;
                        if (newCost < costToVertex[edge.DestinationVertex])
                        {
                            costToVertex[edge.DestinationVertex] = newCost;
                            predecessorVertex[edge.DestinationVertex] = minVertex;
                        }
                    }
                }
            }

            if (costToVertex[endVertex] == int.MaxValue)
                return new ShortestPathInfo 
            { 
                IsPathFound = false, 
                RelaxationCount = relaxationCount, 
                VisitedNodesCount = visitedCount,
                AlgorithmName = "Dijkstra"
            };

            var reconstructedPath = new List<string>();
            string current = endVertex;
            while (current != null)
            {
                reconstructedPath.Insert(0, current);
                current = predecessorVertex[current];
            }

            return new ShortestPathInfo
            {
                IsPathFound = true,
                VertexSequence = reconstructedPath,
            TotalCost = costToVertex[endVertex],
                RelaxationCount = relaxationCount,
                VisitedNodesCount = visitedCount,
                AlgorithmName = "Dijkstra"
            };
        }
    }

    public class ShortestPathInfo
    {
        public bool IsPathFound;
        public List<string> VertexSequence;
        public int TotalCost;
        public int RelaxationCount;
        public int VisitedNodesCount;
        public string AlgorithmName;
    }

    public class PerformanceResult
    {
        public string AlgorithmName;
        public int VertexCount;
        public int EdgeCount;
        public int RelaxationCount;
        public int VisitedNodesCount;
        public double ExecutionTimeMs;
        public int TotalCost;
    }

    public class MainForm : Form
    {
        private WeightedGraph myGraph;
        private Panel visualizationPanel;
        private TextBox nodeNameBox, edgeFromBox, edgeToBox, weightBox, startBox, endBox, resultBox, analysisBox;
        private Button addNodeBtn, delNodeBtn, addEdgeBtn, delEdgeBtn, findPathBtn, resetBtn, clearBtn;
        private ComboBox algorithmCombo;
        private Button analyzeBtn;
        private ShortestPathInfo calculatedPath;

        public MainForm()
        {
            BuildUserInterface();
            myGraph = new WeightedGraph();
            LoadDefaultGraph();
        }

        private void BuildUserInterface()
        {
            Text = "Алгоритм Дейкстры - Граф";
            Size = new Size(1200, 700);
            StartPosition = FormStartPosition.CenterScreen;

            visualizationPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(700, 600),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            visualizationPanel.Paint += DrawGraph;
            Controls.Add(visualizationPanel);

            int x = 730, y = 10;

            Controls.Add(new Label { Text = "Управление вершинами:", Location = new Point(x, y), Size = new Size(180, 20), Font = new Font("Arial", 10, FontStyle.Bold) });
            y += 25;
            Controls.Add(new Label { Text = "Имя:", Location = new Point(x, y), Size = new Size(80, 20) });
            y += 20;
            nodeNameBox = new TextBox { Location = new Point(x, y), Size = new Size(200, 25) };
            Controls.Add(nodeNameBox);
            y += 30;

            addNodeBtn = new Button { Text = "Добавить", Location = new Point(x, y), Size = new Size(95, 30) };
            addNodeBtn.Click += (s, e) => HandleAddNode();
            Controls.Add(addNodeBtn);

            delNodeBtn = new Button { Text = "Удалить", Location = new Point(x + 105, y), Size = new Size(95, 30) };
            delNodeBtn.Click += (s, e) => HandleDeleteNode();
            Controls.Add(delNodeBtn);
            y += 45;

            Controls.Add(new Label { Text = "Управление рёбрами:", Location = new Point(x, y), Size = new Size(180, 20), Font = new Font("Arial", 10, FontStyle.Bold) });
            y += 25;
            Controls.Add(new Label { Text = "От:", Location = new Point(x, y), Size = new Size(80, 20) });
            y += 20;
            edgeFromBox = new TextBox { Location = new Point(x, y), Size = new Size(200, 25) };
            Controls.Add(edgeFromBox);
            y += 30;

            Controls.Add(new Label { Text = "До:", Location = new Point(x, y), Size = new Size(80, 20) });
            y += 20;
            edgeToBox = new TextBox { Location = new Point(x, y), Size = new Size(200, 25) };
            Controls.Add(edgeToBox);
            y += 30;

            Controls.Add(new Label { Text = "Вес:", Location = new Point(x, y), Size = new Size(80, 20) });
            y += 20;
            weightBox = new TextBox { Location = new Point(x, y), Size = new Size(200, 25) };
            Controls.Add(weightBox);
            y += 30;

            addEdgeBtn = new Button { Text = "Добавить", Location = new Point(x, y), Size = new Size(95, 30) };
            addEdgeBtn.Click += (s, e) => HandleAddEdge();
            Controls.Add(addEdgeBtn);

            delEdgeBtn = new Button { Text = "Удалить", Location = new Point(x + 105, y), Size = new Size(95, 30) };
            delEdgeBtn.Click += (s, e) => HandleDeleteEdge();
            Controls.Add(delEdgeBtn);
            y += 45;

            Controls.Add(new Label { Text = "Поиск пути:", Location = new Point(x, y), Size = new Size(180, 20), Font = new Font("Arial", 10, FontStyle.Bold) });
            y += 20;
            algorithmCombo = new ComboBox 
            { 
                Location = new Point(x, y), 
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            algorithmCombo.Items.AddRange(new string[] { "Dijkstra", "A*", "Naive O(n²+m)" });
            algorithmCombo.SelectedIndex = 0;
            Controls.Add(algorithmCombo);
            y += 30;

            y += 25;
            Controls.Add(new Label { Text = "Начало:", Location = new Point(x, y), Size = new Size(80, 20) });
            y += 20;
            startBox = new TextBox { Location = new Point(x, y), Size = new Size(200, 25) };
            Controls.Add(startBox);
            y += 30;

            Controls.Add(new Label { Text = "Конец:", Location = new Point(x, y), Size = new Size(80, 20) });
            y += 20;
            endBox = new TextBox { Location = new Point(x, y), Size = new Size(200, 25) };
            Controls.Add(endBox);
            y += 30;

            findPathBtn = new Button { Text = "Найти путь", Location = new Point(x, y), Size = new Size(200, 35), BackColor = Color.LightGreen, Font = new Font("Arial", 10, FontStyle.Bold) };
            findPathBtn.Click += (s, e) => HandleFindPath();
            Controls.Add(findPathBtn);
            y += 45;

            analyzeBtn = new Button { Text = "Сравнить алгоритмы", Location = new Point(x, y), Size = new Size(200, 35), BackColor = Color.LightYellow, Font = new Font("Arial", 9, FontStyle.Bold) };
            analyzeBtn.Click += (s, e) => HandleAnalyzeAlgorithms();
            Controls.Add(analyzeBtn);
            y += 45;


            resetBtn = new Button { Text = "Граф по умолчанию", Location = new Point(x, y), Size = new Size(200, 30), BackColor = Color.LightBlue };
            resetBtn.Click += (s, e) => { LoadDefaultGraph(); resultBox.Clear(); ShowMessage("Восстановлен граф по умолчанию"); };
            Controls.Add(resetBtn);
            y += 35;

            clearBtn = new Button { Text = "Очистить", Location = new Point(x, y), Size = new Size(200, 30), BackColor = Color.LightCoral };
            clearBtn.Click += (s, e) => { myGraph.ClearGraph(); calculatedPath = null; resultBox.Clear(); visualizationPanel.Invalidate(); ShowMessage("Граф очищен"); };
            Controls.Add(clearBtn);
            y += 40;

            Controls.Add(new Label { Text = "Результат:", Location = new Point(x, y), Size = new Size(100, 20), Font = new Font("Arial", 10, FontStyle.Bold) });
            y += 25;
            resultBox = new TextBox { Location = new Point(x, y), Size = new Size(430, 80), Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true };
            Controls.Add(resultBox);
            y += 90;

            Controls.Add(new Label { Text = "Анализ эффективности:", Location = new Point(x, y), Size = new Size(180, 20), Font = new Font("Arial", 10, FontStyle.Bold) });
            y += 25;
            analysisBox = new TextBox { Location = new Point(x, y), Size = new Size(430, 120), Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true, Font = new Font("Courier New", 8) };
            Controls.Add(analysisBox);
        }

        private void LoadDefaultGraph()
        {
            myGraph.ClearGraph();
            calculatedPath = null;

            myGraph.CreateVertex("A", 150, 100);
            myGraph.CreateVertex("B", 350, 80);
            myGraph.CreateVertex("C", 550, 120);
            myGraph.CreateVertex("D", 150, 300);
            myGraph.CreateVertex("E", 350, 350);
            myGraph.CreateVertex("F", 550, 320);

            myGraph.CreateEdge("A", "B", 4);
            myGraph.CreateEdge("A", "D", 2);
            myGraph.CreateEdge("B", "A", 4);
            myGraph.CreateEdge("B", "C", 3);
            myGraph.CreateEdge("B", "E", 6);
            myGraph.CreateEdge("C", "B", 3);
            myGraph.CreateEdge("C", "F", 2);
            myGraph.CreateEdge("D", "A", 2);
            myGraph.CreateEdge("D", "E", 1);
            myGraph.CreateEdge("E", "B", 6);
            myGraph.CreateEdge("E", "D", 1);
            myGraph.CreateEdge("E", "F", 5);
            myGraph.CreateEdge("F", "C", 2);
            myGraph.CreateEdge("F", "E", 5);

            visualizationPanel.Invalidate();
        }

        private void HandleAddNode()
        {
            string name = nodeNameBox.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(name))
            {
                ShowWarning("Введите имя вершины");
                return;
            }

            if (myGraph.GetVertices().ContainsKey(name))
            {
                ShowWarning("Вершина существует");
                return;
            }

            var rnd = new Random();
            myGraph.CreateVertex(name, rnd.Next(50, 650), rnd.Next(50, 550));
            nodeNameBox.Clear();
            calculatedPath = null;
            visualizationPanel.Invalidate();
            ShowMessage($"Добавлена вершина {name}");
        }

        private void HandleDeleteNode()
        {
            string name = nodeNameBox.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(name))
            {
                ShowWarning("Введите имя вершины");
                return;
            }

            if (!myGraph.GetVertices().ContainsKey(name))
            {
                ShowWarning("Вершина не найдена");
                return;
            }

            myGraph.RemoveVertex(name);
            nodeNameBox.Clear();
            calculatedPath = null;
            visualizationPanel.Invalidate();
            ShowMessage($"Удалена вершина {name}");
        }

        private void HandleAddEdge()
        {
            string from = edgeFromBox.Text.Trim().ToUpper();
            string to = edgeToBox.Text.Trim().ToUpper();
            string wStr = weightBox.Text.Trim();

            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to) || string.IsNullOrEmpty(wStr))
            {
                ShowWarning("Заполните все поля");
                return;
            }

            if (!int.TryParse(wStr, out int weight) || weight <= 0)
            {
                ShowWarning("Вес должен быть положительным числом");
                return;
            }

            if (!myGraph.GetVertices().ContainsKey(from) || !myGraph.GetVertices().ContainsKey(to))
            {
                ShowWarning("Вершины не существуют");
                return;
            }

            myGraph.CreateEdge(from, to, weight);
            edgeFromBox.Clear();
            edgeToBox.Clear();
            weightBox.Clear();
            calculatedPath = null;
            visualizationPanel.Invalidate();
            ShowMessage($"Добавлено ребро {from}→{to} вес {weight}");
        }

        private void HandleDeleteEdge()
        {
            string from = edgeFromBox.Text.Trim().ToUpper();
            string to = edgeToBox.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                ShowWarning("Введите вершины");
                return;
            }

            myGraph.RemoveEdge(from, to);
            edgeFromBox.Clear();
            edgeToBox.Clear();
            weightBox.Clear();
            calculatedPath = null;
            visualizationPanel.Invalidate();
            ShowMessage($"Удалено ребро {from}→{to}");
        }

        private void HandleFindPath()
        {
            string start = startBox.Text.Trim().ToUpper();
            string end = endBox.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
            {
                ShowWarning("Введите начало и конец");
                return;
            }

            if (!myGraph.GetVertices().ContainsKey(start) || !myGraph.GetVertices().ContainsKey(end))
            {
                ShowWarning("Вершины не существуют");
                return;
            }
            // Select algorithm based on combo box
            string selectedAlgorithm = algorithmCombo.SelectedItem.ToString();
            calculatedPath = FindPathWithAlgorithm(selectedAlgorithm, start, end);
            DisplayPathResult(calculatedPath, start, end);
            visualizationPanel.Invalidate();
        }

        private ShortestPathInfo FindPathWithAlgorithm(string algorithm, string start, string end)
        {
            switch (algorithm)
            {
                case "A*":
                    return myGraph.AStarAlgorithm(start, end);
                case "Naive O(n²+m)":
                    return myGraph.NaiveShortestPath(start, end);
                default: // Dijkstra
                    return myGraph.DijkstraAlgorithm(start, end);
            }
        }

        private void DisplayPathResult(ShortestPathInfo path, string start, string end)
        {
            if (!path.IsPathFound)
            {
                resultBox.Text = $"Путь {start}→{end} не найден\r\nГраф может быть несвязанным\r\nАлгоритм: {path.AlgorithmName}";
                ShowMessage("Путь не существует");
            }
            else
            {
                string pathStr = string.Join(" → ", path.VertexSequence);
                resultBox.Text = $"Алгоритм: {path.AlgorithmName}\r\n" +
                                $"Кратчайший путь {start}→{end}:\r\n{pathStr}\r\n" +
                                 $"Расстояние: {path.TotalCost}\r\n" +
                                 $"Вершин в пути: {path.VertexSequence.Count}\r\n" +
                                 $"Релаксаций: {path.RelaxationCount}\r\n" +
                                 $"Посещено вершин: {path.VisitedNodesCount}";
                ShowMessage($"Найден путь! Расстояние: {path.TotalCost}");
            }
        }

        private void HandleAnalyzeAlgorithms()
        {
            string start = startBox.Text.Trim().ToUpper();
            string end = endBox.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
            {
                ShowWarning("Введите начало и конец");
                return;
            }

            if (!myGraph.GetVertices().ContainsKey(start) || !myGraph.GetVertices().ContainsKey(end))
            {
                ShowWarning("Вершины не существуют");
                return;
            }

            var results = new List<PerformanceResult>();
            int vertexCount = myGraph.GetVertices().Count;
            int edgeCount = myGraph.GetEdges().Values.Sum(list => list.Count);

            // Run Dijkstra
            var dijkstraResult = RunAlgorithmWithTiming("Dijkstra", start, end);
            results.Add(dijkstraResult);

            // Run A*
            var astarResult = RunAlgorithmWithTiming("A*", start, end);
            results.Add(astarResult);

            // Run Naive
            var naiveResult = RunAlgorithmWithTiming("Naive O(n²+m)", start, end);
            results.Add(naiveResult);

            // Display comparison table
            DisplayAnalysisResults(results, vertexCount, edgeCount);
        }

        private PerformanceResult RunAlgorithmWithTiming(string algorithm, string start, string end)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var pathInfo = FindPathWithAlgorithm(algorithm, start, end);
            stopwatch.Stop();

            return new PerformanceResult
            {
                AlgorithmName = algorithm,
                VertexCount = myGraph.GetVertices().Count,
                TotalCost = pathInfo.TotalCost,
                EdgeCount = myGraph.GetEdges().Values.Sum(list => list.Count),
                RelaxationCount = pathInfo.RelaxationCount,
                VisitedNodesCount = pathInfo.VisitedNodesCount,
                ExecutionTimeMs = stopwatch.Elapsed.TotalMilliseconds
            };
        }

        private void DisplayAnalysisResults(List<PerformanceResult> results, int vertexCount, int edgeCount)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("СРАВНИТЕЛЬНЫЙ АНАЛИЗ АЛГОРИТМОВ");
            sb.AppendLine("═══════════════════════════════════════════════");
            sb.AppendLine($"Граф: вершин = {vertexCount}, рёбер = {edgeCount}");
            sb.AppendLine();
            sb.AppendLine("Алгоритм          | Релаксации | Посещено | Время(мс)");
            sb.AppendLine("──────────────────┼────────────┼──────────┼──────────");

            foreach (var result in results)
            {
                string algoName = result.AlgorithmName.PadRight(17);
                string relaxations = result.RelaxationCount.ToString().PadLeft(10);
                string visited = result.VisitedNodesCount.ToString().PadLeft(8);
                string time = result.ExecutionTimeMs.ToString("F3").PadLeft(9);
                sb.AppendLine($"{algoName} | {relaxations} | {visited} | {time}");
            }

            sb.AppendLine();
            sb.AppendLine("ТЕОРЕТИЧЕСКАЯ СЛОЖНОСТЬ:");
            sb.AppendLine($"Naive:    O(n²+m) = O({vertexCount}²+{edgeCount}) = O({vertexCount * vertexCount + edgeCount})");
            sb.AppendLine($"Dijkstra: O(n²+m) = O({vertexCount * vertexCount + edgeCount})");
            sb.AppendLine($"A*:       O(b^d) зависит от эвристики");
            sb.AppendLine();

            var bestRelaxations = results.Min(r => r.RelaxationCount);
            var bestTime = results.Min(r => r.ExecutionTimeMs);
            
            sb.AppendLine("ВЫВОДЫ:");
            foreach (var result in results)
            {
                double efficiency = (double)bestRelaxations / result.RelaxationCount * 100;
                sb.AppendLine($"• {result.AlgorithmName}: эффективность {efficiency:F1}% " +
                             $"({result.RelaxationCount} vs {bestRelaxations} оптим.)");
            }

            analysisBox.Text = sb.ToString();
            ShowMessage("Анализ завершён! См. таблицу ниже.");
            visualizationPanel.Invalidate();
        }

        private void DrawGraph(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var normalPen = new Pen(Color.Black, 2);
            var highlightPen = new Pen(Color.Red, 3);
            var weightFont = new Font("Arial", 9, FontStyle.Bold);

            foreach (var kvp in myGraph.GetEdges())
            {
                var vertices = myGraph.GetVertices();
                var fromVertex = vertices[kvp.Key];
                int fromX = fromVertex.PositionX;
                int fromY = fromVertex.PositionY;

                foreach (var edge in kvp.Value)
                {
                    var toVertex = vertices[edge.DestinationVertex];
                    int toX = toVertex.PositionX;
                    int toY = toVertex.PositionY;

                    bool highlight = false;
                    if (calculatedPath != null && calculatedPath.IsPathFound)
                    {
                        for (int i = 0; i < calculatedPath.VertexSequence.Count - 1; i++)
                        {
                            if (calculatedPath.VertexSequence[i] == edge.SourceVertex && 
                                calculatedPath.VertexSequence[i + 1] == edge.DestinationVertex)
                            {
                                highlight = true;
                                break;
                            }
                        }
                    }

                    g.DrawLine(highlight ? highlightPen : normalPen, fromX, fromY, toX, toY);
                    DrawArrowTip(g, highlight ? highlightPen : normalPen, fromX, fromY, toX, toY);

                    int midX = (fromX + toX) / 2;
                    int midY = (fromY + toY) / 2;
                    string wText = edge.EdgeWeight.ToString();
                    var textSize = g.MeasureString(wText, weightFont);
                    g.FillEllipse(Brushes.White, midX - textSize.Width / 2 - 2, midY - textSize.Height / 2 - 2, textSize.Width + 4, textSize.Height + 4);
                    g.DrawString(wText, weightFont, Brushes.Blue, midX - textSize.Width / 2, midY - textSize.Height / 2);
                }
            }

            int radius = 25;
            var nodeFont = new Font("Arial", 12, FontStyle.Bold);
            var nodePen = new Pen(Color.Black, 2);

            foreach (var vertex in myGraph.GetVertices().Values)
            {
                bool inPath = calculatedPath != null && calculatedPath.IsPathFound && calculatedPath.VertexSequence.Contains(vertex.VertexName);
                var brush = inPath ? Brushes.Yellow : Brushes.LightBlue;

                g.FillEllipse(brush, vertex.PositionX - radius, vertex.PositionY - radius, radius * 2, radius * 2);
                g.DrawEllipse(nodePen, vertex.PositionX - radius, vertex.PositionY - radius, radius * 2, radius * 2);

                var textSize = g.MeasureString(vertex.VertexName, nodeFont);
                g.DrawString(vertex.VertexName, nodeFont, Brushes.Black, vertex.PositionX - textSize.Width / 2, vertex.PositionY - textSize.Height / 2);
            }
        }

        private void DrawArrowTip(Graphics g, Pen pen, int x1, int y1, int x2, int y2)
        {
            const int arrowLen = 10;
            const int nodeRad = 25;

            double dx = x2 - x1;
            double dy = y2 - y1;
            double dist = Math.Sqrt(dx * dx + dy * dy);

            if (dist == 0) return;

            dx /= dist;
            dy /= dist;

            int tipX = (int)(x2 - dx * nodeRad);
            int tipY = (int)(y2 - dy * nodeRad);

            double angle = Math.Atan2(dy, dx);
            int leftX = (int)(tipX - arrowLen * Math.Cos(angle - Math.PI / 6));
            int leftY = (int)(tipY - arrowLen * Math.Sin(angle - Math.PI / 6));
            int rightX = (int)(tipX - arrowLen * Math.Cos(angle + Math.PI / 6));
            int rightY = (int)(tipY - arrowLen * Math.Sin(angle + Math.PI / 6));

            g.DrawLine(pen, tipX, tipY, leftX, leftY);
            g.DrawLine(pen, tipX, tipY, rightX, rightY);
        }

        private void ShowMessage(string msg)
        {
            MessageBox.Show(msg, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowWarning(string msg)
        {
            MessageBox.Show(msg, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
