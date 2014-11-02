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


		//相手が Red Ball で, レイヤー8 にいたら 自分の強度を弱める 点数は後で考える
		if ((collis.gameObject.layer == 8) && collis.gameObject.CompareTag ("RedBall")) 
		{

			//自分もレイヤー8にいたら無条件で消える
			if(this.gameObject.layer == 8)
			{
				Destroy (this.gameObject);
				collis.gameObject.GetComponent<RedBall>().updateChain();

			}

			else
			{
				this.updateStrength(collis.gameObject.GetComponent<RedBall>().returnStrength());

				if(this.returnStrength() <= 0)
					collis.gameObject.GetComponent<RedBall>().updateChain();
			}
		}


	}
}
