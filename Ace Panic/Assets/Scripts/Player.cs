using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour {
	private Rigidbody2D playerRb;
	public float speed;
	public Text scoreTxt;
	public int score;

	// Use this for initialization
	void Start () {
		Screen.SetResolution (332,540,false);
		playerRb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//vai ser atualizada no Espinho.cs scriptPlayer.score += pontos;
		scoreTxt.text = score.ToString ();
		//apertar o btn esquerdo do mouse
		if(Input.GetMouseButtonDown(0)){

			Flip ();
		}


		playerRb.velocity = new Vector2 (speed,playerRb.velocity.y);



	}



	void Flip(){
	
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		speed *= -1;
	
	}



	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag=="espinho"){

			GameOver ();

		}
	
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag=="espinho"){

			GameOver ();

		}


	}






	void GameOver(){

		PlayerPrefs.SetInt ("score",score);
		if(score > PlayerPrefs.GetInt("recorde")){

			PlayerPrefs.SetInt ("recorde",score);
		}

	
		//print ("Morreu");

		SceneManager.LoadScene ("GameOver");
	
	}


}
