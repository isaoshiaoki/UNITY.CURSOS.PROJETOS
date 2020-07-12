using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSet : MonoBehaviour
{

    public float velocidade = 0.1f;
    public Renderer quad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offSet = new Vector2(velocidade * Time.deltaTime,0);
        quad.material.mainTextureOffset += offSet;

    }
}
