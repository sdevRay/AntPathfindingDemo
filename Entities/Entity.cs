using ConsoleApp1.States;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
	public abstract class Entity
	{
		protected IState State = new IdleState();
		protected Texture2D Texture;
		public Vector2 Position, Velocity;
        public float Rotation, Speed;
        public float Radius = 5; // used for circular collision detection
		public bool IsExpired; // true if the entity was destroyed and should be deleted
		public Color Color = Color.WHITE;
        
        public Vector2 PixelOrigin { get { return new Vector2(Position.X + Texture.width / 2, Position.Y + Texture.height / 2); } }
        public Rectangle SourceRectangle { get { return new Rectangle(0, 0, Texture.width, Texture.height);  } }
        public Rectangle DestinationRectangle { get { return new Rectangle(Position.X, Position.Y, Texture.width, Texture.height); } }

        public abstract void Update();

		public virtual void SetState(IState state) => State = state;

		public virtual void Draw()
		{
            Raylib.DrawTexturePro(Texture, SourceRectangle, DestinationRectangle, new Vector2(Texture.width / 2, Texture.height / 2), Rotation * (float)(180.0f / Math.PI), Color);
        }
    }
}
