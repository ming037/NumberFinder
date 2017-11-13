using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetUICtrl : MonoBehaviour {
	public  GameObject BoardCanvas; //control item panels on start
	public  GameObject popupCanvas;
	public GameObject GameManager;

	public void SetBoardCanvasOn()
	{
		GameManager.GetComponent<NetGameManage> ().iteminsertMode = true;
		BoardCanvas.SetActive (true);
	}

	public void SetPopupCancvas(bool input)
	{
		popupCanvas.SetActive (input);
	}
}
