using UnityEngine;
using System.Collections;

public class Deleteball : MonoBehaviour {

	void	OnBecameInvisible(){
		Destroy (this.gameObject);
		}
}
