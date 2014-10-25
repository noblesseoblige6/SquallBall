using UnityEngine;
using System.Collections;

public class DeleteGreenBalls : MonoBehaviour {
	//大きさの判別用に使う予定
	public GameObject[] GreenBalls = new GameObject[3];


	void OnCollisionEnter2D(Collision2D collis){

		if(collis.gameObject.CompareTag ("GreenBall") && collis.gameObject.layer == 8) {

			//ボールの大きさに応じて点数に重みをつける予定
			//ボール大 同士
			if(this.gameObject == GreenBalls[0] && collis.gameObject == GreenBalls[0])
				FindObjectOfType<Score>().addScore(20);
			//ボール中 同士
			else if(this.gameObject == GreenBalls[1] && collis.gameObject == GreenBalls[1])
				FindObjectOfType<Score>().addScore (30);
			//ボール小 同士
			else if(this.gameObject == GreenBalls[2] && collis.gameObject == GreenBalls[2])
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
		//this.isKicked = true;
		this.gameObject.tag = "KickedGreenBall";
	}

}