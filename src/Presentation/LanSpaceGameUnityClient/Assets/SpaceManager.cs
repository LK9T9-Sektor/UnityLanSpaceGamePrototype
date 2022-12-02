using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class SpaceManager : NetworkManager 
{
	static SpaceManager single;

	Stopwatch watch;
	float last;

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
		GUI.Label(new Rect(300,10,200,20), "RTT:"+last+"ms");

		if (!NetworkClient.active)
			return;

		int posY = 30;
		foreach (var rtt in rtts)
		{
			int n = (int)(rtt / 5);
			if (n > 20) n = 20;

			string s = "";
			for (int i=0; i < n; i++)
			{
				s = s + ".";
			}
			GUI.Label(new Rect(340,10+posY,200,20), s);
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
		GameObject player = (GameObject)Instantiate(base.playerPrefab, spawnPos, Quaternion.identity);
 		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}

	public void StartGame()
	{
		NetworkManager.singleton.StartHost();
	}

	public void JoinGame()
	{
		NetworkManager.singleton.StartClient();
	}
}
