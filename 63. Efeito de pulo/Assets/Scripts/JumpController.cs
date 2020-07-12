using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    public float forca = 100f;
    public Rigidbody2D bola;
    public bool liberaPulo=false;
    public int duplo = 2;


    // Start is called before the first frame update
    void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {

        /*  */
         

         
        if (duplo > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bola.AddForce(new Vector2(0, forca * Time.deltaTime), ForceMode2D.Impulse);

                duplo --;
            }
        }


    }


     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            duplo = 2;
            liberaPulo = true;
        }

        print("colidio");

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            liberaPulo = false;
        }
    }

}
