using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetItemSpawner : NetworkBehaviour {

	[SyncVar]public int clientCnt;
	[SyncVar]public int hostCnt;

	[SyncVar]public string clientres;
	[SyncVar]public string hostres;

	[SyncVar]public bool gameover;

	public int itemcnt;
	private Transform[] points; //spawn location
	public GameObject itemPanel; //control item panels on start
	public GameObject itemPrefab;
	public Text ScoreText;

	public override void OnStartServer()
	{
		gameover = false;
		clientCnt = 3;
		hostCnt = 3;
		itemcnt = 73;
		itemPanel.SetActive (true); 
		points = GameObject.Find ("SpawnPointGroup").GetComponentsInChildren<Transform> ();
		itemSpawn ();


	}
		
	public override void OnStartClient()
	{



		itemPanel.SetActive (true); 
		ClientScene.RegisterPrefab(itemPrefab);
	}

	public void itemSpawn()
	{
		for (int i = 0; i < points.Length; i++) {
			Quaternion spawnRotation = Quaternion.Euler (270.0f, 0.0f, 0);
			GameObject item = (GameObject)Instantiate (itemPrefab, points[i].position, spawnRotation);
			item.name = "item_" + i.ToString ("00");
			NetworkServer.Spawn (item);
		}
	}

	void Update()
	{
		if (isServer && itemcnt == 0) {
			itemSpawn ();
			itemcnt = 73;

		}

		if (clientCnt == 0) {
			clientres = "You Win!!";
			hostres = "You Lost!!";
			gameover = true;
		}

		if (hostCnt == 0) {
			hostres = "You Win!!";
			clientres = "You Lost!!";
			gameover = true;
		}
	}

	public void setResult()
	{
		if (isServer) {
			ScoreText.text = hostres;
		} else {
			ScoreText.text = clientres;
		}
	}

	public void editLeft()
	{
		if (isServer) {
			hostCnt -= 1;
		} else {
			CmdeditLeftClient ();
		}
	}

	[Command]
	void CmdeditLeftClient()
	{
		clientCnt -= 1;
	}
}
