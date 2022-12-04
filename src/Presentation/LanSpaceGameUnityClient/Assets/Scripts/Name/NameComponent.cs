using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Scripts.Name
{
    public class NameComponent : NetworkBehaviour
    {
        [SyncVar] public string Name;

        private void Start()
        {
            var textComp = gameObject.transform.GetComponent<Canvas>();
            Debug.Log("NAME_COMPONENT: " + textComp);
            if (textComp != null)
            {
                //textComp.text = Name;
            }
        }

    }
}
