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
		
			//点数に重みをつける予定
			//if(this.gameObject == RedBalls[2]){
			//	FindObjectOfType<Score>().addScore(50);
			//}
			//else{
				FindObjectOfType<Score>().addScore(10);
				
			//}
			Destroy (this.gameObject);
			Destroy (collis.gameObject);
		}
	}
}
