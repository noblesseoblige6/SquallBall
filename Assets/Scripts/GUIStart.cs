using UnityEngine;
using System.Collections;

public class GUIStart : MonoBehaviour {
	void OnGUI () {
		GUILayout.BeginArea(new Rect(Screen.width/2.0f - 10, Screen.height/2.0f + 100, 50, 70));
		if(GUILayout.Button ("Start")) {
				Application.LoadLevel("SquallBall");		
		}
		GUILayout.EndArea();

	}
}
