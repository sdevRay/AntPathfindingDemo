using Raylib_cs;

namespace ConsoleApp1.Pathfinding
{
    public enum TerrainType
    {
        Grass,
        Mud,
        Dirt
    }

    public class Terrain
    {
        private static readonly IDictionary<TerrainType, int> _movementCostTerrainTypeMapping = new Dictionary<TerrainType, int>()
        {
            { TerrainType.Mud, 0 },
            { TerrainType.Grass, 1 },
            { TerrainType.Dirt, 2 },
        };

        public TerrainType Type { get; }
        public Texture2D Texture { get; }
        public float MovementCost { get; }
        public bool Impassable { get; } = false;

        public Terrain(TerrainType type, Texture2D texture, bool impassable)
        {
            Type = type;
            Texture = texture;
            MovementCost = _movementCostTerrainTypeMapping.TryGetValue(type, out int movementCost) ? movementCost : 0;
            Impassable = impassable;
        }

        public static Terrain CreateGrass()
        {
            return new Terrain(TerrainType.Grass, Art.Grass, false);
        }

        public static Terrain CreateDirt()
        {
            return new Terrain(TerrainType.Dirt, Art.Dirt, false);
        }

        public static Terrain CreateMud()
        {
            return new Terrain(TerrainType.Mud, Art.Mud, true);
        }

        public static float ApplyMovementCost(Node node)
        {
            switch (node.Terrain.Type)
            {
                case TerrainType.Dirt:
                    return 75f;
                case TerrainType.Grass:
                    return 125f;
                case TerrainType.Mud:
                    return 0f;
                default:
                    throw new InvalidOperationException($"Invalid {nameof(TerrainType)}: ${node.Terrain.Type}");
            }
        }
    }
}
