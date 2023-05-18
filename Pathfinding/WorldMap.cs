using Raylib_cs;

namespace ConsoleApp1.Pathfinding
{

    internal static class WorldMap
    {
        public static List<Cell> Grid = new List<Cell>();
        private static void AddCell(int x, int y, int pixelBoundWidth, int pixelBoundHeight) 
            => Grid.Add(new Cell(x, y, pixelBoundWidth, pixelBoundHeight));

        public static void CreateGrid()
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
                    AddCell(x, y, pixelBoundWidth, pixelBoundHeight);
                }

                y = 0;
            }   
        }
        public static void Draw()
        {
            foreach (var cell in Grid)
            {
                Raylib.DrawRectangleLines(cell.PixelBounds.X, cell.PixelBounds.Y, cell.PixelBounds.X + cell.PixelBounds.Width, cell.PixelBounds.Y + cell.PixelBounds.Height, Color.BLACK);
                Raylib.DrawText(cell.Point.ToString(), cell.PixelBounds.X, cell.PixelBounds.Y, 20, Color.WHITE);
            }
        }
    }

  
}
