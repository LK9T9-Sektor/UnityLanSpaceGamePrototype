using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Rotation
{
    public class RotationComponent : NetworkBehaviour
    {
        [SyncVar] public float Speed;
        [SyncVar] public float Direction;
        //[SyncVar] 
        public Transform Transform;
        //[SyncVar] 
        public Rigidbody2D Rigidbody2D;

        private void Start()
        {
            RpcStartRotation();
        }

        /// <summary>
        /// Меняет видимость гейм-объекта с данным скриптом. Вызывается сервером, исполняется на клиентах
        /// </summary>
        /// <param name="active"></param>
        [ClientRpc]
        public void RpcStartRotation()
        {
            if (Transform == null)
                Transform = GetComponent<Transform>();

            if (Rigidbody2D == null)
                Rigidbody2D = GetComponent<Rigidbody2D>();

            Direction = 170.0f;
            Transform.rotation = Quaternion.Euler(0, 180, Direction);
            Rigidbody2D.angularVelocity = Direction;
        }

    }
}
