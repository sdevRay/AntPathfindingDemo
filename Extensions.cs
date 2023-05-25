using System.Drawing;
using System.Numerics;

namespace ConsoleApp1
{
    internal static class Extensions
    {        
        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }
    }
}
