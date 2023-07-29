using ConsoleApp1.Pathfinding;
using ConsoleApp1.States;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    internal class Ant : AnimatedEntity
    {
        public static int Count { get; private set; }
        public Ant(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Count++;

            // If the entity spawns on impassable terrain it will still move towards passable terrain
            Speed = 10f;
        }

        public static Ant CreateAnt(Vector2 position)
        {
            var insect = new Ant(Art.Ant, position);
            insect.SetState(new PathfindingState(insect, WorldMap.GetRandomNode()));
            return insect;
        }
    }
}
