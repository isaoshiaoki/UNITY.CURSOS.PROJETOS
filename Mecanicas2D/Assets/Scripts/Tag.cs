using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tag : MonoBehaviour
{
    public float forca;
    public Rigidbody2D bola;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (this.gameObject.CompareTag("bola")) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bola.AddForce(new Vector2(0, forca * Time.deltaTime), ForceMode2D.Impulse);
                print("Tag bola");
            }
        }


        if (this.gameObject.CompareTag("solo"))
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                print("Tag solo");
            }
        }




    }
}
