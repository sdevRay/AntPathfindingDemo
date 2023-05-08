using ConsoleApp1.States;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    internal class Insect : Entity
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

        public override void Update()
        {
            State.Update(this);

            Position += Velocity;
        }
    }
}
