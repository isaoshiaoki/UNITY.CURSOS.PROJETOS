using UnityEngine;
using System.Collections;

public class Barra : MonoBehaviour {


	float escalaBarra;
	bool terminou;
	bool comecou;
	public GameObject cameraCena;

	public AudioClip somAcaba;

	void Start () { 
		escalaBarra = this.transform.localScale.x; 
	}

	void Update () {
		if(comecou){
			if(escalaBarra> 0.05f){
				escalaBarra = (escalaBarra - 0.15f*Time.deltaTime);
				this.transform.localScale = new Vector2(escalaBarra,1);
			}else{
				if(!terminou){
					terminou = true;
					cameraCena.SendMessage("FimDeJogo");
					GetComponent<AudioSource>().PlayOneShot(somAcaba);
				}  
			}
		} 
	}

	void AumentaBarra () { 
		escalaBarra = escalaBarra+0.035f;
		if (escalaBarra>1.0f) {escalaBarra = 1.0f;}
	}

	void ComecouJogo () { 
		comecou = true;
	}


}
