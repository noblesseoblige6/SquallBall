using UnityEngine;
using System.Collections;

public class GenGreenBall : MonoBehaviour {

	public GameObject obj;
	public float interval = 30f;

	void Start(){
		StartCoroutine ("Spawn");
	}

	IEnumerator Spawn(){

		while (true) {
			yield return new WaitForSeconds (interval);

			Instantiate (obj, transform.position, Quaternion.identity);


		}

	}



}