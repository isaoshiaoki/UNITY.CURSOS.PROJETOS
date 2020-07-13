using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceJoint2DQuebra : MonoBehaviour
{
    public DistanceJoint2D bola;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            bola.breakForce = 0;
        }  
    }
}
