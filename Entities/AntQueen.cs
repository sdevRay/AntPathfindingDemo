using ConsoleApp1.Pathfinding;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
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
            Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"[{DateTime.Now.TimeOfDay}] [{nameof(Entity)}] {nameof(AntQueen)} created.");         
        }
    }
}
