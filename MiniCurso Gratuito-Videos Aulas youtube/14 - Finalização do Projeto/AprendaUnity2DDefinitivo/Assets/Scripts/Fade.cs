using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    
    [Header("Lista de GameObject")]
    public GameObject painelFume;


    [Header("Lista de imagem")]
    public Image fume;

    [Header("Array de cores")]
    public Color[] corTransicao;

    [Header("Incremento")]
    public float step;

    [Header("Boleanos")]
    public bool emTransicao;




    public void fadeIn() {
        if (emTransicao==false) {
            painelFume.SetActive(true);
            StartCoroutine("fadeI");

        }

    }

   public void fadeOut()
    {
        StartCoroutine("fadeO");

    }


   IEnumerator fadeI()
    {
        //evita bug em painel fume
        emTransicao = true;

        for (float i=0;i<=1;i+=step) {
            fume.color=Color.Lerp(corTransicao[0], corTransicao[1],i);
            yield return new WaitForEndOfFrame();

        }
    }

    IEnumerator fadeO()
    {
        yield return new WaitForSeconds(0.5f);
        for (float i = 0; i <= 1; i += step)
        {

            fume.color = Color.Lerp(corTransicao[1], corTransicao[0], i);
            yield return new WaitForEndOfFrame();
           emTransicao = false;
        }
        
    }




}
