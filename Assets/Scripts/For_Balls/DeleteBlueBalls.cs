using UnityEngine;
using System.Collections;

public class DeleteBlueBalls : MonoBehaviour {
	//大きさの判別用に使う予定
	public GameObject[] BlueBalls = new GameObject[3];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collis){

		//別色のボールのKicked ボールがぶつかったら, Kicked フラグをたてる
		//if (collis.gameObject.CompareTag ("KickedRedBall") || collis.gameObject.CompareTag ("KickedGreenBall")) 
		//{
		//	fragKicked();

		//}

		// 同じ色の Kicked ボールがぶつかったら点を加えて削除
		if (collis.gameObject.CompareTag ("BlueBall"))
		{

			//ボールの大きさに応じて点数に重みをつける
			//ボール大 同士
			if(this.gameObject == BlueBalls[0] && collis.gameObject == BlueBalls[0])
				FindObjectOfType<Score>().addScore(20);
			//ボール中 同士
			else if(this.gameObject == BlueBalls[1] && collis.gameObject == BlueBalls[1])
				FindObjectOfType<Score>().addScore (30);
			//ボール小 同士
			else if(this.gameObject == BlueBalls[2] && collis.gameObject == BlueBalls[2])
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
		this.gameObject.tag = "KickedBlueBall";
	}
}

