using UnityEngine;
using System.Collections;

public class StartDisplay : MonoBehaviour {
	public Vector2 bottunPos;
	// Use this for initialization
	void Start () {
		bottunPos.x = Screen.width;
		bottunPos.y = Screen.height;
	}
	
	// Update is called once per frame
	void onGUI () {
		if (GUI.Button (new Rect(bottunPos.x - 30, bottunPos.y - 30, bottunPos.x + 30, bottunPos.y - 60)
		               , "Start")) {
			Application.LoadLevel("playMode");		
		}
	}
}
