using ConsoleApp1.Entities;
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
                framesCounter++;
                if (framesCounter == 60)
                {
                    insect.Velocity = Vector2.UnitX;
                } 
                else if (framesCounter == 120)
                {
                    insect.Velocity = -Vector2.UnitY;
                }
                else if (framesCounter == 180)
                {
                    insect.Velocity = -Vector2.UnitX;
                }
                else if (framesCounter == 240)
                {
                    insect.Velocity = Vector2.UnitY;
                }
                else if (framesCounter == 300)
                {
                    insect.SetState(new IdleState());
                }
            }
        }
    }
}
