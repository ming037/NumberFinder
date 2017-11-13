using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countDownTimer : MonoBehaviour {

    float timeRemaining = 180;
    public Text timeText;

    private GameObject GameManager;
    public GameObject TimeupPanel;
    

    private void Start()
    {
        timeText.text = "";
    }
    void Update () {
        GameManager = GameObject.Find("GameManager");
        if (GameManager.gameObject.GetComponent<GameMgr>().timeFlow) timeRemaining -= Time.deltaTime;
	}
   
    private void OnGUI()
    {
        if(timeRemaining > 0)
        {
            timeText.text = "Time: " + (int)timeRemaining;
        }
        else
        {
            float highscore = GameManager.gameObject.GetComponent<GameMgr>().highScore;
            GameManager = GameObject.Find("GameManager");
            GameManager.gameObject.GetComponent<GameMgr>().EndtimeText.text = "Score "+ highscore;
            timeText.text = "Finished";
            
            TimeupPanel.SetActive(true);  //팝업 나오게 하기
        }
    }
}
