using Assets.Scripts.Game;
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
        public GameObject Prefab;

        public bool IsLocalPlayer { get; private set; }

        private void Start()
        {
            IsLocalPlayer = isLocalPlayer;
            Debug.Log("WeaponBehaviour | Start");
            GameWorldHandler.Singleton.Components.Add(gameObject);
        }

        [Command]
        public void CmdLaunchProjectile(GameObject gameObject)
        {
            Debug.Log("CmdLaunchProjectile".ToUpper());
            Destroy(gameObject, 2.5f);

            NetworkServer.Spawn(gameObject);
        }

    }
}
