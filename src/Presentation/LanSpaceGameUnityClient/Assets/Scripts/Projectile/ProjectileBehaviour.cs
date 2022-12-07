using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Projectile
{
    public class ProjectileBehaviour : NetworkBehaviour
    {
        public string Name;
        public float Speed;
        public int Damage;
        public float Lifetime;
        //public GameObject Prefab;

        public void SetDamage(int damage)
        {
            Damage = damage;
        }

        [Command]
        public void CmdLaunchProjectile(GameObject gameObject)
        {
            Debug.Log("CmdLaunchProjectile".ToUpper());
            Destroy(gameObject, Lifetime);

            NetworkServer.Spawn(gameObject);
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            //var combat = collider.GetComponent<Combat>();
            //if (combat != null)
            //{
            //	combat.TakeDamage(damage);
            //}
            //Destroy(gameObject);		
        }

    }
}
