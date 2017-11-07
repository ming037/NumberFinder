using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour {

    public static GameMgr instance = null;

    public Transform[] points; //위치 정보.
    public GameObject item;
    public Text scoreText;
    public float highScore = 0.0f; //점수
	public GameObject BoardPanel;
    public GameObject ResultPanel;
    public GameObject popuppanel;
    public GameObject TimeupPanel;

    public GameObject[] inventoryIcons;
    public GameObject equalsymbol;
    public Text EndtimeText;
 
    public bool iteminsertMode; //아이템 넣는 판에 갔을 때 참이 된다. 그 때 말고는 아이템 클릭해도 아무일도 안나게 해야한다. 
    public bool timeFlow; //true면 시간 가고, false면 시간 안감

    public int resultnum;//랜덤으로 나오는 숫자 결과

    public int itemOnBoard=0; //보드에 있는 아이템 수.

    [Header("- Object pooling")]
    public List<GameObject> itemPool = new List<GameObject>(); //objectpooling 용
    private int maxPool = 50; //몇 개 까지 만들 건지
    private float createTime = 0.5f;//1초 간격으로 아이템 생성
    public bool isGameOver = false;
	

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
        IncScore(0);
        inventoryIcons = GameObject.Find("Character").gameObject.GetComponent<FireCtrl>().inventoryIcons;
        Makeresult(); //새로운 랜덤 결과 만들기.

        timeFlow = true;
        iteminsertMode = false;
        itemOnBoard = 0;
        BoardPanel.SetActive (false);
        popuppanel.SetActive(false);
        TimeupPanel.SetActive(false);

        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();//여러개의 포인트를 가지고 옴, find함수이기 때문에 느림, Start에서만 사용하자.
		item = Resources.Load<GameObject>("item");
        StartCoroutine(CreateItem());
        CreatePooling();
	}

    void CreatePooling()
    {
        for (int i = 0; i < maxPool; i++)
        {
            GameObject _item = Instantiate(item) as GameObject;
			_item.name = "Item_" + i.ToString("00"); //_item에 번호 붙임.
			_item.SetActive(false);//비활성화.
			itemPool.Add(_item);
        }
    }

    IEnumerator CreateItem()
    {
        yield return new WaitForSeconds(createTime);
        while (!isGameOver)
        {
			foreach (var _item in itemPool)
            {
				if (_item.activeSelf == false)//비활성화 되어 있으면 사용가능한 몬스터
                {
                    int idx = Random.Range(1, points.Length);
					_item.transform.position = points[idx].position; //위치 설정
					_item.SetActive(true); //활성화
                    break; //브레이크 안해주면 다 활성화 됨
                }
            }
            yield return new WaitForSeconds(createTime);
        }
    }

    public void IncScore(float score)
    {
        if (highScore + score < 0) highScore = 0.0f;
        else highScore += score;
        scoreText.text = "SCORE " + highScore.ToString("00000");
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

    
}
