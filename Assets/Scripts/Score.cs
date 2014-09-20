using UnityEngine;
using System;

public class Score : MonoBehaviour {
	public int score;

	void Start(){
		this.score = 0;
	}

	void OnGUI(){
		Vector2 scorePos = new Vector2 (Screen.width - Screen.width/3.0f, 80.0f);
		GUILayout.BeginArea(new Rect(scorePos.x, scorePos.y, 200.0f, 40.0f));
		GUILayout.Box(String.Format("Score: {0}", this.score, GUILayout.Width(100), GUILayout.Height(40)));
		GUILayout.EndArea();
	}

	public void addScore(int input){
		score = score + input;
	}


}
