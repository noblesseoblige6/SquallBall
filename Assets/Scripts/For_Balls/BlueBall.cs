using UnityEngine;
using System.Collections;

public class BlueBall : Ball {

	// Use this for initialization
	void Start () {
		//ボールを飛ばす向きが、真下, 真横にならないよう調整
		
		
		//** Kamada < 速度の変化を確認するため, 初速度は一定の値にしてあります // **
		//		direction = new Vector2 (-1,-1);
		
		direction = new Vector2 (0, Random.Range (-minSpeed, -maxSpeed));
		int rnd = Random.Range (0, 1);
		if (rnd == 0) {
			direction.x = Random.Range (-maxSpeed, -minSpeed);
		} else {
			direction.x = Random.Range (minSpeed, maxSpeed);
		}
		
		this.rigidbody2D.velocity = speed * direction;
		
		//slowdown 状態のときは 初速度に 1/16
		if (GameObject.Find("BallGenerator").GetComponent<GenBall>().returnIsSlowdown())
		{
			this.rigidbody2D.velocity /= 16;
		}	
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		checkTouch ();
		checkDestroy ();
	}



	// Green Ball がぶつかったら点を加えて削除
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

		//相手が Green Ball で, どちらかが レイヤー8 にいたら 消える
		if (collis.gameObject.CompareTag ("GreenBall") && (collis.gameObject.layer == 8|| this.gameObject.layer == 8)) {
			
			//ボール大 同士
			if(checkThisBall(0) && checkCollisBall(collis, 3))
				addScore(20);
			//ボール中 同士
			else if(checkThisBall(1) && checkCollisBall(collis, 4))
				addScore (30);
			//ボール小 同士
			else if(checkThisBall(2) && checkCollisBall(collis, 5))
				addScore (50);
			else
				addScore(10);

			Destroy (this.gameObject);
			//Destroy (collis.gameObject);
		}
	}
}
