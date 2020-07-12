using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoDeVida : MonoBehaviour {
	public float tempoDeVida;
	private float tempTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		tempTime += Time.deltaTime;

		if(tempTime >= tempoDeVida){

			Destroy (this.gameObject);
			}

	}
}
