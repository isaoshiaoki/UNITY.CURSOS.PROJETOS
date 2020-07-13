using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{

    private float vel = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {

            transform.Translate(new Vector3(vel*Time.deltaTime,0,0));
           
            print("Pressionou a seta da direita");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.Translate(new Vector3(-vel * Time.deltaTime, 0, 0));

            print("Pressionou a seta da esquerda");
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {

            transform.Translate(new Vector3(0,vel * Time.deltaTime, 0));

            print("Pressionou a seta para cima");
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {

            transform.Translate(new Vector3(0,-vel * Time.deltaTime, 0));

            print("Pressionou a seta para baixo");
        }











    }
}
