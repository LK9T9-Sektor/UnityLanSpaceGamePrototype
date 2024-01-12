using Assets.Scripts.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Spawn
{
    public class TestSpawnSystem : NetworkBehaviour
    {
        public static TestSpawnSystem Singleton;

        private void Awake()
        {
            if (Singleton == null)
            {
                Singleton = this;
            }
        }

        public void Auth(            
            GameObject fromGO,
            GameObject prefabForSpawn)
        {
            //if (isServer)
            //{
            //    Debug.Log("isServer = true");
                CmdSpawnWithNetworkAuthority(
                    fromGO,
                    prefabForSpawn);
            //}
            //else
            //{
            //    Debug.Log("isServer = false");
            //}
        }

        [Command]
        public void CmdSpawnWithNetworkAuthority(
            GameObject fromGO,
            GameObject prefabForSpawn)
        {
            //If -> cube has a owner && owner isn't the actual owner

            GameObject projectile = (GameObject)Instantiate(
                prefabForSpawn,
                fromGO.transform.position + fromGO.transform.up,
                Quaternion.Euler(0, 0, fromGO.GetComponent<Rigidbody2D>().rotation));

            var projectileBehaviour = projectile.GetComponent<ProjectileBehaviour>();
            var projectileRigidBody2D = projectile.GetComponent<Rigidbody2D>();
            projectileRigidBody2D.velocity = fromGO.transform.up * projectileBehaviour.Speed;

            //var fromNetIdent = fromGO.GetComponent<NetworkIdentity>();
            //var projNetIdent = projectile.GetComponent<NetworkIdentity>();

            //if (projNetIdent.clientAuthorityOwner != null 
            //    && projNetIdent.clientAuthorityOwner != fromNetIdent.connectionToClient)
            //{
            //    Debug.Log("RemoveClientAuthority");
                // Remove authority
            //    projNetIdent.RemoveClientAuthority(projNetIdent.clientAuthorityOwner);
            //}

            NetworkServer.Spawn(projectile);
            //NetworkServer.SpawnWithClientAuthority(projectile, fromNetIdent.connectionToClient);

            //If -> cube has no owner
            //if (projNetIdent.clientAuthorityOwner == null)
            //{
            //    Debug.Log("AssignClientAuthority " + fromNetIdent.connectionToClient);
                // Add client as owner
            //    projNetIdent.AssignClientAuthority(fromNetIdent.connectionToClient);
            //}

        }

    }
}
