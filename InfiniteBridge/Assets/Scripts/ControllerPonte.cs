using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPonte : MonoBehaviour
{

    private ControllerJogo controllerJogo;
    private Rigidbody2D rBody;
    private bool instanciado;


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

        if (transform.position.x <= 0 && instanciado==false)
        {
            instanciado = true;
            controllerJogo.instanciarPonte(transform.position.x);
        }

if (transform.position.x <= -8) {
            Destroy(this.gameObject);
        }




    }






        
            
            

    }


