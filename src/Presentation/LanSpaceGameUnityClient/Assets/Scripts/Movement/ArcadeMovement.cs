using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Movement
{
    // С обратным ходом
    public class ArcadeMovement : NetworkBehaviour
    {
        //public float acceleration = 8f;
        public float movementSpeed = 2.5f;
        public float rotateSpeed = 30f;

        void Start()
        {
            Debug.Log(GetType().ToString());
        }

        void Update()
        {
            if (!isLocalPlayer) return;

            if (Input.GetKey(KeyCode.W))
            {
                transform.position -= transform.up * Time.deltaTime * movementSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position += transform.up * Time.deltaTime * movementSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, 0, Time.deltaTime * movementSpeed * -rotateSpeed);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, 0, Time.deltaTime * movementSpeed * rotateSpeed);
            }

            // center camera..
            Vector3 pos = transform.position;
            pos.z = -10;
            Camera.main.transform.position = pos;
        }

    }
}
