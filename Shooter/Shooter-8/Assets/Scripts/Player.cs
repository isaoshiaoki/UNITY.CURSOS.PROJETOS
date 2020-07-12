using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D playerRb;
	private Animator playerAnimator;
	private int direcao;
	public float vida,vidaMax,vidaAtual;
	public float velocidade,percentualDeVida;

	public Transform barraVida;

	private Transform top,left,right,down;
	public GameObject explosaoPrefab;

	public GameObject[] armasExtras;
	public int powerUsColetados;

	private GameController gameController;

	// Use this for initialization
	void Start () {

		gameController = FindObjectOfType (typeof(GameController))as GameController;

		top= GameObject.Find ("Top").transform;
		left= GameObject.Find ("Left").transform;
		right= GameObject.Find ("Right").transform;
		down= GameObject.Find ("Down").transform;

		playerRb = GetComponent<Rigidbody2D> ();
		playerAnimator = GetComponent<Animator> ();

		vida = vidaMax;

		barraVida = GameObject.Find ("BarraDeVida").transform;
		barraVida.localScale = new Vector3 (1,1,1);
		percentualDeVida = vidaAtual / vidaMax;


	}


	void Update(){



		
//		if(Input.GetButtonDown("Fire1")){
//
//			Atirar ();
//		}


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

//		float x = transform.position.x;
//		float y = transform.position.y;
//		if(x>left.position.x && x < right.position.x && y < top.position.y && y > down.position.y){
//
//			playerRb.velocity = new Vector2 (movimentoX * velocidade,movimentoY * velocidade);
//			playerAnimator.SetInteger ("direcao",direcao);
//
//		}
		playerRb.velocity = new Vector2 (movimentoX * velocidade,movimentoY * velocidade);

	//limite da camera

		if(transform.position.x < left.position.x){
			transform.position = new Vector3 (left.position.x,transform.position.y,transform.position.z);

		}else if(transform.position.y > right.position.x){


			transform.position = new Vector3 (right.position.x,transform.position.y,transform.position.z);
		}


		else if(transform.position.y > top.position.y){
			transform.position = new Vector3 (transform.position.x,top.position.y,transform.position.z);

		}else if(transform.position.y < down.position.y){


			transform.position = new Vector3 (transform.position.x,down.position.y,transform.position.z);
		}







		playerAnimator.SetInteger ("direcao",direcao);


	}

	void OnTriggerEnter2D(Collider2D col){
		switch (col.gameObject.tag){

		case "tiroInimigo":
			tomarDano (1);
			break;
		

		case "powerUp":
			powerUp ();
			Destroy (col.gameObject);
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










//	void Atirar(){
//		GameObject tempPrefab = Instantiate (tiroPersonagemPrefab) as GameObject;
//
//		tempPrefab.transform.position = arma.position;
//		tempPrefab.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0,forcaTiro));
//
//	}




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
		//chama o metodo da classe gameController
		gameController.morreu ();
		Destroy (this.gameObject);
	}



	void powerUp(){

		//armasExtras.SetActive (true);
		powerUsColetados+=1;

		if(powerUsColetados<=armasExtras.Length -1){
		armasExtras[powerUsColetados].SetActive(true);
		}

		gameController.pontos += 250 +powerUsColetados;
	}







}
