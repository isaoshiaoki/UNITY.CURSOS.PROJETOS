using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private _GameController _GameController;
    public int valor;
    // Start is called before the first frame update

    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
       


    }






    public void coletar()
    {
        _GameController.gold +=valor;
        Destroy(this.gameObject);
    }


}
