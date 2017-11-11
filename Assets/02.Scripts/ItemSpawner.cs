using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemSpawner : NetworkBehaviour {


	private Transform[] points; //spawn location

	public GameObject itemPrefab;
	public override void OnStartServer()
	{
		points = GameObject.Find ("SpawnPointGroup").GetComponentsInChildren<Transform> ();

		for (int i = 0; i < points.Length; i++) {
			Quaternion spawnRotation = Quaternion.Euler (0.0f, Random.Range (0.0f, 180.0f), 0);

			GameObject item = (GameObject)Instantiate (itemPrefab, points[i].position, spawnRotation);
			item.name = "item_" + i.ToString ("00");
			NetworkServer.Spawn (item);
		}
	
	}
		
	public override void OnStartClient()
	{
		ClientScene.RegisterPrefab(itemPrefab);
	}


}
