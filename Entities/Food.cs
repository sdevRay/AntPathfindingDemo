using ConsoleApp1.Pathfinding;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    public class Food : Entity
    {
        public Food(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            SourceRectangle = new Rectangle(0, 0, Texture.width, Texture.height);
            Origin = new Vector2(Texture.width / 2, Texture.height / 2);
            DestinationRectangle = new Rectangle(Position.X, Position.Y, Texture.width, Texture.height);
        }

        public static Food CreatePizza(Vector2 position)
        {
            var food = new Food(Art.Pizza, position);
            return food;
        }

        public override void Update()
        {
            DestinationRectangle.x = Position.X;
            DestinationRectangle.y = Position.Y;
        }

        public void Eaten()
        {
            var randomNode = WorldMap.GetRandomNode();
            Position = randomNode.PixelOrigion;
            //IsExpired = true;
        }
    }
}