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

        private Transform _transform;

        void Start()
        {
            _transform = transform;

            Debug.Log(GetType().ToString());
        }

        void Update()
        {
            if (!isLocalPlayer) return;

            if (Input.GetKey(KeyCode.W))
            {
                _transform.position += _transform.up * Time.deltaTime * movementSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _transform.position -= _transform.up * Time.deltaTime * movementSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _transform.Rotate(0, 0, Time.deltaTime * movementSpeed * rotateSpeed);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _transform.Rotate(0, 0, Time.deltaTime * movementSpeed * -rotateSpeed);
            }

            // center camera..
            Vector3 pos = _transform.position;
            pos.z = -10;
            Camera.main.transform.position = pos;
        }

    }
}
