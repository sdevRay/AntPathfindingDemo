using ConsoleApp1.Entities;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.States
{
    internal class IdleState : IState
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
            if(entity is Insect insect)
            {
                if(framesCounter == 0)
                {
                    insect.Velocity = Vector2.Zero;
                }

                framesCounter++;
 
               insect.Rotation += 60.0f * Raylib.GetFrameTime(); // Rotate the rectangles, 60 degrees per second

                if (framesCounter == 60)
                {
                    insect.SetState(new SeekFoodState());                    
                }
            }
        }
    }
}
