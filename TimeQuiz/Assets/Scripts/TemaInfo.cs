using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemaInfo : MonoBehaviour {
	public int idTema;
	private int idModo;
	public GameObject start1,start2,start3;
	// Use this for initialization
	void Start () {

	idModo=PlayerPrefs.GetInt ("PlayerPrefsIdModo");
	int notaF = PlayerPrefs.GetInt ("PlayerPrefsNotaF" + idModo.ToString() + idTema.ToString());
		start1.SetActive (false);
		start2.SetActive (false);
		start3.SetActive (false);

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
	

}
