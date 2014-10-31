using UnityEngine;
using System.Collections;

public class GenBall : MonoBehaviour {

	// ボールオブジェクトの指定
	public GameObject[] obstacles = new GameObject[9];

	// 増える間隔
	public float interval = 3.0f;

	public bool isSlowdown = false;		//スロー状態か否か
	public float SlowdownStartTime = -3.0f;	//スロー状態開始時刻
	public float SlowdownEndTime = -3.0f;		//スロー状態終了時刻
	
	public GameObject[] blueballs;
	public GameObject[] redballs;
	public GameObject[] greenballs;



	IEnumerator Start(){

		Initialize ();
		// 無限ループ
		while (true) {
			// 乱数で生成するボールの指定
			int num = Random.Range (0,9);
			// 増えます
			Instantiate(obstacles[num], transform.position, obstacles[num].transform.rotation);
			// interval の分だけ wait
			yield return new WaitForSeconds(interval);

		}
	}

	void Update()
	{
			makeaccelerate ();
				
	}

	//初期化
		void Initialize ()
		{
				this.isSlowdown = false;
				this.SlowdownEndTime = -3.0f;
				this.SlowdownStartTime = -3.0f;
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
		if (FindObjectOfType<Clock> ().timer - this.SlowdownStartTime > 3.0f)
			return true;
		else 
			return false;
	}

	
	//スロー状態終了から3秒たったらtrue を返す
	bool checkSlowdownEndTime()
	{
		if (FindObjectOfType<Clock> ().timer - this.SlowdownEndTime > 3.0f)
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
								e.rigidbody2D.velocity /= 16;
						}
						foreach (var e in greenballs) {
								e.rigidbody2D.velocity /= 16;
						}	
						foreach (var e in blueballs) {
								e.rigidbody2D.velocity /= 16;
						}
				
						updateSlowdownStartTime ();
				
						this.isSlowdown = true;
				}
	}

	//ボールの速度を元に戻す
	public void makeaccelerate()
	{
		//SlowdownEnd, Start Time と, isSlowdown の確認をして, accerelate
		if (checkSlowdownEndTime () && checkSlowdownStartTime () && this.isSlowdown) {

						redballs = GameObject.FindGameObjectsWithTag ("RedBall");
						blueballs = GameObject.FindGameObjectsWithTag ("BlueBall");
						greenballs = GameObject.FindGameObjectsWithTag ("GreenBall");
		
						foreach (var e in redballs) {
								e.rigidbody2D.velocity *= 16;
						}
						foreach (var e in greenballs) {
								e.rigidbody2D.velocity *= 16;
						}	
						foreach (var e in blueballs) {
								e.rigidbody2D.velocity *= 16;
						}

						updateSlowdownEndTime ();
						this.isSlowdown = false;
				}
	}
}