using Assets.Scripts.Name;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Lan
{
    public class LanSystem : NetworkManager
    {
        private readonly string _className = typeof(LanSystem).Name;

        #region Disgusting Singleton

        public static LanSystem _instance;
        public static LanSystem Singleton { get { return _instance; } }

        private void Start()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        #endregion

        private readonly Hashtable _players = new Hashtable();
        //List<NetworkPlayer> players = new List<NetworkPlayer>();

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            Debug.Log("OnServerAddPlayer, conn.address: " + conn.address);
            Debug.Log("OnServerAddPlayer, conn.connectionId: " + conn.connectionId);
            Debug.Log("OnServerAddPlayer, conn.hostId: " + conn.hostId);

            Vector3 spawnPos = Vector3.right * conn.connectionId;
            // , Quaternion.Euler(new Vector3(0, 0, 90)));
            // , Quaternion.Euler(new Vector3(0, 0, 0)));
            GameObject player = (GameObject)Instantiate(base.playerPrefab, spawnPos, Quaternion.Euler(new Vector3(0, 0, 0)));
            //GameObject player = (GameObject)Network.Instantiate(base.playerPrefab, 
            //    spawnPos, 
            //    Quaternion.Euler(new Vector3(0, 0, 0)),
            //    0);

            SetPlayerNameWithConnId(player, playerControllerId);
            _players[player] = player;

            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            Debug.Log("Deleting player game object for player " + conn);
            //GameObject go = _players[player] as GameObject;
            //Network.RemoveRPCs(go.networkView.viewID);   // remove buffered Instantiate calls
            //Network.Destroy(go);                         // destroy the game object on all clienst
            //_players.Remove(player);                      // remove player from server list
        }

        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private void SetPlayerNameWithConnId(GameObject player, short playerControllerId)
        {
            var nameComponent = player.GetComponent<NameComponent>();

            _stringBuilder.Append("Player №");
            _stringBuilder.Append(playerControllerId);
            nameComponent.Name += _stringBuilder.ToString();
        }

        #region Main Scene Button OnClick

        public void StartGame()
        {
            singleton.StartHost();
        }

        public void JoinGame()
        {
            singleton.StartClient();
        }

        #endregion

    }

}
