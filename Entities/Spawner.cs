using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    static class Spawner
    {
        static bool _first = true;

        public static void Update()
        {
            if (_first)
            {
                // Might just make this a spawner class for everything
                // send commands here to a dictionary or something that has delegates for what methods to call

                EntityManager.Add(Environment.Food.CreatePizza(GetSpawnPosition()));
                EntityManager.Add(Insect.CreateAnt(GetSpawnPosition()));

                _first = false;
            }
        }

        private static Vector2 GetSpawnPosition()
        {
            return new Vector2(Raylib.GetRandomValue(0, Raylib.GetScreenWidth()), Raylib.GetRandomValue(0, Raylib.GetScreenHeight()));
        }
    }
}
