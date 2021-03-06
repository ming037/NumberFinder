﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

[System.Serializable] //class를 inspector에 노출시키기 위함.
public class PlayerAnim
{
    public AnimationClip Idle;
    public AnimationClip Walk;

}

public class PlayerCtrl : MonoBehaviour {
    public float speed = 10.0f;//캐릭터 속도
    public PlayerAnim playerAnim;
    public Animator anim;
    public AudioClip footstepSfx;

    public AudioSource _audio;

    [SerializeField]
    private Transform tr; //캐쉬처리 하는게 속도가 빠르다.

	private Camera cam;

    const int walknum = 10;
    private int walkcount = walknum;
    
    public GameObject popuppanel;
    private GameObject GameManager;

    // Use this for initialization
    void Start () 
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();  //걷는 소리.

    }
	
	void Update () 
    {
        /*
        if(tr.rotation.x >30 && tr.rotation.x < -30)
        {
            tr.Rotate(0, tr.rotation.y, tr.rotation.z);
        }
        Debug.Log(tr.rotation.x);
        if(tr.rotation.y > 30 && tr.rotation.y < -30)
        {
            tr.Rotate(tr.rotation.x, tr.rotation.y, 0);
        }
        */
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager = GameObject.Find("GameManager");
            GameManager.gameObject.GetComponent<GameMgr>().timeFlow = false;
            popuppanel.SetActive(true); //팝업창 켜기
            Debug.Log("end");
        }

        if (tr.position.y >= 0)
        {//넘어지지 않게 하기 
            Debug.Log("falling");
            tr.SetPositionAndRotation(new Vector3(tr.position.x, 0, tr.position.z), new Quaternion(0, tr.rotation.y, 0, tr.rotation.w));//new Quaternion(0,-88,0,0)
        }
       
        //transform.position += new Vector3(0, 0, 0.1f);
		float v= CrossPlatformInputManager.GetAxis ("Vertical");//Input.GetAxis("Vertical"); // -1.0f ~ 0.0f ~ 1.0f
		float h= CrossPlatformInputManager.GetAxis ("Horizontal");//Input.GetAxis("Horizontal");
		//float r= Input.GetAxis("Mouse X");
		float u = CrossPlatformInputManager.GetAxis ("Up");

      

        Vector3 moveDir = (Vector3.forward * v ) + (Vector3.right * h); // 백터합.
        tr.Translate(moveDir.normalized * Time.deltaTime * speed); //그냥 moveDir만 하면 대각선일때 속도가 빨라진다. normalized를 이용하면 방향만을 가져온다.(벡터의 크기를 1로 만듦).

		tr.Rotate(Vector3.up * Time.deltaTime * 100.0f * u); //up을 기준으로, 100은 회전속도.
   
        if (v != 0)
        {   
            if(walkcount == walknum)
            {
                _audio.PlayOneShot(footstepSfx, 1f); //걷는 소리
            }
            walkcount--;
            if (walkcount == 0) walkcount = walknum;

            anim.SetFloat("Speed_f",1.0f);
        }
        else
        {
            anim.SetFloat("Speed_f", 0);
        }
        
    }

    
}
