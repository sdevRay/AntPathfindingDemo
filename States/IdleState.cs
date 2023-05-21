using ConsoleApp1.Entities;
using System.Numerics;

namespace ConsoleApp1.States
{
    internal class IdleState : IState
    {

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
                insect.Target = null;
                insect.Velocity = Vector2.Zero;
                insect.SetState(new RandomState());
            }
        }
    }
}
