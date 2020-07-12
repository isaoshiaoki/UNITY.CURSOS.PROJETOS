using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ControllerPlayer : MonoBehaviour
{
    private ControllerJogo controllerJogo;
    private Rigidbody2D rBody;


    // Start is called before the first frame update
    void Start()
    {
        controllerJogo = FindObjectOfType(typeof(ControllerJogo) )as ControllerJogo;
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float velocidade = controllerJogo.velocidadePersonagem;

        rBody.velocity = new Vector2(horizontal * velocidade,vertical * velocidade);


        //Verifica posicao Y do personagem e ajusta conforme o limite definido

        if (transform.position.y > controllerJogo.limiteYMaximo) {

            transform.position = new Vector3(transform.position.x, controllerJogo.limiteYMaximo, 0);

        }
        else if (transform.position.y < controllerJogo.limiteYMinimo) {


            transform.position = new Vector3(transform.position.x, controllerJogo.limiteYMinimo, 0);


        }


        //Verifica posicao X do personagem e ajusta conforme o limite definido

        if (transform.position.x > controllerJogo.limiteXMaximo)
        {

            transform.position = new Vector3( controllerJogo.limiteXMaximo, transform.position.y, 0);

        }
        else if (transform.position.x < controllerJogo.limiteXMinimo)
        {


            transform.position = new Vector3(controllerJogo.limiteXMinimo, transform.position.y, 0);


        }

                              }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        controllerJogo.gameOver();
    }



}
