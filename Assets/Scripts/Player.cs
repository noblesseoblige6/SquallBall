using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 4.0f;
	public Vector2 direction;
	private float kickRange = 2.5f;


	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = direction * speed;
	}
	/*
	 * OnCllisionEnter2D 
	 * プレイヤーの当たり判定を行う関数
	 */

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag("BlueBall") || other.gameObject.CompareTag("GreenBall") || other.gameObject.CompareTag("RedBall")) {
			//障害物に当たったらゲームオーバー画面に遷移
			Application.LoadLevel ("GameOver");
			}

	}

	  //getKickRange()
	  //障害物を蹴れる範囲を返す
	public float getKickRange(){
		return kickRange;
	}
	public Vector2 getPlayerPos(){

		return transform.position;
	}
	//移動方向を与える
	public void setDirection(float x){
		direction = new Vector2(x, 0).normalized;
	}
}
