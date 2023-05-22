using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using System.Numerics;

namespace ConsoleApp1.States
{
    internal class PathfindingState : IState
    {
        bool _firstRun = true;
        float _time;
        Stack<Node?> _path = new();
        Node? _currentTarget;
        bool _pointTowardsTargetBool = true;
        bool _moveTowardsTargetBool = false;

        private void SwapTowardsTarget()
        {
            _pointTowardsTargetBool = !_pointTowardsTargetBool;
            _moveTowardsTargetBool = !_moveTowardsTargetBool;
        }

        public void HandleAction(Entity entity, Actions action)
        {
            throw new NotImplementedException();
        }

        public void Update(Entity entity)
        {
            if (entity is Insect insect)
            {
                if (_firstRun && insect.Target is not null)
                {
                    var startNode = WorldMap.GetStartingNode(insect);
                    var goalNode = insect.Target;

                    if (startNode is not null && goalNode is not null)
                    {
                        _path = WorldMap.GetPath(startNode, goalNode);
                    }

                    _firstRun = !_firstRun;

                    if (_path.TryPop(out Node? value))
                    {
                        _currentTarget = value;
                    }
                    else
                    {
                        // Set new state
                    }
                }

                if (_currentTarget is not null)
                {
                    if (_pointTowardsTargetBool)
                    {
                        if (EntityMathUtil.PointTowardsTarget(insect, _currentTarget.Centroid))
                        {
                            SwapTowardsTarget();
                        }
                    }
                    else if (_moveTowardsTargetBool)
                    {
                        //_time += Raylib.GetFrameTime();
                        if (EntityMathUtil.MoveTowardsTarget(insect, _currentTarget.Centroid/*, _time, 0.1f*/))
                        {
                            if (_path.TryPop(out Node? value))
                            {
                                insect.ApplyMovementCost(_currentTarget.MovementCost);
                                _currentTarget = value;
                                SwapTowardsTarget();
                            }
                            else
                            {
                                // Reset all node's colors back to default
                                foreach (var node in WorldMap.Graph)
                                {
                                    node.Color = Raylib_cs.Color.BLACK;
                                }

                                insect.Velocity = Vector2.Zero;
                                insect.Target = null;
                                insect.SetState(new IdleState());
                            }
                        }
                    }
                }
            }
        }
    }
}
