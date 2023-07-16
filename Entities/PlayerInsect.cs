using ConsoleApp1.Pathfinding;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    internal class PlayerInsect : Entity
    {
        private static PlayerInsect? _instance;
        public static PlayerInsect Instance
        {
            get
            {
                _instance ??= new PlayerInsect(Art.Ant, WorldMap.GetRandomNode().PixelOrigion);
                return _instance;
            }
        }

        public PlayerInsect(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Color = Color.WHITE;
            Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"[{DateTime.Now.TimeOfDay}] [{nameof(Entity)}] {nameof(PlayerInsect)} created.");         
        }

        public override void Update()
        {
            State.Update(this);
            Position += Velocity * Speed * Raylib.GetFrameTime();
        }
    }
}
