using Raylib_cs;

namespace AntPathfindingDemo.States
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

    internal struct CountdownTimer
    {
        private float _lifeTime;
        private float _backingLifeTime { get; }

        public CountdownTimer(float lifeTime)
        {
            _lifeTime = lifeTime;
            _backingLifeTime = lifeTime;
        }

        public void Update()
        {
            if (_lifeTime > 0)
            {
                _lifeTime -= Raylib.GetFrameTime();
            }
        }

        public void Reset()
        {
            _lifeTime = _backingLifeTime;
        }

        public bool IsComplete()
        {
            return _lifeTime <= 0;
        }
    }
}

