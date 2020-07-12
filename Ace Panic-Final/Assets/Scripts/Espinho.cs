using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinho : MonoBehaviour {
	private Player scriptPlayer;
	private Rigidbody2D espinhoRb;
	public float tempoEspera;
	public int atritoMin,atritoMax,pontos;
	private Vector3 posicaoInicial;
	public GameObject espinhoPrefab,explosaoPrefab;

	// Use this for initialization
	void Start () {
		scriptPlayer = FindObjectOfType (typeof(Player)) as Player;


		posicaoInicial = transform.position;
		espinhoRb = GetComponent<Rigidbody2D> ();
		espinhoRb.isKinematic = true;
		int atrito = Random.Range (atritoMin,atritoMax);
		espinhoRb.drag = atrito;

		StartCoroutine ("soltar");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator soltar(){
		yield return new WaitForSeconds (Random.Range (0.05f,tempoEspera));
		espinhoRb.isKinematic = false;

	
	}



	void OnCollisionEnter2D(){
	
		GameObject tempEspinho=Instantiate (espinhoPrefab,posicaoInicial,transform.rotation) as GameObject;
		tempEspinho.GetComponent<Rigidbody2D> ().isKinematic=true;

		GameObject explosao=Instantiate (explosaoPrefab,transform.position,transform.rotation) as GameObject;
		Destroy (explosao,0.3f);

		scriptPlayer.score += pontos;
		//Instantiate (espinhoPrefab,posicaoInicial,transform.rotation);
		Destroy (this.gameObject);
	}
}
