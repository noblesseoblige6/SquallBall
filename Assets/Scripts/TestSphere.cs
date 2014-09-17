using UnityEngine;
using System.Collections;

public class TestSphere : MonoBehaviour {
	public float speed = 3.0f;
	public float maxSpeed = 5.0f;
	public Vector2 direction;
	public bool isKicked;
	// Use this for initialization
	void Start () {
		direction = new Vector2(0.0f, -1.0f);
		isKicked = false;
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = speed * direction;
	}

	void setDirection(Vector2 dirc){
		direction = dirc;
	}
}
