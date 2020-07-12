using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonPlayer : MonoBehaviour {
	//private Text texto;
	private BatleController batleController;
	private PokemonInimigo pokemonInimigo;
	private string fala;
	private Transform barraHP,barraXP;

	private Vector3 vetor3;
	public string nome;
	public int level, xp;

	public float pv,percentualVida,pvMax;
	public string[] acoes;
	public int[] danoAcoes;
	private int hit,idComando,idFase;
	private GameObject botaoA, botaoB, botaoC, botaoD;
	public GameObject[] animacoes;

	// Use this for initialization
	void Start () {
		batleController = FindObjectOfType (typeof(BatleController)) as BatleController;
		pokemonInimigo=FindObjectOfType (typeof(PokemonInimigo)) as PokemonInimigo;

		pv = pvMax;
		barraHP = GameObject.Find ("HPPlayer").transform;
		barraXP = GameObject.Find ("XPPlayer").transform;
//		percentualVida = pv / pvMax;
//		vetor3 = barraHP.localScale;
//		vetor3.x = barraHP.localScale;
//		barraHP.localScale = vetor3;
//		percentualVida = xp / 100;

		vetor3 = barraHP.localScale;





	}
	

	public void tomarDano (int hit) {
		pv -= hit;

		if(pv<0){
			pv = 0;
			GetComponent<SpriteRenderer> ().enabled = false;

		}
		percentualVida = pv / pvMax;
		vetor3 = barraHP.localScale;
		vetor3.x = percentualVida;
		barraHP.localScale = vetor3;




	}


	public void renomearBotoes(){
	
		botaoA=GameObject.Find ("textoA");
		botaoB=GameObject.Find ("textoB");
		botaoC=GameObject.Find ("textoC");
		botaoD=GameObject.Find ("textoD");


		botaoA.GetComponent<Text>().text=acoes[0];
		botaoB.GetComponent<Text>().text=acoes[1];
		botaoC.GetComponent<Text>().text=acoes[2];
		botaoD.GetComponent<Text>().text=acoes[3];

	}

	public IEnumerator comando(int idAcao){
	
		switch(idAcao){
		case 1:
			//print (acoes[0]);
			idComando=1;
			fala = nome  +"use " + acoes[0];
			StartCoroutine ("dialogo",fala);
			break;

		case 2:

			idComando=2;
			fala = nome  +"use " + acoes[0];
			StartCoroutine ("dialogo",fala);
			break;


		case 3:
			idComando=3;
			fala = nome  +"use " + acoes[0];
			StartCoroutine ("dialogo",fala);

			break;


		case 4:

			idComando=4;
			fala = nome  +"use " + acoes[0];
			StartCoroutine ("dialogo",fala);
			break;

		}


		return null;
	
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
			pokemonInimigo.StartCoroutine ("acaoInicial");

			break;

		case 3:
			batleController.menuA.SetActive (true);
			break;



		case 4:
			
//			yield return new WaitForSeconds (1f);

			fala = pokemonInimigo.nome = " foi derrotado";
			StartCoroutine ("dialogo", fala);
			idFase = 5;
			break;

		case 5:
			StartCoroutine ("ganhaXP", pokemonInimigo.xp);
			break;




	}


	}

	public  IEnumerator ganhaXP(int xpGanho){

		fala = nome + "recebeu" + xpGanho + "xp.";
		StartCoroutine ("dialogo",fala);
		xp += xpGanho;
		percentualVida = xp / 100;
		vetor3 = barraHP.localScale;
		vetor3.x = percentualVida;
		barraHP.localScale = vetor3;


		idFase = 5;
		return null;

	}

	public  IEnumerator aplicarDano(){

		GameObject tempPrefab = Instantiate (animacoes[idComando] as GameObject);

		tempPrefab.transform.position = pokemonInimigo.transform.position;               ;
		
		hit = Random.Range (1,danoAcoes[idComando]);
		fala = acoes + "Usou" + acoes [idComando] + "e causou " + hit + "de dano";
		StartCoroutine ("dialogo",fala);

		//yield return new WaitForSeconds (1f);
		pokemonInimigo.tomarDano (hit);
		Destroy (tempPrefab);
		if (pokemonInimigo.pv <= 0) {

//			fala = pokemonInimigo.nome + " foi derrotado!";
//			StartCoroutine ("dialogo",fala);
			idFase = 4;
		} else {
			idFase = 2;

		}


		return null;
	}




	public void comandoInicial(){

		fala = "O que fazer";

		StartCoroutine ("dialogo",fala);
		idFase = 3;
	}


//	public IEnumerator ganhaXP (int xpGanho)
//	{
//	
//		fala=acoes + "recebeu" + xpGanho + "xp .";
//	
//	}




}
