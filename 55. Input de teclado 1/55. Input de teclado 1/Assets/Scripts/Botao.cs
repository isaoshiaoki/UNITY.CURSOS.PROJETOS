using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao : MonoBehaviour
{
    public float velocidade = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         //tem que estar com a aba game em aberto

        if (Input.GetButtonDown("Fire1"))
        {
            print("Pressionou a tecla CTRL");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            print("Pressionou a tecla seta da direita");

             transform.Translate(new Vector3(velocidade * Time.deltaTime,0,0));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            print("Pressionou a tecla seta da esquerda");

            transform.Translate(new Vector3(-velocidade * Time.deltaTime, 0, 0));
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            print("Pressionou a tecla seta para cima");

            transform.Translate(new Vector3(0, velocidade * Time.deltaTime, 0));
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            print("Pressionou a tecla seta para baixo");

            transform.Translate(new Vector3(0, -velocidade * Time.deltaTime, 0));
        }


















        //print("TESTE DE PRINT");

    }
}
