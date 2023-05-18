﻿using Raylib_cs;
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
			for(int i = 0; i < _insects.Count; i++)
			{
				for(int j = i + 1;  j < _insects.Count; j++)
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
		}

		public static void Draw()
		{
			foreach(var entity in _entities) 
			{ 
				entity.Draw();
			}
		}
	}
}
