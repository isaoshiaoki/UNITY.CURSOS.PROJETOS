using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    private _GameController _GameController;

    [Header("Array de GameObjets")]     
    public GameObject[] itemColetar;

    [Header("Boleano")]
  
    public bool coletado;

    // Start is called before the first frame update

    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;



    }






    public void coletar()
    {
        if (coletado==false) {
            coletado = true;
            _GameController.coletarArma(itemColetar[Random.Range(0,itemColetar.Length)]);
        }
        Destroy(this.gameObject);
    }

}
