using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.States
{
    internal class RandomState : IState
    {
        public void HandleAction(Entity entity, Actions action)
        {
            throw new NotImplementedException();
        }

        public void Update(Entity entity)
        {
            if (entity is Insect insect)
            {
                if (insect.Target == null)
                {
                    insect.Target = WorldMap.Graph[Raylib.GetRandomValue(0, WorldMap.Graph.Count - 1)];
                    insect.Speed = Raylib.GetRandomValue(100, 250);
                }

                // Calculate the angle between the sprite and the target position using Atan2
                float angleToTarget = (float)Math.Atan2(insect.Target.Centroid.Y - insect.Position.Y, insect.Target.Centroid.X - insect.Position.X);

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
                    insect.SetState(new SeekState());
                }
            }
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
    }
}
