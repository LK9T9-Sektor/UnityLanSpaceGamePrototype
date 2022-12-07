using Assets.Scripts.Rotation;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Projectile
{
    public class ProjectileSystem : NetworkBehaviour
    {
        private readonly string _className = typeof(RotationSystem).Name;

        #region Disgusting Singleton

        private ProjectileSystem() { }

        private static readonly ProjectileSystem _instance = new ProjectileSystem();

        public static ProjectileSystem Singleton { get { return _instance; } }

        #endregion

        // Looks like an extension method
        public ProjectileBehaviour GetProjectile(GameObject projectileLauncher, GameObject projectilePrefab)
        {
            //if (!isLocalPlayer) return null;

            Debug.Log(_className + " | GetProjectile");

            GameObject projectile = (GameObject)Instantiate(
                projectilePrefab,
                //new Vector3(projectileLauncher.transform.position.x, projectileLauncher.transform.position.y, 0),
                //projectileLauncher.transform.rotation);
                projectileLauncher.transform.position + projectileLauncher.transform.up,
                Quaternion.Euler(0, 0, projectileLauncher.GetComponent<Rigidbody2D>().rotation));

            var projectileBehaviour = projectile.GetComponent<ProjectileBehaviour>();
            var projectileRigidBody2D = projectile.GetComponent<Rigidbody2D>();
            projectileRigidBody2D.velocity = projectileLauncher.transform.up * projectileBehaviour.Speed;

            return projectileBehaviour;
        }

        // Looks like an extension method
        public void CreateProjectile2(GameObject projectileOwner, GameObject projectilePrefab)
        {
            var direction = projectileOwner.transform.position;
            //TODO: add owner? need object references!
            var projectile = (GameObject)Instantiate(
                projectilePrefab,
                projectileOwner.transform.position + direction, 
                Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = projectileOwner.GetComponent<Rigidbody2D>().velocity;
            projectile.GetComponent<Rigidbody2D>().velocity += (Vector2)(direction) * 10;
            //projectile.GetComponent<Bullet>().Config(projectileLauncher, damage, bounce, bulletLifetime);

            //ClientScene.SpawnClientObject(projectile, 0);
            var projectileBehaviour = projectile.GetComponent<ProjectileBehaviour>();
            //projectileBehaviour.CmdLaunchProjectile();
        }

    }
}
