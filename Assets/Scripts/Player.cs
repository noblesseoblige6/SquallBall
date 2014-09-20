using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 4.0f;
	public Vector2 direction;
	private float range = 3.0f;


	// Update is called once per frame
	void Update () {
		float x = Input.GetAxisRaw ("Horizontal");
		//方向を求める
		direction = new Vector2(x, 0).normalized;
		rigidbody2D.velocity = direction * speed;


		if (Input.GetKey ("space")) {
			//タグでRespawnとつけられたオブジェクトを取得
			GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Respawn");
			//それらの中で距離がrange以内のものはベクトルを逆にする
			foreach(GameObject obstacle in obstacles){
				Vector2 relativeVec = obstacle.transform.position - this.transform.position;
				float distance = relativeVec.magnitude;
				if(distance < range){
					print (obstacle.GetComponent<TestSphere>());
					TestSphere ballProp = obstacle.GetComponent<TestSphere>();
					ballProp.speed = ballProp.maxSpeed/distance;

					if(!ballProp.isKicked){
						ballProp.isKicked = true;
						ballProp.direction *= -1;
						ballProp.rigidbody2D.velocity = ballProp.speed * ballProp.direction;
					}
				}
			}
		}
	}
	/*
	 * OnCllisionEnter2D 
	 * プレイヤーの当たり判定を行う関数
	 */
	/*
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag("BlueBall") || other.gameObject.CompareTag("GreenBall") || other.gameObject.CompareTag("RedBall")) {
			//障害物に当たったらゲームオーバー画面に遷移
			Application.LoadLevel ("GameOver");
			}

	}
	*/
}
