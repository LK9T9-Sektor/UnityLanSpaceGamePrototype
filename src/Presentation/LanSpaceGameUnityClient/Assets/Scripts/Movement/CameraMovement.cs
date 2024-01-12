using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class CameraMovement : MonoBehaviour
    {
        public float CameraSpeed = 0.3f;

        private void Update()
        {
            if (Input.GetKey("w"))
            {
                MoveUp();
            }
            if (Input.GetKey("s"))
            {
                MoveDown();
            }
            if (Input.GetKey("a"))
            {
                MoveLeft();
            }
            if (Input.GetKey("d"))
            {
                MoveRigh();
            }
        }

        private void MoveLeft()
        {
            transform.Translate(Vector3.left * CameraSpeed);
        }

        private void MoveRigh()
        {
            transform.Translate(Vector3.right * CameraSpeed);
        }

        private void MoveUp()
        {
            transform.Translate(Vector3.up * CameraSpeed);
        }

        private void MoveDown()
        {
            transform.Translate(Vector3.down * CameraSpeed);
        }

    }

}
