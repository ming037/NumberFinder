using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchItem : MonoBehaviour {
    //playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>(); //다른 오브젝트이기 때문에 이런식으로
    private GameObject GameManager;
    private const int MAX = 6;
    Text warningText;
   
    public void selectItem()
	{
		GameObject BoardPanel = GameObject.FindWithTag("boardpanel");
        GameObject itempanel = GameObject.FindWithTag("itempanel");
        GameObject t;
        GameManager = GameObject.Find("GameManager"); //보드에 있는 아이템 수를 알아내기 위해서 스크립트 가져오는 용도.
        int number = 0 ;
        if(GameManager.gameObject.GetComponent<GameMgr>().iteminsertMode == true)
        {//평소에는 클릭 안되게 만들기
            if (this.transform.Find("Text").GetComponent<Text>().text == " ")
            {
                //결과 창(아이템 클릭시 아래로 내려가는 것)에 있는 숫자이면.
                return;
            }


            if (!(this.transform.Find("Text").GetComponent<Text>().text == ""))  //판에 들어가 있는 객체가 아니면.
            {
                if (GameManager.gameObject.GetComponent<GameMgr>().itemOnBoard == MAX) //아이템 6개이면
                {
                    StartCoroutine(ShowMessage("6개 이상 놓을 수 없습니다!", 2));
                    return;
                }
                GameManager.gameObject.GetComponent<GameMgr>().itemOnBoard++;
                //Debug.Log(obj.gameObject.GetComponent<GameMgr>().itemOnBoard);
                number = System.Int32.Parse(this.transform.Find("Text").GetComponent<Text>().text);

                this.transform.Find("Text").GetComponent<Text>().text = "";
                t = Instantiate(this.gameObject);
                t.transform.SetParent(BoardPanel.transform);

                if (number > 1)
                {
                    int tcount = number - 1;
                    this.transform.Find("Text").GetComponent<Text>().text = "" + tcount;
                }
                else
                {
                    //1개 일 때.
                    Destroy(this.gameObject);
                }
            }

            else //보드에 있는 경우 다시 돌리기.
            {
                GameManager.gameObject.GetComponent<GameMgr>().itemOnBoard--;
                //Debug.Log(obj.gameObject.GetComponent<GameMgr>().itemOnBoard);
                //일단 찾고.
                foreach (Transform child in itempanel.transform)
                {
                    if (child.gameObject.tag == this.gameObject.tag) //있으면 숫자보드는 올려주고 판에 있는거 삭제
                    {
                        //Debug.Log("child.tag"+child.gameObject.tag);
                        //Debug.Log("object.tag" + this.gameObject.tag);
                        string c = child.Find("Text").GetComponent<Text>().text;
                        int tcount = System.Int32.Parse(c) + 1;
                        child.Find("Text").GetComponent<Text>().text = "" + tcount;
                        Destroy(this.gameObject);
                        return;
                    }
                }
                // 숫자 보드에 없으면 판에 있는거에 숫자 달아서 올려주기.
                this.transform.Find("Text").GetComponent<Text>().text = "" + 1;
                t = Instantiate(this.gameObject);
                t.transform.SetParent(itempanel.transform);
                Destroy(this.gameObject);
                return;

            }
        }
        else return;
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        warningText = GameObject.FindGameObjectWithTag("warningtest").GetComponent<Text>();
        warningText.text = message;
        yield return new WaitForSeconds(delay);
        warningText.text = "";
    }


}
