using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Scripts.Health
{
    public class HealthBehaviour : NetworkBehaviour
    {
        public int DefaultHp;

        [SyncVar(hook = "OnHpChange")]
        private int _hp;
        public int HP
        { 
            get
            {
                return _hp;
            }
        }

        public Image HealthBarImage;

        public RectTransform HPRectTransform;

        private void Awake()
        {
            HPRectTransform = GetComponent<RectTransform>();

            //HealthBarImage = HPRectTransform.GetComponent<Image>();

            Debug.Log("HealthBarImage: " + HealthBarImage);
            _hp = DefaultHp;
        }

        public void ResetHp()
        {
            _hp = DefaultHp;
            HPRectTransform.sizeDelta = new Vector2(_hp * 2, HPRectTransform.sizeDelta.y);
        }

        public void ApplyDamage(int damage)
        {
            Debug.Log("ApplyDamage: " + damage);
            _hp -= damage;

            HealthBarImage.fillAmount = Mathf.Clamp(_hp / DefaultHp, 0, 1f);
        }

        // called on a client and server-client
        void OnHpChange(int newHealth)
        {
            if (newHealth < 100)
            {
                //MakeExplosion(0.3f);
            }
            _hp = newHealth;
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
        //            GameManager.Singleton.score += 1;
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
