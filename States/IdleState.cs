using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using Raylib_cs;

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
                // TODO
                // Randomly rotate while waiting for mouse input


                // For testing, automatically enter random node
                var passable = WorldMap.Graph.Where(n => !n.Impassable);
                insect.Target = passable.ElementAt(Raylib.GetRandomValue(0, passable.Count() - 1));
                insect.SetState(new PathfindingState());
            }
        }
    }
}
