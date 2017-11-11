using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour {

	private Transform[] points; //spawn location
	public bool isPlayerSpawned;
	public GameObject itemPrefab;
	void Start () {
		isPlayerSpawned = false;
		points = GameObject.Find ("SpawnPointGroup").GetComponentsInChildren<Transform> ();
		for (int i = 0; i < points.Length; i++) {
			Quaternion spawnRotation = Quaternion.Euler (0.0f, Random.Range (0.0f, 180.0f), 0);
			GameObject item = (GameObject)Instantiate (itemPrefab, points[i].position, spawnRotation);
			item.name = "item_" + i.ToString ("00");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlayerSpawned) {
			
		}
	}
}
