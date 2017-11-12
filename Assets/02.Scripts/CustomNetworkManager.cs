using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager {

	public GameObject NetworkCanvas;
	public InputField ipInput;
	public void StartupHost()
	{
		NetworkCanvas.SetActive (false);
		SetPort ();
		NetworkManager.singleton.StartHost ();
	}

	public void JoinGame()
	{
		NetworkCanvas.SetActive (false);
		SetIPAddress ();
		SetPort ();
		NetworkManager.singleton.StartClient ();
	}

	void SetIPAddress()
	{
		NetworkManager.singleton.networkAddress = ipInput.text;
	}

	public void SetPort()
	{
		NetworkManager.singleton.networkPort = 7777;
	}

	public void onClickBack()
	{
		NetworkManager.singleton.StopHost();
		SceneManager.LoadScene("Menu", LoadSceneMode.Single); //LoadScene는 열려있는 씬을 지우고 여는 것 
	}

}
