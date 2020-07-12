using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//classe para mudar de cenas
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class _GC : MonoBehaviour {

	public Text recorde;
	public Text pontos;



	// Use this for initialization
	void Start () {
		//recebe os playerPrefs dos pontos e recordes

		pontos.text=PlayerPrefs.GetInt ("pontosPlayerPrefs").ToString ();


		recorde.text=PlayerPrefs.GetInt ("recordePlayerPrefs").ToString ();

	}

	// Update is called once per frame
	void Update () {

	}


	public void MudarCena(){

		SceneManager.LoadScene ("cena");

	}


}
