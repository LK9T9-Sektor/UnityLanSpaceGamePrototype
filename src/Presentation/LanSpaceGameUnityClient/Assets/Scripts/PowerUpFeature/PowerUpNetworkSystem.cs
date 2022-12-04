using Assets.Scripts.Game;
using Assets.Scripts.Rotation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Scripts.PowerUpFeature
{
    public class PowerUpNetworkSystem : NetworkBehaviour
    {
        [SerializeField] private GameObject _powerUp;

        private readonly Dictionary<string, Color> _powerUps = new Dictionary<string, Color>()
        {
            { "Speed", Color.red },
            { "Rotate", Color.blue },
            { "Triple", Color.cyan },
            { "Double", Color.yellow },
            { "Health", Color.green },
            { "Energy", Color.magenta },
            { "QuadDamage", new Color(1, 0.5f, 0) },
            { "Bounce", new Color(0, 1, 0.5f) },
        };

        //[SyncVar]
        //public PowerUpData PowerUp;

        //[SyncVar]
        //public int testMe;

        public override void OnStartClient()
        {
            //float dir = 170.0f;
            //transform.rotation = Quaternion.Euler(0, 180, dir);
            //GetComponent<Rigidbody2D>().angularVelocity = dir;

            //Color c = Buf.bufColors[(int)mbuf];
            //GetComponent<Renderer>().material.color = c;

            //if (!isServer)
            //{
            //    numPowerups += 1;
            //}
        }

        public override void OnStartServer()
        {
            Spawn();
        }

        public int PowerUpsSpawnNumber = 15;
        public int spread = 25;
        private void Spawn()
        {
            for (int i = 0; i < PowerUpsSpawnNumber; i++)
            {
                var randomNumber = Random.Range(0, _powerUps.Count);

                var name = _powerUps.Keys.ElementAt(randomNumber);
                var color = _powerUps.Values.ElementAt(randomNumber);

                Vector2 pos = new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread));
                GameObject powerUp = (GameObject)Instantiate(_powerUp, pos, Quaternion.identity);
                _objectsPool.Add(powerUp);

                GameWorldHandler.Singleton.Components.Add(powerUp);

                var powerUpComponent = powerUp.GetComponent<PowerUpData>();
                powerUpComponent.Name = name;
                powerUpComponent.Color = color;
                powerUpComponent.PowerUpNetworkSystem = this;

                NetworkServer.Spawn(powerUp);
            }
        }

        private readonly List<GameObject> _objectsPool = new List<GameObject>();


        void OnGUI()
        {
            //GUI.color = Buf.bufColors[(int)mbuf];
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            //GUI.Label(new Rect(pos.x - 20, Screen.height - pos.y - 30, 100, 30), mbuf.ToString());
        }

        public override void OnNetworkDestroy()
        {
            //AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
        }

        public void OnEnterTrigger(Collider2D collided, GameObject powerUp)
        {
            if (!collided.CompareTag("Player")) { return; }

            if (!NetworkServer.active) { return; }

            //ShipControl s = other.gameObject.GetComponent<ShipControl>();
            //if (s != null)
            //{
            //s.AddBuf(mbuf);
            //Destroy(gameObject);
            //}

            var textComponent = collided.GetComponent<Text>();
            if (textComponent != null)
            {
                textComponent.color = powerUp.GetComponent<PowerUpData>().Color;
            }

            var audioSource = powerUp.GetComponent<AudioSource>().clip;
            AudioSource.PlayClipAtPoint(audioSource, powerUp.transform.position);

            // Прячем powerUp у клиентов
            powerUp.GetComponent<PowerUpData>().RpcSetActive(false);
        }

        void OnDestroy()
        {
            //numPowerups -= 1;
        }

    }

}
