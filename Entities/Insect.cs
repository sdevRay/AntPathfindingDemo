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
			insect.SetState(new ExploreState());
			return insect;
		}

		public void Ortientate()
        {
            float rotation = (float)Math.Atan2(Velocity.Y, Velocity.X) * (float)(180.0f / Math.PI);
            Rotation = rotation;
        }


        public override void Update()
        {
            State.Update(this);

            Position += Velocity;
            Position = Vector2
				.Clamp(
				Position, TextureSize / 2, 
				new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()) - TextureSize / 2
				);

            //// If the entity reaches the edge of the screen, bounce off it
            //if (Position.X < 0 || Position.X + Texture.width > Raylib.GetScreenWidth())
            //{
            //    Velocity.X *= -1;
            //}
            //if (Position.Y < 0 || Position.Y + Texture.height > Raylib.GetScreenHeight())
            //{
            //    Velocity.Y *= -1;
            //}
        }
    }
}
