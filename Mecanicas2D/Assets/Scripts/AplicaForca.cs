using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AplicaForca : MonoBehaviour
{
    public float forca ;
    public Rigidbody2D bola;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bola.AddForce(new Vector2(0,forca * Time.deltaTime),ForceMode2D.Impulse);
        }
    }
}
