using Assets.Scripts.Movement;
using Assets.Scripts.Rotation;
using Assets.Scripts.Weapons;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class ComponentsContainer
    {
        private readonly string _className = typeof(ComponentsContainer).Name.ToUpper();

        private readonly List<MovementComponent> _movementComponents = new List<MovementComponent>();
        public ReadOnlyCollection<MovementComponent> Movements
        {
            get { return _movementComponents.AsReadOnly(); }
        }

        private readonly List<RotationComponent> _rotationComponents = new List<RotationComponent>();
        public ReadOnlyCollection<RotationComponent> Rotations
        {
            get { return _rotationComponents.AsReadOnly(); }
        }

        private readonly List<WeaponBehaviour> _weaponBehaviours = new List<WeaponBehaviour>();
        public ReadOnlyCollection<WeaponBehaviour> Weapons
        {
            get { return _weaponBehaviours.AsReadOnly(); }
        }


        public void Add(GameObject gameObject)
        {
            //Debug.Log(_className + " | Add: " + gameObject);

            var movementComponent = gameObject.GetComponent<MovementComponent>();
            if (movementComponent != null) { _movementComponents.Add(movementComponent); }

            var rotationComponent = gameObject.GetComponent<RotationComponent>();
            if (rotationComponent != null)
            { 
                _rotationComponents.Add(rotationComponent);
                //Debug.Log(_className + " | rotationComponent ADD" + rotationComponent);
            }

            var weaponBehaviour = gameObject.GetComponent<WeaponBehaviour>();
            if (weaponBehaviour != null)
            {
                _weaponBehaviours.Add(weaponBehaviour);
                Debug.Log(_className + " | weaponBehaviour ADD" + weaponBehaviour);
            }
        }
    
    }
}
