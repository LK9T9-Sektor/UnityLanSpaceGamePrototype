using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Units.SlimeAlien
{
    // on a server
    public class SlimeAlienSpawner : NetworkBehaviour
    {
        public GameObject alien;
        public int numAliens = 10;
        public int spread = 25;

        public override void OnStartServer()
        {
            Debug.Log("SlimeAlienSpawner");

            for (int i = 0; i < numAliens; i++)
            {
                Vector2 pos = new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread));
                NetworkServer.Spawn((GameObject)Instantiate(alien, pos, Quaternion.identity));
            }
        }

    }
}