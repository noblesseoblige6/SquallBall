using UnityEngine;
using System;

public class Clock : MonoBehaviour {
	public float timer;

	// Use this for initialization
	void Start () {
		reset();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
	}

	void reset(){

		timer = 0.0f;
	}
	void OnGUI(){
		Vector2 timerPos = new Vector2 (Screen.width - 500.0f, 20.0f);
		GUILayout.BeginArea(new Rect(timerPos.x, timerPos.y, timerPos.x+400.0f, timerPos.y+40.0f));
		GUILayout.Box(String.Format("{1:00}:{2:00}:{3:00}", 
		                     Math.Floor(timer / 3600f), Math.Floor(timer / 60f), Math.Floor(timer % 60f), timer % 1 * 100), 
		              GUILayout.Width(200), GUILayout.Height(40));
		GUILayout.EndArea();


	}
}
