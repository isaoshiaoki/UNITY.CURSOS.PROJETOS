using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	public Text score, recorde;
	// Use this for initialization
	void Start () {
		
		score.text = PlayerPrefs.GetInt ("score").ToString ();
		recorde.text = PlayerPrefs.GetInt ("recorde").ToString ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void irPara(string nomeCena){
	
		SceneManager.LoadScene (nomeCena);
	
	}
}
