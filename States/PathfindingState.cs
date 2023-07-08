using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using Raylib_cs;

namespace ConsoleApp1.States
{
    internal class PathfindingState : IState
    {
        Stack<Node?> _path = new();
        Node? _target = null;

        public PathfindingState(Entity pathfindingEntity, Node? target)
        {
            _target = target;

            if (WorldMap.TryGetNode(pathfindingEntity.PixelOrigin, out Node? startNode) && _target is not null)
            {
                _path = AStarSearch.GetPath(startNode, _target);
            }

            SetNextTarget(pathfindingEntity);
        }

        public void HandleAction(Entity entity, Actions action)
        {
            throw new NotImplementedException();
        }

        private void SetNextTarget(Entity pathfindingEntity)
        {
            if (_path.TryPop(out Node? newTarget))
            {
                _target = newTarget;
            }
            else
            {
                pathfindingEntity.SetState(new IdleState());
            }
        }

        public void Update(Entity pathfindingEntity)
        {
            if (_target is not null)
            {
                //Check the movementCost of the terrain
                if (WorldMap.TryGetNode(pathfindingEntity.PixelOrigin, out Node? occupiedNode) && Raylib.CheckCollisionRecs(pathfindingEntity.DestinationRectangle, occupiedNode.DestinationRectangle))
                {
                    pathfindingEntity.Speed = Terrain.ApplyMovementCost(occupiedNode);
                }

                if (EntityMathUtil.RotateTowardsTarget(pathfindingEntity, _target.PixelOrigion)
                    && EntityMathUtil.MoveTowardsTarget(pathfindingEntity, _target.PixelOrigion))
                {
                    SetNextTarget(pathfindingEntity);
                }
            }
        }
    }
}
