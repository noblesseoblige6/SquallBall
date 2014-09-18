using UnityEngine;
using System.Collections;

public class Deleteball : MonoBehaviour {
	// 画面外に出たら削除
	void	OnBecameInvisible(){
		Destroy (this.gameObject);
		}
}
