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

            var offsetFromEdgeOfScreen = 20;
            randomPoint = Vector2.Clamp(
                randomPoint, new Vector2(offsetFromEdgeOfScreen, offsetFromEdgeOfScreen), new Vector2(Raylib.GetScreenWidth() - offsetFromEdgeOfScreen, Raylib.GetScreenHeight() - offsetFromEdgeOfScreen));

            return randomPoint;
        }

        public void Update(Entity entity)
        {
            if (entity is Insect insect)
            {
                if (!_targetPosition.HasValue)
                {
                    _targetPosition = GetRandomPoint(insect.Position, Raylib.GetRandomValue(200, 600));
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

                // If the sprite is facing the target position
                if (Math.Abs(shortestAngle) < 0.1f)
                {
                    insect.SetState(new SeekState(_targetPosition.Value));
                }

                if (Program.Debug)
                {
                    Raylib.DrawLineV(insect.Position, _targetPosition.Value, Color.BLACK);
                }
            }
        }
    }
}
