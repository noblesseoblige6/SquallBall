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



	//各色の Ball がぶつかったときの処理
	void OnCollisionEnter2D(Collision2D collis){


		//相手が Green Ball 
		if (collis.gameObject.CompareTag ("GreenBall")) 
		{

			//相手も自分もレイヤー8にいたら無条件で消える
			if(this.gameObject.layer == 8 && collis.gameObject.layer == 8)
			{
				Destroy (this.gameObject);
				Destroy (collis.gameObject);
			}

			//相手だけレイヤー8にいたら自分の強度を下げる
			else if(this.gameObject.layer != 8 && collis.gameObject.layer == 8)
			{
				this.updateStrength(collis.gameObject.GetComponent<GreenBall>().returnStrength());
				//強度が一定以下ならエフェクトを出して消える
				if(this.checkDestroy()){
					this.genEffect(collis);
				}
			}

			//自分だけレイヤー8にいたら 連鎖数を-1
			else if(this.gameObject.layer == 8 && collis.gameObject.layer != 8)
			{
				this.minusChain();

			}
		}


		//相手が Blue Ball 
		if (collis.gameObject.CompareTag ("BlueBall")) 
		{
			//相手も自分もレイヤー8にいたら無条件で消える
			if(this.gameObject.layer == 8 && collis.gameObject.layer == 8)
			{
				Destroy (this.gameObject);
				Destroy (collis.gameObject);
			}
			

			
			//自分だけレイヤー8にいたら 連鎖数を+1 し, 相手を消す
			else if(this.gameObject.layer == 8 && collis.gameObject.layer != 8)
			{
				this.updateChain();
				Destroy (collis.gameObject);
			}

		}

	
			
	}
	public void genEffect (Collision2D collis)
	{
		GameObject effect;
		int chain = collis.gameObject.GetComponent<GreenBall> ().returnChain ();
		Transform effectTransform = this.gameObject.transform;

		//@akama GreenボールのエフェクトのPrehabを取得
		if (chain <= 2) {
			effect = (GameObject)Resources.Load ("Prefabs/Water1");
			effect.GetComponent<BoxCollider2D>().size.Set(1.0f, effectTransform.position.y * 0.5f);
			effect.GetComponent<BoxCollider2D>().center.Set (0.0f, effectTransform.position.y * 0.5f);
		} else if (chain <= 4) {
			effect = (GameObject)Resources.Load ("Prefabs/Water1");
			effect.GetComponent<BoxCollider2D>().size.Set(3.0f, effectTransform.position.y * 0.5f);
			effect.GetComponent<BoxCollider2D>().center.Set (0.0f, effectTransform.position.y * 0.5f);
		} else {
			effect = (GameObject)Resources.Load ("Prefabs/Water1");
			effect.GetComponent<BoxCollider2D>().size.Set(5.0f, effectTransform.position.y * 0.5f);
			effect.GetComponent<BoxCollider2D>().center.Set (0.0f, effectTransform.position.y * 0.5f);

		}
		// プレハブからインスタンスを生成
		Instantiate (effect, effectTransform.position, Quaternion.identity);
		
	}
}
