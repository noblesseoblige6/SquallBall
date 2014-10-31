using UnityEngine;
using System.Collections;

public class RedBall : Ball {

	// Use this for initialization
	void Start () {
		Initialize ();
	}

	
	// Update is called once per frame
	void Update () {
		checkTouch ();
	}

	//RedBall コンストラクタ
	public RedBall()
	{
		firstVelosity ();
		strength = 1;
		chain = 0;
	
	}

	//RedBall 初期化 GenBall.cs で複製しているため,こちらの方が好ましいか
/*
	public void Initialize()
	{
		firstVelosity ();
		strength = Random.Range (2, 10);
		chain = 0;
	}
	*/

	// Blue Ball がぶつかったら点を加えて削除
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

		else if (collis.gameObject.CompareTag ("BlueBall") && collis.gameObject.layer == 8) {
			
			//ボール大 同士
			if(this.gameObject == RedBalls[0] && collis.gameObject == BlueBalls[0])
				addScore(20);
			//ボール中 同士
			else if(this.gameObject == RedBalls[1] && collis.gameObject == BlueBalls[1])
				addScore (30);
			//ボール小 同士
			else if(this.gameObject == RedBalls[2] && collis.gameObject == BlueBalls[2])
				addScore (50);
			else
				addScore(10);
			
			Destroy (this.gameObject);
			//Destroy (collis.gameObject);
		}
	}
}
