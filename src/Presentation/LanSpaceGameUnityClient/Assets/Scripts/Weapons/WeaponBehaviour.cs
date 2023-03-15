using Assets.Scripts.Game;
using Assets.Scripts.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Weapons
{
    public class WeaponBehaviour : NetworkBehaviour
    {
        //public WeaponData WeaponData;
        public string Name;
        public int Damage;
        public float ReloadTime;

        public GameObject ProjectilePrefab;

        private void Start()
        {
            Debug.Log("WeaponBehaviour | Start");
            GameWorldHandler.Singleton.Components.Add(gameObject);
        }

        /// <summary>
        /// Вызывается клиентом (Client), исполняется на сервере (Host).
        /// </summary>
        [Command]
        public void CmdSpawnProjectile()
        {
            Debug.Log("CmdSpawnProjectile".ToUpper());

            GameObject projectile = (GameObject)Instantiate(
                ProjectilePrefab,
                //new Vector3(projectileLauncher.transform.position.x, projectileLauncher.transform.position.y, 0),
                //projectileLauncher.transform.rotation);
                gameObject.transform.position + gameObject.transform.up,
                Quaternion.Euler(0, 0, gameObject.GetComponent<Rigidbody2D>().rotation));

            var projectileBehaviour = projectile.GetComponent<ProjectileBehaviour>();
            var projectileRigidBody2D = projectile.GetComponent<Rigidbody2D>();
            projectileRigidBody2D.velocity = gameObject.transform.up * projectileBehaviour.Speed;

            //NetworkServer.SpawnWithClientAuthority(projectile, connectionToClient);
            NetworkServer.Spawn(projectile);
        }

        [Command]
        public void CmdLaunchProjectile(GameObject go)
        {
            Debug.Log("CmdLaunchProjectile".ToUpper());

            //NetworkIdentity.AssignClientAuthority(connectionToClient);
            Destroy(go, 2.5f);

            NetworkServer.Spawn(go);
            //NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
        }

    }
}
