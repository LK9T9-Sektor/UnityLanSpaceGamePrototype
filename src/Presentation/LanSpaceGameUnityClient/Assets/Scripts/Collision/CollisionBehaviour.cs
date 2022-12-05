using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Collision
{
    public class CollisionBehaviour : NetworkBehaviour
    {
        public float PushPower = 0;
        public AudioClip PushAudio;

        public int Damage = 0;
        public int SelfDamage = 0;

        void OnCollisionEnter2D(Collision2D collision)
        {
            Collision2DProcessor.Singleton.Handle(collision, gameObject);
        }

    }
}
