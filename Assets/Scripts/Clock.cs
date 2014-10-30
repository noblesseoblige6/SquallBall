using UnityEngine;
using System;

public class Clock : MonoBehaviour {
	public float timer;
//	private bool isSlowdown = false;		//スロー状態か否か
//	private float SlowdownStartTime = 0;	//スロー状態開始時刻
//	private float SlowdownEndTime = 0;		//スロー状態終了時刻

	// Use this for initialization
	void Start () {
		reset();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;


	}

	void reset(){
		//ゲームスピードの初期化
		Time.timeScale = 1;

		timer = 0.0f;

	}


	void OnGUI(){

		Vector2 timerPos = new Vector2 (Screen.width - Screen.width/3.0f, 20.0f);
		GUILayout.BeginArea(new Rect(timerPos.x, timerPos.y, 400.0f, 40.0f));
		GUILayout.Box(String.Format("{1:00}:{2:00}:{3:00}", 
		                     Math.Floor(timer / 3600f), Math.Floor(timer / 60f), Math.Floor(timer % 60f), timer % 1 * 100), 
		              GUILayout.Width(200), GUILayout.Height(30));
		GUILayout.EndArea();


	}
}
