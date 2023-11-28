using Raylib_cs;
using System.Numerics;

namespace AntPathfindingDemo.Pathfinding
{
    internal class WorldMap
    {
        public static List<Node> Graph = new();
        public static IEnumerable<Node> GetPassableNodes() { return Graph.Where(n => !n.Terrain.Impassable); }
        private static void AddNode(int x, int y, int pixelBoundWidth, int pixelBoundHeight)
        {
            Graph.Add(new Node(x, y, pixelBoundWidth, pixelBoundHeight));
        }

        public static void CreateGraph()
        {
            int x = 0;
            int y = 0;

            int width = 20;
            int height = 15;

            var pixelBoundWidth = Raylib.GetScreenWidth() / width;
            var pixelBoundHeight = Raylib.GetScreenHeight() / height;

            for (; x < width; x++)
            {
                for (; y < height; y++)
                {
                    // Nodes are the key decision points where you might have to change direction.
                    AddNode(x, y, pixelBoundWidth, pixelBoundHeight);
                }

                y = 0;
            }
        }

        public static bool TryGetNode(Vector2 position, out Node? node)
        {
            node = default;

            var result = Graph.FirstOrDefault(n => Raylib.CheckCollisionPointRec(position, n.DestinationRectangle));
            if (result != null)
            {
                node = result;
                return true;
            }

            return false;
        }

        public static bool TryGetPassableNode(Vector2 position, out Node? node)
        {
            node = default;

            var result = GetPassableNodes().FirstOrDefault(n => Raylib.CheckCollisionPointRec(position, n.DestinationRectangle));
            if (result != null)
            {
                node = result;
                return true;
            }

            return false;
        }

        public static Node GetRandomNode()
        {
            var passable = GetPassableNodes();
            return passable.ElementAt(Raylib.GetRandomValue(0, passable.Count() - 1));
        }

        public static void Draw()
        {
            foreach (var node in Graph)
            {
                Raylib.DrawTexture(node.Terrain.Texture, (int)node.DestinationRectangle.x, (int)node.DestinationRectangle.y, node.Color);
            }
        }
    }
}
