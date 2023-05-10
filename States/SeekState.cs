using ConsoleApp1.Entities;
using System.Numerics;

namespace ConsoleApp1.States
{
    internal class SeekState : IState
    {
        private readonly Entity _target;

        public SeekState(Entity target)
        {
            _target = target;
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
                // Moving oject seeks other object
                Vector2 desiredVelocity = Vector2.Normalize(_target.Position - insect.Position);
                insect.Velocity += (desiredVelocity - insect.Velocity) * 0.1f;
                insect.Ortientate();

                // check and handle collision
                // if (entity is whatever)
                // handle action, like if an insect collison with food, the eat action would increase health
                // but if it collides with another ant the attack action will take health away
                // this should interlock the two ants into combat
            }
        }
    }
}
