using ConsoleApp1.Entities;
using Raylib_cs;

namespace ConsoleApp1.States
{
    internal class IdleState : IState
    {
        private Timer _timer;

        public void HandleAction(Entity entity, Actions action)
        {
            //if (input == RELEASE_DOWN)
            //{
            //    // Change to standing state...
            //    heroine.setGraphics(IMAGE_STAND);
            //}
        }

        public IdleState() 
        {
            _timer = new Timer((float)Raylib.GetRandomValue(1, 2));
        }

        public void Update(Entity entity)
        {
           if(entity is Insect insect)
            {
                if (!_timer.IsComplete())
                {
                    insect.Rotation = (float)Raylib.GetTime() * 90;
                    _timer.Update();
                }
                else
                {
                    insect.SetState(new ExploreState());
                }

                Raylib.DrawText(insect.Rotation.ToString(), (int)(insect.Position.X + insect.DestRec.height), (int)(insect.Position.Y + insect.DestRec.height), 20, Color.BLACK);
            }
        }
    }
}
