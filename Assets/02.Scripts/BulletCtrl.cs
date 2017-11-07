using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour {


    public float damage = 20.0f; //총알의 데미지.

    //public Rigidbody rb;
	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody>();
        //rb.AddRelativeForce(Vector3.forward * 800.0f);
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1500.0f);//local 방향으로 힘을 가함. 800은 속도, forward는 방향.
	}
	
}
