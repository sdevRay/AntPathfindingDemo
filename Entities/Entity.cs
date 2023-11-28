using AntPathfindingDemo.States;
using Raylib_cs;
using System.Numerics;

namespace AntPathfindingDemo.Entities
{
    public abstract class Entity
    {
        protected IState State = new IdleState();
        protected Texture2D Texture;
        public Vector2 Position, Velocity, Origin;
        public float Rotation, Speed;
        public float Radius = 5; // used for circular collision detection
        public bool IsExpired; // true if the entity was destroyed and should be deleted
        public Color Color = Color.WHITE;

        public Vector2 PixelOrigin { get { return new Vector2(Position.X + Origin.X, Position.Y + Origin.Y); } }
        public Rectangle SourceRectangle, DestinationRectangle;

        public abstract void Update();

        public virtual void SetState(IState state) => State = state;

        public virtual void Draw()
        {
            Raylib.DrawTexturePro(Texture, SourceRectangle, DestinationRectangle, Origin, Rotation * (float)(180.0f / Math.PI), Color);
        }
    }
}
