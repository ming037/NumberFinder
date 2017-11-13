using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCtrl : MonoBehaviour {

    private int hitCount = 0; // 누적 hit.
    public Texture[] textures;
    public MeshRenderer _renderer;


    void Start()
    {
		//int idx = Random.Range(0, textures.Length);
		/*
        _renderer = GetComponentInChildren<MeshRenderer>();
        _renderer.material.mainTexture = textures[idx];
        */
    }

	

    public void OnDamage(Vector3 pos) //raycast로 쏠 때 데미지 입음
    {
        if (++hitCount == 1)
        {
            InactiveItem();//아이템 비활성.
        }
    }


    void InactiveItem()
    {//아이템 비활성.
     //Rigidbody rb = this.gameObject.AddComponent<Rigidbody>(); //Ridgidbody 컴포넌트가 동적으로 추가되고 리턴 함.
     //rb.AddForce(Vector3.up * 1200.0f); //world좌표계 기준, 위로 올라감.
     //Destroy(this.gameObject, 0.2f); 
     //GetComponent<BoxCollider>().enabled = false;
        hitCount = 0;
        Invoke("ReturnPooling", 0.1f); // 0.1초 있다가 몬스터 회수 + 풀링 복구
		
		  
    }

	void ReturnPooling()
	{
		//GetComponent<BoxCollider>().enabled = true;
		this.gameObject.SetActive(false);//다시 비활성화
	}

}




