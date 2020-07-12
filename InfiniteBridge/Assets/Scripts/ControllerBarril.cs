using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBarril : MonoBehaviour
{
    private ControllerJogo controllerJogo;
    private Rigidbody2D rBody;
    private bool pontuado;


    // Start is called before the first frame update
    void Start()
    {
        controllerJogo = FindObjectOfType(typeof(ControllerJogo)) as ControllerJogo;
        rBody = GetComponent<Rigidbody2D>();




    }

    // Update is called once per frame
    void Update()
    {
        rBody.velocity = new Vector2(controllerJogo.velocidadeObjetos,0);

        //verifica se a posiçao x barril e menor que a do personagem entao chama a funcao de pontuacao
        if (transform.position.x <= controllerJogo.tPlayer.position.x && pontuado==false) {

            controllerJogo.pontuar();
            pontuado = true;
        }


    }
}
