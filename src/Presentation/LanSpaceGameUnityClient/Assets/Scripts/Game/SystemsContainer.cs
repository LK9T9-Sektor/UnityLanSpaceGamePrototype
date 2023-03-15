using Assets.Scripts.Game;
using Assets.Scripts.Rotation;
using Assets.Scripts.Weapons;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assets.Scripts.States
{
    public class SystemsContainer
    {
        private readonly List<IGameSystem> _systems = new List<IGameSystem>()
        {
            new Assets.Scripts.Movement.MovementSystem(),
            //Assets.Scripts.Movement.MovementSystem.Singleton,
            new RotationSystem(),
            new WeaponSystem(),
        };

        public ReadOnlyCollection<IGameSystem> RunnableSystems
        {
            get { return _systems.AsReadOnly(); }
        }

    }
}
