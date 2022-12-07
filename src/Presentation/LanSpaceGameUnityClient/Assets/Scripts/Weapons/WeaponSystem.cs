using Assets.Scripts.Game;
using Assets.Scripts.Projectile;
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
                if (!component.isLocalPlayer) { return; }
                //if (!component.IsLocalPlayer) { return; }
                //Debug.Log(_className + " | Run");

                //Debug.Log(_className + " | Run | NotLocal");
                // fire
                //if (Input.GetKeyDown(KeyCode.Space))
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Debug.Log(_className + " | Run | Mouse0 KeyDown");

                    var projectile = ProjectileSystem.Singleton.GetProjectile(component.gameObject, component.Prefab);
                    if (projectile != null)
                    {
                        component.CmdLaunchProjectile(projectile.gameObject);
                        SpaceManager.Reset();
                    }
                }
            }
        }

    }
}
