using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkItemCtrl : NetworkBehaviour {

	public bool destroyOnHit;
	public void OnDamage(Vector3 pos) //raycast로 쏠 때 데미지 입음
	{
		if (!isServer) {
			return;
		}
		Destroy (gameObject);
	}


}
