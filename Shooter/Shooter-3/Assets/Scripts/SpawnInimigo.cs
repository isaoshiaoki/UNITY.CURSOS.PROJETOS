using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigo : MonoBehaviour {
	public GameObject inimigoPrefab;
	public float tempoSpawn;
	public Transform limiteEsquerda, limiteDireita;
	private float tempTime,minX,maxX;


	// Use this for initialization
	void Start () {
		minX = limiteEsquerda.position.x;
		maxX= limiteDireita.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		tempTime += Time.deltaTime;

		if(tempTime >= tempoSpawn){

			tempTime = 0;
			Spawn();

		}

	}

	void Spawn (){

		GameObject tempPrefab = Instantiate (inimigoPrefab) as GameObject;
		float posX = Random.Range (minX,maxX);
		tempPrefab.transform.position = new Vector3 (posX,transform.position.y,transform.position.z);


	}






}
