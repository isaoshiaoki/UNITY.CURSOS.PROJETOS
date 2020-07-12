using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float x = Input.GetAxis("Fire1");
        float y = Input.GetAxis("Fire2");



        float eixoX = Input.GetAxis("Mouse X");
        float eixoY = Input.GetAxis("Mouse Y");

        transform.Translate(new Vector3(h * Time.deltaTime, v * Time.deltaTime, 0));

        transform.Translate(new Vector3(eixoX * Time.deltaTime, eixoY * Time.deltaTime, 0));
        transform.Translate(new Vector3(x * Time.deltaTime, y* Time.deltaTime, 0));

        /*
         float rodaDoMouse = Input.GetAxis("Mouse ScrollWheel");

         transform.Translate(new Vector3(rodaDoMouse * Time.deltaTime, 0, 0));
           */

    }
}
