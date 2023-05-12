using ConsoleApp1.Entities;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.States
{
    internal class SeekState : IState
    {
        private Vector2 _targetPosition;
        private readonly Entity? _target;

        public SeekState(Entity target) : this(target.Position)
        {
            _target = target;      
        }

        public SeekState(Vector2 targetPosition)
        {
            _targetPosition = targetPosition;
        }

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
                // Calculate direction towards target
                Vector2 direction = _targetPosition - insect.Position;

                // ??? Set direction to targer ^ random interval set it to a random offset ??? This to cause slight curving in path
                // Update direction towards random target point
                //if (direction == Vector2.Zero || Vector2.Distance(position, direction) <= speed * Raylib.GetFrameTime())
                //{
                //    direction = new Vector2(Raylib.GetRandomValue(0, Raylib.GetScreenWidth()), Raylib.GetRandomValue(0, Raylib.GetScreenHeight()));
                 
                //    direction -= position;
                //}

                // Normalize direction to get unit vector
                Vector2 normalizedDirection = Vector2.Normalize(direction);

                // Set velocity based on direction
                insect.Velocity = normalizedDirection;

                // Target reached
                if (Vector2.Distance(insect.Position, _targetPosition) < 1.0f)
                {
                    insect.SetState(new IdleState());
                }

                Raylib.DrawLineV(insect.Position, _targetPosition, Color.BLACK);                                     

                // check and handle collision
                // if (entity is whatever)
                // handle action, like if an insect collison with food, the eat action would increase health
                // but if it collides with another ant the attack action will take health away
                // this should interlock the two ants into combat
            }
        }
    }
}
