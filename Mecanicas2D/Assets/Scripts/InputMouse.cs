using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        float fire1 = Input.GetAxis("Fire1");
        float fire2 = Input.GetAxis("Fire2");
        float fire3 = Input.GetAxis("Fire3");




        // transform.Translate(new Vector3(horizontal * Time.deltaTime, 0, 0)); //movimeto horizontal
        transform.Translate(new Vector3(0, vertical * Time.deltaTime, 0)); //movimento vertical
        transform.Translate(new Vector3(scrollWheel * Time.deltaTime, 0, 0)); //movimeto horizontal



        transform.Translate(new Vector3(fire1 * Time.deltaTime, 0, 0));
        transform.Translate(new Vector3( 0, fire2 * Time.deltaTime, 0));





    }
}
