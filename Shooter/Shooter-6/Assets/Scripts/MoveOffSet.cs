using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffSet : MonoBehaviour {

	private Material currentMaterial;
	public float speed;
	private float offSet;


	// Use this for initialization
	void Start () {
		currentMaterial = GetComponent<Renderer>().material;
	}

	// Update is called once per frame
	void Update () {
		//offSet += speed * Time.deltaTime;
		offSet += speed * 0.001f;
		//move no eixo X
		//currentMaterial.SetTextureOffset ("_MainTex",new Vector2(eixoX,eixoY));

		//move no eixo Y
		currentMaterial.SetTextureOffset ("_MainTex",new Vector2(0,offSet));
	}
}