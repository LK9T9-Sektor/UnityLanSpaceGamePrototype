using UnityEngine;
using UnityEngine.Networking;

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

        // TODO: Remove
        void OnGUI()
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

            // draw the name with a shadow (colored for buf)	
            GUI.color = Color.black;
            GUI.Label(new Rect(pos.x - 20, Screen.height - pos.y - 40, 100, 30), Name);

            GUI.color = Color.white;
            GUI.Label(new Rect(pos.x - 21, Screen.height - pos.y - 41, 100, 30), Name);
        }

    }
}
