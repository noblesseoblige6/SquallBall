using UnityEngine;
using System.Collections;

public class DeleteRedBalls : MonoBehaviour {
	//大きさの判別用に使う予定
	public GameObject[] RedBalls = new GameObject[3];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 同じ色のボールがぶつかったら点を加えて削除
	void OnCollisionEnter2D(Collision2D collis){
		if (collis.gameObject.CompareTag ("RedBall")) {
			Debug.Log (collis.gameObject.name);
		
			if(this.gameObject == RedBalls[2] && collis.gameObject == RedBalls[2])
				FindObjectOfType<Score>().addScore(20);
			else if(this.gameObject == RedBalls[1] && collis.gameObject == RedBalls[1])
				FindObjectOfType<Score>().addScore (30);
			else if(this.gameObject == RedBalls[0] && collis.gameObject == RedBalls[0])
				FindObjectOfType<Score>().addScore (50);
			else
				FindObjectOfType<Score>().addScore(10);

			Destroy (this.gameObject);
			Destroy (collis.gameObject);
		}
	}
}
