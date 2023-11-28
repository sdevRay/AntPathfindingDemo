using AntPathfindingDemo.Pathfinding;
using AntPathfindingDemo.States;
using Raylib_cs;
using System.Numerics;

namespace AntPathfindingDemo.Entities
{
    public class Food : Entity
    {
        private static CountdownTimer _spawnDelayTimer;
        private static bool _spawnDelay = true;
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

        public static void UpdateSpawner()
        {
            if (_spawnDelay)
            {
                if (!_spawnDelayTimer.IsComplete())
                {
                    _spawnDelayTimer.Update();
                }
                else
                {
                    EntityManager.Add(CreatePizza(WorldMap.GetRandomNode().PixelOrigion));
                    _spawnDelay = false;
                }
            }
        }

        public override void Update()
        {
            DestinationRectangle.x = Position.X;
            DestinationRectangle.y = Position.Y;
        }

        public void Eaten()
        {
            _spawnDelay = true;
            _spawnDelayTimer = new CountdownTimer(3f);
            IsExpired = true; 
        }
    }
}