using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private _GameController _GameController;

    [Header("Id Item")]
    public int idItem;


    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void usarItem()
    {
        print(" Este item " + idItem + " foi utilizado ");

        _GameController.usarItemArma(idItem);

    }
   






}
