using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NotaFinal : MonoBehaviour {
	public Text nota,infoAcertos;
	public GameObject start1, start2, start3;


	private int idModo, idTema;
	// Use this for initialization
	void Start () {

		idModo=PlayerPrefs.GetInt ("PlayerPrefsIdModo" );
		idTema=PlayerPrefs.GetInt ("PlayerPrefsNotaF" );



		int notaF=PlayerPrefs.GetInt ("PlayerPrefsNotaFTemp" + idModo.ToString() + idTema.ToString());
		int acertos=PlayerPrefs.GetInt ("PlayerPrefsAcertosTemp" + idModo.ToString() + idTema.ToString());

		nota.text = nota.ToString ();

		start1.SetActive (false);
		start2.SetActive (false);
		start3.SetActive (false);

		infoAcertos.text = "Você acertou " + acertos.ToString () + "de 20 questões";

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
	
	// Update is called once per frame
	void Update () {
		
	}
}
