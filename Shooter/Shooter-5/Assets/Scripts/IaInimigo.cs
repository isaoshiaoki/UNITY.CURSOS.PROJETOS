using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaInimigo : MonoBehaviour {
	private Rigidbody2D inimigoRb;
	private Animator inimigoAnimator;
	private int direcao;
	public int chanceDeMudanca,chanceDeTiro,vida;
	public float velocidade,tempoCurva,tempTime,tempTimeTiro,tempoTiro;

	public Transform arma;
	public GameObject tiroInimigoPrefab,explosaoPrefab;
	public float forcaTiro;
	private int movimentoX , movimentoY,rand;
	public int pontosGanhos;

	private GameController gameController;

	// Use this for initialization
	void Start () {
		//inicializa a classe GameController
		gameController = FindObjectOfType (typeof(GameController))as GameController;

		inimigoRb = GetComponent<Rigidbody2D> ();
		inimigoAnimator = GetComponent<Animator> ();

		movimentoY = -1;

	}
	
	// Update is called once per frame
	void Update () {


		tempTime += Time.deltaTime;
		tempTimeTiro += Time.deltaTime;


		if(tempTime >= tempoCurva){
			tempTime = 0;
			rand = Random.Range (0,100);

			if(rand <= chanceDeMudanca){

				rand = Random.Range (0,100);

				if (rand < 50) {

					movimentoX = -1;
					direcao = 1;

				} else {
					movimentoX = 1;
					direcao = -1;
				}




			}


			else {
				movimentoX = 0;
				direcao = 0;
			}




		}



		//tiro nave inimiga

		if(tempTimeTiro >= tempoTiro){


			tempTimeTiro = 0;
			rand = Random.Range (0,100);
			if (rand <= chanceDeTiro) {
				Atirar ();
			}


		}








		inimigoAnimator.SetInteger ("direcao",direcao);
		inimigoRb.velocity = new Vector2 (movimentoX * velocidade,movimentoY * velocidade);

	}




	void Atirar(){
		GameObject tempPrefab = Instantiate ( tiroInimigoPrefab) as GameObject;

		tempPrefab.transform.position = arma.position;
		tempPrefab.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0,forcaTiro));

	}






void OnTriggerEnter2D(Collider2D col){
		switch (col.gameObject.tag){

		case "tiroPersonagem":
			tomarDano (1);
			break;

		}


	}


	void OnCollisionEnter2D(Collision2D col){
		
		switch (col.gameObject.tag){

		case "Player":
			explodir();
			break;

		}

	}










	void tomarDano (int danoTomado){
		vida -= danoTomado;
		if(vida<=0){
			//antes de desaparecer aparece a explosao
			explodir();

		}


	
	}




	void explodir(){
	
	
		GameObject tempPrefab = Instantiate (explosaoPrefab) as GameObject;
		tempPrefab.transform.position = transform.position;
		tempPrefab.GetComponent<Rigidbody2D> ().velocity=new Vector2(0,velocidade*-1);


		Destroy (this.gameObject);

		gameController.pontos += pontosGanhos;
	}




}
