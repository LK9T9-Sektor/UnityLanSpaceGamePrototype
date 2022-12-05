using Assets.Scripts.Audio;
using Assets.Scripts.Health;
using UnityEngine;

namespace Assets.Scripts.Collision
{
    public class Collision2DProcessor
    {
        #region Disgusting Singleton

        private Collision2DProcessor() { }

        private static readonly Collision2DProcessor _instance = new Collision2DProcessor();

        public static Collision2DProcessor Singleton { get { return _instance; } }

        #endregion


        // TODO: Add OnCollide Self-destruction rule, for bullets and etc
        // TODO: Calc on Collision Resistance or a Defence or an any other stat???

        public void Handle(Collision2D collisionOn, GameObject triggerFrom)
        {
            // TODO: Add marker component
            if (!collisionOn.collider.CompareTag("Player")) { return; }

            Debug.Log("Collision2DProcessor | collisionOn: " + collisionOn  
                + " | triggerFrom: " + triggerFrom.name);

            var collisionBehaviour = triggerFrom.GetComponent<CollisionBehaviour>();
            if (collisionBehaviour != null)
            {
                CollisionSystem.Singleton.PushAway(collisionOn.rigidbody,
                    triggerFrom.GetComponent<Rigidbody2D>(),
                    collisionBehaviour.PushPower);

                AudioSystem.Singleton.PlaySoundAtPosition(collisionBehaviour.PushAudio,
                    triggerFrom.transform.position);

                HealthConditionProcessor.Singleton.ChangeTargetHp(collisionOn.gameObject,
                    collisionBehaviour.Damage);

                HealthConditionProcessor.Singleton.ChangeTargetHp(triggerFrom,
                    collisionBehaviour.SelfDamage);
            }            
        }

    }
}
