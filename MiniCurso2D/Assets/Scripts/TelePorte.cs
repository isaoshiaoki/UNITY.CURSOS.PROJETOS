using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePorte : MonoBehaviour
{
    private GameController _GameController;
    public Transform pontoSaida;
    public Transform posCamera;

    public Transform limiteCamEsc, limiteCamDir, limiteCamSup, limiteCamBaixo;

    public musicaFase novaMusica;
    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;


    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="Player")
        {

            _GameController.trocarMusica(musicaFase.CAVERNA);
            col.transform.position = pontoSaida.position;
            Camera.main.transform.position = posCamera.position;

            _GameController.limiteCamEsc = limiteCamEsc;
            _GameController.limiteCamDir = limiteCamDir;
            _GameController.limiteCamSup = limiteCamSup;
            _GameController.limiteCamBaixo = limiteCamBaixo;

        } 
    }
}
