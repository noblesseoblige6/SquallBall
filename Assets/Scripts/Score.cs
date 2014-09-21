using UnityEngine;
using System;

public class Score : MonoBehaviour {
	private int score;
	private int high_score=100;
	private int number_of_combo = 1;
	private float combo_time = 0;



	void Start(){
		this.score = 0;

	}
	//ハイスコア更新 (ゲームオーバー時にするかどうか)
	void Update(){
		if (this.score > high_score) {
			high_score = score;
			Debug.Log (this.high_score);

		}
	}

	void OnGUI(){
		// score 表示
		Vector2 scorePos = new Vector2 (Screen.width - Screen.width/3.0f, 80.0f);
		GUILayout.BeginArea(new Rect(scorePos.x, scorePos.y, 200.0f, 40.0f));
		GUILayout.Box(String.Format("Score: {0}", this.score, GUILayout.Width(100), GUILayout.Height(40)));
		GUILayout.EndArea();

		// high score 表示
		/*
		Vector2 high_scorePos = new Vector2 (Screen.width - Screen.width/3.0f, 80.0f);
		GUILayout.BeginArea(new Rect(high_scorePos.x, high_scorePos.y, 200.0f, 40.0f));
		GUILayout.Box(String.Format("Score: {0}", this.score, GUILayout.Width(100), GUILayout.Height(40)));
		GUILayout.EndArea();
		*/
	}


	//スコア加算
	public void addScore(int input){

		score = score + input*number_of_combo;

		//　前回にボールを消してから1秒以内だったら
		if ( FindObjectOfType<Clock> ().timer - combo_time < 1) {
			number_of_combo++;

		}

		//そうでなければ
		else {

			number_of_combo = 1;

		}

		//タイム更新
		combo_time = FindObjectOfType<Clock> ().timer;

	}




}
