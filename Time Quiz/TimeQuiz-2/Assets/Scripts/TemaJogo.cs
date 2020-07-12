using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class TemaJogo : MonoBehaviour {
	public Button playButton;
	public Text temaTxt;
	public Text infoLevelTxt;
	private int idModo;
	public GameObject start1,start2,start3,infoWindow;
	public string[] temasNomes;
 	// Use this for initialization
	void Start () {
		idModo=PlayerPrefs.GetInt ("PlayerPrefsIdJogo");

		temaTxt.text=temasNomes[0];
		infoWindow.SetActive (false);
		start1.SetActive (false);
		start2.SetActive (false);
		start3.SetActive (false);


	}
	

	public void selectTema (int idtema) {
		temaTxt.text = temasNomes[idtema];
		infoWindow.SetActive (true);
		playButton.interactable = true;

		PlayerPrefs.SetInt ("PlayerPrefsIdTema",idtema);
		print ("notaF" + idModo.ToString() + idtema.ToString());
		int notaF = PlayerPrefs.GetInt ("PlayerPrefsNotaF" + idModo.ToString() + idtema.ToString());
	}



}
