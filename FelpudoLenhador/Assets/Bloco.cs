using UnityEngine;
using System.Collections;

public class Bloco : MonoBehaviour {

	public int direcaoBloco;

	void Start(){
	}  
	void TomaPancadaDireita(){
		GetComponent<Rigidbody2D>().velocity = new Vector2(-10,2);
		GetComponent<Rigidbody2D>().isKinematic = false;
		GetComponent<Rigidbody2D>().AddTorque(100f);
		Invoke("ApagaBloco", 2f);
	}

	void TomaPancadaEsquerda(){
		GetComponent<Rigidbody2D>().velocity = new Vector2(10,2);
		GetComponent<Rigidbody2D>().AddTorque(-100f);
		GetComponent<Rigidbody2D>().isKinematic = false;
		Invoke("ApagaBloco", 2f);
	}
	void ApagaBloco(){ 
		Destroy(this.gameObject); 
	}

}
