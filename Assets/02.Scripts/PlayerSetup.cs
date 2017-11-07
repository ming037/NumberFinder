using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup :NetworkBehaviour
{
	[SerializeField]
	Behaviour[] componetntsToDisable;

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

	void OnDisalbe()
	{
		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive (true);
		}
	}
}
