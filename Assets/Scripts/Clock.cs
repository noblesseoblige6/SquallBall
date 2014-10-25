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

		//Kamada < スロー状態が1秒続いたら, スロー解除
		/*
		if (this.isSlowdown == true && FindObjectOfType<Clock> ().timer - this.SlowdownStartTime > 1) {
			
			this.isSlowdown = false;
			Time.timeScale = 1;
			this.SlowdownEndTime = FindObjectOfType<Clock> ().timer;
			Debug.Log ("slow end: " + this.SlowdownEndTime);


		}
		*/
	}

	void reset(){
		//ゲームスピードの初期化
		Time.timeScale = 1;

		timer = 0.0f;
	/*
		this.isSlowdown = false;
		this.SlowdownEndTime = 0;
		this.SlowdownStartTime = 0;
		*/
	}

	//Kamada < クリックされたボール以外を遅くする処理 if (スロー状態でなく,前回のスロー状態から3秒以上たっていたら)

	/*
	 * if((isSlowdown == false && FindObjectOfType<Clock> ().timer - this.SlowdownEndTime > 3))
	{
		
		Time.timeScale = 0.33f;
		float x = FindObjectOfType<Clock> ().timer - this.SlowdownEndTime;
		
		this.isSlowdown = true;
		this.SlowdownStartTime = FindObjectOfType<Clock> ().timer;
		Debug.Log ("slow start: " + this.SlowdownStartTime);
	}
	*/

	void OnGUI(){

		Vector2 timerPos = new Vector2 (Screen.width - Screen.width/3.0f, 20.0f);
		GUILayout.BeginArea(new Rect(timerPos.x, timerPos.y, 400.0f, 40.0f));
		GUILayout.Box(String.Format("{1:00}:{2:00}:{3:00}", 
		                     Math.Floor(timer / 3600f), Math.Floor(timer / 60f), Math.Floor(timer % 60f), timer % 1 * 100), 
		              GUILayout.Width(200), GUILayout.Height(30));
		GUILayout.EndArea();


	}
}
