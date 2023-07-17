using ConsoleApp1.Pathfinding;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    public class Food : Entity
    {
        public static int Count;
        public Food(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public static Food CreatePizza(Vector2 position)
        {
            var food = new Food(Art.Pizza, position);
            return food;
        }

        public override void Update()
        {

        }

        public void GotEaten()
        {
            Count++;

            var randomNode = WorldMap.GetRandomNode();
            Position = randomNode.PixelOrigion;

            //IsExpired = true;
        }
    }
}