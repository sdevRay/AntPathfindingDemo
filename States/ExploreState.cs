using ConsoleApp1.Entities;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.States
{
    internal class ExploreState : IState
    {
        private Vector2 _randomDirection;
        private Timer _timer;

        public void HandleAction(Entity entity, Actions action)
        {
            throw new NotImplementedException();
        }

        public void Update(Entity entity)
        {
            if (entity is Insect insect)
            {
                Raylib.DrawText("_randomDirection: " + _randomDirection, 12, 12, 20, Color.BLACK);

                if (!_timer.IsComplete())
                {
                    // move this to the seek state, infact remove this state completely and just put the random
                    // point within proximity from the idlestate and send it right to the seekstate
                    foreach (var food in EntityManager.Foods)
                    {
                        var distance = Vector2.Distance(insect.Position, food.Position);

                        if (distance <= 300)
                        {
                            insect.SetState(new SeekState(food));
                        }
                    }

                    insect.Velocity = _randomDirection;
                    insect.Ortientate();
                    _timer.Update();
                }
                else
                {
                    // Pick a random point within proximity and then set the seek state
                    _timer = new Timer((float)Raylib.GetRandomValue(0, 3));
                    _randomDirection = new Vector2(Raylib.GetRandomValue(-1, 1), Raylib.GetRandomValue(-1, 1));

                    if (_randomDirection == Vector2.Zero)
                    {
                        insect.Velocity = _randomDirection;
                        insect.SetState(new IdleState());
                    }
                }
            }
        }
    }
}
