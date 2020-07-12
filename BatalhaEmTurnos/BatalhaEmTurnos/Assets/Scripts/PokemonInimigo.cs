using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonInimigo : MonoBehaviour {
	private string fala;
	private Transform barraHP;
	public GameObject[] animacoes;
	private int hit,idComando,idFase;
	public string nome;
	public int level, xp;
	private Vector3 vetor3;
	public float pv,percentualVida,pvMax;
	public string[] acoes;
	public int[] danoAcoes;
	private BatleController batleController;
	private PokemonPlayer pokemonPlayer;
	// Use this for initialization
	void Start () {
		
		batleController = FindObjectOfType (typeof(BatleController)) as BatleController;
		pokemonPlayer=FindObjectOfType (typeof(PokemonPlayer)) as PokemonPlayer;

		barraHP = GameObject.Find ("HPInimigo").transform;
		pv = pvMax;
//		percentualVida = pv / pvMax;
//		vetor3.x = barraHP.localScale;
//		barraHP.localScale = vetor3;




	}
	
	public void tomarDano (int hit) {
		pv -= hit;

		if(pv<=0){
			pv = 0;
			GetComponent<SpriteRenderer> ().enabled = false;

		}
		percentualVida = pv / pvMax;
		vetor3 = barraHP.localScale;
		vetor3.x = percentualVida;
		barraHP.localScale = vetor3;




	}





	public IEnumerator acaoInicial(){
	
		int rand = Random.Range (0,acoes.Length);
		yield return new WaitForSeconds(1f);

		StartCoroutine ("comando",idComando);
	}


	public IEnumerator comando(int idAcao){

		switch(idAcao){
		case 0:
			StartCoroutine ("aplicarDano");
			break;

		case 1:
			StartCoroutine ("aplicarDano");
			break;

		case 2:

			StartCoroutine ("aplicarDano");
			break;


		case 3:
			
			StartCoroutine ("aplicarDano");
			break;


		

		}


		yield return new WaitForSeconds(1f);

	}



	public IEnumerator dialogo(string txt){

		int letra = 0;
		//texto.text = "";
		batleController.texto.text="";


		while(letra <= txt.Length - 1){
			batleController.texto.text +=txt[letra];

			letra+=1;
			yield return new WaitForSeconds(0.05f);
		}

		yield return new WaitForSeconds (1f);

		switch(idFase){
		case 1:
			StartCoroutine ("aplicarDano");
			break;

		case 2:
			pokemonPlayer.comandoInicial ();

			break;



		}


	}


	public  IEnumerator aplicarDano(){


		GameObject tempPrefab = Instantiate (animacoes[idComando] as GameObject);

		tempPrefab.transform.position = pokemonPlayer.transform.position;



		hit = Random.Range (1,danoAcoes[idComando]);
		fala = acoes + "Usou" + acoes [idComando] + "e causou " + hit + "de dano";
		StartCoroutine ("dialogo",fala);

		yield return new WaitForSeconds (1f);
		Destroy (tempPrefab);
		pokemonPlayer.tomarDano (hit);

		idFase = 2;
	}








}
