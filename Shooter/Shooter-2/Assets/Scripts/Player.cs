using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D playerRb;
	private Animator playerAnimator;
	private int direcao;

	public float velocidade;

	public Transform arma;
	public GameObject tiroPersonagemPrefab;
	public float forcaTiro;
	// Use this for initialization
	void Start () {
		playerRb = GetComponent<Rigidbody2D> ();
		playerAnimator = GetComponent<Animator> ();

	}


	void Update(){


		if(Input.GetButtonDown("Fire1")){

			Atirar ();
		}


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float movimentoX = Input.GetAxis ("Horizontal");


	

		if(movimentoX<0){
			direcao = -1;

		}
		else if(movimentoX == 0){

			direcao = 0;
		}

		else if(movimentoX > 0){

			direcao = 1;
		}



		float movimentoY = Input.GetAxis ("Vertical");

		//if(Input.GetButtonDown("Fire1")){

		//	Atirar ();
		//}




		playerRb.velocity = new Vector2 (movimentoX * velocidade,movimentoY * velocidade);
		playerAnimator.SetInteger ("direcao",direcao);
	}





	void Atirar(){
		GameObject tempPrefab = Instantiate (tiroPersonagemPrefab) as GameObject;

		tempPrefab.transform.position = arma.position;
		tempPrefab.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0,forcaTiro));

	}




}
