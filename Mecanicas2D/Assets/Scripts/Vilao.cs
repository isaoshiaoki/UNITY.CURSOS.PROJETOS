using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vilao : MonoBehaviour {


	public float vel = 1.0f;
	public bool liberaPer = false;
	public float distancia;
	public Transform Hero;
	public bool face = true;
	public Animator vilao;

	void Start () {


		vilao = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		distancia = Vector2.Distance (this.transform.position, Hero.transform.position);

		//Flip

		if ((Hero.transform.position.x > this.transform.position.x) && !face) {
			flip ();
		} else if ((Hero.transform.position.x < this.transform.position.x) && face) {

			flip ();
		}


		if((liberaPer == true) && distancia > 2.8f)
		{

			vilao.SetBool ("InimigoAnda", true);
			vilao.SetBool ("InimigoIdle", false);

			if (Hero.transform.position.x < this.transform.position.x) {
				transform.Translate (new Vector2 (-vel * Time.deltaTime, 0));

			} else if (Hero.transform.position.x > this.transform.position.x) {
				transform.Translate (new Vector2 (vel * Time.deltaTime, 0));

			} 
		}else {

			vilao.SetBool ("InimigoAnda",false);
			vilao.SetBool ("InimigoIdle",true);
		}


	}


	void flip()
	{

		face = !face;

		Vector3 scala = this.transform.localScale;
		scala.x *= -1;
		this.transform.localScale = scala;
	}

	void OnTriggerEnter2D(Collider2D outro)
	{
		if(outro.gameObject.CompareTag("Player"))
		{
			liberaPer = true;
		}
	}

}
