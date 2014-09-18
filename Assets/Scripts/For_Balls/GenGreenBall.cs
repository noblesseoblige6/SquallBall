using UnityEngine;
using System.Collections;

public class GenGreenBall : MonoBehaviour {

	public GameObject obstacle;
	public float interval = 30f;
	IEnumerator Start(){
		StartCoroutine ("Spawn");
		while (true) {
			Instantiate(obstacle, clickPosition, obstacle.transform.rotation);
			yield return new WaitForSeconds(interval);
		}
	}

//	void Update(){
//		if(Input.GetMouseButton (0)){
//			clickPosition = Input.mousePosition;
//			clickPosition.z = 0f;
///			Instantiate (obstacle, clickPosition, obstacle.transform.rotation);
//		}

//	}


}