using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriaBalas : MonoBehaviour
{
    public GameObject balas;
    public GameObject cano;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //nao aconpanha a rotacao da arma
            /*
            Instantiate(balas, new Vector3(cano.transform.position.x, cano.transform.position.y, cano.transform.position.z), Quaternion.identity);
             */


            Instantiate(balas, new Vector3(cano.transform.position.x, cano.transform.position.y, cano.transform.position.z),cano.transform.rotation);




        }
    }
}
