using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecameInvisible : MonoBehaviour {

	//ao sair da area visivel ele sera destruido

	void OnBecameInvisible(){
	
		Destroy (this.gameObject);
	
	}



}
