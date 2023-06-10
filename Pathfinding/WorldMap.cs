using ConsoleApp1.Entities;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Pathfinding
{

    // https://www.redblobgames.com/pathfinding/a-star/introduction.html
    internal class WorldMap
    {
        public static List<Node> Graph = new List<Node>();

        public static IEnumerable<Node> GetPassableNodes() { return Graph.Where(n => !n.Impassable); }
        private static void AddNode(int x, int y, int pixelBoundWidth, int pixelBoundHeight, int movementCost)
        {
            Texture2D texture;
            TerrainType terrainType;
            bool impassable = false;
            if (movementCost == 4)
            {
                terrainType = TerrainType.Mud;
                texture = Art.Mud;
                impassable = true;
            }
            else if(movementCost == 3)
            {
                terrainType = TerrainType.Dirt;
                texture = Art.Dirt;
            }
            else
            {
                terrainType = TerrainType.Grass;
                movementCost = 1; // All grass treated the same
                texture = Art.Grass;
            }
            
            Graph.Add(new Node(x, y, pixelBoundWidth, pixelBoundHeight, texture, terrainType, impassable, movementCost));
        }

        public static void CreateGraph()
        {
            // Waypoints are the key decision points where you might have to change direction.
            // Consider designing this around waypoints, and setting the insects seek state to each waypoint.

            int x = 0;
            int y = 0;

            int width = 15;
            int height = 15;

            var pixelBoundWidth = Raylib.GetScreenWidth() / width;
            var pixelBoundHeight = Raylib.GetScreenHeight() / height;

            for (; x < width; x++)
            {
                for (; y < height; y++)
                {
                    // Testing Astar with random heavy movement cost nodes
                    var movementCost = Raylib.GetRandomValue(1, 4);
                    AddNode(x, y, pixelBoundWidth, pixelBoundHeight, movementCost);
                }

                y = 0;
            }
        }
     
        public static Node? GetStartingNode(Entity entity)
        {
            return GetNode(entity.Position);
        }

        public static Node? GetNode(Vector2 position)
        {
            var passableNodes = GetPassableNodes();
            return passableNodes.FirstOrDefault(n => Raylib.CheckCollisionPointRec(position, n.DestinationRectangle));
        }

        public static void DrawGraph()
        {
            foreach (var node in Graph)
            {
                Raylib.DrawTexture(node.Texture, (int)node.DestinationRectangle.x, (int)node.DestinationRectangle.y, Raylib_cs.Color.WHITE);
                //Raylib.DrawText(node.Point.ToString(), (int)node.DestinationRectangle.x + 5, (int)node.DestinationRectangle.y + 5, 12, node.Color);
                //Raylib.DrawText(node.MovementCost.ToString(), (int)node.DestinationRectangle.x + 10, (int)node.DestinationRectangle.y + 20, 20, node.Color);
            }
        }
    }
}
