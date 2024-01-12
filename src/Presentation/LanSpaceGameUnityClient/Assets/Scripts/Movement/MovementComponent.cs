using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Movement
{
    // PositionComponent для хранения вектора позиции

    // для вектора движения
    public class MovementComponent : NetworkBehaviour
    {
        public float ForwardSpeed;
        public float BackSpeed;
        public float RotateSpeed;

        public readonly float Z = -10f;

        public Transform Transform;
        public Rigidbody2D Rigidbody2D;

        private Action<MovementComponent> _handleUpdate;

        void Awake()
        {
            Transform = transform;
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            _handleUpdate = (o) => { MovementSystem.Singleton.Handle(o); };

            Debug.Log("MovementComponent.Start, NetworkBehaviour.isServer: " + isServer);
            Debug.Log("MovementComponent.Start, NetworkBehaviour.isClient: " + isClient);
            Debug.Log("MovementComponent.Start, NetworkBehaviour.isLocalPlayer: " + isLocalPlayer);
            Debug.Log("MovementComponent.Start, NetworkBehaviour.hasAuthority: " + hasAuthority);
            Debug.Log("MovementComponent.Start, NetworkBehaviour.netId: " + netId);
            Debug.Log("MovementComponent.Start, NetworkBehaviour.playerControllerId: " + playerControllerId);
            
            //MovementSystem.Singleton.AddMovementComponent(this);

            Scripts.Game.GameWorldHandler.Singleton.Components.Add(gameObject);
        }

        //private void Update()
        //{
        //    if (_handleUpdate != null)
        //        _handleUpdate.Invoke(this);
        //}

        [Client]
        public void RpcUpdateTransform(Vector3 newPosition)
        {
            Debug.Log("MovementComponent.RpcUpdateTransform, NetworkBehaviour.isClient: " + isClient);
            Debug.Log("MovementComponent.RpcUpdateTransform, NetworkBehaviour.isLocalPlayer: " + isLocalPlayer);
            Debug.Log("MovementComponent.RpcUpdateTransform, NetworkBehaviour.isServer: " + isServer);
            Debug.Log("MovementComponent.RpcUpdateTransform, NetworkBehaviour.netId: " + netId);
            Debug.Log("MovementComponent.RpcUpdateTransform, NetworkBehaviour.playerControllerId: " + playerControllerId);

            Transform.position = newPosition;
        }

        [Client]
        public void RpcUpdateRotate(float x, float y, float z)
        {
            Debug.Log("MovementComponent.RpcUpdateRotate, NetworkBehaviour.isClient: " + isClient);
            Debug.Log("MovementComponent.RpcUpdateRotate, NetworkBehaviour.isLocalPlayer: " + isLocalPlayer);
            Debug.Log("MovementComponent.RpcUpdateRotate, NetworkBehaviour.isServer: " + isServer);
            Debug.Log("MovementComponent.RpcUpdateRotate, NetworkBehaviour.netId: " + netId);
            Debug.Log("MovementComponent.RpcUpdateRotate, NetworkBehaviour.playerControllerId: " + playerControllerId);

            Transform.Rotate(x, y, z);
        }

    }
}
