using UnityEngine;
using System.Collections;

public class DeleteGreenBalls : MonoBehaviour {

	public GameObject[] GreenBalls = new GameObject[3];


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	// 同じ色のボールがぶつかったら削除
	void OnCollisionEnter2D(Collision2D collis){
		if (collis.gameObject.CompareTag ("GreenBall")) {
			Debug.Log (collis.gameObject.name);
			Destroy (this.gameObject);
			Destroy (collis.gameObject);

		}
	}
}
