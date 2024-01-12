using UnityEngine.Networking;

namespace Assets.Scripts.Session
{
    public class SessionSystem : NetworkBehaviour
    {
        public static SessionSystem Singleton;

        public static LevelData LevelData = new LevelData();

        private void Awake()
        {
            Singleton = this;
        }

    }
}
