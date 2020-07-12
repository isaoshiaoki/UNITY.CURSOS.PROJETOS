using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Responder : MonoBehaviour {
	public Text pergunta;
	public Text respostaA;
	public Text respostaB;
	public Text respostaC;
	public Text respostaD;
	public Text infoModoJogo;
	public Text infoResposta;
	public GameObject barraTempo;
	public int idModo,idTema;

	public string[] modoDeJogo;
	public float tempTime;
	public int idPergunta;
	public float duracao;
	public string[] perguntas;
	public string[] respostasA;
	public string[] respostasB;
	public string[] respostasC;
	public string[] respostasD;
	public string[] corretas;

	private int idPerguntas;
	private float acertos;
	private int media;
	private int notaF;

	private float questoes;


	// Use this for initialization
	void Start () {
		//barraTempo.SetActive (false);

		idTema=PlayerPrefs.GetInt ("PlayerPrefsIdTema");
		idModo=PlayerPrefs.GetInt ("PlayerPrefsIdModo");

		idPerguntas = 0;
		questoes =perguntas.Length;
		infoModoJogo.text=modoDeJogo[idModo];

		pergunta.text=perguntas[idPergunta];
		respostaA.text=respostasA[idPergunta];
		respostaB.text=respostasB[idPergunta];
		respostaC.text=respostasC[idPergunta];
		respostaD.text=respostasD[idPergunta];

		infoResposta.text = "Respondendo" + (idPergunta + 1) + "de" + questoes.ToString () + "questões";


		switch (idModo) {
		case 1:
			barraTempo.SetActive (false);
			duracao = 0;
			break;


		case 2:

			barraTempo.SetActive (true);
			duracao = 10;
			break;


		case 3:

			barraTempo.SetActive (true);
			duracao = 3;
			break;








		}
	}



	public void resposta(string alternativa){

		switch(alternativa){
		case "A":
			if(respostasA[idPergunta]==corretas[idPergunta]){

				acertos += 1;
			}

			break;

		case "B":
			if(respostasB[idPergunta]==corretas[idPergunta]){

				acertos += 1;
			}
			break;

		case "C":
			if(respostasC[idPergunta]==corretas[idPergunta]){

				acertos += 1;
			}
			break;

		case "D":
			if(respostasD[idPergunta]==corretas[idPergunta]){

				acertos += 1;
			}
			break;
		}


		proxima ();

	}


	public void proxima(){

		tempTime = 0;
		idPergunta += 1;
		if (idPergunta <= (questoes - 1)) {

			pergunta.text = perguntas [idPergunta];
			respostaA.text = respostasA [idPergunta];
			respostaB.text = respostasB [idPergunta];
			respostaC.text = respostasC [idPergunta];
			respostaD.text = respostasD [idPergunta];
			infoResposta.text = "Respondendo" + (idPergunta + 1) + "de" + questoes.ToString () + "questões";
		} else {
			float media =10 * acertos / questoes;
			int notaF = Mathf.RoundToInt (media);


			PlayerPrefs.SetInt ("PlayerPrefsNotaF" + idModo.ToString() + idTema.ToString(),notaF);

			notaF=Mathf.RoundToInt(media);

			if(acertos > PlayerPrefs.GetInt ("PlayerPrefsNotaF" + idModo.ToString() + idTema.ToString())){

				PlayerPrefs.SetInt ("PlayerPrefsNotaF" + idModo.ToString() + idTema.ToString(),notaF);
			}



			if(acertos > PlayerPrefs.GetInt ("PlayerPrefsAcertos" + idModo.ToString() + idTema.ToString())){

				PlayerPrefs.SetInt ("PlayerPrefsAcertos" + idModo.ToString() + idTema.ToString(),(int) acertos);
			}

		
			PlayerPrefs.SetInt ("PlayerPrefsAcertosTemp" + idModo.ToString() + idTema.ToString(),(int) acertos);
		SceneManager.LoadScene ("notaFinal");
		
		}
	
	}








	
	// Update is called once per frame
	void Update () {

		if(duracao>0){

			tempTime += Time.deltaTime;
			float percentual=(tempTime/duracao) * 100;

			float tamanhoBarra=100-percentual;

			if(tamanhoBarra < 0){ tamanhoBarra = 0;  }
			barraTempo.transform.localScale = new Vector3 (tamanhoBarra,100,100);

			if(tempTime >= duracao){

				proxima ();
			}



		}







	}






}
