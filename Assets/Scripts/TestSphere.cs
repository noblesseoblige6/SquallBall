using UnityEngine;
using System.Collections;

public class TestSphere : MonoBehaviour {
	public float speed = 5.0f;
	public float maxSpeed = 5.0f;
	public Vector2 direction;
	public bool isKicked;
	private int rnd = Random.Range (0,2);


	// Use this for initialization
	void Start () {

		//ボールを飛ばす向きが、真下にならないよう調整
		direction = new Vector2 (0, Random.Range (0, -2));

		if (rnd == 0) {
			direction.x = Random.Range (-2f, -0.1f);
		}

		else {
			direction.x = Random.Range (0.1f, 2f);
		}



		rigidbody2D.velocity = speed * direction;
		isKicked = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void setDirection(Vector2 dirc){
		direction = dirc;
	}
}
