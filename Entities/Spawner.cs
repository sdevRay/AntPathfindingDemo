using ConsoleApp1.Pathfinding;
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

                EntityManager.Add(Environment.Food.CreatePizza(GetRandomSpawnPosition()));

                foreach (int value in Enumerable.Range(1, 0))
                {
                    EntityManager.Add(Insect.CreateAnt(GetRandomSpawnPosition()));
                }

                _first = false;
            }
        }

        private static Vector2 GetRandomSpawnPosition()
        {
            var passableNodes = WorldMap.GetPassableNodes();
            var randomPosition = passableNodes.ElementAt(Raylib.GetRandomValue(0, passableNodes.Count() - 1));
            return randomPosition.Centroid;
        }
    }
}
