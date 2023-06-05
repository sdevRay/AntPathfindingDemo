using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using Raylib_cs;
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
            if (entity is PathfindingEntity pathfindingEntity)
            {
                if(pathfindingEntity.Target is null)
                {
                    pathfindingEntity.Velocity = Vector2.Zero;
                    pathfindingEntity.SetState(new IdleState());
                }
                else if (_firstRun)
                {
                    var startNode = WorldMap.GetStartingNode(pathfindingEntity);
                    var goalNode = pathfindingEntity.Target;

                    if (startNode is not null && goalNode is not null)
                    {
                        _path = AStarSearch.GetPath(startNode, goalNode);
                        _firstRun = !_firstRun;
                    }

                    if (_path.TryPop(out Node? value))
                    {
                        _currentTarget = value;
                    }
                    else
                    {
                        pathfindingEntity.Velocity = Vector2.Zero;
                        pathfindingEntity.SetState(new IdleState());
                    }
                }

                if (_currentTarget is not null)
                {
                    if (_pointTowardsTargetBool)
                    {
                        if (EntityMathUtil.PointTowardsTarget(pathfindingEntity, _currentTarget.Centroid))
                        {
                            SwapTowardsTarget();
                        }
                    }
                    else if (_moveTowardsTargetBool)
                    {
                        //_time += Raylib.GetFrameTime();
                        if(Raylib.CheckCollisionRecs(pathfindingEntity.DestinationRectangle, _currentTarget.DestinationRectangle))
                        {
                            pathfindingEntity.ApplyMovementCost(_currentTarget);
                        }

                        if (EntityMathUtil.MoveTowardsTarget(pathfindingEntity, _currentTarget.Centroid/*, _time, 0.1f*/))
                        {
                            if (_path.TryPop(out Node? value))
                            {
                                _currentTarget = value;
                                SwapTowardsTarget();
                            }
                            else
                            {
                                pathfindingEntity.Velocity = Vector2.Zero;
                                pathfindingEntity.Target = null;
                            }
                        }
                    }
                }
            }
        }
    }
}
