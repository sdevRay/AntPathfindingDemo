using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    static class EntityManager
    {
        static List<Entity> _entities = new();
        private static List<Insect> _insects = new();
        static List<Food> _foods = new();

        public static void Add(Entity entity)
        {
            _entities.Add(entity);

            if (entity is Insect insect)
            {
                _insects.Add(insect);
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
            _insects = _insects.Where(entity => !entity.IsExpired).ToList();
        }

        public static void HandleCollisions()
        {
            // Insect on insect collision
            for (int i = 0; i < _insects.Count; i++)
            {
                for (int j = i + 1; j < _insects.Count; j++)
                {
                    // If the sprites are colliding, bump them away from each other
                    if (Raylib.CheckCollisionCircles(_insects[i].Position, _insects[i].Radius, _insects[j].Position, _insects[j].Radius))
                    {
                        // Calculate the vector between the two sprites
                        Vector2 collisionVector = _insects[j].Position - _insects[i].Position;
                        collisionVector = Vector2.Normalize(collisionVector);

                        // Move the sprites apart along the collision vector
                        _insects[i].Position -= collisionVector * 2f;
                        _insects[j].Position += collisionVector * 2f;

                    }
                }
            }

            // Insect on player collision
            for (int i = 0; i < _insects.Count; i++)
            {
                // If the sprites are colliding, bump them away from each other
                if (Raylib.CheckCollisionCircles(_insects[i].Position, _insects[i].Radius, PlayerInsect.Instance.Position, PlayerInsect.Instance.Radius))
                {
                    // Calculate the vector between the two sprites
                    Vector2 collisionVector = PlayerInsect.Instance.Position - _insects[i].Position;
                    collisionVector = Vector2.Normalize(collisionVector);

                    // Move the sprites apart along the collision vector
                    _insects[i].Position -= collisionVector * 2f;
                    PlayerInsect.Instance.Position += collisionVector * 2f;
                }
            }

            // Inset on food collision
            for (int i = 0; i < _insects.Count; i++)
            {
                for (int j = 0; j < _foods.Count; j++)
                {
                    if (Raylib.CheckCollisionCircles(_foods[j].Position, _foods[j].Radius, _insects[i].Position, _insects[i].Radius))
                    {
                        _foods[j].GotEaten();
                    }
                }
            }

            // Player on food collision
            for (int i = 0; i < _foods.Count; i++)
            {
                if (Raylib.CheckCollisionCircles(_foods[i].Position, _foods[i].Radius, PlayerInsect.Instance.Position, PlayerInsect.Instance.Radius))
                {
                    _foods[i].GotEaten();
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
