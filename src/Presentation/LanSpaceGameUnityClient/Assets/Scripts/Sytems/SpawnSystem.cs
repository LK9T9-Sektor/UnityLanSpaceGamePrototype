using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Sytems
{
    public class SpawnSystem : NetworkManager
    {
        private readonly string _className = typeof(SpawnSystem).Name;

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            Debug.Log(_className + " | OnServerAddPlayer | " + conn + " | " + playerControllerId);

            Vector3 spawnPos = Vector3.right * conn.connectionId;
            GameObject player = (GameObject)Instantiate(base.playerPrefab, spawnPos, Quaternion.identity);

            SetPlayerNameWithConnId(player, playerControllerId);

            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private void SetPlayerNameWithConnId(GameObject player, short playerControllerId)
        {
            var combat = player.GetComponent<Combat>();

            _stringBuilder.Append(" №");
            _stringBuilder.Append(playerControllerId);
            combat.playerName += _stringBuilder.ToString();
        }

    }
}
