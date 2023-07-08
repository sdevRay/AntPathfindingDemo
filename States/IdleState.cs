using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.States
{
    internal class IdleState : IState
    {
        private CountdownTimer _rotateTimer;
        private CountdownTimer _newStateTimer;
        private Vector2 _target;

        public IdleState()
        {
            var randomLifetime = Raylib.GetRandomValue(0, 6);
            _newStateTimer = new CountdownTimer(randomLifetime);

            _rotateTimer = new CountdownTimer(3);
            _target = WorldMap.GetRandomNode().PixelOrigion;
        }

        public void HandleAction(Entity entity, Actions action)
        {
        }

        public void Update(Entity entity)
        {
            if (entity.Velocity != Vector2.Zero)
                entity.Velocity = Vector2.Zero;

            if (EntityMathUtil.RotateTowardsTarget(entity, _target))
            {
                if (!_rotateTimer.IsComplete())
                {
                    _rotateTimer.Update();
                }
                else
                {
                    _target = WorldMap.GetRandomNode().PixelOrigion;
                    _rotateTimer.Reset();
                }
            }

            if (entity is Insect insect)
            {
                if (!_newStateTimer.IsComplete())
                {
                    _newStateTimer.Update();
                }
                else
                {
                    insect.SetState(new PathfindingState(insect, WorldMap.GetRandomNode()));
                }
            }
        }
    }
}
