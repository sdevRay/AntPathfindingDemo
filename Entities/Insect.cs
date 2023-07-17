using ConsoleApp1.Pathfinding;
using ConsoleApp1.States;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    internal class Insect : Entity
    {
        public static int Count;
        public Insect(Texture2D texture, Vector2 position)
        {
            Count++;
            Texture = texture;
            Position = new Vector2(position.X + texture.width / 2, position.Y + texture.height / 2);
            Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"[{DateTime.Now.TimeOfDay}] [{nameof(Entity)}] {nameof(Insect)} created.");
        }

        public static Insect CreateAnt(Vector2 position)
        {
            var insect = new Insect(Art.Ant, position);
            insect.SetState(new PathfindingState(insect, WorldMap.GetRandomNode()));
            return insect;
        }

        public override void Update()
        {
            State.Update(this);
            Position += Velocity * Speed * Raylib.GetFrameTime();
        }
    }
}
