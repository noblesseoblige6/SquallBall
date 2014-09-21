using UnityEngine;
using System.Collections;

public class TestSphere : MonoBehaviour
{
		public float speed = 5.0f;
		public float maxSpeed = 5.0f;
		public Vector2 direction;
		public Vector2 touchedPos = new Vector2 ();
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
				rigidbody2D.velocity *= 0;
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
										touchedPos = point;
										//あたっていればそのオブジェクトのOnMouseDownを呼び出す
										//hit.transform.gameObject.SendMessage ("OnMouseDown");
										OnMouseDown();
								}
								if (touch.phase == TouchPhase.Ended) {
										direction = touch.position - touchedPos;
										rigidbody2D.velocity = speed * direction;

								}
						}
				}
		}

}
