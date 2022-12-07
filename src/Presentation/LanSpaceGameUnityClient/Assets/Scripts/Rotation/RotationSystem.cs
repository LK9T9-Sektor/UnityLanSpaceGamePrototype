using Assets.Scripts.Game;

namespace Assets.Scripts.Rotation
{
    public class RotationSystem : IGameSystem
    {
        private readonly string _className = typeof(RotationSystem).Name;

        public void Run()
        {
            //Debug.Log(_className + " | Run");
            if (GameWorldHandler.Singleton.Components.Rotations.Count < 1) return;

            foreach (var component in GameWorldHandler.Singleton.Components.Rotations)
            {
                //float dir = 170.0f;
                //component.transform.rotation = Quaternion.Euler(0, 180, dir);
                //component.Rigidbody2D.angularVelocity = dir;
                //component.Transform.Rotate(0f, component.Speed * Time.deltaTime, 0f);
            }
        }

    }
}
