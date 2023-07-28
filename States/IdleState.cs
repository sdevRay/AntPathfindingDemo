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
        private CountdownTimer _delayTimer;

        private Vector2 _target;

        public IdleState()
        {
            _newStateTimer = new CountdownTimer(Raylib.GetRandomValue(0, 4));
            _rotateTimer = new CountdownTimer(Raylib.GetRandomValue(3, 10));
            _delayTimer = new CountdownTimer(Raylib.GetRandomValue(0, 2));

            _target = WorldMap.GetRandomNode().PixelOrigion;

        }

        public void HandleAction(Entity entity, Actions action)
        {
            throw new NotImplementedException();
        }

        public void Update(Entity entity)
        {
            if (entity.Velocity != Vector2.Zero)
                entity.Velocity = Vector2.Zero;

            // Short delay before random rotations
            if (!_delayTimer.IsComplete())
            {
                _delayTimer.Update();
            }
            else
            {
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

                if (entity is Ant insect)
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
}
