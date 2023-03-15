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
        //public GameObject ProjectilePrefab;

        public void SetDamage(int damage)
        {
            Damage = damage;
        }

        /// <summary>
        /// Вызывается клиентом (Client), исполняется на сервере (Host).
        /// </summary>
        [Command]
        public void CmdLaunchProjectile()
        {
            Debug.Log("CmdLaunchProjectile".ToUpper());
            Destroy(gameObject, Lifetime);

            NetworkServer.SpawnWithClientAuthority(gameObject, connectionToClient);
            //NetworkServer.Spawn(gameObject);
        }

        /// <summary>
        /// Вызывается клиентом (Client), исполняется на сервере (Host).
        /// </summary>
        [Command]
        public void CmdSpawnProjectile(
            GameObject projectilePrefab, 
            Vector3 position, 
            float rotation)
        {
            Debug.Log("CmdSpawnProjectile".ToUpper());

            GameObject projectile = (GameObject)Instantiate(
                projectilePrefab,
                position,
                Quaternion.Euler(0, 0, rotation));

            //NetworkServer.SpawnWithClientAuthority(projectile, connectionToClient);
            NetworkServer.Spawn(projectile);
        }


        /// <summary>
        /// Вызывается сервером, исполняется на клиентах.
        /// Не вызывать из клиента!
        /// </summary>
        /// <param name="active"></param>
        [ClientRpc]
        public void RpcLaunchProjectile()
        {
            Debug.Log("CmdLaunchProjectile".ToUpper());
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
