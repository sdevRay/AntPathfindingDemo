using ConsoleApp1.States;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
	abstract class Entity
	{
		protected IState State = new IdleState();
		protected Texture2D Texture;
		protected Color Color = Color.WHITE;
		public Vector2 Position, Velocity;
		public float Rotation;
		public float Radius = 20; // used for circular collision detection
		public bool IsExpired; // true if the entity was destroyed and should be deleted
        
		public Vector2 TextureOrigin { get { return new(Texture.width / 2, Texture.height / 2); } }
		public Rectangle SourceRec { get { return new Rectangle(0, 0, Texture.width, Texture.height);  } }
        public Rectangle DestRec { get { return new Rectangle(Position.X, Position.Y, Texture.width, Texture.height); } }

        public abstract void Update();

		public virtual void SetState(IState state)
		{
			State = state;
		}

		public virtual void Draw()
		{
            Raylib.DrawTexturePro(Texture, SourceRec, DestRec, TextureOrigin, Rotation, Color);
		}
	}
}
