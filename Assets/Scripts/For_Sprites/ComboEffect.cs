using UnityEngine;
using System.Collections;

public class ComboEffect : MonoBehaviour {
	void OnAnimationFinish(){
		Destroy (gameObject);
	}

}
