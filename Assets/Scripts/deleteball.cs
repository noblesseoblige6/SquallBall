using UnityEngine;
using System.Collections;

public class deleteball : MonoBehaviour {

	void	OnBecameInvisible(){
		Destroy (this.gameObject);
		}
}
