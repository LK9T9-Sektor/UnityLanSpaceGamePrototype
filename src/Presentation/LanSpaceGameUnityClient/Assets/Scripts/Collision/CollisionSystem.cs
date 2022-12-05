using UnityEngine;

namespace Assets.Scripts.Collision
{
    public class CollisionSystem
    {
        #region Disgusting Singleton

        private CollisionSystem() { }

        private static readonly CollisionSystem _instance = new CollisionSystem();

        public static CollisionSystem Singleton { get { return _instance; }}

        #endregion


        /// <summary>
        /// Отталкивает объект относительно точки с указанной силой
        /// <summary>
        /// <param name="pushFrom">Точка относительно которой должен отскочить объект</param>
        /// <param name="pushPower">Сила с которой должен отскочить объект</param>
        public void PushAway(Rigidbody2D targetRigidbody,
                             Rigidbody2D pusherRigidbody,
                             float pushPower)
        {
            // Если нет прикреплённого Rigidbody2D, то выйдем из функции
            if (targetRigidbody == null || pushPower == 0)
            {
                return;
            }

            Debug.Log("CollisionSystem | targetRigidbody: " + targetRigidbody.name
                + " | pusherRigidbody: " + pusherRigidbody.name);

            //получаем вектор, направленный от двигаемого объекта к объекту цели и нормализуем его.
            Vector3 direction = (targetRigidbody.transform.position - pusherRigidbody.transform.position).normalized;

            // Толкаем объект в нужном направлении с силой pushPower
            pusherRigidbody.AddForce(-direction * pushPower);
        }

    }
}
