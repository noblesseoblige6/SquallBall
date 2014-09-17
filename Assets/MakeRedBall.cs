using UnityEngine;
using System.Collections;

public class MakeRedBall : MonoBehaviour {
	// // Kamada < prepare for making balls
	public 	GameObject 		RedBall;
	int		num		=		0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		num++;

		if (num == 60) {
			num = 0;			
			Instantiate (this.RedBall);

		}
	}

		

	


}
