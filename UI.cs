using ConsoleApp1.Entities;
using Raylib_cs;

namespace ConsoleApp1
{
    public class UI
    {
        public static void Draw()
        {
            Raylib.DrawText("Ants " + Insect.Count, 20, 20, 25, Raylib_cs.Color.WHITE);
            Raylib.DrawText("Pizzas " + Food.Count, 20, 50, 25, Raylib_cs.Color.WHITE);
        }
    }
}
