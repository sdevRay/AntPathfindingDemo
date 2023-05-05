using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
	abstract class Entity
	{
		protected Texture2D Texture;
		protected Color Color = Color.WHITE;
		public Vector2 Position, Velocity;
		public float Rotation, Scale;

		public bool IsExpired; // true if the entity was destroyed and should be deleted

		public abstract void Update();
		public virtual void Draw()
		{
			Raylib.DrawTextureEx(Texture, Position, Rotation, Scale, Color);
		}
	}
}
