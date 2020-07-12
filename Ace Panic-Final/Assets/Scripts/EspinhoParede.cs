using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspinhoParede : MonoBehaviour {
	public float tempoEspera, velocidade;
	public Transform pontoA,pontoB;
	private Vector3 destino;
	private bool esperando;

	// Use this for initialization
	void Start () {
		destino = pontoA.position;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.MoveTowards (transform.position,destino,velocidade*Time.deltaTime);

		if(transform.position==destino && esperando==false){

			StartCoroutine ("moverEspinho");
		}
	}



	IEnumerator moverEspinho(){
		esperando = true;
		yield return new WaitForSeconds (tempoEspera);
		if(destino==pontoA.position){
			destino = pontoB.position;

		}else if(destino==pontoB.position){

			destino = pontoA.position;
		}


		esperando =false;


	}

}
