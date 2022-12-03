using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Units.SlimeAlien
{
    public class SlimeAlienUnit : NetworkBehaviour
    {
        public int Health;
        public GameObject PlayerTarget;

        public SlimeAlienNetworkSystem SlimeAlienNetworkSystem;

        void OnCollisionEnter2D(Collision2D collider)
        {
            if (SlimeAlienNetworkSystem == null)
            {
                return;
            }
            else
            {
                SlimeAlienNetworkSystem.OnCollision(collider, gameObject);
            }
        }

        void OnDestroy()
        {
            GameManager.singleton.score += 1;
        }

    }
}