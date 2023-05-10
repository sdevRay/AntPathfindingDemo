using Raylib_cs;

namespace ConsoleApp1.States
{
    internal struct Timer
    {
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

        public bool IsComplete()
        {
            return _lifeTime <= 0;
        }
    }
}
