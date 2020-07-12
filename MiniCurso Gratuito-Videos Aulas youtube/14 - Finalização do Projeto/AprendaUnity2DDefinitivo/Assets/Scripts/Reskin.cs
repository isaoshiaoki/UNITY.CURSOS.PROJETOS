using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Reskin : MonoBehaviour
{
    private _GameController _GameController;
    private SpriteRenderer sRender;
    private Dictionary<string,Sprite> spriteSheet;


    [Header("Array de Sprites")]

    public Sprite[] sprites;
    public string spriteSheetName;//nome do sprite que queremos ultilizar
    public string loadedSpriteSheetName; //nome do spritesheet atual

    [Header("Personagem")]
    public bool isPlayer;//indica se o script esta associado ao personagem jogavel


    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;

      //tem q estar marcado o checkbox no player e reskin  
        if (isPlayer) {
            spriteSheetName = _GameController.spriteSheetName[_GameController.idPersonagem].name;
        }

        sRender = GetComponent<SpriteRenderer>();
        loadSpritesheet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()   

    {
        if (isPlayer)
        {
            if (_GameController.idPersonagem != _GameController.idPersonagemAtual)
            {
                spriteSheetName = _GameController.spriteSheetName[_GameController.idPersonagem].name;

                _GameController.idPersonagemAtual = _GameController.idPersonagem;
            }


            _GameController.validarArma();
        }


        if (loadedSpriteSheetName != spriteSheetName)
        {

            loadSpritesheet();

        }

        sRender.sprite = spriteSheet[sRender.sprite.name];


    }


    private void loadSpritesheet() {

        sprites = Resources.LoadAll<Sprite>(spriteSheetName);
        spriteSheet = sprites.ToDictionary(x => x.name,x => x);
        loadedSpriteSheetName = spriteSheetName;



    }


}
