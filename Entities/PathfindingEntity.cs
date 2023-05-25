using ConsoleApp1.Pathfinding;

namespace ConsoleApp1.Entities
{
    internal class PathfindingEntity : Entity
    {
        public Node? Target;
        public float Speed = 150f;

        public void ApplyMovementCost(int movementCost)
        {
            switch (movementCost)
            {
                case >= 10:
                    Speed = 75f;
                    break;
                case > 5:
                    Speed = 150f;
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
