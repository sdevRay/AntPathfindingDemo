using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    static class InsectSpawner
    {
        static bool _first = true;

        public static void Update()
        {
            if (_first)
            {
                // This is for testing, food will not come from the InsectSpanwer.
				EntityManager.Add(Environment.Food.CreatePizza(GetSpawnPosition()));

                EntityManager.Add(Insect.CreateAnt(GetSpawnPosition()));
				_first = false;
            }
        }

        private static Vector2 GetSpawnPosition()
        {
            return new Vector2(Raylib.GetRandomValue(0, (int)Program.ScreenSize.X), Raylib.GetRandomValue(0, (int)Program.ScreenSize.Y));
        }
    }
}
