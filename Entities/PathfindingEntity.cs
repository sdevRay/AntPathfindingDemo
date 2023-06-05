using ConsoleApp1.Pathfinding;

namespace ConsoleApp1.Entities
{
    internal class PathfindingEntity : Entity
    {
        public Node? Target;
        public float Speed = 150f;

        public void ApplyMovementCost(Node node)
        {
            // This keeps breaking if I try and make a corner 
            switch (node.TerrainType)
            {
                case TerrainType.Dirt:
                    Speed = 75f;
                    break;
                default:
                    Speed = 200f;
                    break;
            }
        }

        public override void Update()
        {
        }
    }
}
