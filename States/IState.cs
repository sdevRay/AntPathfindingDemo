using ConsoleApp1.Entities;

namespace ConsoleApp1.States
{
    public interface IState
    {
        public void HandleAction(Entity entity, Actions action);
        public void Update(Entity entity);
    }
}
