using System.Drawing;
using System.Numerics;

namespace ConsoleApp1.Pathfinding
{
    internal class Node
    {
        public Point Point { get; }
        public Rectangle DestinationRectangle { get; }
        public Vector2 Centroid { get { return new Vector2(DestinationRectangle.X + DestinationRectangle.Width / 2, DestinationRectangle.Y + DestinationRectangle.Height / 2); } }
        public int MovementCost { get; }
        public Raylib_cs.Color Color { get; set; } = Raylib_cs.Color.BLACK;
        public Node(int x, int y, int pixelWidth, int pixelHeight, int movementCost = 1)
        {
            Point = new Point(x, y);
            DestinationRectangle = new Rectangle(x * pixelWidth, y * pixelHeight, pixelWidth, pixelHeight);
            MovementCost = movementCost;
        }
    }
}
