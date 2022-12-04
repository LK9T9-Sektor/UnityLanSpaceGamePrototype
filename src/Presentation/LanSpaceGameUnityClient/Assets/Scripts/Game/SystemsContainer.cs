using Assets.Scripts.Game;
using Assets.Scripts.Movement;
using Assets.Scripts.Rotation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assets.Scripts.States
{
    public class SystemsContainer
    {
        private readonly List<IGameSystem> _systems = new List<IGameSystem>()
        {
            new MovementSystem(),
            new RotationSystem()
        };

        public ReadOnlyCollection<IGameSystem> Systems
        {
            get { return _systems.AsReadOnly(); }
        }

    }
}
