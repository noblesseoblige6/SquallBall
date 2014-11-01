using UnityEngine;
using System.Collections;

public class DeleteGreenBalls : MonoBehaviour {
	//大きさの判別用に使う予定
	public GameObject[] GreenBalls = new GameObject[3];


	void OnCollisionEnter2D(Collision2D collis){

		if(collis.gameObject.CompareTag ("RedBall") && collis.gameObject.layer == 8) {

			//ボールの大きさに応じて点数に重みをつける予定
			//ボール大 同士
			if(this.gameObject == FindObjectOfType<GenBall>().GetComponent<GenBall>().obstacles[3] && collis.gameObject == FindObjectOfType<GenBall>().GetComponent<GenBall>().obstacles[6])
				FindObjectOfType<Score>().addScore(20);
			//ボール中 同士
			else if(this.gameObject == FindObjectOfType<GenBall>().GetComponent<GenBall>().obstacles[4] && collis.gameObject == FindObjectOfType<GenBall>().GetComponent<GenBall>().obstacles[7])
				FindObjectOfType<Score>().addScore (30);
			//ボール小 同士
			else if(this.gameObject == FindObjectOfType<GenBall>().GetComponent<GenBall>().obstacles[5] && collis.gameObject == FindObjectOfType<GenBall>().GetComponent<GenBall>().obstacles[8])
				FindObjectOfType<Score>().addScore (50);
			else
				FindObjectOfType<Score>().addScore(10);

			Destroy (this.gameObject);
			//Destroy (collis.gameObject);

		}
	}


}