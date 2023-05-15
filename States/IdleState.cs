using ConsoleApp1.Entities;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.States
{
    internal class IdleState : IState
    {
        private Vector2? _targetPosition;
        public void HandleAction(Entity entity, Actions action)
        {
            //if (input == RELEASE_DOWN)
            //{
            //    // Change to standing state...
            //    heroine.setGraphics(IMAGE_STAND);
            //}
        }

        public IdleState()
        {
        }

        private static Vector2 GetRandomPoint(Vector2 position, int radius)
        {
            // Generate random point within radius
            Vector2 randomPoint = new Vector2(
            Raylib.GetRandomValue((int)position.X - radius, (int)position.X + radius),
            Raylib.GetRandomValue((int)position.Y - radius, (int)position.Y + radius));

            var offset = 20;
            randomPoint = Vector2.Clamp(
                randomPoint, new Vector2(offset, offset), new Vector2(Raylib.GetScreenWidth() - offset, Raylib.GetScreenHeight() - offset));

            return randomPoint;
        }

        public void Update(Entity entity)
        {
            if (entity is Insect insect)
            {
                if (!_targetPosition.HasValue)
                {
                    _targetPosition = GetRandomPoint(insect.Position, 500);
                    insect.Velocity = Vector2.Zero;
                }

                // Calculate the angle between the sprite and the target position using Atan2
                float angleToTarget = (float)Math.Atan2(_targetPosition.Value.Y - insect.Position.Y, _targetPosition.Value.X - insect.Position.X);

                // Calculate the shortest angle between the current rotation and the angle to the target position
                float shortestAngle = angleToTarget - insect.Rotation;
                if (shortestAngle > Math.PI)
                {
                    shortestAngle -= 2 * (float)Math.PI;
                }
                else if (shortestAngle < -Math.PI)
                {
                    shortestAngle += 2 * (float)Math.PI;
                }

                // Gradually rotate the sprite towards the target position
                insect.Rotation += shortestAngle * 10f * Raylib.GetFrameTime();

                // If the sprite is facing the target position, choose a new random target
                if (Math.Abs(shortestAngle) < 0.1f)
                {
                    insect.SetState(new SeekState(_targetPosition.Value));
                }

                if (Program.Debug)
                {
                    Raylib.DrawLineV(insect.Position, _targetPosition.Value, Color.BLACK);
                    Raylib.DrawText(insect.Rotation.ToString(), (int)insect.Position.X + 10, (int)insect.Position.Y + 10, 5, Color.BLACK);    
                }
            }
        }
    }
}
