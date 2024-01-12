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

        public void Auth(NetworkIdentity clientId)
        {
            if (isServer)
            {
                Debug.Log("isServer = true");
                CmdAssignNetworkAuthority(GetComponent<NetworkIdentity>(), clientId);
            }
            else
            {
                Debug.Log("isServer = false");
            }
        }

        [Command]
        public void CmdAssignNetworkAuthority(NetworkIdentity cubeId, NetworkIdentity clientId)
        {
            //If -> cube has a owner && owner isn't the actual owner
            if (cubeId.clientAuthorityOwner != null && cubeId.clientAuthorityOwner != clientId.connectionToClient)
            {
                Debug.Log("RemoveClientAuthority");
                // Remove authority
                cubeId.RemoveClientAuthority(cubeId.clientAuthorityOwner);
            }

            //If -> cube has no owner
            if (cubeId.clientAuthorityOwner == null)
            {
                Debug.Log("AssignClientAuthority " + clientId.connectionToClient);
                // Add client as owner
                cubeId.AssignClientAuthority(clientId.connectionToClient);
            }
        }

        /// <summary>
        /// Вызывается клиентом (Client), исполняется на сервере (Host).
        /// </summary>
        [Command]
        public void CmdLaunchProjectile(GameObject authGO)
        {
            Debug.Log("CmdLaunchProjectile".ToUpper());

            // get the object's network ID
            var networkIdentity = authGO.GetComponent<NetworkIdentity>();
            Debug.Log(networkIdentity.connectionToClient);

            gameObject.GetComponent<NetworkIdentity>()
                .AssignClientAuthority(networkIdentity.connectionToClient);
            Debug.Log(connectionToClient);

            Destroy(gameObject, Lifetime);

            // Warning: Trying to send command for object without authority.
            NetworkServer.SpawnWithClientAuthority(gameObject, connectionToClient);
        }

        /// <summary>
        /// Вызывается клиентом (Client), исполняется на сервере (Host).
        /// </summary>
        [Command]
        public void CmdLaunchProjectile2()
        {
            Debug.Log("CmdLaunchProjectile".ToUpper());
            Destroy(gameObject, Lifetime);

            // Warning: Trying to send command for object without authority.
            NetworkServer.Spawn(gameObject);
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
            // Warning: ClientRpc call on un-spawned object
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
