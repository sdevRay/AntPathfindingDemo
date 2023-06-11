using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using Raylib_cs;
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

        private static void StopVelocity(Entity entity)
        {
            if (entity.Velocity != Vector2.Zero)
                entity.Velocity = Vector2.Zero;
        }

        public void Update(Entity entity)
        {
            if(entity is PlayerInsect playerInsect)
            {
                StopVelocity(playerInsect);

            }
            else if (entity is Insect insect)
            {
                StopVelocity(insect);

                // TODO
                // Randomly rotate while waiting for mouse input


                // For testing, automatically enter random node
                var passable = WorldMap.GetPassableNodes();
                insect.Target = passable.ElementAt(Raylib.GetRandomValue(0, passable.Count() - 1));
                insect.SetState(new PathfindingState());
            }
        }
    }
}
