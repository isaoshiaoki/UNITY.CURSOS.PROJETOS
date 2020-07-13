using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoParallax : MonoBehaviour

//https://www.luiztools.com.br/post/como-criar-parallax-scrolling-com-unity-5/#:~:text=Um%20dos%20efeitos%20mais%20legais,deslocamento%20do%20background%20da%20cena.
{
    private Renderer back;
    private float vel;
    private string nomeBack;

    void Start()
    {

        back = GetComponent<Renderer>();
        nomeBack = this.gameObject.tag;

    }

    // Update is called once per frame
    void Update()
    {



        switch (nomeBack)
        {

            case "ceu1":
                vel = 0.1f;
                break;

            case "ceu2":
                vel = 0.5f;
                break;

            case "ceu3":
                vel = 1.0f;
                break;

        }

        Vector2 offset = new Vector2(vel * Time.deltaTime, 0);

        back.material.mainTextureOffset += offset;

    }
}
