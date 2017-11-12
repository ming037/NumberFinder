using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))] //이거 지우기 전까지 컴퍼넌트 못지움
public class FireCtrl : NetworkBehaviour 
{
	private GameObject itemPanel;
	public GameObject UIControl;
    public Transform firePos; //좌표값만 필요하기 때문에 Transform으로.
    public AudioClip fireSfx;

	private GameObject ItemSpawner;

	public GameObject[] inventoryIcons;



    public float fireRate = 0.5f; //0.1초 간격으로 아이템 먹음.
    private float nextFire = 0.5f; //다음에 발사할 시간;

	private int countraycast = 20;
	private float raycastdis = 3.0f;

    private AudioSource _audio;
    private RaycastHit hit;
   
	private string []name = {"0","1","2","3","4","5","6","7","8","9","add","minus","mul","div"
    ,"add","minus","mul","div","0","0"};
	// Use this for initialization
	void Start () {
        _audio = GetComponent<AudioSource>();  //아이템 줍는 소리.
		itemPanel = GameObject.Find ("itempanel");
		UIControl = GameObject.Find ("UIControl");
		ItemSpawner = GameObject.Find ("ItemSpawner");
	}

	[Command]
	void CmdDestroy(GameObject pickeditem)
	{
		ItemSpawner.GetComponent<ItemSpawner> ().itemcnt -= 1;
		//print (ItemSpawner.GetComponent<ItemSpawner> ().itemcnt);
		string todestroy = pickeditem.name.Substring(5,2);
		int destroyindex = System.Int32.Parse (todestroy);
		//print (destroyindex);
		this.gameObject.GetComponent<PlayerSetup> ().AddtoList(destroyindex);
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		Vector3 pos = firePos.position;
		Vector3 addpos = new Vector3 (0f, 0.2f, 0f);

		for (int i = 0; i < countraycast; i++) {
			Debug.DrawRay(pos+i*addpos, firePos.forward * raycastdis, Color.green); //초록색 광선의 Raycast         
		}
			
		if (CrossPlatformInputManager.GetButtonDown ("Shoot"))//Input.GetMouseButton(0)) //getMouseButtonDown//getMouseButton만 해리면 누르고 있는 동안 계속해서 발사됨. 0은 왼쪽 1은 오른쪽 2는 가운데 버튼.
        {
            if (Time.time >= nextFire) //Time.time은 시간 누적
            {
                nextFire = Time.time + fireRate;
				for (int i = 0; i < countraycast; i++)
				{
					if (Physics.Raycast(firePos.position + i*addpos, firePos.forward, out hit, raycastdis, 1 << 8))//전진방향으로 10m만큼 광선을 발사하는데 무언가 맞으면 hit 이라는 레이케스트 변수에 담아서 리턴한다.
					{
                        _audio.PlayOneShot(fireSfx, 0.8f); //아이템 줍는 소리.
						//hit.collider.gameObject.GetComponent<NetworkItemCtrl>().CmdDestroy(this.gameObject); //hit.point는 맞은 지점
						CmdDestroy(hit.collider.gameObject);
						int itemindex = Random.Range(0, name.Length); // 1에서 길이까지 랜덤.

						//안에 있는지 없는지 확인.
						foreach(Transform child in itemPanel.transform)
						{
							if (child.gameObject.tag == name[itemindex]) 
							{
								string c = child.Find ("Text").GetComponent<Text>().text;
								int tcount = System.Int32.Parse (c) + 1;
								child.Find("Text").GetComponent<Text>().text = "" + tcount;
								return;
							}
						}

						GameObject t;
						switch (itemindex) 
						{
						case 0:
                            case 18:
                            case 19:
							t = Instantiate (inventoryIcons [0]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 1:
							t = Instantiate (inventoryIcons [1]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 2:
							t = Instantiate (inventoryIcons [2]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 3:
							t = Instantiate (inventoryIcons [3]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 4:
							t = Instantiate (inventoryIcons [4]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 5:
							t = Instantiate (inventoryIcons [5]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 6:
							t = Instantiate (inventoryIcons [6]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 7:
							t = Instantiate (inventoryIcons [7]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 8:
							t = Instantiate (inventoryIcons [8]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 9:
							t = Instantiate (inventoryIcons [9]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 10:
                            case 14://+
							t = Instantiate (inventoryIcons [10]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 11:
                            case 15://-
                            t = Instantiate (inventoryIcons [11]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 12:
                            case 16://x
							t = Instantiate (inventoryIcons [12]);
							t.transform.SetParent (itemPanel.transform);
							break;
						case 13:
                            case 17://나누기
							t = Instantiate (inventoryIcons [13]);
							t.transform.SetParent (itemPanel.transform);
							break;
						}
						break;
					}

					else if (Physics.Raycast (firePos.position + i * addpos, firePos.forward, out hit, raycastdis, 1 << 9)) //메인 보드 일 때
					{
                        //보드 일때.
						print(hit.collider.gameObject.name);
						UIControl.GetComponent<UICtrl>().SetBoardCanvasOn();
					}
				}
				
            }
        }
    }
}
