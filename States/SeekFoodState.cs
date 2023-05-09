using ConsoleApp1.Entities;
using Raylib_cs;
using System;
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
				foreach(var food in EntityManager.Foods)
				{

					


					// Draw a line between the two objects to indicate their direction
					Raylib.DrawLineEx(insect.Position, food.Position, 2f, Color.BLACK);


					insect.Rotation = (float)angle;
					Raylib.DrawText(angle.ToString(), 12, 12, 20, Color.BLACK);

					//// Move the moving circle towards the stationary circle
					//Vector2 desiredVelocity = Vector2.Normalize(stationaryCircle.center - movingCircle.center) * 5;
					//velocity += (desiredVelocity - velocity) * 0.1f;
					//movingCircle.center += velocity;
				}



				//public static float ToAngle(this Vector2 vector)
				//{
				//	return (float)Math.Atan2(vector.Y, vector.X);
				//}

				//framesCounter++;
				//if (framesCounter == 60)
				//{
				//    insect.Velocity = Vector2.UnitX;
				//} 
				//else if (framesCounter == 120)
				//{
				//    insect.Velocity = -Vector2.UnitY;
				//}
				//else if (framesCounter == 180)
				//{
				//    insect.Velocity = -Vector2.UnitX;
				//}
				//else if (framesCounter == 240)
				//{
				//    insect.Velocity = Vector2.UnitY;
				//}
				//else if (framesCounter == 300)
				//{
				//    insect.SetState(new IdleState());
				//}
			}
        }
    }
}
