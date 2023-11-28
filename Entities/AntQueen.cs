using AntPathfindingDemo.Pathfinding;
using Raylib_cs;
using System.Numerics;

namespace AntPathfindingDemo.Entities
{
    internal class AntQueen : AnimatedEntity
    {
        private static AntQueen? _instance;
        public static AntQueen Instance
        {
            get
            {
                _instance ??= new AntQueen(Art.AntQueen, WorldMap.GetRandomNode().PixelOrigion);
                return _instance;
            }
        }

        public AntQueen(Texture2D texture, Vector2 position) : base(texture, position)
        { 
        }
    }
}
