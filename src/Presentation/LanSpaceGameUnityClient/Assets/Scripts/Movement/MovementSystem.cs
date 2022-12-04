using Assets.Scripts.Game;

namespace Assets.Scripts.Movement
{
    public class MovementSystem : IGameSystem
    {
        public void Run()
        {
            if (GameWorldHandler.Singleton.Components.Movements.Count < 1) return;

            foreach (var movementComponent in GameWorldHandler.Singleton.Components.Movements)
            {

            }
        }

    }
}
