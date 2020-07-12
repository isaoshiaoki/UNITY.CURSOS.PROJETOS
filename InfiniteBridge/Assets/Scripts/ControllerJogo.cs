using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ControllerJogo : MonoBehaviour
{


    //Titulo do painel Inspector
    //titulo das configurações referentes ao personagem 

    [Header("Configuração Personagem")]
    public float velocidadePersonagem;
    public Transform tPlayer;

    [Header("Configuração limite Movimento Personagem")]
    
    public float limiteYMaximo;
    public float limiteYMinimo;
    public float limiteXMaximo;
    public float limiteXMinimo;

    //titulo das configurações referentes ao GamePlay
    [Header("Configuração da GamePlay")]
    public float velocidadeObjetos;
    public float intervaloEntreSpawnBarril;
    public int pontosGanhosPorBarril;

    //titulo das configurações referentes ao barril
    [Header("Configuração dos Barril")]
    public GameObject prefabBarril;
    public float posicaoXBarril;
    public float[] posicaoYBarril;
    public int[] ordemExibicao;

    //titulo das configurações referentes a ponte
    [Header("Configuração da Ponte")]
    public GameObject prefabPonte;    
    public float tamanhoPrefabPonte;

    //titulo das configurações referentes ao hud
    [Header("HUD")]

    public Text txtPontos;
    private int pontos;

    [Header("Fx")]
    public AudioSource sFx;
    public AudioClip fxPontos;





    public void instanciarPonte(float posicaoX) {

        GameObject tempPonte = Instantiate(prefabPonte);
        tempPonte.transform.position = new Vector3(posicaoX + tamanhoPrefabPonte,tempPonte.transform.position.y,0); ;


    }

    private void Start() {


        StartCoroutine("spawnBarril");
    }



    IEnumerator spawnBarril() {

        yield return new WaitForSeconds(intervaloEntreSpawnBarril);
        GameObject tempBarril = Instantiate(prefabBarril);
        int rand = Random.Range(0,2);
        tempBarril.transform.position = new Vector3(posicaoXBarril,posicaoYBarril[rand],0);
        tempBarril.GetComponent<SpriteRenderer>().sortingOrder = ordemExibicao[rand];

        StartCoroutine("spawnBarril");

    }


    public void pontuar() {

        pontos += pontosGanhosPorBarril;
        txtPontos.text = "PONTOS :" + pontos.ToString();

        sFx.PlayOneShot(fxPontos);

    }



    public void gameOver() {

        

            SceneManager.LoadScene("GameOver");
         

      
    }



}
