using System.Numerics;

namespace ConsoleApp1.Entities
{
    static class InsectSpawner
    {
        static readonly Random _rand = new Random();
        static bool _first = true;

        public static void Update()
        {
            if (_first)
            {
                EntityManager.Add(Insect.CreateAnt(GetSpawnPosition()));
                _first = false;
            }
        }

        private static Vector2 GetSpawnPosition()
        {
            return new Vector2(_rand.Next((int)Program.ScreenSize.X), _rand.Next((int)Program.ScreenSize.Y));
        }
    }
}
