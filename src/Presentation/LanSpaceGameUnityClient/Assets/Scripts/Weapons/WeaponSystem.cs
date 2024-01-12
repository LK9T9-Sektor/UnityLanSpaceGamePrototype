using Assets.Scripts.Game;
using Assets.Scripts.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Weapons
{
    public class WeaponSystem : IGameSystem
    {
        private readonly string _className = typeof(WeaponSystem).Name;

        public void Run()
        {
            //Debug.Log(_className + " | Run");
            if (GameWorldHandler.Singleton.Components.Weapons.Count < 1) return;

            foreach (var component in GameWorldHandler.Singleton.Components.Weapons)
            {
                //Debug.Log(_className + " | Run");

                // isLocalPlayer will be true if the NetworkPlayer instance is the instance representing
                // the local machine in the game. Only one NetworkPlayer instance will have
                // an isLocalPlayer value of true in a running instance of your game. 
                // Think of this variable as the "me" flag: think "this object is me".
                if (component.isLocalPlayer)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Debug.Log(_className + " | Run | KeyDown.Mouse0");

                        // Рабочий вариант!!!
                        //component.CmdSpawnProjectile();

                        //var projectileBehaviour = ProjectileSystem.Singleton.GetProjectile(component.gameObject, component.ProjectilePrefab);
                        //var projectile = ProjectileSystem.Singleton.GetProjectileGO(component.gameObject, component.ProjectilePrefab);
                        //projectile.GetComponent<ProjectileBehaviour>()
                        //    .Auth(component.gameObject.GetComponent<NetworkIdentity>());

                        component.CmdFire(component.gameObject);

                        //CmdSpawn(
                        //    component.gameObject,
                        //    component.ProjectilePrefab);

                        //projectile.GetComponent<ProjectileBehaviour>()
                        //    .CmdLaunchProjectile(component.gameObject);

                        //var networkConnection = component.gameObject
                        //    .GetComponent<NetworkIdentity>().connectionToClient;
                        //Debug.Log(_className + " | NetworkConnection | " + networkConnection);

                        //var networkIdentity = projectile.GetComponent<NetworkIdentity>();
                        //Debug.Log(_className + " | networkIdentity | " + networkIdentity);

                        //var assigned = networkIdentity.AssignClientAuthority(networkConnection);
                        //Debug.Log(_className + " | assigned | " + assigned);

                        //projectile.GetComponent<ProjectileBehaviour>()
                        //    .RpcLaunchProjectile();

                        //projectileBehaviour.GetComponent<NetworkIdentity>()
                        //    .AssignClientAuthority(component
                        //    .GetComponent<NetworkIdentity>().connectionToClient);

                        //NetworkServer.Spawn(projectileBehaviour);
                        //NetworkIdentity.AssignClientAuthority();
                        //NetworkServer.SpawnWithClientAuthority(projectileBehaviour, projectileLauncher);

                        //if (projectileBehaviour != null)
                        //{
                        //    component.CmdLaunchProjectile(projectileBehaviour.gameObject);
                        //projectileBehaviour.CmdLaunchProjectile();
                        //    SpaceManager.Reset();
                        //}
                        // TODO: Call [Command] on a server to Instantiate and Spawn
                    }
                }

            }
        }

    }
}
