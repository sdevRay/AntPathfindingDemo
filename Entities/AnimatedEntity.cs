using AntPathfindingDemo.States;
using Raylib_cs;
using System.Numerics;

namespace AntPathfindingDemo.Entities
{
    internal class AnimatedEntity : Entity
    {
        private int _currentFrame, _framesCounter = 0;
        private readonly int _framesSpeed = 12;

        public AnimatedEntity(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            SourceRectangle = new Rectangle(0.0f, 0.0f, (float)Texture.width / 4, (float)Texture.height);
            Origin = new Vector2((Texture.width / 4) / 2, Texture.height / 2);
            DestinationRectangle = new Rectangle(Position.X, Position.Y, Texture.width / 4, Texture.height);
        }

        public override void Update()
        {
            DestinationRectangle.x = Position.X;
            DestinationRectangle.y = Position.Y;

            State.Update(this);
            Position += Velocity * Speed * Raylib.GetFrameTime();

            if (State is PathfindingState)
            {
                _framesCounter++;

                if (_framesCounter >= (60 / _framesSpeed))
                {
                    _framesCounter = 0;
                    _currentFrame++;

                    if (_currentFrame > 4) _currentFrame = 0;
                    SourceRectangle.x = (float)_currentFrame * (float)Texture.width / 4;
                }
            }
            else if (State is IdleState)
            {
                if (SourceRectangle.x != 0)
                    SourceRectangle.x = 0;
            }
        }
    }
}
