using Assets.Scripts.Game;
using UnityEngine;

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

                        //var projectileBehaviour = ProjectileSystem.Singleton.GetProjectile(component.gameObject, component.ProjectilePrefab);

                        //projectileBehaviour.GetComponent<NetworkIdentity>()
                        //    .AssignClientAuthority(component
                        //    .GetComponent<NetworkIdentity>().connectionToClient);

                        component.CmdSpawnProjectile();
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
