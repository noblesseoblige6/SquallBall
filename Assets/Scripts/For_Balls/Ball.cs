using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float speed = 3.0f;
	public float maxSpeed = 1.0f;
	public float minSpeed = 0.1f;
	public Vector2 direction;
	public Vector2 touchedPos;
	public bool isTouched = false;
	public float minVelo = 1.0f;

	public int strength;					//ボール強度
	public int chain;						//ボールを消した回数
	public GameObject num;					//読み込む Number オブジェクト

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

	}

	//	コンストラクタ 1
	public Ball()
	{
//		firstVelosity();
		strength = (int)Random.Range (2, 10);
		chain = 0;
	}

	//	コンストラクタ 2(強度を初期化する場合)
	public Ball(int firstSTR)
	{
		strength = firstSTR;
		chain = 0;

	}


	public void Initialize ()
	{
		strength = Random.Range (2, 10);
		chain = 0;
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
				this.isTouched = true;
			}
			
			
		}		

		
	}
	//マウスボタンから離れたとき
	public void OnMouseUp ()
	{
		Vector2 releasedPos = (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition);
		
		//オブジェクトをクリックしていればはじいた方向にオブジェクトを飛ばす
		if (this.isTouched) {
			float bias = 1.0f;
			float length = (releasedPos - touchedPos).magnitude * bias;
			if(length < 1.0f){
				length = 1.0f;
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

	// 画面外に出たら削除
	public void OnBecameInvisible ()
	{
		destroy ();

	}

	//速度が一定以下になったら削除フラグ
	public bool checkVelosity()
	{
		Vector2 velo;
		float v;
		velo = this.rigidbody2D.velocity;
		v = velo.x * velo.x + velo.y * velo.y;
		if (v * 100 < minVelo)		//スロー状態も考慮
			return true;
		else
			return false;
	}

	//強度が0以下になったら削除フラグ
	public bool checkStrength()
	{
		if (this.strength <= 0)
						return true;
				else
						return false;
	}

	//連鎖数が 負になったら削除フラグ
	public bool checkChain()
	{
		if (this.chain < 0)
						return true;
				else
						return false;
	}

	public void destroy()
	{
		Destroy (this.num);
		Destroy (this.gameObject);
	}

	//	削除チェック
	public bool checkDestroy()
	{
		if (checkStrength() || checkChain()) 
		{
			destroy ();
			return true;
		}
		return false;
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

	//連鎖数の更新(-1)
	public void minusChain(){
		this.chain--;
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

	public void makeSTR()
	{
		switch (this.strength) {
				case 1:
						num = (GameObject)Resources.Load ("Prefabs/Numbers/1");
						break;

				case 2:
						num = (GameObject)Resources.Load ("Prefabs/Numbers/2");
						break;

				case 3:
						num = (GameObject)Resources.Load ("Prefabs/Numbers/3");
						break;

				case 4:
						num = (GameObject)Resources.Load ("Prefabs/Numbers/4");
						break;

				case 5:
						num = (GameObject)Resources.Load ("Prefabs/Numbers/5");
						break;

				case 6:
						num = (GameObject)Resources.Load ("Prefabs/Numbers/6");
						break;
			
				case 7:
						num = (GameObject)Resources.Load ("Prefabs/Numbers/7");
						break;
			
				case 8:
						num = (GameObject)Resources.Load ("Prefabs/Numbers/8");
						break;
			
				case 9:
						num = (GameObject)Resources.Load ("Prefabs/Numbers/9");
						break;
				}
		num = (GameObject)Instantiate(num, this.gameObject.transform.position, this.gameObject.transform.rotation);

	}

}
