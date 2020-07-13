using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuloDuplo : MonoBehaviour
{
    public float forca;
    public Rigidbody2D bola;
    public bool liberaPulo = false;
    public int duplo = 2;
    public AudioClip puloSom;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (duplo>0     )
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //bola.AddForce(new Vector2(0,forca*Time.deltaTime),ForceMode2D.Impulse);
                bola.AddForce(new Vector2(0, forca * Time.deltaTime), ForceMode2D.Impulse);
                Gerenciador.inst.PlayAudio(puloSom);
                duplo--;
            
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("solo"))
        {
            duplo = 2;
            liberaPulo = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("solo"))
        {
            liberaPulo = false;
        }
    }
}
