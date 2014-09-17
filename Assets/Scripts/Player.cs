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
			GameObject nearBall = GameObject.Find("GreenMiddleBall");
			Vector2 relativeVec = nearBall.transform.position - this.transform.position;
			float distance = relativeVec.magnitude;
			if(distance < range){
				TestSphere ballProp = nearBall.GetComponent<TestSphere>();
				ballProp.speed = ballProp.maxSpeed/distance;

				if(!ballProp.isKicked){
					ballProp.isKicked = true;
					ballProp.direction *= -1;
				}
			}
		}
	}
}
