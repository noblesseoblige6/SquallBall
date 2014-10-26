using UnityEngine;
using System.Collections;

public class GUIArrowButton : MonoBehaviour {
	private bool isPushed;
	private GameObject player;

	//起動時に実行
	void Start(){
		isPushed = false;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	void OnMouseDown(){
		isPushed = true;
	}
	void OnMouseUp(){
		isPushed = false;
		player.GetComponent<Player>().setDirection(0);
	}
	void OnMouseOver(){
		//クリックされたボタンのタグがRightButtonの場合
		if (isPushed && gameObject.CompareTag("RightButton")) {
			player.GetComponent<Player>().setDirection(1);
		}
		else if (isPushed && gameObject.CompareTag("LeftButton")) {
			player.GetComponent<Player>().setDirection(-1);
		}
	}
}
