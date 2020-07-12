using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Principal : MonoBehaviour {
	 
	public GameObject jogador;
	public GameObject felpudoIdle;
	public GameObject felpudoBate;

	public GameObject blocoEsq;
	public GameObject blocoDir;
	public GameObject blocoCentro;
	public GameObject barra;

	public Text textoScore;

	public AudioClip somBate;
	public AudioClip somPerde;


	bool acabou;
	bool comecou;
	bool ladoJogador; 
	int scoreJogo;

	private float escalaHorizontalJogador;

	private List<GameObject> listaBlocos;

	void Start () {
		felpudoBate.SetActive(false);
		escalaHorizontalJogador = jogador.transform.localScale.x; 
		listaBlocos = new List<GameObject>(); 
		CriaBlocosNaCena();

		textoScore.transform.position = new Vector2(Screen.width/2,Screen.height/2+200);  
		textoScore.text = "Toque para iniciar!";
		textoScore.fontSize = 25;  
	}

	void Update () {
		if(!acabou){
			if(Input.GetButtonDown("Fire1")){  

				if(!comecou){
					comecou=true;
					barra.SendMessage("ComecouJogo");
				}

				if(Input.mousePosition.x > Screen.width/2)
				{
					bateDireita(); 
				}else{
					bateEsquerda();
				}
				Invoke("VoltaAnimacao", 0.25f);

				felpudoBate.SetActive(true); 
				felpudoIdle.SetActive(false); 

				listaBlocos.RemoveAt(0); 
				ReposicionaBlocos(); 
				confereJogada();
			}
		}
	}

	void bateDireita(){ 
		ladoJogador = false;
		listaBlocos[0].SendMessage("TomaPancadaDireita"); 
		jogador.transform.position = new Vector2(1.1f, jogador.transform.position.y);
		jogador.transform.localScale = new Vector2(-escalaHorizontalJogador,jogador.transform.localScale.y); 
	}

	void bateEsquerda(){ 
		ladoJogador = true;
		listaBlocos[0].SendMessage("TomaPancadaEsquerda"); 
		jogador.transform.position = new Vector2(-1.1f, jogador.transform.position.y);
		jogador.transform.localScale = new Vector2(escalaHorizontalJogador,jogador.transform.localScale.y);
	}

	void VoltaAnimacao(){ 
		felpudoBate.SetActive(false); 
		felpudoIdle.SetActive(true); 
	}
 
	void CriaBlocosNaCena(){
		for(int i=0; i<=7; i++)
		{
			//			CriaNovoBarril(new Vector2(0,-2.162f+(i*0.835f)));
			GameObject novoObj = CriaNovoBarril(new Vector2(0,-2.162f+(i*0.835f)));
			listaBlocos.Add(novoObj);
		} 
	}

	void ReposicionaBlocos(){
		GameObject novoBarril = CriaNovoBarril(new Vector2(0,-2.162f+(8*0.835f)));  
		listaBlocos.Add(novoBarril);

		for(int i=0; i<=7; i++)
		{
			listaBlocos[i].transform.position = new Vector2(listaBlocos[i].transform.position.x,listaBlocos[i].transform.position.y-0.835f);
		} 
	}

	GameObject CriaNovoBarril(Vector2 posicao){
		GameObject novoBarril; 
		if((Random.value > 0.5f) || listaBlocos.Count<2 ){
			novoBarril= Instantiate(blocoCentro);
		}else{

			if(Random.value > 0.5f){ 
				novoBarril= Instantiate(blocoDir);
			}else{
				novoBarril= Instantiate(blocoEsq);
			}
		}  
		novoBarril.transform.position = posicao;  
		return novoBarril;
	} 

	void confereJogada(){ 
		if(listaBlocos[0].gameObject.CompareTag("Inimigo")){


			if((listaBlocos[0].name=="inimigoEsq(Clone)" && ladoJogador) || (listaBlocos[0].name=="inimigoDir(Clone)" && !ladoJogador))
			{ 
				FimDeJogo();
				GetComponent<AudioSource>().PlayOneShot(somPerde);

			}else{
				barra.SendMessage("AumentaBarra");
				MarcaPonto(); 
			} 

		}  else{MarcaPonto();}
	}
	void RecarregaCena(){
		Application.LoadLevel("MINHACENA");
	}


	void MarcaPonto(){
		scoreJogo++;
		textoScore.text = scoreJogo.ToString();
		textoScore.fontSize = 100;
		textoScore.color = new Color(0.95f,1.0f,0.35f);

		GetComponent<AudioSource>().PlayOneShot(somBate);
	}

	void FimDeJogo(){
		acabou = true; 

		felpudoBate.GetComponent<SpriteRenderer>().color = new Color(1f,0.25f,0.25f,1.0f);
		felpudoIdle.GetComponent<SpriteRenderer>().color = new Color(1f,0.25f,0.25f,1.0f);
		jogador.GetComponent<Rigidbody2D>().isKinematic = false;
		jogador.GetComponent<Rigidbody2D>().AddTorque(100f);

		if(ladoJogador){
			jogador.GetComponent<Rigidbody2D>().velocity = new Vector2(-5,3);
		}else{jogador.GetComponent<Rigidbody2D>().velocity = new Vector2(5,3);}

		Invoke("RecarregaCena", 1);

	}
}
