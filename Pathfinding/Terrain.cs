using AntPathfindingDemo.Entities;
using Raylib_cs;

namespace AntPathfindingDemo.Pathfinding
{
    public enum TerrainType
    {
        Grass,
        Impassable,
        Rocky
    }

    public class Terrain
    {
        private static readonly IDictionary<TerrainType, int> _movementCostTerrainTypeMapping = new Dictionary<TerrainType, int>()
        {
            { TerrainType.Impassable, 0 },
            { TerrainType.Grass, 1 },
            { TerrainType.Rocky, 2 },
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
            return new Terrain(TerrainType.Grass, Art.Grass, impassable: false);
        }

        public static Terrain CreateRocky()
        {
            return new Terrain(TerrainType.Rocky, Art.Rocky, impassable: false);
        }

        public static Terrain CreateImpassable()
        {
            return new Terrain(TerrainType.Impassable, Art.Impassable, impassable: true);
        }

        public static float ApplyMovementCost(Node node, Entity pathfindingEntity)
        {
            return (node.Terrain.Type, pathfindingEntity) switch
            {
                (TerrainType.Rocky, Ant) => 75f,
                (TerrainType.Rocky, AntQueen) => 55f,
                (TerrainType.Grass, Ant) => 125f,
                (TerrainType.Grass, AntQueen) => 105f,
                _ => throw new InvalidOperationException($"Invalid {nameof(TerrainType)}: ${node.Terrain.Type}"),
            };
        }
    }
}
