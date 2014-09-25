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

	// 同じ色のボールがぶつかったら点を加えて削除
	void OnCollisionEnter2D(Collision2D collis){
		if (collis.gameObject.CompareTag ("BlueBall")) {
			Debug.Log (collis.gameObject.name);

			//ボールの大きさに応じて点数に重みをつける
			if(this.gameObject == BlueBalls[0] && collis.gameObject == BlueBalls[0])
				FindObjectOfType<Score>().addScore(20);
			else if(this.gameObject == BlueBalls[1] && collis.gameObject == BlueBalls[1])
				FindObjectOfType<Score>().addScore (30);
			else if(this.gameObject == BlueBalls[2] && collis.gameObject == BlueBalls[2])
				FindObjectOfType<Score>().addScore (50);
			else
				FindObjectOfType<Score>().addScore(10);



			Destroy (this.gameObject);
			Destroy (collis.gameObject);

		}
	}
}

