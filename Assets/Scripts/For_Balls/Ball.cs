using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float speed = 3.0f;
	public float maxSpeed = 3.0f;
	public float minSpeed = 0.1f;
	public Vector2 direction;
	public Vector2 touchedPos;
	public bool isTouched = false;
	public float minVelo = 1.0f;

	protected int strength;					//ボール強度
	protected int chain;						//ボールを消した回数

	public GameObject[] RedBalls = new GameObject[3];
	public GameObject[] GreenBalls = new GameObject[3];
	public GameObject[] BlueBalls = new GameObject[3];


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

	}

	//	コンストラクタ 1
	public Ball()
	{
		firstVelosity();
		strength = (int)Random.Range (2, 10);
		chain = 0;
	}

	//	コンストラクタ 2(強度を初期化する場合)
	public Ball(int firstSTR)
	{
		firstVelosity ();
		strength = firstSTR;
		chain = 0;

	}


	public void Initialize ()
	{
		firstVelosity ();
		strength = Random.Range (2, 10);
		chain = 0;
	}

	//	初速度の設定
	public void firstVelosity()
	{
		//ボールを飛ばす向きが、真下, 真横にならないよう調整
		
		
		//** Kamada < 速度の変化を確認するため, 初速度は一定の値にしてあります // **
		//		direction = new Vector2 (-1,-1);
		
		direction = new Vector2 (0, Random.Range (-minSpeed, -maxSpeed));
		int rnd = Random.Range (0, 1);
		if (rnd == 0) {
			direction.x = Random.Range (-maxSpeed, -minSpeed);
		} else {
			direction.x = Random.Range (minSpeed, maxSpeed);
		}
		
		rigidbody2D.velocity = speed * direction;
		
		//slowdown 状態のときは 初速度に 1/4
		if (GameObject.Find("BallGenerator").GetComponent<GenBall>().returnIsSlowdown())
		{
			rigidbody2D.velocity /= 16;
		}

	}

	//マウスクリックされたとき
	public void OnMouseDown ()
	{
		touchedPos = (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition);
		
		//プレイヤーとの距離をチェック
		if (checkDisPlayer (touchedPos)) {
			
			//@akama 蹴られればレイヤーを変更
			
			if (gameObject.CompareTag ("RedBall") ||
			    gameObject.CompareTag ("GreenBall") || 
			    gameObject.CompareTag ("BlueBall")
			    ) {
				gameObject.layer = 8;
				
				//**タッチされたら GenBall.cs 内の makeslowdown 関数を持ってきます**//
				GameObject.Find("BallGenerator").GetComponent<GenBall>().makeSlowDown();
			}
			
			
		}		

		
	}
	//マウスボタンから離れたとき
	public void OnMouseUp ()
	{
		
		Vector2 releasedPos = (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition);
		
		//オブジェクトをクリックしていればはじいた方向にオブジェクトを飛ばす
		if (this.isTouched) {
			float bias = 5.0f;
			float length = (releasedPos - touchedPos).magnitude * bias;
			if(length < 3.0f){
				length = 3.0f;
			}
			Vector2 direction = (releasedPos - touchedPos).normalized;
			rigidbody2D.velocity = length * direction;
			isTouched = false;
		}
	}
	
	public void setDirection (Vector2 dirc)
	{
		direction = dirc;
	}
	
	//タッチされているかチェックする
	public void checkTouch ()
	{
		if (Input.touchCount > 0) {
			
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {
				Vector2 point = touch.position;
				RaycastHit hit = new RaycastHit ();
				Ray ray = Camera.main.ScreenPointToRay (point);
				//MainCameraが設定されていない場合に現在使っているカメラを使う
				if (Camera.main == null) {
					ray = Camera.current.ScreenPointToRay (point);
				}
				//オブジェクトにあたっているか判定
				if (Physics.Raycast (ray, out hit)) {
					//あたっていればそのオブジェクトのOnMouseDownを呼び出す
					
					OnMouseDown ();
					
				}
			} 
			//@akamaタッチ操作が終わったとき
			else if (touch.phase == TouchPhase.Ended && isTouched) {
				OnMouseUp ();
			}
			
		}
	}
	//障害物とプレイヤーの距離を測る
	public bool checkDisPlayer (Vector2 touchPosition)
	{
		Player player = GameObject.Find ("main").GetComponent<Player> ();
		Vector2 playerPos = player.getPlayerPos ();
		//タッチした位置とplayerの距離を計算
		float disObstacleAndPlayer = (touchPosition - playerPos).magnitude;
		//障害物を蹴れる半径を計算		
		float kickRange = player.getKickRange ();
		
		//範囲内であれば蹴れる
		
		if (disObstacleAndPlayer < kickRange) {
			return true;
		}
		return false;
	}
	//他のオブジェクトと衝突したとき
	/*
	public void OnCollisionEnter2D (Collision2D other)
	{
		//自分が蹴られている場合かつ相手が蹴られていない		
		if (this.gameObject.layer == 8 && other.gameObject.layer == 0) {
			//相手を蹴られていることにする			
			if (other.gameObject.CompareTag ("RedBall") ||
			    other.gameObject.CompareTag ("GreenBall") ||
			    other.gameObject.CompareTag ("BlueBall")) {
				other.gameObject.layer = 8;
			}	
		}
		
	}
*/
	// 画面外に出たら削除
	public void OnBecameInvisible ()
	{
		Destroy (this.gameObject);
	}

	//速度が一定以下になったら削除フラグ
	public bool checkVelosity()
	{
		Vector2 velo;
		float v;
		velo = this.rigidbody2D.velocity;
		v = velo.x * velo.x + velo.y * velo.y;
		if (v * 16 < minVelo)		//スロー状態も考慮
			return true;
		else
			return false;
	}
	
	//	削除チェック
	public void 	checkDestroy()
	{
		if (checkVelosity ()) 
		{
			Destroy (this.gameObject);
			
		}
		
	}
	
	//	強度を返す
	public int returnStrength(){

		return this.strength;
	}

	//	強度の減衰
	public void updateStrength(int vsStrength){
		this.strength -= vsStrength;

	}

	//連鎖数を返す
	public int returnChain(){
		return this.chain;
	
	}

	//連鎖数の更新
	public void updateChain(){
		this.chain++;
	}

	//score 加算
	public void addScore(int score)
	{
		FindObjectOfType<Score>().addScore (score);

	}

	//自分のボールの種類をチェック
	public bool checkThisBall(int kind)
	{
		if (this.gameObject == FindObjectOfType<GenBall> ().GetComponent<GenBall> ().obstacles [kind]) 
			return true;
	

		else
			return false;
	}

	//相手のボールの種類チェック
	public bool checkCollisBall(Collision2D collis, int kind)
	{
		if (collis.gameObject == FindObjectOfType<GenBall> ().GetComponent<GenBall> ().obstacles [kind]) 
			return true;
		

		else 
			return false;
	}
}
