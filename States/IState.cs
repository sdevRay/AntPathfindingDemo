using ConsoleApp1.Entities;

namespace ConsoleApp1.States
{
    internal interface IState
    {
        void HandleAction(Entity entity, Actions action);
        void Update(Entity entity);
    }
}
