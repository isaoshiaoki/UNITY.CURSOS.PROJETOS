using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisao : MonoBehaviour
{
    public float forca;
    public Rigidbody2D bola;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bola.AddForce(new Vector2(0, forca * Time.deltaTime), ForceMode2D.Impulse);
            
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("plataforma") || collision.gameObject.CompareTag("solo"))
        {

            bola.AddForce(new Vector2(0, 500 * Time.deltaTime), ForceMode2D.Impulse);
            //Destroy(collision.gameObject);
        }




        //print("colidiu");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("plataforma")  )
        {

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("solo"))
        {

            Destroy(collision.gameObject);
        }

    }






}
