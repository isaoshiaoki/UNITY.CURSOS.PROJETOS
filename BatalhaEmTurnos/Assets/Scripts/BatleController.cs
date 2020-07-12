using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class BatleController : MonoBehaviour {
	
	private PokemonPlayer pokemonPlayer;
	private PokemonInimigo pokemonInimigo;

	private Transform treinador, pokemon,posA,posB;


	public string fala;
	public int idFase;
	public Text texto;
	public GameObject menuA, menuB;


	void Update(){
		if(idFase==1){

			treinador.GetComponent<Animator> ().SetBool("lancar",true);


			float step = 2 * Time.deltaTime;
			treinador.position = Vector3.MoveTowards (treinador.position,posB.position,step);
			pokemon.position = Vector3.MoveTowards (pokemon.position,posA.position,step);
		}

	}
	// Use this for initialization
	void Start () {

		pokemonPlayer = FindObjectOfType (typeof(PokemonPlayer)) as PokemonPlayer;
		pokemonInimigo=FindObjectOfType (typeof(PokemonInimigo)) as PokemonInimigo;

		treinador = GameObject.Find ("Treinador").transform;
		pokemon = GameObject.Find ("pokemonPlayer").transform;

		posA=GameObject.Find ("PosA").transform;
		posA=GameObject.Find ("PosB").transform;


		pokemon= pokemonPlayer.transform;

		menuA.SetActive (false);
		menuB.SetActive (false);


		idFase=0;
		fala = " ola!fala teste";

		StartCoroutine ("dialogo",fala);
	}
	


	public IEnumerator dialogo(string txt){
	
		int letra = 0;
		//texto.text = "";
		texto.text="";


		while(letra <= txt.Length - 1){
			texto.text +=txt[letra];

			letra+=1;
			yield return new WaitForSeconds(0.05f);
		}
		idFase += 1;
		yield return new WaitForSeconds (1f);

		switch(idFase){

		case 1:
			fala = "Vá " + pokemon.name + "!";
			StartCoroutine ("dialogo",fala);
			break;


		case 2:
//			fala = "O que fazer? ";
//			StartCoroutine ("dialogo",fala);
//			break;
			pokemonPlayer.comandoInicial();

			break;

//		case 3:
//			menuA.SetActive (true);
//			break;

//		case 3:
//			batleController.menuA.SetActive (true);
//			break;
		}


	}
	public void lutar(){
	
		menuA.SetActive (false);
		menuB.SetActive (true);
		pokemonPlayer.renomearBotoes ();
	}







//	public void botaoA(){
//		menuB.SetActive (false);
//	}
//	public void botaoB(){
//		menuB.SetActive (false);
//	}
//
//	public void botaoC(){
//		menuB.SetActive (false);
//	}
//	public void botaoD(){
//		menuB.SetActive (false);
//	}

	public void comandos (int idComando){

		menuB.SetActive (false);
		pokemonPlayer.StartCoroutine ("comando",idComando);
	}

}
