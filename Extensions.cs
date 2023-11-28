﻿using System.Drawing;
using System.Numerics;

namespace AntPathfindingDemo
{
    internal static class Extensions
    {
        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }
    }
}
