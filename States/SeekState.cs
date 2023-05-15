﻿using ConsoleApp1.Entities;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.States
{
    internal class SeekState : IState
    {
        private Vector2 _targetPosition;
        private readonly Entity? _target;
        private float _time = 0f;
        private CountDownTimer _timer = new CountDownTimer(1f);

        public SeekState(Entity target) : this(target.Position)
        {
            _target = target;
        }

        public SeekState(Vector2 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        public void HandleAction(Entity entity, Actions action)
        {
            //if (input == RELEASE_DOWN)
            //{
            //    // Change to standing state...
            //    heroine.setGraphics(IMAGE_STAND);
            //}
        }

        public void Update(Entity entity)
        {
            if (entity is Insect insect)
            {
                _time += Raylib.GetFrameTime();

                // Calculate direction towards target
                Vector2 direction = _targetPosition - insect.Position;

                // Normalize direction to get unit vector
                Vector2 normalizedDirection = Vector2.Normalize(direction);

                // Movement like a sinewave 
                normalizedDirection.Y += (float)Math.Sin(_time) * 0.2f;
                normalizedDirection.X += (float)Math.Cos(_time) * 0.2f;

                // Set velocity based on direction
                insect.Velocity = normalizedDirection;

                // Orientate towards direction of movement
                insect.Rotation = (float)Math.Atan2(insect.Velocity.Y, insect.Velocity.X);

                // Target reached
                if (Vector2.Distance(insect.Position, _targetPosition) < 10.0f)
                {
                    // check and handle collision
                    // if (entity is whatever)
                    // handle action, like if an insect collision with food, the eat action would increase health
                    // but if it collides with another ant the attack action will take health away
                    // this should interlock the two ants into combat

                    insect.SetState(new IdleState());
                }

                Raylib.DrawLineV(insect.Position, _targetPosition, Color.BLACK);
            }
        }
    }
}
