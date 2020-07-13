using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class InputGetAxis : MonoBehaviour
{

     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal* Time.deltaTime,0,0)); //movimeto horizontal
        transform.Translate(new Vector3(0, vertical * Time.deltaTime, 0)); //movimento vertical

    }






}
