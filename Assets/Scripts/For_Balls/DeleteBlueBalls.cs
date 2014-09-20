using UnityEngine;
using System.Collections;

public class DeleteBlueBalls : MonoBehaviour {

	public GameObject[] BlueBalls = new GameObject[3];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 同じ色のボールがぶつかったら削除
	void OnCollisionEnter2D(Collision2D collis){
		if (collis.gameObject.CompareTag ("BlueBall")) {
			Debug.Log (collis.gameObject.name);

			Destroy (this.gameObject);
			Destroy (collis.gameObject);

		}
	}
}

