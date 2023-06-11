using ConsoleApp1.Pathfinding;

namespace ConsoleApp1.Entities
{
    internal class PathfindingEntity : Entity
    {
        public Node? Target;
        public float Speed = 125f;

        public void ApplyMovementCost(Node node)
        {
            switch (node.TerrainType)
            {
                case TerrainType.Dirt:
                    Speed = 75f;
                    break;
                case TerrainType.Grass:
                    Speed = 125f;
                    break;
                case TerrainType.Mud:
                    break;
                default:
                    throw new InvalidOperationException($"Invalid {nameof(TerrainType)}: ${node.TerrainType}");
            }
        }

        public override void Update()
        {
        }
    }
}
