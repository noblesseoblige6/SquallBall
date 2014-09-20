﻿using UnityEngine;
using System.Collections;

public class DeleteRedBalls : MonoBehaviour {

	public GameObject[] RedBalls = new GameObject[3];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 同じ色のボールがぶつかったら削除
	void OnCollisionEnter2D(Collision2D collis){
		if (collis.gameObject.CompareTag ("RedBall")) {
			Debug.Log (collis.gameObject.name);

			Destroy (this.gameObject);
			Destroy (collis.gameObject);
		}
	}
}