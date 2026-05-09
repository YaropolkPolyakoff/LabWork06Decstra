using System;
using System.Collections.Generic;
using System.Linq;

namespace DijkstraApp
{
    public class Graph
    {
        private Dictionary<int, List<EdgeInfo>> vertices;
        
        public Graph()
        {
            vertices = new Dictionary<int, List<EdgeInfo>>();
        }
        
        public void AddVertex(int vertexId)
        {
            if (!vertices.ContainsKey(vertexId))
            {
                vertices[vertexId] = new List<EdgeInfo>();
            }
        }
        
        public void AddEdge(int from, int to, int weight)
        {
            if (!vertices.ContainsKey(from))
                AddVertex(from);
            if (!vertices.ContainsKey(to))
                AddVertex(to);
            
            vertices[from].Add(new EdgeInfo { Target = to, Weight = weight });
            vertices[to].Add(new EdgeInfo { Target = from, Weight = weight });
        }
        
        public void RemoveVertex(int vertexId)
        {
            if (!vertices.ContainsKey(vertexId))
                return;
            
            foreach (var key in vertices.Keys.ToList())
            {
                vertices[key].RemoveAll(e => e.Target == vertexId);
            }
            
            vertices.Remove(vertexId);
        }
        
        public void RemoveEdge(int from, int to)
        {
            if (vertices.ContainsKey(from))
                vertices[from].RemoveAll(e => e.Target == to);
            if (vertices.ContainsKey(to))
                vertices[to].RemoveAll(e => e.Target == from);
        }
        
        public List<int> GetAllVertices()
        {
            return vertices.Keys.ToList();
        }
        
        public List<EdgeInfo> GetEdgesFrom(int vertexId)
        {
            if (vertices.ContainsKey(vertexId))
                return vertices[vertexId];
            return new List<EdgeInfo>();
        }
        
        public void ClearGraph()
        {
            vertices.Clear();
        }
        
        public DijkstraResult FindShortestPath(int start)
        {
            var dist = new Dictionary<int, int>();
            var prev = new Dictionary<int, int>();
            var queue = new HashSet<int>();
            
            foreach (var v in vertices.Keys)
            {
                dist[v] = int.MaxValue;
                prev[v] = -1;
                queue.Add(v);
            }
            
            dist[start] = 0;
            
            while (queue.Count > 0)
            {
                int current = -1;
                int minDist = int.MaxValue;
                
                foreach (var v in queue)
                {
                    if (dist[v] < minDist)
                    {
                        minDist = dist[v];
                        current = v;
                    }
                }
                
                if (current == -1 || dist[current] == int.MaxValue)
                    break;
                
                queue.Remove(current);
                
                foreach (var edge in vertices[current])
                {
                    if (queue.Contains(edge.Target))
                    {
                        int alt = dist[current] + edge.Weight;
                        if (alt < dist[edge.Target])
                        {
                            dist[edge.Target] = alt;
                            prev[edge.Target] = current;
                        }
                    }
                }
            }
            
            return new DijkstraResult { Distances = dist, Previous = prev };
        }
    }
    
    public class EdgeInfo { public int Target; public int Weight; }
    
    public class DijkstraResult { public Dictionary<int, int> Distances; public Dictionary<int, int> Previous; }
}
