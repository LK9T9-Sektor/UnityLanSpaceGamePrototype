using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Movement
{
    public class TopDown2DMovement : NetworkBehaviour
    {
        public float speed = 0.003f;
        public Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            if (!isLocalPlayer) return;

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * speed * Time.deltaTime;

            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.right * -speed * Time.deltaTime;

            }

            else if (Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.up * speed * Time.deltaTime;

            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.up * -speed * Time.deltaTime;

            }
        }

    }
}
