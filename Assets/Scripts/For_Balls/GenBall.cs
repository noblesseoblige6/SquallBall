using UnityEngine;
using System.Collections;

public class GenBall : MonoBehaviour {

	// ボールオブジェクトの指定
	public GameObject[] obstacles = new GameObject[9];

	// 増える間隔
	public float interval = 3f;

	IEnumerator Start(){

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




}