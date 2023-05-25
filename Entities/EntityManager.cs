using ConsoleApp1.Pathfinding;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    static class EntityManager
    {
        static List<Entity> _entities = new();
        static List<Insect> _insects = new();
        public static List<Environment.Food> Foods = new();

        public static void Add(Entity entity)
        {
            _entities.Add(entity);

            if (entity is Insect insect)
            {
                _insects.Add(insect);
            }

            if (entity is Environment.Food food)
            {
                Foods.Add(food);
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
            Foods = Foods.Where(entity => !entity.IsExpired).ToList();
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

            // Impassable node collision
            foreach (var node in WorldMap.Graph.Where(n => n.Impassable))
            {
                foreach(var insect in _insects)
                {
                    var center = new Vector2(insect.DestinationRectangle.x + (insect.DestinationRectangle.width / 2), insect.DestinationRectangle.y + (insect.DestinationRectangle.height / 2));
                    if (Raylib.CheckCollisionCircleRec(center, insect.Radius, node.DestinationRectangle))
                    {

                        Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"[{DateTime.Now.TimeOfDay}] Collision at [{node.Point}].");
                    }
                }

                var playerAntCenter = new Vector2(PlayerInsect.Instance.DestinationRectangle.x + (PlayerInsect.Instance.DestinationRectangle.width / 2), PlayerInsect.Instance.DestinationRectangle.y + (PlayerInsect.Instance.DestinationRectangle.height / 2));
                if (Raylib.CheckCollisionCircleRec(playerAntCenter, 5, node.DestinationRectangle))
                {
                    //Moving right
                    if(PlayerInsect.Instance.Velocity.X > 0)
                    {
                        if (PlayerInsect.Instance.DestinationRectangle.x + PlayerInsect.Instance.DestinationRectangle.width >= node.DestinationRectangle.x) 
                        {
                            //PlayerInsect.Instance.Velocity = Vector2.Zero;
                            PlayerInsect.Instance.Position.X = node.DestinationRectangle.x - PlayerInsect.Instance.DestinationRectangle.width;
                        }

                    }
                    
                    // Moving left
                    if (PlayerInsect.Instance.Velocity.X < 0) 
                    {
                        if (PlayerInsect.Instance.DestinationRectangle.x <= node.DestinationRectangle.width)
                        {
                            //PlayerInsect.Instance.Velocity = Vector2.Zero;
                            PlayerInsect.Instance.Position.X = PlayerInsect.Instance.DestinationRectangle.width;
                        }
                    }

                    Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"[{DateTime.Now.TimeOfDay}] Collision at [{node.Point}].");
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
