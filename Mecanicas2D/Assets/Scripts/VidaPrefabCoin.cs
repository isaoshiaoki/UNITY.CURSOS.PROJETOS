using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPrefabCoin : MonoBehaviour
{
    private float tempo = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempo -= Time.deltaTime;
        if (tempo<=0)
        {
            Destroy(this.gameObject);
        }
        
    }

   


}
