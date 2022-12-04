using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Units.SlimeAlien
{
    public class SlimeAlienNetworkSystem : NetworkBehaviour
    {
        private readonly string _className = typeof(SlimeAlienNetworkSystem).Name;

        [SerializeField] private GameObject _slimeAlienPrefab;

        private readonly Dictionary<GameObject, SlimeAlienUnit> _aliensPool = new Dictionary<GameObject, SlimeAlienUnit>();

        [SerializeField] private int _aliensNumber = 10;
        [SerializeField] private int _spawnSpread = 25;

        void FixedUpdate()
        {
            foreach (var alien in _aliensPool.Where(v => v.Key.activeSelf))
            {
                if (!isServer) return;

                //if (!alien.Key.activeInHierarchy) return;

                //Debug.Log(_className + " | FixedUpdate | " + alien.Key.GetInstanceID());

                if (alien.Value.PlayerTarget != null)
                {
                    Vector2 diff = (Vector2)(alien.Value.PlayerTarget.transform.position - alien.Value.transform.position).normalized;

                    alien.Key.GetComponent<Rigidbody2D>().AddForce(diff * Time.fixedDeltaTime * 20);
                }
            }
        }

        void Update()
        {
            foreach (var alien in _aliensPool.Where(v => v.Key.activeSelf))
            {
                if (!isServer) return;

                //if (!alien.Key.activeInHierarchy) return;

                //Debug.Log(_className + " | Update | " + alien.Key.GetInstanceID());

                if (Random.Range(0, 20) != 0)
                {
                    Vector2 v2 = new Vector2(Random.Range(-10, 11) * 0.1f, Random.Range(-10, 11) * 0.1f);
                    alien.Key.GetComponent<Rigidbody2D>().AddForce(v2 * 2);
                    alien.Key.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-10, 10) * 0.01f);
                }

                Collider2D[] colliders = Physics2D.OverlapCircleAll(alien.Key.transform.position, 20);
                foreach (var collider in colliders)
                {
                    var controls = collider.gameObject.GetComponent<Controls>();
                    if (controls != null)
                    {
                        alien.Value.PlayerTarget = collider.gameObject;
                        break;
                    }
                }
            }
        }

        public void OnCollision(Collision2D collided, GameObject slimeAlien)
        {
            var controls = collided.gameObject.GetComponent<Controls>();
            if (controls != null)
            {
                // hit a player
                var combat = collided.gameObject.GetComponent<Combat>();
                combat.TakeDamage(10);

                // Прячем моба у клиентов
                slimeAlien.GetComponent<Combat>().RpcSetActive(false);

                //slimeAlien.GetComponent<Combat>().TakeDamage(1000);
                var audioSource = slimeAlien.GetComponent<AudioSource>().clip;

                AudioSource.PlayClipAtPoint(audioSource, slimeAlien.transform.position);
            }
        }

        public override void OnStartServer()
        {
            Spawn();
        }

        private void Spawn()
        {
            Debug.Log(_className + " | Spawn");

            for (int i = 0; i < _aliensNumber; i++)
            {
                Vector2 position = new Vector2(Random.Range(-_spawnSpread, _spawnSpread),
                    Random.Range(-_spawnSpread, _spawnSpread));

                var gameObject = (GameObject)Instantiate(_slimeAlienPrefab, position, Quaternion.identity);
                var slimeAlien = gameObject.GetComponent<SlimeAlienUnit>();
                slimeAlien.SlimeAlienNetworkSystem = this;

                _aliensPool.Add(gameObject, slimeAlien);

                NetworkServer.Spawn(gameObject);
            }
        }

    }
}
