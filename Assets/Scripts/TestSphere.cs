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
		}
		//マウスクリックされたとき
		void OnMouseDown ()
		{

				touchedPos = (Vector2)Input.mousePosition;
				//プレイヤーとの距離をチェック
				if (checkDisPlayer (touchedPos)) {

						isTouched = true;
						rigidbody2D.velocity *= 0;
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
			//タッチ操作が終わったとき
			else if (touch.phase == TouchPhase.Ended && isTouched) {
								onMouseUp ();
						}
						
				}
		}
		//障害物とプレイヤーの距離を測る
		bool checkDisPlayer (Vector2 touchPosition)
		{
				Player player = GameObject.Find ("main").GetComponent<Player> ();
				float disObstacleAndPlayer = Mathf.Abs ((touchPosition - (Vector2)player.transform.position).magnitude);
				float kickRange = player.getKickRange ();
				
				if (disObstacleAndPlayer <= kickRange) {
						return true;
				}
				return false;
		}
}
