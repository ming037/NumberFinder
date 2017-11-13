using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetButtonCtrl : MonoBehaviour {
    public GameObject BoardCanvas;
    public GameObject BoardPanel;
    public GameObject ResultPanel;
    public GameObject GameManager;
    private AudioSource _audio;
    public Text warningText;

    private const int MAX = 6;
    private string []items = new string[MAX]; //아이템 최대 개수는 6개.
    private int[] check = new int[MAX]; //연산기호 체크용 배열. 연산장치는1, 숫자느 0.
    private int itemCount = 0; //아이템 개수.
    public AudioClip submitsound;

	public bool isPenalty;


    public struct symbol
    {
        public string value; //연산자 종류
        public int index; //위치
        public int priority; //우선순위
        public int sequence; //순서
    }

    public struct numbers
    {
        public string value;
        public int start_i;
        public int end_i;
    }

	void start()
	{
		isPenalty = false;
	}


    public void Submit()//제출버튼.
	{
		if (GameManager.GetComponent<NetcountDownTimer> ().timeRemaining > 0)
			return;
        string result = "";
        warningText.text = "";
        symbol[] symbols = new symbol[MAX]; //연산 기호 담을 배열
        numbers[] number = new numbers[MAX]; //숫자 담을 배열

       // GameManager = GameObject.Find("GameManager"); //보드에 있는 아이템 수를 알아내기 위해서 스크립트 가져오는 용도.
        _audio = GetComponent<AudioSource>();
        itemCount = 0;
        /*
        if (GameManager.gameObject.GetComponent<GameMgr>().itemOnBoard != MAX)
        {
            StartCoroutine(ShowMessage("6개 이상의 숫자나 연산기호를 넣어주세요!", 2));
            Debug.Log("error, less than 6");
            return;
        }*/


        //기호 있는지 없는지 체크하기 위함
        int checksymbolcount = 0;
        foreach (Transform child in BoardPanel.transform)
        {
            if (child.gameObject.tag == "add" || child.gameObject.tag == "minus"
                || child.gameObject.tag == "mul" || child.gameObject.tag == "div")
            {
                checksymbolcount++;
            }
        }
        if (checksymbolcount == 0)//기호가 없을 때
        {
            StartCoroutine(ShowMessage("At lease one operation!", 2));
            return;          
        }


        //아이템 전부 삭제*******************************
        int index = 0;
        foreach (Transform child in BoardPanel.transform)
        {
            if(child != null)
            { 
                itemCount++;
                items[index++] = child.gameObject.tag;//아이템 배열에 담기.
                Destroy(child.gameObject);
            }
        }
        /***************************************/

        //Debug.Log("itemCount=" + itemCount);
        int symbol_cnt = 0; //연산자 개수 초기화
        for (int i = 0; i < itemCount; i++)
        {
            Debug.Log("Check item"+items[i]);
            if (items[i] == "add" || items[i] == "minus")
            {
                symbols[symbol_cnt].value = items[i];
                symbols[symbol_cnt].index = i;
                symbols[symbol_cnt].priority = 1;
                check[i] = 1;
                symbol_cnt++;
            }
            else if (items[i] == "mul" || items[i] == "div")
            {
                symbols[symbol_cnt].value = items[i];
                symbols[symbol_cnt].index = i;
                symbols[symbol_cnt].priority = 2;
                check[i] = 1;
                symbol_cnt++;
            }
        }

       

        //연속으로 연산기호 오거나, 시작과 끝에 연산기호 올경우 
        if(symbolruleViolation1() || symbolruleViolation2()) //둘 중 하나라도 false면 실패
        {
            resetsetting(); //세팅 초기화
            //Debug.Log("ERROR, SYMBOL ERROR");
			StartCoroutine(ShowMessage("Something wrong on your equation!", 2));
            return;
        }


        if (symbol_cnt == 0)//기호가 없을 때
        {
            /*
            string res = "";
            for (int j = 0; j < itemCount; j++)
                res += items[j];
            result = res;
            */
        }
        else //연산기호 하나라도 있을 때
        {
            //******************숫자 시작 끝 인덱스 구하기
            //첫 숫자
            number[0].start_i = 0;
            number[0].end_i = symbols[0].index - 1;
            //끝 숫자
            number[symbol_cnt].start_i = symbols[symbol_cnt - 1].index + 1;
            number[symbol_cnt].end_i = itemCount - 1;
            //중간 숫자들
            for (int i = 1; i < symbol_cnt; i++)
            {
                number[i].start_i = symbols[i - 1].index + 1;
                number[i].end_i = symbols[i].index - 1;
            }
            //***************************************

            //********************숫자 값 구하기
            for(int i=0; i<symbol_cnt+1; i++)
            {
                for (int j = number[i].start_i; j <= number[i].end_i; j++)
                    number[i].value += items[j];
            }
            //*********************************

            //*************************우선순위 정하기
            int p_cnt = 0;
            for (int i = 0; i < symbol_cnt; i++)
            {
                if (symbols[i].priority == 2)
                {
                    symbols[i].sequence = p_cnt;
                    p_cnt++;
                }
            }
            for (int i = 0; i < symbol_cnt; i++)
            {
                if (symbols[i].priority == 1)
                {
                    symbols[i].sequence = p_cnt;
                    p_cnt++;
                }
            }
            //******************** 연산
            int count = 0;
            int prevPos = 0; //이 전 연산자의 위치.

            for (int i = 0; i < symbol_cnt; i++) {
                if (symbols[i].sequence == 0)
                {
                    result = number[i].value; //초기값 설정
                    prevPos = i;
                }
            }
            //현재 연산자가 이 전 연산자 보다 오른쪽에 있으면 result가 왼쪽
            while (symbol_cnt > count)
            {
                for (int i = 0; i < symbol_cnt; i++)
                {
                    if (symbols[i].sequence == count)
                    {
                        if (symbols[i].value == "add")
                        {
                            if (i < prevPos) result = "" + (System.Int32.Parse(number[i].value) + System.Int32.Parse(result));
                            else if (i > prevPos) result = "" + (System.Int32.Parse(result) + System.Int32.Parse(number[i+1].value));
                            else result = "" + (System.Int32.Parse(result) + System.Int32.Parse(number[i + 1].value));
                        
                        }
                        else if (symbols[i].value == "minus")
                        {
                            if (i < prevPos) result = "" + (System.Int32.Parse(number[i].value) - System.Int32.Parse(result));
                            else if (i > prevPos)  result = "" + (System.Int32.Parse(result) - System.Int32.Parse(number[i + 1].value));
                            else result = "" + (System.Int32.Parse(result) - System.Int32.Parse(number[i + 1].value));
                        }
                        else if (symbols[i].value == "mul")
                        {
                            if (i < prevPos) result = "" + (System.Int32.Parse(number[i].value) * System.Int32.Parse(result));
                            else if(i>prevPos) result = "" + (System.Int32.Parse(result) * System.Int32.Parse(number[i + 1].value));
                            else result = "" + System.Int32.Parse(result) * System.Int32.Parse(number[i+1].value);
                        }
                        else //div
                        {
                            if (i < prevPos)
                            {
                                if (System.Int32.Parse(result) == 0)
                                {
                                
                                    StartCoroutine(ShowMessage("Can't divide by 0!", 2));
                                    resetsetting(); //설정 초기화
                                    return;
                                }
                                else
                                    result = "" + (System.Int32.Parse(number[i].value) / System.Int32.Parse(result));
                            }
                            else if (i > prevPos)
                            {
                                if (System.Int32.Parse(number[i + 1].value) == 0)
                                {
                                    
									StartCoroutine(ShowMessage("Can't divide by 0!", 2));
                                    resetsetting(); //설정 초기화
                                    return;
                                }
                                else
                                    result = "" + (System.Int32.Parse(result) / System.Int32.Parse(number[i + 1].value));
                            }
                            else
                            {
                                if (System.Int32.Parse(number[i + 1].value) == 0)
                                {
                                    
									StartCoroutine(ShowMessage("Can't divide by 0!", 2));
                                    resetsetting(); //설정 초기화
                                    return;
                                }
                                result = "" + (System.Int32.Parse(result) / System.Int32.Parse(number[i + 1].value));
                            }
                        }
                        count++;
                        prevPos = i;
                    }
                }
            }
        }

        //Debug.Log(result);
        //Debug.Log(System.Int32.Parse(result));
        

        //Debug.Log((GameManager.gameObject.GetComponent<GameMgr>().resultnum));
		if (GameManager.GetComponent<NetGameManage>().resultnum == System.Int32.Parse (result)) //결과가 같았을 경우.
        {
			//GameManager.GetComponent<GameManage>().IncScore(100.0f); //점수 100점 추가
			GameManager.GetComponent<NetGameManage>().LeftScore();
            submitsound = Resources.Load("sounds/correct") as AudioClip;
            _audio.PlayOneShot(submitsound, 1f);
            StartCoroutine(ShowMessage("Correct!", 2));
            //Debug.Log("SAME");
            
        }
        else //결과가 달랐을경우.
        {
			//GameManager.GetComponent<GameManage>().IncScore(-50.0f); //점수 50점 감소
            submitsound = Resources.Load("sounds/wrong") as AudioClip;
            _audio.PlayOneShot(submitsound, 1f);
            StartCoroutine(ShowMessage("Wrong!", 2));
			GameManager.GetComponent<NetcountDownTimer> ().timeRemaining = 10;
            //Debug.Log("Different");
        }

        resetsetting(); //설정 초기화
    }

	public void Skip()
	{
		if (GameManager.GetComponent<NetcountDownTimer> ().timeRemaining > 0)
			return;

		GameManager.GetComponent<NetcountDownTimer> ().timeRemaining = 10;
		foreach (Transform child in ResultPanel.transform)
		{
			Destroy(child.gameObject);
		}
		GameManager.gameObject.GetComponent<NetGameManage>().itemOnBoard = 0;
		GameManager.gameObject.GetComponent<NetGameManage>().Makeresult();
	}

	public void Cancel() //취소버튼.
	{
       
        //1GameManager = GameObject.Find("GameManager");
        //1GameManager.gameObject.GetComponent<GameMgr>().timeFlow = true; //다시 시간 흐르게 하기
		GameManager.gameObject.GetComponent<NetGameManage>().iteminsertMode = false;//게임판 아닐 때 숫자 클릭 못하도록
        BoardCanvas.SetActive (false);
	}

    private bool symbolruleViolation1()
    {//처음과 끝이 연산기호일 경우, 어기면 true리턴
        if (check[0] == 1 || check[itemCount - 1] == 1) return true; 
        else return false;
    }

    private bool symbolruleViolation2()
    {//연산기호가 연속으로 나올 경우
        //연속으로 연산기호 이면 실패
        for (int i = 0; i < itemCount - 1; i++) if (check[i] == 1 && check[i + 1] == 1) return true;
        return false;
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        warningText.text = message;
        yield return new WaitForSeconds(delay);
        warningText.text = "";
    }

    private void resetsetting()
    {
        //기존의 결과 숫자 삭제 && 새로운 숫자 추가
        foreach (Transform child in ResultPanel.transform)
        {
            Destroy(child.gameObject);
        }
        //계산식 초기화
        for (int i = 0; i < MAX; i++)
        {
            check[i] = 0;
        }

		GameManager.gameObject.GetComponent<NetGameManage>().itemOnBoard = 0;
		GameManager.gameObject.GetComponent<NetGameManage>().Makeresult();
        //점수 올리고, 숫자 바꾸기

    }


}
