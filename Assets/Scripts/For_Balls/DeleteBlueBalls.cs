﻿using UnityEngine;
using System.Collections;

public class DeleteBlueBalls : MonoBehaviour {
	//大きさの判別用に使う予定
	public GameObject[] BlueBalls = new GameObject[3];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 同じ色のボールがぶつかったら点を加えて削除
	void OnCollisionEnter2D(Collision2D collis){
		if (collis.gameObject.CompareTag ("BlueBall")) {
			Debug.Log (collis.gameObject.name);
			FindObjectOfType<Score>().addScore(50);

			Destroy (this.gameObject);
			Destroy (collis.gameObject);

		}
	}
}

