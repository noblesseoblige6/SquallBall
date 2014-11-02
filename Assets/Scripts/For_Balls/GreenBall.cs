using UnityEngine;
using System.Collections;

public class GreenBall : Ball {

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
		//slowdown 状態のときは 初速度に 1/5
		if (GameObject.Find("BallGenerator").GetComponent<GenBall>().returnIsSlowdown())
		{
			this.rigidbody2D.velocity /= 5;
		}
		Initialize ();

	}
	
	// Update is called once per frame
	void Update () {
		checkTouch ();
		checkDestroy ();

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

		//相手が Red Ball で, どちらかが レイヤー8 にいたら 消える
		if (collis.gameObject.CompareTag ("RedBall") && (collis.gameObject.layer == 8 || this.gameObject.layer == 8)) {
			
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

			this.genEffect(collis);
			Destroy (this.gameObject);
			//Destroy (collis.gameObject);
		}
	}

	public void genEffect(Collision2D collis){
		//@akama GreenボールのエフェクトのPrehabを取得
		GameObject TreePrefab = (GameObject)Resources.Load ("Prefabs/Tree1");
		//当たったボールの方向から木の角度を求める
		Vector3 collidedVec = collis.gameObject.rigidbody2D.velocity.normalized;
		float dot = Vector3.Dot(collidedVec, new Vector3(0,1,0));
		float angle = Mathf.Rad2Deg*Mathf.Acos(dot);
		
		Transform effectTransform = this.gameObject.transform;
		// プレハブからインスタンスを生成
		Instantiate (TreePrefab, effectTransform.position, Quaternion.AngleAxis(angle, new Vector3(0,0,1)));

	}
}
