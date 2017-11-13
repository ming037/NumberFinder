using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetcountDownTimer : MonoBehaviour {

    public float timeRemaining;
    public Text timeText;

    private GameObject GameManager;
    public GameObject TimeupCanvas;
    

    private void Start()
    {
		timeRemaining = 0;
		timeText.text = "penalty: " + (int)timeRemaining;
    }

    void Update () {
		if (timeRemaining > 0) timeRemaining -= Time.deltaTime;

	}
   
    private void OnGUI()
    {
        if(timeRemaining > 0)
        {
			timeText.text = "penalty: " + (int)timeRemaining;
        }
        else
		{
            //float highscore = gameObject.GetComponent<GameManage>().highScore;
            //gameObject.GetComponent<GameManage>().EndtimeText.text = "Score "+ highscore;
            //timeText.text = "시간 종료";
            //TimeupCanvas.SetActive(true);  //팝업 나오게 하기
        }
    }
}
