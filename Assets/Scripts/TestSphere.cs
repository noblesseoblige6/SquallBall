﻿using UnityEngine;
using System.Collections;

public class TestSphere : MonoBehaviour
{
		public float speed = 3.0f;
		public float maxSpeed = 3.0f;
		public Vector2 direction;
		public Vector2 touchedPos;
		public bool isTouched = false;


		public bool isSlowdown = false;		//スロー状態か否か
		public float nowTime = 0f;
		public float SlowdownStartTime = 0f;	//スロー状態開始時刻
		public float SlowdownEndTime = 0f;		//スロー状態終了時刻

		public GameObject[] blueballs;
		public GameObject[] redballs;
		public GameObject[] greenballs;


		private int rnd = Random.Range (0, 2);

		// Use this for initialization
		void Start ()
		{
				//ボールを飛ばす向きが、真下にならないよう調整
				direction = new Vector2 (0, Random.Range (0, -2));

				if (rnd == 0) {
						direction.x = Random.Range (-2f, -0.1f);
				} else {
						direction.x = Random.Range (0.1f, 2f);
				}


				rigidbody2D.velocity = speed * direction;
				
		}


		// Update is called once per frame
		void Update ()
		{
				checkTouch ();

		redballs = GameObject.FindGameObjectsWithTag ("RedBall");
		blueballs = GameObject.FindGameObjectsWithTag ("BlueBall");
		greenballs = GameObject.FindGameObjectsWithTag ("GreenBall");


				//Kamada < スロー状態が1秒続いたら, スロー解除

		if (isSlowdown == true && FindObjectOfType<Clock> ().timer - SlowdownStartTime > 1) {

						isSlowdown = false;
						Time.timeScale = 1;
						SlowdownEndTime = FindObjectOfType<Clock> ().timer;

			foreach (var e in redballs) {
				e.rigidbody2D.velocity *= 3;
			}
			foreach (var e in greenballs) {
				e.rigidbody2D.velocity *= 3;
			}	
			foreach (var e in blueballs) {
				e.rigidbody2D.velocity *= 3;
			}


		}

		}

		//初期化
		void Initialize ()
		{
				this.isSlowdown = false;
				this.SlowdownEndTime = 0f;
				this.SlowdownStartTime = 0f;
				this.nowTime = 0;
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

				//Kamada < クリックされたボール以外を遅くする処理 if (スロー状態でなく,前回のスロー状態から3秒以上たっていたら <kokogaumakuikanai)
				if (isSlowdown == false && FindObjectOfType<Clock> ().timer - SlowdownEndTime > 3 && FindObjectOfType<Clock> ().timer - SlowdownStartTime> 1)
				    {


					isSlowdown = true;

					//for test: Red, Green, Blue のボールを見つけて, 速度を遅くする

					foreach (var e in redballs) {
						e.rigidbody2D.velocity *= 1/2;
					}
					foreach (var e in greenballs) {
						e.rigidbody2D.velocity *= 1/2;
					}	
					foreach (var e in blueballs) {
						e.rigidbody2D.velocity *= 1/2;
					}

				}
			}
			
			
		}		


			
	}
		//マウスボタンから離れたとき
		void onMouseUp ()
		{
				//オブジェクトをクリックしていればはじいた方向にオブジェクトを飛ばす
				if (isTouched) {
						float bias = 30.0f;
						float length = ((Vector2)Input.mousePosition - touchedPos).magnitude / bias;
						direction = ((Vector2)Input.mousePosition - touchedPos).normalized;
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
								onMouseUp ();
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
