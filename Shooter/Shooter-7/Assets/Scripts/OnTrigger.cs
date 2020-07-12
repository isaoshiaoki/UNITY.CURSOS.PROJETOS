using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour {


	//ignora os triggers
	void OnTriggerEnter2d(Collider col){

		Destroy (this.gameObject);

	}


}
