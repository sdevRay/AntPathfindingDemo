using ConsoleApp1.States;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
	abstract class Entity
	{
		protected IState State = new IdleState();
		protected Texture2D Texture;
		public Vector2 Position, Velocity;
		public float Rotation;
		public bool IsExpired; // true if the entity was destroyed and should be deleted
        
		public Vector2 TextureSize { get { return new Vector2(Texture.width, Texture.height); } }
		public Vector2 Origin { get { return new Vector2(Texture.width / 2, Texture.height / 2); } }
		public Rectangle SourceRec { get { return new Rectangle(0, 0, Texture.width, Texture.height);  } }
        public Rectangle DestRec { get { return new Rectangle(Position.X, Position.Y, Texture.width, Texture.height); } }

        public abstract void Update();

		public virtual void SetState(IState state)
		{
			State = state;
		}

		public virtual void Draw()
		{
            Raylib.DrawTexturePro(Texture, SourceRec, DestRec, Origin, Rotation * (float)(180.0f / Math.PI), Color.WHITE);
		}
	}
}
