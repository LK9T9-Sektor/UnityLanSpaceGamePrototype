﻿using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.PowerUpFeature
{
    public class PowerUpData : NetworkBehaviour
    {
        [SyncVar]
        public string Name;

        [SyncVar]
        public Color Color;

        public PowerUpNetworkSystem PowerUpNetworkSystem;

        void Start()
        {            
            GetComponent<Renderer>().material.color = Color;
        }

        void OnGUI()
        {
            // PowerUp name setup
            GUI.color = Color;
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            GUI.Label(new Rect(pos.x - 20, Screen.height - pos.y - 30, 100, 30), Name);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) { return; }

            Debug.Log(typeof(PowerUpData).Name + " | OnTriggerEnter2D");
            if (!NetworkServer.active) { return; }

            //ShipControl s = other.gameObject.GetComponent<ShipControl>();
            //if (s != null)
            //{
            //s.AddBuf(mbuf);
            //Destroy(gameObject);
            //}

            if (PowerUpNetworkSystem != null)
                PowerUpNetworkSystem.OnEnterTrigger(gameObject);
        }

    }
}
