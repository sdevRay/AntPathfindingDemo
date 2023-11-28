using AntPathfindingDemo.Entities;

namespace AntPathfindingDemo.States
{
    public interface IState
    {
        public void HandleAction(Entity entity, Actions action);
        public void Update(Entity entity);
    }
}
