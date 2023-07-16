using ConsoleApp1.Entities;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Pathfinding
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
            // Waypoints are the key decision points where you might have to change direction.
            // Design this around waypoints, and setting the insects seek state to each waypoint.

            int x = 0;
            int y = 0;

            int width = 20;
            int height = 20;

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
     
        public static bool TryGetNode(Vector2 position, out Node? node)
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

        public static void DrawGraph()
        {
            foreach (var node in Graph)
            {
                Raylib.DrawTexture(node.Terrain.Texture, (int)node.DestinationRectangle.x, (int)node.DestinationRectangle.y, Raylib_cs.Color.WHITE);
                Raylib.DrawText("Ant count" + EntityManager.Insects.Count, -50, -50, 50, Raylib_cs.Color.WHITE);
            }
        }
    }
}
