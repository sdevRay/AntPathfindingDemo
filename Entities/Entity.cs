using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
	abstract class Entity
	{
		protected Texture2D texture;
		protected Color color = Color.WHITE;
		public Vector2 Position, Velocity;

		public bool IsExpired; // true if the entity was destroyed and should be deleted

		public abstract void Update();
		public virtual void Draw()
		{
			Raylib.DrawTexture(texture, (int)Position.X, (int)Position.Y, color);
		}
	}
}
