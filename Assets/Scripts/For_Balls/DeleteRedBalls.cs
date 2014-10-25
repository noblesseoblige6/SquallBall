using UnityEngine;
using System.Collections;

public class DeleteRedBalls : MonoBehaviour {
	//大きさの判別用に使う予定
	public GameObject[] RedBalls = new GameObject[3];

	// 同じ色のボールがぶつかったら点を加えて削除
	void OnCollisionEnter2D(Collision2D collis){
		if (collis.gameObject.CompareTag ("RedBall") && collis.gameObject.layer == 8) {
		
			//ボール大 同士
			if(this.gameObject == RedBalls[0] && collis.gameObject == RedBalls[0])
				FindObjectOfType<Score>().addScore(20);
			//ボール中 同士
			else if(this.gameObject == RedBalls[1] && collis.gameObject == RedBalls[1])
				FindObjectOfType<Score>().addScore (30);
			//ボール小 同士
			else if(this.gameObject == RedBalls[2] && collis.gameObject == RedBalls[2])
				FindObjectOfType<Score>().addScore (50);
			else
				FindObjectOfType<Score>().addScore(10);

			Destroy (this.gameObject);
			Destroy (collis.gameObject);
		}
	}

	//Kicked のtagをたてる
	void fragKicked()
	{
		this.gameObject.tag = "KickedRedBall";
	}
}
