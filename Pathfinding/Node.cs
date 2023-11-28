using Raylib_cs;
using System.Drawing;
using System.Numerics;

namespace AntPathfindingDemo.Pathfinding
{
    public class Node
    {
        public Terrain Terrain { get; set; }
        public Point Point { get; }
        public Raylib_cs.Rectangle DestinationRectangle { get; }
        public Vector2 PixelOrigion { get; }
        public Raylib_cs.Color Color { get; set; } = Raylib_cs.Color.WHITE;
        public Node(int x, int y, int pixelWidth, int pixelHeight)
        {
            Point = new Point(x, y);
            DestinationRectangle = new Raylib_cs.Rectangle(x * pixelWidth, y * pixelHeight, pixelWidth, pixelHeight);
            PixelOrigion = new Vector2(DestinationRectangle.x + DestinationRectangle.width / 2, DestinationRectangle.y + DestinationRectangle.height / 2);
            
            // Randomly generate terrain type
            var random = Raylib.GetRandomValue(0, 5);
            Terrain = random switch
            {
                0 => Terrain.CreateImpassable(),
                1 or 2 => Terrain.CreateRocky(),
                _ => Terrain.CreateGrass(),
            };
        }
    }
}
