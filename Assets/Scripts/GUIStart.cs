using UnityEngine;
using System.Collections;

public class GUIStart : MonoBehaviour {
	void Update () {
		for (int i = 0; i < Input.touchCount; i++) {
						// タッチ情報を取得する
						Touch touch = Input.GetTouch (i);

						if (touch.phase == TouchPhase.Began) {
								Application.LoadLevel ("SquallBall");	
						}
				}
	}
}
