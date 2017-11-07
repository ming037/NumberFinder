using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupCtrl : MonoBehaviour {

    public GameObject popuppanel;
    private GameObject GameManager;
    public InputField inputText;
    

    public void restartbtn()
    {
        GameManager = GameObject.Find("GameManager");
        saveScore(GameManager.gameObject.GetComponent<GameMgr>().highScore);//점수 저장 로직 
        SceneManager.LoadScene("NumberFinder");
    }

    public void finishbtn()
    {
        GameManager = GameObject.Find("GameManager");
        saveScore(GameManager.gameObject.GetComponent<GameMgr>().highScore);//점수 저장 로직 
        SceneManager.LoadScene("Main");
    }

    public void cancelbtn()
    {
        GameManager = GameObject.Find("GameManager");
        GameManager.gameObject.GetComponent<GameMgr>().timeFlow = true;
        popuppanel.SetActive(false);
    }

    private void saveScore(float score)
    {
        
        //score += 15; 테스트용
        const int ten = 10;
        float[] TopTen = new float[ten]; //상위 10등
        string[] TopTenName = new string[ten];
        for (int i = 0; i < ten; i++)
        {
            TopTen[i] = PlayerPrefs.GetFloat("SCORE" + i, -1); //점수 불러오기, 0은 디폴트
            TopTenName[i] = PlayerPrefs.GetString("NAME" + i, "NONE");
        }

        int index = 0;
        for (int i = 0; i < ten; i++)
        {//TopTen[0]이 제일 높은 점수
            if (TopTen[i] >= score)
            {
                index++;
 
            }
            else break;
        }
       
        for (int i = ten - 1; i > index; i--)
        {//점수 미루기
            TopTen[i] = TopTen[i - 1];
            TopTenName[i] = TopTenName[i - 1];
        }
        TopTen[index] = score;
        if(inputText.text == "") inputText.text = "unknown";
        TopTenName[index] = inputText.text;
        for (int i = 0; i < ten; i++)
        {
            PlayerPrefs.SetFloat("SCORE" + i, TopTen[i]); //점수 저장하기.
            PlayerPrefs.SetString("NAME" + i, TopTenName[i]);//이름 저장하기.
        }
    }
}
