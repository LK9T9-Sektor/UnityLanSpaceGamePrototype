using Assets.Scripts.Audio;
using Assets.Scripts.Health;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Lifecycle
{
    public class LifecycleBehaviour : NetworkBehaviour
    {
        public bool CanRespawn = false;

        // TODO: DRY, make class with fields
        // ActionPrefab
        // ActionSound
        public GameObject SpawnPrefab;
        public AudioClip SpawnAudio;

        public GameObject LifeGettingBetter;    // :D
        public AudioClip LifeGettingBetterAudio;

        public GameObject LifeGettingWorse;     // D:
        public AudioClip LifeGettingWorseAudio;

        public GameObject DeathPrefab;
        public AudioClip DeathAudio;

        /// <summary>
        /// Меняет видимость гейм-объекта с данным скриптом. Вызывается сервером, исполняется на клиентах
        /// </summary>
        /// <param name="active"></param>
        [ClientRpc]
        public void RpcSetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        [ClientRpc]
        public void RpcRespawn()
        {
            AudioSystem.Singleton.PlaySoundAtPosition(DeathAudio, gameObject.transform.position);

            if (DeathPrefab != null)
            {
                LifecycleProcessor.Singleton.CreateTempPrefab(DeathPrefab,
                    gameObject.transform.position,
                    gameObject.transform.rotation,
                    1.6f);
            }            

            if (isLocalPlayer)
            {
                gameObject.GetComponent<HealthBehaviour>().ResetHp();

                gameObject.transform.position = Vector3.zero;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            }
        }

        public override void OnNetworkDestroy()
        {
            LifecycleProcessor.Singleton.CreateTempPrefab(DeathPrefab,
                gameObject.transform.position, 
                gameObject.transform.rotation,
                3f);
        }

    }

}
