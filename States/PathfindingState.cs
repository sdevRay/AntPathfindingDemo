using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using Raylib_cs;

namespace ConsoleApp1.States
{
    internal class PathfindingState : IState
    {
        //float _time;
        //float amplitude = 0.5f;
        Stack<Node?> _path = new();
        Node? _currentTarget;
        bool _shouldSetTarget = true;

        public void HandleAction(Entity entity, Actions action)
        {
            throw new NotImplementedException();
        }

        private void SetTarget(PathfindingEntity pathfindingEntity)
        {
            if (_path.TryPop(out Node? value))
            {
                _currentTarget = value;
            }
            else
            {
                pathfindingEntity.Target = null;
            }
        }

        public void Update(Entity entity)
        {
            if (entity is PathfindingEntity pathfindingEntity)
            {
                if (pathfindingEntity.Target is null)
                {
                    pathfindingEntity.SetState(new IdleState());
                }
                else if (_shouldSetTarget)
                {
                    var startNode = WorldMap.GetStartingNode(pathfindingEntity);
                    var goalNode = pathfindingEntity.Target;

                    if (startNode is not null && goalNode is not null)
                    {
                        _path = AStarSearch.GetPath(startNode, goalNode);
                        _shouldSetTarget = !_shouldSetTarget;
                    }

                    SetTarget(pathfindingEntity);
                }
                else
                {
                    //_time += Raylib.GetFrameTime();

                    // Check the movementCost of the terrain
                    if (Raylib.CheckCollisionRecs(pathfindingEntity.DestinationRectangle, _currentTarget.DestinationRectangle))
                    {
                        pathfindingEntity.ApplyMovementCost(_currentTarget);
                    }

                    if (EntityMathUtil.PointTowardsTarget(pathfindingEntity, _currentTarget.Centroid) 
                        && EntityMathUtil.MoveTowardsTarget(pathfindingEntity, _currentTarget.Centroid/*, _time, amplitude*/))
                    {
                        SetTarget(pathfindingEntity);
                    }
                }
            }
        }
    }
}
