using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using ConsoleApp1.States;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1
{
    internal static class Input
    {
        public static void Update(ref Camera2D camera)
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                var mousePos = Raylib.GetMousePosition();
                Vector2 screenPos = Raylib.GetScreenToWorld2D(new Vector2(mousePos.X, mousePos.Y), camera);

                if (WorldMap.TryGetPassableNode(screenPos, out Node? target))
                {
                    AntQueen.Instance.SetState(new PathfindingState(AntQueen.Instance, target));
                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                EntityManager.Add(Ant.CreateAnt(new Vector2(AntQueen.Instance.Position.X + AntQueen.Instance.Radius, AntQueen.Instance.Position.Y + AntQueen.Instance.Radius)));
            }
        }
    }
}
