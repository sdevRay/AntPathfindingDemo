using ConsoleApp1.Pathfinding;
using ConsoleApp1.States;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    internal class Insect : PathfindingEntity
    {
        public Insect(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Rotation = default;

            Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"[{DateTime.Now.TimeOfDay}] [{nameof(Entity)}] {nameof(Insect)} created.");
        }

        public static Insect CreateAnt(Vector2 position)
        {
            var insect = new Insect(Art.Ant, position);
            insect.SetState(new IdleState());
            return insect;
        }

        public override void Draw()
        {
            if (Program.Debug)
            {
                Raylib.DrawText(State.GetType().ToString(), (int)Position.X, (int)Position.Y, 10, Color.BLACK);
                Raylib.DrawText(Speed.ToString(), (int)Position.X + 15, (int)Position.Y + 15, 15, Color.BLACK);
                if (Target != null)
                {
                    Raylib.DrawLineV(Position, Target.Centroid, Color.RED);
                }
            }

            base.Draw();
        }

        public override void Update()
        {
            State.Update(this);
            Position += Velocity * Speed * Raylib.GetFrameTime();
        }
    }
}
