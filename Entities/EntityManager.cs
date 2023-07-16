using ConsoleApp1.Pathfinding;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    static class EntityManager
    {
        static List<Entity> _entities = new();
        public static List<Insect> Insects = new();
        static List<Food> _foods = new();

        public static void Add(Entity entity)
        {
            _entities.Add(entity);

            if (entity is Insect insect)
            {
                Insects.Add(insect);
            }

            if (entity is Food food)
            {
                _foods.Add(food);
            }
        }

        public static void Update()
        {
            HandleCollisions();

            foreach (var entity in _entities)
            {
                entity.Update();
            }

            _entities = _entities.Where(entity => !entity.IsExpired).ToList();
            _foods = _foods.Where(entity => !entity.IsExpired).ToList();
            Insects = Insects.Where(entity => !entity.IsExpired).ToList();
        }

        public static void HandleCollisions()
        {
            // Insect on insect collision
            for (int i = 0; i < Insects.Count; i++)
            {
                for (int j = i + 1; j < Insects.Count; j++)
                {
                    // If the sprites are colliding, bump them away from each other
                    if (Raylib.CheckCollisionCircles(Insects[i].Position, Insects[i].Radius, Insects[j].Position, Insects[j].Radius))
                    {
                        // Calculate the vector between the two sprites
                        Vector2 collisionVector = Insects[j].Position - Insects[i].Position;
                        collisionVector = Vector2.Normalize(collisionVector);

                        // Move the sprites apart along the collision vector
                        Insects[i].Position -= collisionVector * 2f;
                        Insects[j].Position += collisionVector * 2f;

                    }
                }
            }
            
            // Insect on player collision
            for (int i = 0; i < Insects.Count; i++)
            {
                // If the sprites are colliding, bump them away from each other
                if (Raylib.CheckCollisionCircles(Insects[i].Position, Insects[i].Radius, PlayerInsect.Instance.Position, PlayerInsect.Instance.Radius))
                {
                    // Calculate the vector between the two sprites
                    Vector2 collisionVector = PlayerInsect.Instance.Position - Insects[i].Position;
                    collisionVector = Vector2.Normalize(collisionVector);

                    // Move the sprites apart along the collision vector
                    Insects[i].Position -= collisionVector * 2f;
                    PlayerInsect.Instance.Position += collisionVector * 2f;
                }
            }

            for(int i = 0; i < _foods.Count; i++)
            {
                if(Raylib.CheckCollisionCircles(_foods[i].Position, _foods[i].Radius, PlayerInsect.Instance.Position, PlayerInsect.Instance.Radius))
                {
                    var randomNode = WorldMap.GetRandomNode();
                    _foods[i].Position = randomNode.PixelOrigion;
                }
            }
        }

        public static void Draw()
        {
            foreach (var entity in _entities)
            {
                entity.Draw();
            }
        }
    }
}
