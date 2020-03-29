using System;
using System.Collections.Generic;
using System.Linq;

namespace GrafGry
{
    class Vertex
    {
        public int value;
        public string player;
        public List<Edge> childs;
        public Vertex(int newValue, string newPlayer)
        {
            if (newPlayer == "ant")
            {
                if (newValue > 21)
                {
                    player = "koniec";
                    value = 55;
                }
                else if (newValue == 21)
                {
                    player = "koniec";
                    value = 50;
                }
                else
                {
                    player = "pro";
                    value = newValue;
                }
            }
            else
            {
                if (newValue > 21)
                {
                    player = "koniec";
                    value = 45;
                }
                else if (newValue == 21)
                {
                    player = "koniec";
                    value = 50;
                }
                else
                {
                    player = "ant";
                    value = newValue;
                }
            }
            childs = new List<Edge>();
        }
        public bool equals(Vertex self, Vertex other)
        {
            return self.value == other.value && self.player == other.player;
        }

    }
    class Edge
    {
        public Vertex parent;
        public Vertex child;
        public int value;

        public Edge(Vertex newParent, Vertex newChild, int newValue)
        {
            parent = newParent;
            child = newChild;
            value = newValue;

        }

        public bool Which(Edge self, Edge other)
        {
            return self.parent.player == other.parent.player;
        }
    }

    class Graph
    {
        public List<Vertex> graph;
        public Graph()
        {
            graph = new List<Vertex>();
            graph.Add(new Vertex(0, "ant"));
        }


        public void AddVertex(Vertex vertex)
        {
            graph.Add(vertex);
        }
        public void Grafik(Vertex begin, int[] tab)
        {
            foreach (int item in tab)
            {
                graph.Add(new Vertex(begin.value + item, begin.player));
                begin.childs.Add(new Edge(begin, graph.Last(), item));
                if (begin.value + item < 21) Grafik(graph.Last(), tab);
            }
        }

    }

    class Algorithm
    {
        public List<Edge> prot;
        public List<Edge> ant;

        public Algorithm()
        {
            prot = new List<Edge>();
            ant = new List<Edge>();
        }

        public int MinMax(Vertex vertex)
        {
            if (vertex.player == "koniec")
            {
                return vertex.value;
            }

            if (vertex.player == "pro")
            {
                vertex.value = vertex.childs.Max(edge => MinMax(edge.child));
                return vertex.value;
            }

            if (vertex.player == "ant")
            {
                vertex.value = vertex.childs.Min(edge => MinMax(edge.child));
                return vertex.value;
            }

            return 0;
        }

        public void Edges(Vertex begin)
        {
            if (begin.player == "koniec")
            {
                return;
            }
            var edge = begin.childs.Where(x => x.child.value == begin.value).OrderBy(e => e.value).First();
            if (begin.player == "pro")
            {
                prot.Add(edge);
                Edges(edge.child);
            }
            if (begin.player == "ant")
            {
                ant.Add(edge);
                Edges(edge.child);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] tab = { 4, 5, 6 };
            Vertex begin = new Vertex(0, "ant");
            Graph graf = new Graph();
            graf.Grafik(begin, tab);
            var alg = new Algorithm();
            alg.MinMax(begin);
            alg.Edges(begin);


            foreach (Edge item in alg.prot)
            {
                Console.WriteLine(item.parent.player + " " + item.child.player + " " + item.value);
            }
            Console.WriteLine();
            foreach (Edge item in alg.ant)
            {
                Console.WriteLine(item.parent.player + " " + item.child.player + " " + item.value);
            }
        }
    }
}