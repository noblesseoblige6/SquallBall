using UnityEngine;
using System.Collections;

public class PlayerRange : MonoBehaviour {
	private Player player;
	private Vector2 playerPos;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("main").GetComponent<Player> ();
		playerPos = player.getPlayerPos();
		this.transform.position = playerPos;

		//プレイヤーの弾ける範囲に基づいて画像をスケール
		float imageSize = 600f; //画像のサイズ
		float offset = 200f; //画像サイズに基づいてスケールするための定数
		float playerRange = 200*player.getKickRange () / imageSize;
		this.transform.localScale = new Vector3 (playerRange, playerRange, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		//@comment プレイヤーの動きに合わせて動く
		player = GameObject.Find("main").GetComponent<Player> ();
		playerPos = player.transform.position;
		this.transform.position = playerPos;
	}
}
