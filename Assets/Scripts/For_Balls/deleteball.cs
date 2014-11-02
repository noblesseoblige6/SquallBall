using UnityEngine;
using System.Collections;

public class Deleteball : MonoBehaviour {
	private float minVelo = 1.0f;

	void UpDate()
	{
		checkDestroy ();
	
	}


	// 画面外に出たら削除
	void OnBecameInvisible ()
	{
		Destroy (this.gameObject);
	}

	//速度が一定以下になったら削除フラグ
	bool checkVelosity()
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
	void 	checkDestroy()
	{
		if (checkVelosity ()) 
		{
			Destroy (this.gameObject);

		}

	}
}
