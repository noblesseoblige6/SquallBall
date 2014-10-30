using UnityEngine;
using System.Collections;

public class TestSphere : MonoBehaviour
{
		public float speed = 3.0f;
		public float maxSpeed = 3.0f;
		public Vector2 direction;
		public Vector2 touchedPos;
		public bool isTouched = false;



		private int rnd = Random.Range (0, 2);

		// Use this for initialization
		void Start ()
		{
		//ボールを飛ばす向きが、真下, 真横にならないよう調整


		//** Kamada < 速度の変化を確認するため, 初速度は一定の値にしてあります // **
//		direction = new Vector2 (-1,-1);

		direction = new Vector2 (0, Random.Range (-0.1f, -2));

				if (rnd == 0) {
						direction.x = Random.Range (-2f, -0.1f);
				} else {
						direction.x = Random.Range (0.1f, 2f);
				}

				rigidbody2D.velocity = speed * direction;

		//slowdown 状態のときは 初速度に 1/4
		if (GameObject.Find("BallGenerator").GetComponent<GenBall>().returnIsSlowdown())
		    	 {
					rigidbody2D.velocity /= 4;
				}
		}


		// Update is called once per frame
		void Update ()
		{
				checkTouch ();



		}



		//マウスクリックされたとき
		void OnMouseDown ()
		{
		touchedPos = (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition);

		//プレイヤーとの距離をチェック
				if (checkDisPlayer (touchedPos)) {

			//@akama 蹴られればレイヤーを変更

				if (gameObject.CompareTag ("RedBall") ||
						gameObject.CompareTag ("GreenBall") || 
						gameObject.CompareTag ("BlueBall")
		   ) {
						gameObject.layer = 8;

				//**タッチされたら GenBall.cs 内の makeslowdown 関数を持ってきます**//
				GameObject.Find("BallGenerator").GetComponent<GenBall>().makeSlowDown();
			}
			
			
		}		


			
	}
		//マウスボタンから離れたとき
		void OnMouseUp ()
		{
		
		Vector2 releasedPos = (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition);

		//オブジェクトをクリックしていればはじいた方向にオブジェクトを飛ばす
				if (this.isTouched) {
						float bias = 5.0f;
						float length = (releasedPos - touchedPos).magnitude * bias;
						if(length < 3.0f){
							length = 3.0f;
						}
						Vector2 direction = (releasedPos - touchedPos).normalized;
						rigidbody2D.velocity = length * direction;
						isTouched = false;
				}
		}

		void setDirection (Vector2 dirc)
		{
				direction = dirc;
		}

		//タッチされているかチェックする
		void checkTouch ()
		{
				if (Input.touchCount > 0) {

						Touch touch = Input.GetTouch (0);
						if (touch.phase == TouchPhase.Began) {
								Vector2 point = touch.position;
								RaycastHit hit = new RaycastHit ();
								Ray ray = Camera.main.ScreenPointToRay (point);
								//MainCameraが設定されていない場合に現在使っているカメラを使う
								if (Camera.main == null) {
										ray = Camera.current.ScreenPointToRay (point);
								}
								//オブジェクトにあたっているか判定
								if (Physics.Raycast (ray, out hit)) {
										//あたっていればそのオブジェクトのOnMouseDownを呼び出す
															
										OnMouseDown ();
										
								}
						} 
			//@akamaタッチ操作が終わったとき
			else if (touch.phase == TouchPhase.Ended && isTouched) {
								OnMouseUp ();
						}
						
				}
		}
		//障害物とプレイヤーの距離を測る
		public bool checkDisPlayer (Vector2 touchPosition)
		{
				Player player = GameObject.Find ("main").GetComponent<Player> ();
				Vector2 playerPos = player.getPlayerPos ();
				//タッチした位置とplayerの距離を計算
				float disObstacleAndPlayer = (touchPosition - playerPos).magnitude;
				//障害物を蹴れる半径を計算		
				float kickRange = player.getKickRange ();
				
				//範囲内であれば蹴れる

				if (disObstacleAndPlayer < kickRange) {
						return true;
				}
				return false;
		}
	//他のオブジェクトと衝突したとき
		void OnCollisionEnter2D (Collision2D other)
		{
		//自分が蹴られている場合かつ相手が蹴られていない		
		if (this.gameObject.layer == 8 && other.gameObject.layer == 0) {
			//相手を蹴られていることにする			
			if (other.gameObject.CompareTag ("RedBall") ||
								other.gameObject.CompareTag ("GreenBall") ||
								other.gameObject.CompareTag ("BlueBall")) {
								other.gameObject.layer = 8;
						}	
				}

		}


}
