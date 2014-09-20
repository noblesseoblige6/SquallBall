using UnityEngine;
using System.Collections;

public class GUIRetry : MonoBehaviour {

	void OnGUI(){

		if (Event.current.type == EventType.MouseDown) {
			Application.LoadLevel("SquallBall");
		}
	}
	/*void OnGUI () {
		GUILayout.BeginArea(new Rect(Screen.width/2.0f - 10, Screen.height/2.0f + 100, 50, 70));
		if(GUILayout.Button ("Retry")) {
			Application.LoadLevel("SquallBall");		
		}
		GUILayout.EndArea();
		
	}*/
}
