using ConsoleApp1.States;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    internal class Insect : Entity
    {
        public float Speed = 100.0f;
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

        public override void Update()
        {
            Raylib.DrawText(State.GetType().ToString(), 12, 12, 20, Color.BLACK);

            State.Update(this);

            // Orientate towards direction of movement
            float rotation = (float)Math.Atan2(Velocity.Y, Velocity.X) * (float)(180.0f / Math.PI);
            Rotation = rotation;

            Position += Velocity * Speed * Raylib.GetFrameTime();

            const int radius = 100;

            if (Position.X < 0)
            {
                var randomPoint = new Vector2(
                Raylib.GetRandomValue((int)Position.X, (int)Position.X + radius),
                Raylib.GetRandomValue((int)Position.Y - radius, (int)Position.Y + radius));
                //Velocity.X *= -1;
                SetState(new SeekState(randomPoint));
            }

            if(Position.X > Raylib.GetScreenWidth())
            {
                var randomPoint = new Vector2(
                Raylib.GetRandomValue((int)Position.X - radius, (int)Position.X),
                Raylib.GetRandomValue((int)Position.Y - radius, (int)Position.Y + radius));
                //Velocity.X *= -1;
                SetState(new SeekState(randomPoint));
            }

            if (Position.Y < 0)
            {
                var randomPoint = new Vector2(
                Raylib.GetRandomValue((int)Position.X - radius, (int)Position.X + radius),
                Raylib.GetRandomValue((int)Position.Y, (int)Position.Y + radius));
                //Velocity.Y *= -1;
                SetState(new SeekState(randomPoint));
            }

            if (Position.Y > Raylib.GetScreenHeight())
            {
                var randomPoint = new Vector2(
                Raylib.GetRandomValue((int)Position.X - radius, (int)Position.X + radius),
                Raylib.GetRandomValue((int)Position.Y - radius, (int)Position.Y));
                //Velocity.Y *= -1;
                SetState(new SeekState(randomPoint));
            }
        }
    }
}
