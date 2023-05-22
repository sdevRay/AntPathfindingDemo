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

                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                {
                    var target = WorldMap.GetNode(Raylib.GetMousePosition());
                    
                    insect.Target = target;
                    insect.SetState(new PathfindingState());
                }
                else // For testing, automatically enter random state
                {
                    insect.Target = WorldMap.Graph[Raylib.GetRandomValue(0, WorldMap.Graph.Count - 1)];
                    insect.SetState(new PathfindingState());
                }
            }
        }
    }
}
