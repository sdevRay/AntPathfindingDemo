using ConsoleApp1.Entities;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.States
{
    internal class SeekFoodState : IState
    {
        int framesCounter = default;
        public void HandleAction(Entity entity, Actions action)
        {
            //if (input == RELEASE_DOWN)
            //{
            //    // Change to standing state...
            //    heroine.setGraphics(IMAGE_STAND);
            //}
        }

        public void Update(Entity entity)
        {
            if (entity is Insect insect)
            {
                foreach (var food in EntityManager.Foods)
                {
                    var distance = Vector2.Distance(insect.Position, food.Position);

                    if (distance < 300)
                    {
                        // Draw a line between the two objects to indicate their direction
                        Raylib.DrawLineEx(insect.Position, food.Position, 2f, Color.BLACK);
                        Raylib.DrawText(distance.ToString(), (int)(insect.Position.X + insect.DestRec.height), (int)(insect.Position.Y + insect.DestRec.height), 20, Color.BLACK);
                    }

                    // Moving oject seeks other object
                    Vector2 desiredVelocity = Vector2.Normalize(food.Position - insect.Position);
                    insect.Velocity += (desiredVelocity - insect.Velocity) * 0.1f;
                }
            }
        }
    }
}
