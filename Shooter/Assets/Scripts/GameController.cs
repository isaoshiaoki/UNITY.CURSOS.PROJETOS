using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int pontos;
	//public Text recorde;
	public Text pontuacao;
	public int numeroVidasExtras;
	public Transform vidasExtras,spawnPlayer;
	public GameObject iconeDeVida,player;

	public GameObject[] extras;

	// Use this for initialization
	void Start () {
		//recebe os playerPrefs dos pontos e recordes	
		vidaExtras ();
	
		//recorde.text=PlayerPrefs.GetInt ("recordePlayerPrefs").ToString ();

	}

	// Update is called once per frame
	void Update () {
		


		//pontuacao recebera os pontos que seram atualizados no IaInimigo.cs qdo o inmigo explodir
		pontuacao.text=pontos.ToString ();
	}



	public void MudarCena(){

		SceneManager.LoadScene ("cena");

	}

	void vidaExtras(){

	

		GameObject tempVida;
		float posXIconeVida;

		foreach(GameObject v in extras){

			if(v !=null){
				Destroy (v);
			}
		}



		for (int i = 0; i < numeroVidasExtras; i++) {

			posXIconeVida = vidasExtras.position.x + (0.5f + i);
			//Transform vidasExtras
			tempVida = Instantiate (iconeDeVida) as GameObject;
			extras [i] = tempVida;
			tempVida.transform.position = new Vector3 (posXIconeVida, vidasExtras.position.y, vidasExtras.position.z);


		}

		GameObject tempPlayer = Instantiate (player) as GameObject;
		tempPlayer.transform.position = spawnPlayer.position;
		
	}

	public void morreu(){
		if (numeroVidasExtras >= 0) {

			numeroVidasExtras -= 1;
	
			vidaExtras ();
		}else{

			SceneManager.LoadScene ("GameOver");
		}


	}





}
