using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    private Fade fade;
    private Player playerScript;

    [Header("Destino")]

    public Transform destino;


    [Header("Boleanos da Luz")]
    public bool escuro;

    [Header("Materiais")]
    public Material luz2D;
    public Material luz2DPadrao;


    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType(typeof(Fade)) as Fade;
        playerScript = FindObjectOfType(typeof(Player)) as Player;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interacao()
    {



        StartCoroutine("acionarPorta");
       


    }



  IEnumerator acionarPorta()
    {
        fade.fadeIn();
        yield return new WaitWhile(()=> fade.fume.color.a < 0.9f );
       playerScript.gameObject.SetActive(false);

        switch (escuro)
        {
            case true:
                playerScript.changeMaterial(luz2D);
               // tPlayer.gameObject.GetComponent<SpriteRenderer>().material = luz2D;

                break;

            case false:

                playerScript.changeMaterial(luz2DPadrao);
                // tPlayer.gameObject.GetComponent<SpriteRenderer>().material = luz2DPadrao;


                break;






        }


        playerScript.transform.position = destino.position;
        //yield return new WaitForSeconds(0.5f);
        playerScript.gameObject.SetActive(true);

        fade.fadeOut();

    }


}
