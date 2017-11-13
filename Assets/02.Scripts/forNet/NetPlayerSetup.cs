using UnityEngine;
using UnityEngine.Networking;

public class NetPlayerSetup :NetworkBehaviour
{
	[SerializeField]
	Behaviour[] componetntsToDisable;

	SyncListInt itemlist = new SyncListInt ();

	Camera sceneCamera;
	void Start ()
	{
		
		if (!isLocalPlayer) {
			for (int i = 0; i < componetntsToDisable.Length; i++) {
				componetntsToDisable [i].enabled = false;
			}
		}
		else {
			sceneCamera = Camera.main;
			if (sceneCamera != null) {
				sceneCamera.gameObject.SetActive (false);
			}
		}
	}

	void Update()
	{
		if (isServer) {
			for (int i = 0; i < 73; i++) {
				if (itemlist.Contains (i)) {
					GameObject fordestroy = GameObject.Find ("item_"+i.ToString("00"));
					Destroy (fordestroy);
					itemlist.Remove (i);
				}
			}

		}
	}


	void OnDisalbe()
	{
		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive (true);
		}
	}

	public void AddtoList(int todestroy)
	{
		itemlist.Add (todestroy);
	}


}
