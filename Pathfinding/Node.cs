using System.Drawing;

namespace ConsoleApp1.Pathfinding
{
    internal class Node
    {
        public Point Point { get; }
        public Rectangle PixelBounds { get; }
        public Raylib_cs.Color Color { get; set; } = Raylib_cs.Color.BLACK;
        public Node(int x, int y, int pixelBoundWidth, int pixelBoundHeight) 
        { 
            Point = new Point(x, y);
            PixelBounds = new Rectangle(x * pixelBoundWidth, y * pixelBoundHeight, pixelBoundWidth, pixelBoundHeight);
        }
    }
}
