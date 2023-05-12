using Raylib_cs;

namespace ConsoleApp1.States
{

    internal struct Timer
    {
        // usage
        //if (!_timer.IsComplete())
        //{
        //// Code here
        //_timer.Update();
        //}
        //else
        //{
        //_timer = new Timer((float) Raylib.GetRandomValue(0, 3));
        //}

        private float _lifeTime;

        public Timer(float lifeTime)
        {
            _lifeTime = lifeTime;
        }

        public void Update()
        {
            if (_lifeTime > 0)
            {
                _lifeTime -= Raylib.GetFrameTime();
            }
        }

        public float GetTimeLeft()
        {
            return _lifeTime;
        }

        public bool IsComplete()
        {
            return _lifeTime <= 0;
        }
    }
}

