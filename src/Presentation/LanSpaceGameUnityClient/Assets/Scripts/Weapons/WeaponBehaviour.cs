using Assets.Scripts.Game;
using Assets.Scripts.Projectile;
using Assets.Scripts.Spawn;
using System.Collections;
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

        public SpawnFromPoolSystem SpawnFromPoolSystem;

        private void Start()
        {
            Debug.Log("WeaponBehaviour | Start");

            //SpawnFromPoolSystem = GameObject.Find("SpawnFromPoolSystem")
            //    .GetComponent<SpawnFromPoolSystem>();
            //SpawnFromPoolSystem = gameObject.GetComponent<SpawnFromPoolSystem>();
            SpawnFromPoolSystem = SpawnFromPoolSystem.Singleton;

            GameWorldHandler.Singleton.Components.Add(gameObject);
        }

        [Command]
        public void CmdFire(GameObject spawner)
        {
            // Set up projectile on server
            var projectile = SpawnFromPoolSystem.GetFromPool(
                spawner.transform.position + spawner.transform.up);

            projectile.transform.rotation = Quaternion.Euler(0, 0, spawner.GetComponent<Rigidbody2D>().rotation);
            
            var projectileBehaviour = projectile.GetComponent<ProjectileBehaviour>();
            var projectileRigidBody2D = projectile.GetComponent<Rigidbody2D>();
            projectileRigidBody2D.velocity = gameObject.transform.up * projectileBehaviour.Speed;

            // spawn projectile on client, custom spawn handler is called
            NetworkServer.Spawn(projectile, SpawnFromPoolSystem.AssetId);

            // when the projectile is destroyed on the server, it is automatically destroyed on clients
            StartCoroutine(Destroy(projectile, 2.0f));
        }

        public IEnumerator Destroy(GameObject go, float timer)
        {
            yield return new WaitForSeconds(timer);
            SpawnFromPoolSystem.UnSpawnObject(go);
            NetworkServer.UnSpawn(go);
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

        /// <summary>
        /// Вызывается клиентом (Client), исполняется на сервере (Host).
        /// </summary>
        [Command]
        public void CmdSpawnProjectile2(GameObject go)
        {
            // Видно на хосте для хоста и клиентов, но не видно от клиентов
            Debug.Log("CmdSpawnProjectile2".ToUpper());

            NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
            //NetworkServer.Spawn(go);
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
