using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D playerRb;
	private Animator playerAnimator;
	private int direcao;
	public float vida,vidaMax,vidaAtual;
	public float velocidade,percentualDeVida;

	public Transform arma,barraVida;
	public GameObject tiroPersonagemPrefab,explosaoPrefab;
	public float forcaTiro;




	// Use this for initialization
	void Start () {
		playerRb = GetComponent<Rigidbody2D> ();
		playerAnimator = GetComponent<Animator> ();

		vida = vidaMax;
		percentualDeVida = vidaAtual / vidaMax;


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

	void OnTriggerEnter2D(Collider2D col){
		switch (col.gameObject.tag){

		case "tiroInimigo":
			tomarDano (1);
			break;
		



		}


	}

	void OnCollisionEnter2D(Collision2D col){
		switch (col.gameObject.tag){

		case "naveInimiga":
			tomarDano (5);
			break;




		}


	}










	void Atirar(){
		GameObject tempPrefab = Instantiate (tiroPersonagemPrefab) as GameObject;

		tempPrefab.transform.position = arma.position;
		tempPrefab.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0,forcaTiro));

	}




	void tomarDano (int danoTomado){
		vida -= danoTomado;

		percentualDeVida = vida / vidaMax;
		//pega o scale do objeto
		Vector3 theScale = barraVida.localScale;

		theScale.x = percentualDeVida;
		barraVida.localScale = theScale;



		if(vida<=0){
			//antes de desaparecer aparece a explosao
			explodir();

		}



	}




	void explodir(){


		GameObject tempPrefab = Instantiate (explosaoPrefab) as GameObject;
		tempPrefab.transform.position = transform.position;

		Destroy (this.gameObject);
	}











}
