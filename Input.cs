using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using ConsoleApp1.States;
using Raylib_cs;

namespace ConsoleApp1
{
    internal static class Input
    {
        public static void Update()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                var target = WorldMap.GetNode(Raylib.GetMousePosition());

                if (target != null)
                {
                    PlayerInsect.Instance.Target = target;
                    PlayerInsect.Instance.SetState(new PathfindingState());
                }
            }
        }
    }
}
