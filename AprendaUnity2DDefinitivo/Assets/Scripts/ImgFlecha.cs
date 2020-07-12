using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgFlecha : MonoBehaviour
{
    private _GameController _GameController;
    private SpriteRenderer sRender;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        sRender = GetComponent<SpriteRenderer>();



    }

    // Update is called once per frame
    void Update()
    {
        sRender.sprite = _GameController.imgFlecha[_GameController.idFlechaEquipada];
    }
}
