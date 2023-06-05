using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    internal static class EntityMathUtil
    {
        public static bool PointTowardsTarget(Entity entity, Vector2 target)
        {
            entity.Velocity *= 0.1f;

            // Calculate the angle between the sprite and the target position using Atan2
            float angleToTarget = (float)Math.Atan2(target.Y - entity.Position.Y, target.X - entity.Position.X);

            // Calculate the shortest angle between the current rotation and the angle to the target position
            float shortestAngle = angleToTarget - entity.Rotation;
            if (shortestAngle > Math.PI)
            {
                shortestAngle -= 2 * (float)Math.PI;
            }
            else if (shortestAngle < -Math.PI)
            {
                shortestAngle += 2 * (float)Math.PI;
            }

            // Gradually rotate the sprite towards the target position
            entity.Rotation += shortestAngle * 10f * Raylib.GetFrameTime();

            // If the sprite is facing the target position
            return (Math.Abs(shortestAngle) < 0.1f);
        }

        //time += Raylib.GetFrameTime();
        public static bool MoveTowardsTarget(Entity entity, Vector2 target/*, float time, float amplitude*/)
        {
            // Calculate direction towards target
            Vector2 direction = target - entity.Position;

            // Normalize direction to get unit vector
            Vector2 normalizedDirection = Vector2.Normalize(direction);

            //// Movement like a sinewave 
            //normalizedDirection.Y += (float)Math.Sin(time) * amplitude;
            //normalizedDirection.X += (float)Math.Cos(time) * amplitude;

            // Set velocity based on direction
            entity.Velocity = normalizedDirection;

            // Orientate towards direction of movement
            entity.Rotation = (float)Math.Atan2(entity.Velocity.Y, entity.Velocity.X);

            // Target reached
            return Vector2.Distance(entity.Position, target) < 10.0f;
        }
    }
}
