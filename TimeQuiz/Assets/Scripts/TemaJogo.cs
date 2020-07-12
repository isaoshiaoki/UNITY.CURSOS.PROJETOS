using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TemaJogo : MonoBehaviour {
	public Button playButton;
	public Text temaTxt;
	public Text infoLevelTxt;
	private int recebeIdModo;
	public GameObject start1,start2,start3,infoWindow;
	public string[] temasNomes;

	private int identificacaoTema;
 	// Use this for initialization
	void Start () {
		recebeIdModo=PlayerPrefs.GetInt ("PlayerPrefsIdModo");

		temaTxt.text=temasNomes[0];
		infoWindow.SetActive (false);
		start1.SetActive (false);
		start2.SetActive (false);
		start3.SetActive (false);


	}
	

	public void selectTema (int idTema) {

		identificacaoTema= idTema;
		temaTxt.text = temasNomes[idTema];
		infoWindow.SetActive (true);
		playButton.interactable = true;

		PlayerPrefs.SetInt ("PlayerPrefsIdTema",idTema);

		int notaF = PlayerPrefs.GetInt ("PlayerPrefsNotaF" + recebeIdModo.ToString() + idTema.ToString());
		int acertos = PlayerPrefs.GetInt ("PlayerPrefsAcertos" + recebeIdModo.ToString() + idTema.ToString());
		//print ("notaF" + recebeIdModo.ToString() + idtema.ToString());
		infoLevelTxt.text=" Você acertou  " + acertos.ToString() + "de 20 questões.";

	
		if(notaF == 10){
			start1.SetActive (true);
			start2.SetActive (true);
			start3.SetActive (true);

		}
		else if(notaF >= 7){

			start1.SetActive (true);
			start2.SetActive (true);
			start3.SetActive (false);
		}
		else if(notaF >= 5){

			start1.SetActive (true);
			start2.SetActive (false);
			start3.SetActive (false);
		}







	}
	public void jogar(){

		SceneManager.LoadScene ("T" + identificacaoTema.ToString());
	}


}
