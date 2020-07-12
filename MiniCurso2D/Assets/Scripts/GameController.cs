﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum musicaFase
{

    FLORESTA,
    CAVERNA ,
    GAMEOVER,
    THEEND
}

public enum gameState
{

    TITULO,
    GAMEPLAY,
    END,
    GAMEOVER
}


public class GameController : MonoBehaviour
{
    private Camera cam;  
    public float speedCam;
    public Transform playerTransform;

    public Transform limiteCamEsc,limiteCamDir,limiteCamSup,limiteCamBaixo;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioClip sfxJump;
    public AudioClip sfxAtack;
    public AudioClip[] sfxStep;
    public AudioClip sfxCoin;
    public AudioClip sfxEnemyDead;
    public AudioClip sfxDamage;
    public AudioClip musicFloresta,musicCaverna,musicFim,musicGameOver;
    public musicaFase musicaAtual;


    [Header("Fase")]
    public GameObject[] fase;

    [Header("Estado do Jogo")]

    public gameState currentState;
    public GameObject painelTitulo, painelGameOver, painelEnd;

    [Header("Moedas")]
    public int moedasColetadas;
    public Text moedasTxt;
    public Image[] coracoes;
    public int vida;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

       /*
        foreach (GameObject o in fase)
        {
            o.SetActive(false);
        }
        fase[0].SetActive(true);
       */


    }

    // Update is called once per frame
    void Update()
    {
        if (currentState==gameState.TITULO && Input.GetKeyDown(KeyCode.Space)) {
            currentState = gameState.GAMEPLAY;
            painelTitulo.SetActive(false);

        }
        else if (currentState == gameState.GAMEOVER && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (currentState == gameState.END && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }

     //no mesmo frame
    private void LateUpdate()
    {
        //  Vector3 posCam = new Vector3(playerTransform.position.x,playerTransform.position.y,cam.transform.position.z);
        //cam.transform.position = posCam; 

      camController();


    }


    void camController()
    {

        float posCamX = playerTransform.position.x;
        float posCamY = playerTransform.position.y;

        if (cam.transform.position.x < limiteCamEsc.position.x && playerTransform.position.x < limiteCamEsc.position.x)
        {
            posCamX = limiteCamEsc.position.x;
        }
        else if (cam.transform.position.x > limiteCamDir.position.x && playerTransform.position.x < limiteCamDir.position.x)
        {
            posCamX = limiteCamDir.position.x;
        }

        if (cam.transform.position.y < limiteCamBaixo.position.y && playerTransform.position.y < limiteCamBaixo.position.y)
        {
            posCamY = limiteCamBaixo.position.y;
        }
        else if (cam.transform.position.y > limiteCamSup.position.y && playerTransform.position.y > limiteCamSup.position.y)
        {
            posCamY = limiteCamSup.position.y;
        }




        Vector3 posCam = new Vector3(posCamX, posCamY, cam.transform.position.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);

    }

   public void playSFX(AudioClip sfxClip,float volume)
    {
        sfxSource.PlayOneShot(sfxClip,volume);
    }


    public void trocarMusica(musicaFase novaMusica)
    {
        AudioClip clip = null;
        switch (novaMusica)
        {
            case musicaFase.CAVERNA:
                clip = musicCaverna;
                break;

            case musicaFase.FLORESTA:
                clip = musicFloresta;
                break;

            case musicaFase.GAMEOVER:
                clip = musicGameOver;
                break;

                case musicaFase.THEEND:
                clip = musicFim;
                break;
        }
        StartCoroutine("controleMusica",novaMusica);

    }


    IEnumerator controleMusica(AudioClip musica)
    {
        float volumeMaximo = musicSource.volume;

        for (float volume = volumeMaximo;volume >0;volume-=001f)
        {
            musicSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        musicSource.clip = musica;
        musicSource.Play();


        for (float volume = 0; volume < volumeMaximo; volume += 001f)
        {
            musicSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

    }

     public void getHit()
    {
        vida -= 1;
        heartController();

        if (vida<=1)
        {
            playerTransform.gameObject.SetActive(false);
            painelGameOver.SetActive(true);
            currentState = gameState.GAMEOVER;
            trocarMusica(musicaFase.GAMEOVER);
        }


    }

    public void heartController()
    {
        foreach (Image h in coracoes)
        {
            h.enabled = false;
        }
        for (int v=0;v<vida;v++)
        {
            coracoes[v].enabled = true;
        }


    }


     public void theEnd()
    {
        currentState = gameState.END;
        painelEnd.SetActive(true);
        trocarMusica(musicaFase.THEEND);

    }

    public void getCoin()
    {
        moedasColetadas += 1;
        moedasTxt.text = moedasColetadas.ToString();
    }
}
