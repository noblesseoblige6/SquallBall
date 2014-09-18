using UnityEngine;
using System.Collections;

public class TestSphere : MonoBehaviour {
	public float speed = 5.0f;
	public float maxSpeed = 5.0f;
	public Vector2 direction;
	public bool isKicked;
	// Use this for initialization
	void Start () {
		direction = new Vector2(Random.Range(-1,2), Random.Range(0,-2));
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
