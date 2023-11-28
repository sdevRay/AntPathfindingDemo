using AntPathfindingDemo.Entities;
using AntPathfindingDemo.Pathfinding;
using AntPathfindingDemo.States;
using Raylib_cs;
using System.Numerics;

namespace AntPathfindingDemo
{
    internal static class Input
    {
        private static Node? _previousTarget;
        public static void Update(ref Camera2D camera)
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                var mousePos = Raylib.GetMousePosition();
                Vector2 screenPos = Raylib.GetScreenToWorld2D(new Vector2(mousePos.X, mousePos.Y), camera);

                if (WorldMap.TryGetPassableNode(screenPos, out Node? target))
                {
                    if (_previousTarget is not null && _previousTarget != target)
                    {
                        _previousTarget.Color = Color.WHITE;
                    }

                    if(target is not null)
                        target.Color = Color.RAYWHITE;

                    _previousTarget = target;
                    AntQueen.Instance.SetState(new PathfindingState(AntQueen.Instance, target));
                }
            } 
            else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_RIGHT))
            {
                EntityManager.Add(Ant.CreateAnt(new Vector2(AntQueen.Instance.Position.X + AntQueen.Instance.Radius, AntQueen.Instance.Position.Y + AntQueen.Instance.Radius)));
            }
        }
    }
}
