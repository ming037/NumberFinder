using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetworkItemCtrl : NetworkBehaviour {

	private GameObject ItemSpawner;

	[Command]
	public void CmdDestroy(GameObject player)
	{
		ItemSpawner = GameObject.Find ("ItemSpawner");
		ItemSpawner.GetComponent<ItemSpawner> ().itemcnt -= 1;
		//print (ItemSpawner.GetComponent<ItemSpawner> ().itemcnt);
		string todestroy = this.gameObject.name.Substring(5,2);
		int destroyindex = System.Int32.Parse (todestroy);
		//print (destroyindex);
		player.gameObject.GetComponent<PlayerSetup> ().AddtoList(destroyindex);
	}

}
