using System.Drawing;

namespace ConsoleApp1.Pathfinding
{
    internal class Cell
    {
        public Point Point { get; }
        public Rectangle PixelBounds { get; }
        public Cell(int x, int y, int pixelBoundWidth, int pixelBoundHeight) 
        { 
            Point = new Point(x, y);
            PixelBounds = new Rectangle(x * pixelBoundWidth, y * pixelBoundHeight, pixelBoundWidth, pixelBoundHeight);
        }
    }
}
