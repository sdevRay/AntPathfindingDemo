using AntPathfindingDemo.Entities;
using AntPathfindingDemo.Pathfinding;
using Raylib_cs;

namespace AntPathfindingDemo.States
{
    internal class PathfindingState : IState
    {
        readonly Stack<Node?> _path = new();
        Node? _target = null;

        public PathfindingState(Entity pathfindingEntity, Node? finalTarget)
        {
            if (WorldMap.TryGetNode(pathfindingEntity.PixelOrigin, out Node? startNode)
                && startNode is not null
                && finalTarget is not null)
            {            
                _path = AStarSearch.GetPath(startNode, finalTarget);

                if(_path.Any())
                {
                    SetNextTarget(pathfindingEntity);       
                }
            } 
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
                if (WorldMap.TryGetPassableNode(pathfindingEntity.PixelOrigin, out Node? occupiedNode) 
                    && occupiedNode is not null 
                    && Raylib.CheckCollisionRecs(pathfindingEntity.DestinationRectangle, occupiedNode.DestinationRectangle))
                {
                    pathfindingEntity.Speed = Terrain.ApplyMovementCost(occupiedNode, pathfindingEntity);
                }

                if (EntityMathUtil.RotateTowardsTarget(pathfindingEntity, _target.PixelOrigion)
                    && EntityMathUtil.MoveTowardsTarget(pathfindingEntity, _target.PixelOrigion))
                {
                    if(pathfindingEntity is AntQueen)
                        _target.Color = Color.WHITE;

                    SetNextTarget(pathfindingEntity);
                }
            }
            else
            {
                SetNextTarget(pathfindingEntity);
            }
        }
    }
}
