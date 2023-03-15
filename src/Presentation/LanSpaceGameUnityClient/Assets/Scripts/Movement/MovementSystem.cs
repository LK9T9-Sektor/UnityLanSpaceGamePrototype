using Assets.Scripts.Game;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class MovementSystem : IGameSystem
    {
        private readonly string _className = typeof(MovementSystem).Name;

        #region Disgusting Singleton

        //private MovementSystem() { }

        private static readonly MovementSystem _instance = new MovementSystem();

        public static MovementSystem Singleton { get { return _instance; } }

        #endregion

        private readonly List<MovementComponent> _movementComponents = new List<MovementComponent>();
        public ReadOnlyCollection<MovementComponent> Movements
        {
            get { return _movementComponents.AsReadOnly(); }
        }

        public void AddMovementComponent(MovementComponent component) 
        {
            Debug.Log("MovementSystem.AddMovementComponent, NetworkBehaviour.isClient: " + component.isClient);
            Debug.Log("MovementSystem.AddMovementComponent, NetworkBehaviour.isLocalPlayer: " + component.isLocalPlayer);
            Debug.Log("MovementSystem.AddMovementComponent, NetworkBehaviour.isServer: " + component.isServer);
            Debug.Log("MovementSystem.AddMovementComponent, NetworkBehaviour.netId: " + component.netId);
            Debug.Log("MovementSystem.AddMovementComponent, NetworkBehaviour.playerControllerId: " + component.playerControllerId);

            _movementComponents.Add(component);
        }

        public void Handle(MovementComponent movementComponent)
        {
            //Debug.Log(_className + " | Handle");

            //Debug.Log("Handle, NetworkBehaviour.isClient: " + movementComponent.isClient);
            //Debug.Log("Handle, NetworkBehaviour.isLocalPlayer: " + movementComponent.isLocalPlayer);
            //Debug.Log("Handle, NetworkBehaviour.isServer: " + movementComponent.isServer);
            //Debug.Log("Handle, NetworkBehaviour.netId: " + movementComponent.netId);
            //Debug.Log("Handle, NetworkBehaviour.playerControllerId: " + movementComponent.playerControllerId);

            // Если мы не сервер, не обрабатываем
            if (!movementComponent.isLocalPlayer) return;

            if (Input.GetKey(KeyCode.W))
            {
                movementComponent.Transform.position += movementComponent.Transform.up * Time.deltaTime * movementComponent.ForwardSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                movementComponent.Transform.position -= movementComponent.Transform.up * Time.deltaTime * movementComponent.ForwardSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movementComponent.Transform.Rotate(0, 0, Time.deltaTime * movementComponent.ForwardSpeed * movementComponent.RotateSpeed);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movementComponent.Transform.Rotate(0, 0, Time.deltaTime * movementComponent.ForwardSpeed * -movementComponent.RotateSpeed);
            }

            // center camera..
            Vector3 pos = movementComponent.Transform.position;
            pos.z = movementComponent.Z;
            Camera.main.transform.position = pos;
        }

        public void Run()
        {
            if (GameWorldHandler.Singleton.Components.Movements.Count < 1) return;

            foreach (var movementComponent in GameWorldHandler.Singleton.Components.Movements)
            //foreach (var movementComponent in _movementComponents)
            {
                //Debug.Log("Run, NetworkBehaviour.isClient: " + movementComponent.isClient);
                //Debug.Log("Run, NetworkBehaviour.isLocalPlayer: " + movementComponent.isLocalPlayer);
                //Debug.Log("Run, NetworkBehaviour.isServer: " + movementComponent.isServer);
                //Debug.Log("Run, NetworkBehaviour.netId: " + movementComponent.netId);
                //Debug.Log("Run, NetworkBehaviour.playerControllerId: " + movementComponent.playerControllerId);

                // При реализации в виде запускаемой системы в мире, не нужно выходить из метода если мы не сервер
                //if (!movementComponent.isLocalPlayer) return;
                // Наоборот проверяем, если мы сервер, тогда обработаем

                if (movementComponent.isLocalPlayer)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        movementComponent.Transform.position += movementComponent.Transform.up * Time.deltaTime * movementComponent.ForwardSpeed;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        movementComponent.Transform.position -= movementComponent.Transform.up * Time.deltaTime * movementComponent.ForwardSpeed;
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        movementComponent.Transform.Rotate(0, 0, Time.deltaTime * movementComponent.ForwardSpeed * movementComponent.RotateSpeed);
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        movementComponent.Transform.Rotate(0, 0, Time.deltaTime * movementComponent.ForwardSpeed * -movementComponent.RotateSpeed);
                    }

                    // center camera..
                    Vector3 pos = movementComponent.Transform.position;
                    pos.z = movementComponent.Z;
                    Camera.main.transform.position = pos;
                }
                
            }
        }

    }
}
