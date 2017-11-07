using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[ExecuteInEditMode] //이거 쓰면 런 안해도 버튼이 보임
public class UIMgr : MonoBehaviour
{

    float[] TopTen = new float[10]; //상위 10등
    string[] TopTenName = new string[ten]; //이름
    const int ten = 10;
    public GameObject RankCanvas;
    public GameObject BoardCanvas;
    public GameObject buttonPanel;

    private void Start()
    {
        RankCanvas.SetActive(false);
        buttonPanel.SetActive(false);
    }

    public void OnStartBtnClick()
    {
        SceneManager.LoadScene("NumberFinder", LoadSceneMode.Single); //LoadScene는 열려있는 씬을 지우고 여는 것 
    }

    public void OnRankBtnClick()
    {
        loadScore(); //점수 불러오기

        int i = 0;
        BoardCanvas.SetActive(false);
        buttonPanel.SetActive(true);
        RankCanvas.SetActive(true);
        foreach (Transform child in RankCanvas.transform)
        {
            child.GetComponent<Text>().text = TopTenName[i]+" " + TopTen[i].ToString("00000");
            if (child.GetComponent<Text>().text == "NONE -00001") child.GetComponent<Text>().text = "NONE";
            //Debug.Log(i + ":" + TopTen[i]);
            i++;
        }
    }

    public void OnOkBtnClick()
    {
        BoardCanvas.SetActive(true);
        buttonPanel.SetActive(false);
        RankCanvas.SetActive(false);
    }

    public void OnEndBtnClick()
    {
        Application.Quit();
    }

    private void loadScore()
    {     
        for (int i = 0; i < ten; i++)
        {
           // Debug.Log(PlayerPrefs.GetFloat("SCORE" + i, 0.0f));
            TopTen[i] = PlayerPrefs.GetFloat("SCORE" + i, -1); //점수 불러오기, 0은 디폴트
            TopTenName[i] = PlayerPrefs.GetString("NAME" + i, "NONE");
        }
    }
}


