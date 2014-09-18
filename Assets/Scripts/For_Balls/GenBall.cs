using UnityEngine;
using System.Collections;

public class GenBall : MonoBehaviour {

	// Yuki < fuyasareru object
	public GameObject obstacle;
	// Yuki < fueru interval
	public float interval = 3f;
	public float interval_first=0.1f;

	IEnumerator Start(){
		yield return new WaitForSeconds (interval_first);

		// Yuki < Infinite loop
		while (true) {
			// Yuki < Fueru
			Instantiate(obstacle, transform.position, obstacle.transform.rotation);
			// Yuki < Wait for interval
			yield return new WaitForSeconds(interval);
		}
	}




}