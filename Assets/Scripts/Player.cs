using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 4.0f;
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxisRaw ("Horizontal");
		//方向を求める
		Vector2 direction = new Vector2(x, 0).normalized;
		rigidbody2D.velocity = direction * speed;
	}
}
