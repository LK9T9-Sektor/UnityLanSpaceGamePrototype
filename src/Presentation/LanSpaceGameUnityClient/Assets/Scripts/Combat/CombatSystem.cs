using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class CombatSystem
    {
        #region Disgusting Singleton

        private CombatSystem() { }

        private static readonly CombatSystem _instance = new CombatSystem();

        public static CombatSystem Singleton { get { return _instance; } }

        #endregion

        public int Handle(GameObject target, GameObject attacker)
        {
            Debug.Log("CombatSystem | Handle: " + target.name
                + " | pusherRigidbody: " + attacker.name);

            var targetCombatStats = target.GetComponent<CombatStatsComponent>();
            var attackerCombatStats = attacker.GetComponent<CombatStatsComponent>();
            if (targetCombatStats != null && attackerCombatStats != null)
            {
                return GetDamageValue(targetCombatStats, attackerCombatStats);
            }

            return 0;
        }

        private int GetDamageValue(CombatStatsComponent targetCombatStats,
                                   CombatStatsComponent attackerCombatStats)
        {
            var damageAfterDefenceReduction = attackerCombatStats.Attack - targetCombatStats.Defence;
            if (damageAfterDefenceReduction <= 0)
            {
                return 1;
            }
            else
            {
                return damageAfterDefenceReduction;
            }
        }

    }
}
