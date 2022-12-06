using Assets.Scripts.Name;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SpaceManager : NetworkManager
{
    static SpaceManager single;

    Stopwatch watch;
    float last;

    // Время приема передачи(RTT) – длительность в миллисекундах (МС)
    List<float> rtts;

    void Start()
    {
        single = this;
        watch = new Stopwatch();
        watch.Start();
        rtts = new List<float>();
    }

    void OnGUI()
    {
        string ipadress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 25, 200, 25), 
            "Your IP: " + ipadress
            + " port: " + Network.player.port);

        GUI.Label(new Rect(600, 10, 200, 20), "RTT:" + last + "ms");

        if (!NetworkClient.active)
            return;

        int posY = 30;
        foreach (var rtt in rtts)
        {
            int n = (int)(rtt / 5);
            if (n > 20) n = 20;

            string s = "";
            for (int i = 0; i < n; i++)
            {
                s = s + ".";
            }
            GUI.Label(new Rect(340, 10 + posY, 200, 20), s);
            posY += 8;
        }

    }

    static public void Reset()
    {
        single.watch.Reset();
        single.watch.Start();
    }

    static public float Now()
    {
        single.last = single.watch.ElapsedMilliseconds;

        single.rtts.Add(single.last);
        if (single.rtts.Count > 20)
            single.rtts.RemoveAt(0);

        return single.last;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Vector3 spawnPos = Vector3.right * conn.connectionId;
        // , Quaternion.Euler(new Vector3(0, 0, 90)));
        GameObject player = (GameObject)Instantiate(base.playerPrefab, spawnPos, Quaternion.Euler(new Vector3(0, 0, 0)));

        SetPlayerNameWithConnId(player, playerControllerId);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    // Menu Scene Button OnClick
    public void StartGame()
    {
        singleton.StartHost();
    }

    public void JoinGame()
    {
        singleton.StartClient();
    }

    private readonly StringBuilder _stringBuilder = new StringBuilder();

    private void SetPlayerNameWithConnId(GameObject player, short playerControllerId)
    {
        var nameComponent = player.GetComponent<NameComponent>();

        _stringBuilder.Append("Player №");
        _stringBuilder.Append(playerControllerId);
        nameComponent.Name += _stringBuilder.ToString();
    }

}
