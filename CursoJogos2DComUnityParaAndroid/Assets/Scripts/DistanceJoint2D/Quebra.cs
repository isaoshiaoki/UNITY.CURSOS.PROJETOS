using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quebra : MonoBehaviour
{
    public DistanceJoint2D bola;
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
