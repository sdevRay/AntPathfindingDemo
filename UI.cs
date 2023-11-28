using AntPathfindingDemo.Entities;
using Raylib_cs;
using System.Numerics;

namespace AntPathfindingDemo
{
    public class UI
    {
        public static Vector2 ScreenSize { get; set; }
        private static int FontSize => 20;
        private static int Offset => 5;
        private static Color Color => Color.WHITE;
        public static void Draw()
        {
            Raylib.DrawText("Ants " + Ant.Count, Offset, Offset, FontSize, Color);

            var fps = "FPS " + Raylib.GetFPS().ToString();
            Raylib.DrawText(fps, (int)ScreenSize.X - Raylib.MeasureText(fps, FontSize) - Offset, Offset, FontSize, Color);

            Raylib.DrawText("Left click: move", 10, (int)ScreenSize.Y - ((FontSize * 3) + Offset), FontSize, Color);
            Raylib.DrawText("Right click: spawn ant", 10, (int)ScreenSize.Y - ((FontSize * 2) + Offset), FontSize, Color);
            Raylib.DrawText("Scroll wheel: +/- zoom", 10, (int)ScreenSize.Y - (FontSize + Offset), FontSize, Color);
        }
    }
}
