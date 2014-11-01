﻿using UnityEngine;
using System.Collections;

public class GreenBall : Ball {

	// Use this for initialization
	void Start () {
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		checkTouch ();

	}

	//GreenBall コンストラクタ
	public GreenBall()
	{
		firstVelosity ();
		strength = 1;
		chain = 0;
		
	}

	
	// Red Ball がぶつかったら点を加えて削除
	void OnCollisionEnter2D(Collision2D collis){

		//自分が蹴られている場合かつ相手が蹴られていない		
		if (this.gameObject.layer == 8 && collis.gameObject.layer == 0) {
			//相手を蹴られていることにする			
			if (collis.gameObject.CompareTag ("RedBall") ||
			    collis.gameObject.CompareTag ("GreenBall") ||
			    collis.gameObject.CompareTag ("BlueBall")) {
				collis.gameObject.layer = 8;
			}	
		}
		
		else if (collis.gameObject.CompareTag ("RedBall") && collis.gameObject.layer == 8) {
			
			//ボール大 同士
			if(checkThisBall(3) && checkCollisBall(collis, 6))
				addScore(20);
			//ボール中 同士
			else if(checkThisBall(4) && checkCollisBall(collis, 7))
				addScore (30);
			//ボール小 同士
			else if(checkThisBall(5) && checkCollisBall(collis, 8))
				addScore (50);
			else
				addScore(10);
			
			Destroy (this.gameObject);
			//Destroy (collis.gameObject);
		}
	}
}
