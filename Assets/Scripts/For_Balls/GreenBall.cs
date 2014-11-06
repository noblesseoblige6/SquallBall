using UnityEngine;
using System.Collections;

public class GreenBall : Ball
{

		// Use this for initialization
		void Start ()
		{
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
				if (GameObject.Find ("BallGenerator").GetComponent<GenBall> ().returnIsSlowdown ()) {
						this.rigidbody2D.velocity /= 5;
				}
				Initialize ();
		makeSTR ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				checkTouch ();
				checkDestroy ();
				moveSTR ();
		}

		
	//各色の Ball がぶつかったときの処理
	void OnCollisionEnter2D(Collision2D collis){
		
		
		//相手が Red Ball 
		if (collis.gameObject.CompareTag ("RedBall")) 
		{
			
			//相手も自分もレイヤー8にいたら無条件で消える
			if(this.gameObject.layer == 8 && collis.gameObject.layer == 8)
			{
				destroy ();
				collis.gameObject.GetComponent<RedBall>().destroy();
			}
			
			//相手だけレイヤー8にいたら自分の強度を下げる
			else if(this.gameObject.layer != 8 && collis.gameObject.layer == 8)
			{
				this.updateStrength(collis.gameObject.GetComponent<RedBall>().returnStrength());
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
		
		
		//相手が Green Ball 
		if (collis.gameObject.CompareTag ("GreenBall")) 
		{
			//相手も自分もレイヤー8にいたら無条件で消える
			if(this.gameObject.layer == 8 && collis.gameObject.layer == 8)
			{
				destroy ();
				collis.gameObject.GetComponent<GreenBall>().destroy();
			}
			
			
			
			//自分だけレイヤー8にいたら 連鎖数を+1 し, 相手を消す
			else if(this.gameObject.layer == 8 && collis.gameObject.layer != 8)
			{
				this.updateChain();
				collis.gameObject.GetComponent<GreenBall>().destroy();
			}
			
		}
		
		
		
	}

	public void genEffect (Collision2D collis)
	{
		GameObject effect;
		int chain = collis.gameObject.GetComponent<RedBall> ().returnChain ();
		
		//@akama GreenボールのエフェクトのPrehabを取得
		if (chain <= 2) {
			effect = (GameObject)Resources.Load ("Prefabs/Tree1");
		} else if (chain <= 4) {
			effect = (GameObject)Resources.Load ("Prefabs/Tree1");
		} else {
			effect = (GameObject)Resources.Load ("Prefabs/Tree1");
		}
		//当たったボールの方向から木の角度を求める
		Vector3 collidedVec = collis.gameObject.rigidbody2D.velocity.normalized;
		float dot = Vector3.Dot (collidedVec, new Vector3 (0, 1, 0));
		float angle = Mathf.Rad2Deg * Mathf.Acos (dot);
		
		Transform effectTransform = this.gameObject.transform;
		// プレハブからインスタンスを生成
		Instantiate (effect, effectTransform.position, Quaternion.AngleAxis (angle, new Vector3 (0, 0, 1)));
		
	}
}
