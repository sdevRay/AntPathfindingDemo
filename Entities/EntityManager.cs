﻿using AntPathfindingDemo.Pathfinding;
using AntPathfindingDemo.States;
using Raylib_cs;
using System.Numerics;

namespace AntPathfindingDemo.Entities
{
    static class EntityManager
    {
        private static List<Entity> _entities = new();
        private static List<Entity> _addedEntities = new();
        private static List<Ant> _ants = new();
        private static List<Food> _foods = new();

        private static bool _isUpdating;

        public static void Add(Entity entity)
        {
            if (!_isUpdating)
            {
                AddEntity(entity);
            }
            else
            {
                _addedEntities.Add(entity);
            }
        }

        private static void AddEntity(Entity entity)
        {
            _entities.Add(entity);

            if (entity is Ant insect)
            {
                _ants.Add(insect);
            }

            if (entity is Food food)
            {
                _foods.Add(food);
            }
        }

        public static void Update()
        {
            _isUpdating = true;

            HandleCollisions();
            Food.UpdateSpawner();

            foreach (var entity in _entities)
            {
                entity.Update();
            }

            _isUpdating = false;

            foreach (var entity in _addedEntities)
            {
                AddEntity(entity);
            }

            _addedEntities.Clear();

            _entities = _entities.Where(entity => !entity.IsExpired).ToList();
            _foods = _foods.Where(entity => !entity.IsExpired).ToList();
            _ants = _ants.Where(entity => !entity.IsExpired).ToList();
        }

        public static void HandleCollisions()
        {
            // Insect on insect collision
            for (int i = 0; i < _ants.Count; i++)
            {
                for (int j = i + 1; j < _ants.Count; j++)
                {
                    // If the sprites are colliding, bump them away from each other
                    if (Raylib.CheckCollisionCircles(_ants[i].Position, _ants[i].Radius, _ants[j].Position, _ants[j].Radius))
                    {
                        // Calculate the vector between the two sprites
                        Vector2 collisionVector = _ants[j].Position - _ants[i].Position;
                        collisionVector = Vector2.Normalize(collisionVector);

                        // Move the sprites apart along the collision vector
                        _ants[i].Position -= collisionVector * 2f;
                        _ants[j].Position += collisionVector * 2f;

                    }
                }
            }

            // Insect on player collision
            for (int i = 0; i < _ants.Count; i++)
            {
                // If the sprites are colliding, bump them away from each other
                if (Raylib.CheckCollisionCircles(_ants[i].Position, _ants[i].Radius, AntQueen.Instance.Position, AntQueen.Instance.Radius))
                {
                    // Calculate the vector between the two sprites
                    Vector2 collisionVector = AntQueen.Instance.Position - _ants[i].Position;
                    collisionVector = Vector2.Normalize(collisionVector);

                    // Move the sprites apart along the collision vector
                    _ants[i].Position -= collisionVector * 2f;
                    // Don't move the Queen Ant, she is larger mass
                    //AntQueen.Instance.Position += collisionVector * 2f;
                }
            }

            // Inset on food collision
            for (int i = 0; i < _ants.Count; i++)
            {
                for (int j = 0; j < _foods.Count; j++)
                {
                    if (Raylib.CheckCollisionCircles(_foods[j].Position, _foods[j].Radius, _ants[i].Position, _ants[i].SeekRange) && !_ants[i].SeekingFood)
                    {
                        if (WorldMap.TryGetNode(_foods[j].PixelOrigin, out Node? targetNode))
                        {
                            _ants[i].SeekingFood = true;
                            _ants[i].SetState(new PathfindingState(_ants[i], targetNode));
                            continue;
                        }
                    }

                    if (Raylib.CheckCollisionCircles(_foods[j].Position, _foods[j].Radius, _ants[i].Position, _ants[i].Radius) && _ants[i].SeekingFood)
                    {
                        // Once an ant reaches food, clear all food seeking ants so they're eligible to seek food again.
                        var hungryAnts = _ants.Where(a => a.SeekingFood);
                        foreach (var hungryAnt in hungryAnts)
                        {
                            hungryAnt.SeekingFood = false;
                            hungryAnt.SetState(new IdleState());
                        }

                        _foods[j].Eaten();
                    }
                }
            }

            // Player on food collision
            for (int i = 0; i < _foods.Count; i++)
            {
                if (Raylib.CheckCollisionCircles(_foods[i].Position, _foods[i].Radius, AntQueen.Instance.Position, AntQueen.Instance.Radius))
                {
                    _foods[i].Eaten();
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
