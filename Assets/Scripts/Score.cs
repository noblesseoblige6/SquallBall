using UnityEngine;
using System;

public class Score : MonoBehaviour {
	//スコア
	private int score;
	private int high_score;
	//コンボ数
	private int number_of_combo = 0;
	private int high_noc;

	//前回ボールを消した時刻
	private float combo_time = 0;

	private string highscorekey = "highscore";
	private string highcombokey = "highcombo";


	void Start(){
		Initialize ();

	}

	void Update(){
		
		//ハイスコアの保存(ゲームオーバーの時点で保存する?)
		UpdateScores ();

		checkHighScores ();

		//1秒以上たったらコンボ数を0 に
		if (FindObjectOfType<Clock> ().timer - combo_time > 1) {
			number_of_combo = 0;		
		}
	}

	void OnGUI(){
		// score 表示
		Vector2 scorePos = new Vector2 (Screen.width - Screen.width/3.0f, 80.0f);
		GUILayout.BeginArea(new Rect(scorePos.x, scorePos.y, 200.0f, 40.0f));
		GUILayout.Box(String.Format("Score: {0}", this.score, GUILayout.Width(100), GUILayout.Height(40)));
		GUILayout.EndArea();

		// high score 表示(する?)
		Vector2 high_scorePos = new Vector2 (Screen.width - Screen.width/3.0f, 55.0f);
		GUILayout.BeginArea(new Rect(high_scorePos.x, high_scorePos.y, 200.0f, 40.0f));
		GUILayout.Box(String.Format("High Score: {0}", this.high_score, GUILayout.Width(100), GUILayout.Height(40)));
		GUILayout.EndArea();

		// コンボ数表示
		Vector2 num_comboPos = new Vector2 (Screen.width - Screen.width/3.0f, 105.0f);
		GUILayout.BeginArea(new Rect(num_comboPos.x, num_comboPos.y, 200.0f, 80.0f));
		GUILayout.Box(String.Format("Combo_num: {0}", this.number_of_combo, GUILayout.Width(100), GUILayout.Height(40)));
		GUILayout.EndArea();
	}


	//スコア加算
	public void addScore(int input){

		score = score + input + 10 *number_of_combo;

		//タイム, コンボ数更新
		number_of_combo++;
		combo_time = FindObjectOfType<Clock> ().timer;

	}

	public void checkHighScores()
	{
		if (this.score > this.high_score) {
	
			this.high_score = this.score;
		}

		if (this.number_of_combo > this.high_noc) {
		
			this.high_noc = this.number_of_combo;
		}
	}

	private void Initialize(){
		this.score = 0;
		this.number_of_combo = 0;
		this.combo_time = 0.0f;
		high_score = PlayerPrefs.GetInt (highscorekey, 100);
		high_noc = PlayerPrefs.GetInt (highcombokey, 10);
	}

	public void UpdateScores(){
		PlayerPrefs.SetInt (this.highscorekey, this.high_score);
		PlayerPrefs.SetInt (this.highcombokey, this.high_noc);
		PlayerPrefs.Save ();

	}
}
