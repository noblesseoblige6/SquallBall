using UnityEngine;
using System.Collections;

public class ComboEffect : MonoBehaviour {
	const int RedEffectLayer = 11;
	const int GreenEffectLayer = 12;
	const int BlueEffectLayer = 13;

	void OnAnimationFinish(){
		//Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collision2D collis){
				Destroy (collis.gameObject);
		}
}
