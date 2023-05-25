using ConsoleApp1.Pathfinding;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    internal class PlayerInsect : PathfindingEntity
    {
        private static PlayerInsect? _instance;
        public static PlayerInsect Instance
        {
            get
            {
                _instance ??= new PlayerInsect(Art.Ant);
                return _instance;
            }
        }

        public PlayerInsect(Texture2D texture)
        {
            var passableNodes = WorldMap.Graph.Where(n => !n.Impassable);
            var randomPosition = passableNodes.ElementAt(Raylib.GetRandomValue(0, passableNodes.Count() - 1));
            Texture = texture;
            Position = new Vector2(randomPosition.DestinationRectangle.x, randomPosition.DestinationRectangle.y);
            Rotation = default;

            Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"[{DateTime.Now.TimeOfDay}] [{nameof(Entity)}] {nameof(PlayerInsect)} created.");
        }

        public override void Update()
        {
            State.Update(this);
            Position += Velocity * Speed * Raylib.GetFrameTime();
        }
    }
}
