using Assets.Scripts.Lifecycle;
using UnityEngine;

namespace Assets.Scripts.Health
{
    public class HealthConditionProcessor
    {
        #region Disgusting Singleton

        private HealthConditionProcessor() { }

        private static readonly HealthConditionProcessor _instance = new HealthConditionProcessor();

        public static HealthConditionProcessor Singleton { get { return _instance; } }

        #endregion

        public void ChangeTargetHp(GameObject target, int damage)
        {
            Debug.Log("HealthConditionProcessor | target: " + target.name
                + " | damage: " + damage);

            var healthComponent = target.GetComponent<HealthBehaviour>();
            if (healthComponent == null) { return; }

            if (healthComponent.HP > 0)
            {
                healthComponent.HP -= damage;
            }

            if (healthComponent.HP < 0)
            {
                var lifecycleBehaviour = target.GetComponent<LifecycleBehaviour>();
                if (lifecycleBehaviour != null) 
                {
                    if (lifecycleBehaviour.CanRespawn)
                    {
                        lifecycleBehaviour.RpcRespawn();
                    }
                    else
                    {
                        lifecycleBehaviour.RpcSetActive(false);
                    }
                }
            }
        }

    }
}
