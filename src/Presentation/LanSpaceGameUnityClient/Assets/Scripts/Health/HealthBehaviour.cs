using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Health
{
    public class HealthBehaviour : NetworkBehaviour
    {
        private int _defaultHp;

        [SyncVar(hook = "OnHpChange")]
        public int HP;

        private void Awake()
        {
            _defaultHp = HP;
        }

        public void ResetHp()
        {
            HP = _defaultHp;
        }

        // called on a client and server-client
        void OnHpChange(int newHealth)
        {
            if (newHealth < 100)
            {
                //MakeExplosion(0.3f);
            }
            HP = newHealth;
        }

        // work on a server, do nothing on a client
        //public void TakeDamage(int amount)
        //{
        //    if (!isServer)
        //        return;

        //    health -= amount;

        //    if (health <= 0)
        //    {
        //        if (canRespawn)
        //        {
        //            health = 100;

        //            RpcRespawn();
        //        }
        //        else
        //        {
        //            GameManager.singleton.score += 1;
        //            // прячем моба у клиентов
        //            RpcSetActive(false);
        //        }
        //    }
        //}


        // TODO: Remove

        public Texture HPBox;

        void OnGUI()
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

            // draw health bar background
            GUI.color = Color.grey;
            GUI.DrawTexture(new Rect(pos.x - 26, Screen.height - pos.y + 20, 52, 7), HPBox);

            // draw health bar amount
            GUI.color = Color.green;
            GUI.DrawTexture(new Rect(pos.x - 25, Screen.height - pos.y + 21, HP / 2, 5), HPBox);
        }

    }
}
