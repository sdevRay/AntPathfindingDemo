using Raylib_cs;
using System.Drawing;

namespace ConsoleApp1.Pathfinding
{

    internal static class WorldMap
    {
        public static List<Node> Graph = new List<Node>();

        private static void AddNode(int x, int y, int pixelBoundWidth, int pixelBoundHeight) 
            => Graph.Add(new Node(x, y, pixelBoundWidth, pixelBoundHeight));

        public static void CreateGraph()
        {
            int x = 0;
            int y = 0;

            int width = 10;
            int height = 10;

            var pixelBoundWidth = Raylib.GetScreenWidth() / width;
            var pixelBoundHeight = Raylib.GetScreenHeight() / height;

            for (; x < width; x++)
            {
                for (; y < height; y++) 
                {
                    AddNode(x, y, pixelBoundWidth, pixelBoundHeight);
                }

                y = 0;
            }   
        }

        // https://www.redblobgames.com/pathfinding/a-star/introduction.html
        public static void BreadthFirstSearch(Node start)
        {
            //frontier = Queue()
            //frontier.put(start)
            //reached = set()
            //reached.add(start)

            //while not frontier.empty():
            //   current = frontier.get()
            //   for next in graph.neighbors(current):
            //      if next not in reached:
            //            frontier.put(next)
            //         reached.add(next)
            start = Graph.First(g => g.Point.Equals(new Point(9, 9)));

            var frontier = new Queue<Node>();
            frontier.Enqueue(start);

            // path A->B is stored as came_from[B] == A
            var cameFrom = new Dictionary<Node, Node?>
            {
                { start, null }
            };

            while (frontier.Any()) 
            { 
                var current = frontier.Dequeue();

                foreach (var next in GetNeighbors(current))
                {
                    if (!cameFrom.ContainsKey(next))
                    {
                        frontier.Enqueue(next);
                        cameFrom.Add(next, current);
                    }
                }
            }


            //current = goal
            //path = []
            //while current != start: 
            //   path.append(current)
            //   current = came_from[current]
            //path.append(start) # optional
            //path.reverse() # optional

            var current1 = Graph.First(g => g.Point.Equals(new Point(1, 1)));
            var path = new HashSet<Node>();
            while (current1 != null && current1 != start)
            {
                _ = path.Add(current1);
                current1 = cameFrom.TryGetValue(current1, out Node? value) ? value : null;
            }
            path.Add(start);
            path.Reverse();

            foreach(var p in path)
            {
                p.Color = Raylib_cs.Color.RED;
            }
        }

        public static IEnumerable<Node> GetNeighbors(Node node)
        {
            var p1 = new Point(node.Point.X + 1, node.Point.Y);
            var p2 = new Point(node.Point.X - 1, node.Point.Y);
            var p3 = new Point(node.Point.X, node.Point.Y + 1);
            var p4 = new Point(node.Point.X, node.Point.Y - 1);


            var neighbors = Graph.Where(n => n.Point.Equals(p1) || n.Point.Equals(p2) || n.Point.Equals(p3) || n.Point.Equals(p4));            
            return neighbors;
        }

        public static void DrawGraph()
        {
            foreach (var node in Graph)
            {
                Raylib.DrawRectangleLines(node.PixelBounds.X, node.PixelBounds.Y, node.PixelBounds.X + node.PixelBounds.Width, node.PixelBounds.Y + node.PixelBounds.Height, Raylib_cs.Color.BLACK);
                Raylib.DrawText(node.Point.ToString(), node.PixelBounds.X, node.PixelBounds.Y, 20, node.Color);
            }
        }
    }

  
}
