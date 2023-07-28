using ConsoleApp1.Entities;
using Raylib_cs;

namespace ConsoleApp1
{
    public class UI
    {
        public static void Draw()
        {
            //Raylib.DrawText("Ants " + Insect.Count, 20, 25, 25, Raylib_cs.Color.WHITE);
            Raylib.DrawText("FPS " + Raylib.GetFPS().ToString(), 20, 75, 25, Raylib_cs.Color.WHITE);
        }
    }
}
