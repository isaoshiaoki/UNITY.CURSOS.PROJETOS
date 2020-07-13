using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoDoGameObjeto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //https://docs.unity3d.com/ScriptReference/Transform.Translate.html

        // Move the object to the right relative to the camera 1 unit/second.
       // transform.Translate(Time.deltaTime, 0, 0, Camera.main.transform);

        transform.Translate(Vector3.right * 1 * Time.deltaTime);

        // transform.Rotate(new Vector3(3, 0, 0));

        //  transform.localScale+=new Vector3(0.1f, 0.1f, 0);

        // Move the object to the right relative to the camera 1 unit/second.
        //transform.Translate(Vector3.right * Time.deltaTime, Camera.main.transform);

        //transform.Translate(new Vector3(3,0,0));


        // Move the object forward along its z axis 1 unit/second.
        // transform.Translate(Vector3.forward * Time.deltaTime);

        // Move the object upward in world space 1 unit/second.
        // transform.Translate(Vector3.up * Time.deltaTime, Space.World);
    }
}
