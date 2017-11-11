using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkItemCtrl : NetworkBehaviour {

	public void OnDamage(Vector3 pos)
	{
		CmdDestroy ();

		//gameObject.SetActive (false);
		//Destroy (gameObject);
	}

	[Command]
	void CmdDestroy()
	{
		string todestroy = gameObject.name.Substring(5,2);
		//print (todestroy);
		int destroyindex = System.Int32.Parse (todestroy);
		print (destroyindex);
		this.gameObject.GetComponent<PlayerSetup> ().AddtoList(destroyindex);
		//gameObject.GetComponent<PlayerSetup> ().itemlist.Add (destroyindex);
		//string todestroy = gameObject.name.Substring(5,2);
		//gameObject.GetComponent<PlayerSetup> ().itemlist [System.Int32.Parse (todestroy)] = 0;
	}









}
