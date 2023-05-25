using Raylib_cs;
using System.Drawing;
using System.Numerics;

namespace ConsoleApp1.Pathfinding
{
    public class Node
    {
        public Point Point { get; }
        public int MovementCost { get; }
        public Texture2D Texture { get; }
        public bool Impassable { get; } = false;
        public Raylib_cs.Rectangle DestinationRectangle { get; }
        public Vector2 Centroid { get { return new Vector2(DestinationRectangle.x + DestinationRectangle.width / 2, DestinationRectangle.y + DestinationRectangle.height / 2); } }
        public Raylib_cs.Color Color { get; set; } = Raylib_cs.Color.BLACK;
        public Node(int x, int y, int pixelWidth, int pixelHeight, Texture2D texture, bool impassable = false, int movementCost = 1)
        {
            Point = new Point(x, y);
            DestinationRectangle = new Raylib_cs.Rectangle(x * pixelWidth, y * pixelHeight, pixelWidth, pixelHeight);
            MovementCost = movementCost;
            Texture = texture;
            Impassable = impassable;
        }
    }
}
