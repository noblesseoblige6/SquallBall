using UnityEngine;
using System.Collections;

public class GenGreenBall : MonoBehaviour {

	// Yuki < fuyasareru object
	public GameObject obstacle;
	// Yuki < fueru interval
	public float interval = 3f;


	IEnumerator Start(){
		// Yuki < Infinite loop
		while (true) {
			// Yuki < Fueru
			Instantiate(obstacle, transform.position, obstacle.transform.rotation);
			// Yuki < Wait for interval
			yield return new WaitForSeconds(interval);
		}
	}




}