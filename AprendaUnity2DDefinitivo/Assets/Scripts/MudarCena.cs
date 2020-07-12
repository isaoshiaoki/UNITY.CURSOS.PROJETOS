using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MudarCena : MonoBehaviour
{
    private Fade fade;
    private _GameController _GameController;
    [Header("Cena de Destino")]
    public string cenaDestino;
    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        fade = FindObjectOfType(typeof(Fade)) as Fade;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interacao()
    {



        StartCoroutine("mudancaCena");


    }

    IEnumerator mudancaCena()
    {

        fade.fadeIn();
        yield return new WaitWhile(() => fade.fume.color.a < 0.9f);
        if (cenaDestino=="Titulo") { Destroy(_GameController.gameObject); }
        SceneManager.LoadScene(cenaDestino);
    }




}
