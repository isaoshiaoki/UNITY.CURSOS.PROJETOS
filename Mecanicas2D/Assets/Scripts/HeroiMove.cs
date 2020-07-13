using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroiMove : MonoBehaviour {


	public bool face = true;
	public Transform heroiT;
	public float vel = 2.5f;
	public float vel2 = 10.0f;
	public float force = 10.5f;
	public Rigidbody2D heroiRB;

	public bool liberaPulo = true;
	public Transform check;
	public LayerMask oqEChao;
	public float raio = 0.2f;

	public Animator anim;
	public bool vivo = true;
	public int magnetItem = 0;

	//UI
	public Text txtMagnet;
	public Image magnetImg;





	void Start () {

		heroiT = GetComponent<Transform> ();
		heroiRB = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();



	}

	// Update is called once per frame
	void Update () {



		//Fim

		if(vivo == true)
		{
			if(Input.GetKey(KeyCode.RightArrow) && !face)
			{
				Flip ();
			}

			if(Input.GetKey(KeyCode.LeftArrow) && face)
			{
				Flip ();
			}
		}


		if (vivo == true)
		{

			//É CHÃO?

			liberaPulo = Physics2D.OverlapCircle (check.position, raio, oqEChao);


			//CORRER


			if (Input.GetKey (KeyCode.RightArrow) && Input.GetKey (KeyCode.Q)) {
				transform.Translate (new Vector2 (vel2 * Time.deltaTime, 0));

				Correr ();
					
			} else if (Input.GetKey (KeyCode.LeftArrow) && Input.GetKey (KeyCode.Q)) {
				transform.Translate (new Vector2 (-vel2 * Time.deltaTime, 0));

				Correr ();
			} 

			//ANDAR

			else if(Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.Q))
			{
				transform.Translate(new Vector2(vel * Time.deltaTime,0));

				Andar ();
			}

			else if(Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.Q))
			{
				transform.Translate(new Vector2(-vel * Time.deltaTime,0));
				Andar ();

			}

			else if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
			{
				
					anim.SetBool ("Idle",true);
					anim.SetBool ("Andar",false);
					anim.SetBool ("Correr",false);

			}

			//PULO

			if (Input.GetKeyDown (KeyCode.Space) && liberaPulo == true && Input.GetKey (KeyCode.RightArrow) && Input.GetKey (KeyCode.Q)) {
				heroiRB.AddForce (new Vector2 (0, force), ForceMode2D.Impulse);
				anim.SetBool ("PuloLateral", true);
				anim.SetBool ("Correr", false);
			} else if (Input.GetKeyDown (KeyCode.Space) && liberaPulo == true && Input.GetKey (KeyCode.LeftArrow) && Input.GetKey (KeyCode.Q)) {
				heroiRB.AddForce (new Vector2 (0, force), ForceMode2D.Impulse);
				anim.SetBool ("PuloLateral", true);
				anim.SetBool ("Correr", false);
			} else if (Input.GetKeyDown (KeyCode.Space) && liberaPulo == true && Input.GetKey (KeyCode.RightArrow)) {
				heroiRB.AddForce (new Vector2 (0, force), ForceMode2D.Impulse);
				anim.SetBool ("PuloLateral", true);
				anim.SetBool ("Andar", false);
			} else if (Input.GetKeyDown (KeyCode.Space) && liberaPulo == true && Input.GetKey (KeyCode.LeftArrow)) {
				heroiRB.AddForce (new Vector2 (0, force), ForceMode2D.Impulse);
				anim.SetBool ("PuloLateral", true);
				anim.SetBool ("Andar", false);
			} else if (Input.GetKeyDown (KeyCode.Space) && liberaPulo == true) {
				heroiRB.AddForce (new Vector2 (0, force), ForceMode2D.Impulse);
				anim.SetBool ("PuloLateral", true);
				anim.SetBool ("Idle", false);

			} 


		}
			

	}

	void Flip()
	{
		face = !face;

		Vector3 scala = heroiT.localScale;
		scala.x *= -1;
		heroiT.localScale = scala;
	}

	void OnCollisionEnter2D(Collision2D outro)
	{
		if(outro.gameObject.CompareTag("chao"))
		{	
			
			anim.SetBool ("PuloLateral", false);
			anim.SetBool ("Idle", true);


		}
	}



	void Correr()
	{
		anim.SetBool ("Idle",false);
		anim.SetBool ("Andar",false);
		anim.SetBool ("Correr",true);
	}

	void Andar()
	{
		anim.SetBool ("Idle",false);
		anim.SetBool ("Andar",true);
		anim.SetBool ("Correr",false);
	}


	void OnTriggerEnter2D(Collider2D outro)
	{
		if(outro.gameObject.CompareTag("Magnet"))
		{
			magnetItem++;
			txtMagnet.text = magnetItem.ToString ();
			Destroy (outro.gameObject);
		}
	}


}
