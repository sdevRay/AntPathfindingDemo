using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    internal class Ant : Entity
    {
        public Ant(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Scale = 0.10f;
            Rotation = 0f;

            Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"[{DateTime.Now.TimeOfDay}] [{nameof(Entity)}] {nameof(Ant)} created.");
        }

        public override void Update()
        {
            //throw new NotImplementedException();
        }
    }
}
