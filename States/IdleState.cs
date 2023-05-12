using ConsoleApp1.Entities;
using Raylib_cs;
using System.Numerics;

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
            _timer = new Timer((float)Raylib.GetRandomValue(10, 10));
        }

        public void Update(Entity entity)
        {
           if(entity is Insect insect)
            {
                // TODO
                // Check for food or other things to set the seeker on first, if there ins't anything set this random point.
                // May need to abstract the functions that analyze the surroundings.

                var radius = 300;
                // Generate random point within radius
                Vector2 randomPoint = new Vector2(
                Raylib.GetRandomValue((int)insect.Position.X - radius, (int)insect.Position.X + radius),
                Raylib.GetRandomValue((int)insect.Position.Y - radius, (int)insect.Position.Y + radius));


                // Will probably change this once we get a working camera.
                //randomPoint = Vector2.Clamp(
                //    randomPoint, insect.TextureSize / 2,
                //    new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()) - insect.TextureSize / 2
                //    );

                insect.SetState(new SeekState(randomPoint));
            }
        }
    }
}
