using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Spawn
{
    // https://docs.unity3d.com/2020.3/Documentation/Manual/UNetCustomSpawning.html
    public class SpawnFromPoolSystem : MonoBehaviour
    {
        public static SpawnFromPoolSystem Singleton;

        private void Awake()
        {
            if (Singleton == null)
            {
                Singleton = this;
            }
        }

        public int ObjectPoolSize = 5;
        public GameObject Prefab;
        public GameObject[] Pool;

        public NetworkHash128 AssetId { get; set; }

        public delegate GameObject SpawnDelegate(Vector3 position, NetworkHash128 assetId);
        public delegate void UnSpawnDelegate(GameObject spawned);

        public void Start()
        {
            AssetId = Prefab.GetComponent<NetworkIdentity>().assetId;
            Pool = new GameObject[ObjectPoolSize];
            for (int i = 0; i < ObjectPoolSize; ++i)
            {
                Pool[i] = (GameObject)Instantiate(Prefab, Vector3.zero, Quaternion.identity);
                Pool[i].name = "PoolObject" + i;
                Pool[i].SetActive(false);
            }

            ClientScene.RegisterSpawnHandler(AssetId, SpawnObject, UnSpawnObject);
        }

        public GameObject GetFromPool(Vector3 position)
        {
            foreach (var obj in Pool)
            {
                if (!obj.activeInHierarchy)
                {
                    Debug.Log("Activating GameObject " + obj.name + " at " + position);

                    obj.transform.position = position;
                    obj.SetActive(true);

                    return obj;
                }
            }
            Debug.LogError("Could not grab GameObject from Pool, nothing available");
            return null;
        }

        public GameObject SpawnObject(Vector3 position, NetworkHash128 assetId)
        {
            return GetFromPool(position);
        }

        public void UnSpawnObject(GameObject spawned)
        {
            Debug.Log("Re-pooling GameObject " + spawned.name);
            spawned.SetActive(false);
        }

    }
}
