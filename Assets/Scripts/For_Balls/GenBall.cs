using UnityEngine;
using System.Collections;

public class GenBall : MonoBehaviour {

	// ボールオブジェクトの指定
	public GameObject[] obstacles = new GameObject[9];

	// 増える間隔
	public float interval = 3.0f;

	public bool isSlowdown = false;		//スロー状態か否か
	public float nowTime = 0f;
	public float SlowdownStartTime = 0f;	//スロー状態開始時刻
	public float SlowdownEndTime = 0f;		//スロー状態終了時刻
	
	public GameObject[] blueballs;
	public GameObject[] redballs;
	public GameObject[] greenballs;



	IEnumerator Start(){

		// 無限ループ
		while (true) {
			// 乱数で生成するボールの指定
			int num = Random.Range (0,9);
			// 増えます
			Instantiate(obstacles[num], transform.position, obstacles[num].transform.rotation);
			// interval の分だけ wait
			yield return new WaitForSeconds(interval);
//			Debug.Log(this.isSlowdown);
			Debug.Log (GameObject.Find("BallGenerator").GetComponent<GenBall>().returnIsSlowdown());
			makeaccelerate ();

		}
	}

	void Update()
	{

	}

	//初期化

		void Initialize ()
		{
				this.isSlowdown = false;
				this.SlowdownEndTime = 0f;
				this.SlowdownStartTime = 0f;
				this.nowTime = 0;
		}

	void update()
	{


		}
	// isSlowDown を返す
	public bool returnIsSlowdown()
	{
		
		return this.isSlowdown;
	}

	//スロー状態開始から3秒たったらtrue を返す
	bool checkSlowdownStartTime()
	{
		if (FindObjectOfType<Clock> ().timer - this.SlowdownStartTime > 3)
			return true;
		else 
			return false;
	}

	
	//スロー状態終了から3秒たったらtrue を返す
	bool checkSlowdownEndTime()
	{
		if (FindObjectOfType<Clock> ().timer - this.SlowdownEndTime > 3)
			return true;
		else
			return false;
	}
	
	
	//SlowdownStartTime の更新
	void updateSlowdownStartTime()
	{
		this.SlowdownStartTime = FindObjectOfType<Clock> ().timer;
		
	}
	
	//SlowdownEndTime の更新
	void updateSlowdownEndTime()
	{
		this.SlowdownEndTime = FindObjectOfType<Clock> ().timer;
		
	}

	//ボールの速度を遅くする
	public void makeSlowDown()
	{

		if (!this.isSlowdown && checkSlowdownEndTime ()) {
						redballs = GameObject.FindGameObjectsWithTag ("RedBall");
						blueballs = GameObject.FindGameObjectsWithTag ("BlueBall");
						greenballs = GameObject.FindGameObjectsWithTag ("GreenBall");
		
						foreach (var e in redballs) {
								e.rigidbody2D.velocity /= 4;
						}
						foreach (var e in greenballs) {
								e.rigidbody2D.velocity /= 4;
						}	
						foreach (var e in blueballs) {
								e.rigidbody2D.velocity /= 4;
						}
				
						updateSlowdownStartTime ();
				}
		this.isSlowdown = true;

	}

	//ボールの速度を元に戻す
	public void makeaccelerate()
	{
		if (this.isSlowdown && checkSlowdownStartTime ()) {
						redballs = GameObject.FindGameObjectsWithTag ("RedBall");
						blueballs = GameObject.FindGameObjectsWithTag ("BlueBall");
						greenballs = GameObject.FindGameObjectsWithTag ("GreenBall");
		
						foreach (var e in redballs) {
								e.rigidbody2D.velocity *= 5;
						}
						foreach (var e in greenballs) {
								e.rigidbody2D.velocity *= 5;
						}	
						foreach (var e in blueballs) {
								e.rigidbody2D.velocity *= 5;
						}

						updateSlowdownEndTime ();
				}
		this.isSlowdown = false;

	}
}