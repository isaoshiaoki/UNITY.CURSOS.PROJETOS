using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ModoDeJogo : MonoBehaviour {
	public Text modoDeJogoTxt;
	public Button playButton;
	public string[] modosDeJogos;



	// Use this for initialization
	void Start () {
		playButton.interactable = false;
		modoDeJogoTxt.text = modosDeJogos [0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void selectModo(int idmodo){


		modoDeJogoTxt.text = modosDeJogos [idmodo];
		playButton.interactable = true;

		PlayerPrefs.SetInt ("PlayerPrefsIdJogo",idmodo);
	}
}
