using Assets.Scripts.States;
using UnityEngine.Networking;

namespace Assets.Scripts.Game
{
    public class GameWorldHandler : NetworkBehaviour
    {
        public static GameWorldHandler Singleton;
        public ComponentsContainer Components = new ComponentsContainer();
        public SystemsContainer Systems = new SystemsContainer();

        private void Awake()
        {
            if (Singleton == null)
            {
                Singleton = this;
            }
        }

        private void Update()
        {
            foreach (var system in Systems.Systems)
            {
                system.Run();
            }
        }

    }
}
