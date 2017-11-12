using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour {

	public static GameManage instance = null;

	public GameObject itemPanel;
	public GameObject ItemSpawner;
	public Text scoreText;
	public float highScore = 0.0f; //점수
	public GameObject BoardCanvas;
	public GameObject ResultPanel;
	public GameObject PopupCanvas;
	public GameObject TimeupCanvas;

	public GameObject[] inventoryIcons;
	public GameObject equalsymbol;
	public Text EndtimeText;

	public bool iteminsertMode; //아이템 넣는 판에 갔을 때 참이 된다. 그 때 말고는 아이템 클릭해도 아무일도 안나게 해야한다. 

	public int resultnum;//랜덤으로 나오는 숫자 결과

	public int itemOnBoard=0; //보드에 있는 아이템 수.


	void Awake()
	{
		if (instance != null && instance != this) //다시 이 스크립트로 돌아 왔을 때 중복되는 것을 방지.
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
			//DontDestroyOnLoad(this.gameObject);//다른 씬을 가더라도 지워지지 않게 만듦.
		}
	}


	void Start () {
		//ItemSpawner = GameObject.Find ("ItemSpawner").GetComponent<ItemSpawner>();

		setLeft ();
		Makeresult(); //새로운 랜덤 결과 만들기.

		itemPanel.SetActive (false);
		iteminsertMode = false;
		itemOnBoard = 0;
		BoardCanvas.SetActive (false);
		PopupCanvas.SetActive(false);
		TimeupCanvas.SetActive(false);


	}

	void Update()
	{
		if (ItemSpawner.GetComponent<ItemSpawner> ().gameover) {
			ItemSpawner.GetComponent<ItemSpawner> ().setResult ();
			TimeupCanvas.SetActive (true);
		}

		if (ItemSpawner.GetComponent<ItemSpawner> ().clientCnt == null)
			return;

		scoreText.text = "H: " + ItemSpawner.GetComponent<ItemSpawner> ().hostCnt.ToString ("0") + "  C: "+ ItemSpawner.GetComponent<ItemSpawner> ().clientCnt.ToString ("0");
	
	}


	public void Makeresult()
	{//새로운 랜덤 결과 만들기
		int randomNum1 = Random.Range(0, 10); //첫 번째 번호.
		int randomNum2 = Random.Range(0, 10); //두 번째 번호.
		GameObject t;
		resultnum = 10 * randomNum1 + randomNum2; //결과 저장 
		//랜덤 번호 넣기.
		t = Instantiate(equalsymbol);
		t.transform.Find("Text").GetComponent<Text>().text = " ";
		t.transform.SetParent(ResultPanel.transform);
		t = Instantiate(inventoryIcons[randomNum1]);
		t.transform.Find("Text").GetComponent<Text>().text = " ";
		t.transform.SetParent(ResultPanel.transform);
		t = Instantiate(inventoryIcons[randomNum2]);
		t.transform.Find("Text").GetComponent<Text>().text = " ";
		t.transform.SetParent(ResultPanel.transform);
	}

	public void IncScore(float score)
	{
		if (highScore + score < 0) highScore = 0.0f;
		else highScore += score;
		scoreText.text = "SCORE " + highScore.ToString("00000");
	}  

	public void LeftScore()
	{
		ItemSpawner.GetComponent<ItemSpawner> ().editLeft ();

	}

	public void setLeft()
	{
		highScore = 3;
		scoreText.text = "H: " + highScore.ToString ("0") + "  C: "+ highScore.ToString ("0");
	}
		
}
