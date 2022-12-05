using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Lifecycle
{
    public class LifecycleProcessor : NetworkBehaviour
    {
        #region Disgusting Singleton

        private LifecycleProcessor() { }

        private static readonly LifecycleProcessor _instance = new LifecycleProcessor();

        public static LifecycleProcessor Singleton { get { return _instance; } }

        #endregion


        // TODO: Looks like an extension method
        public void CreateTempPrefab(GameObject prefab,
                                     Vector3 position,
                                     Quaternion rotation,
                                     float lifeDuration)
        {
            GameObject exp = (GameObject)Instantiate(prefab, position, rotation);
            Destroy(exp, lifeDuration);
        }

    }
}
