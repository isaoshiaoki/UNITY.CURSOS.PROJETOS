using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bau : MonoBehaviour
{
    private _GameController gameController;
    private SpriteRenderer spriteRenderer;

    [Header("Quantidade de Itens")]
    public int qtdMaxItens;
    public int qtdMinItens;

    [Header("Array de Sprites")]
    public Sprite[] imagemObjeto;


    [Header("Array de Sprites")]
    public bool open;

    [Header("Array de GameObject")]
    public GameObject[] loots;

    [Header("Loot")]
    public bool gerouLoot;



    // Use this for initialization
    void Start()
    {

        //gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    //metodo chamado plo codigo: objetoInteracao.SendMessage("interacao", SendMessageOptions.DontRequireReceiver);
    public void interacao()
    {
        
        if (open ==false) {
         open = true;
        spriteRenderer.sprite = imagemObjeto[1];
         StartCoroutine("gerarLoot");
         GetComponent<Collider2D>().enabled = false;
        }
             

    }

    IEnumerator gerarLoot() {

        gerouLoot = true;
        //controle de loot  .
        int qtdMoedas = Random.Range(qtdMinItens, qtdMaxItens);
        for (int l = 0; l < qtdMoedas; l++)
        {
            int rand = 0;
            int idLoot = 0;
            
            rand = Random.Range(0, 100);

            //para condicionar a 50% de chance de sair a coin2

           /*
            if (rand>=50)
            {

                 idLoot = 1;

            }
            */


            GameObject lootTemp = Instantiate(loots[idLoot], transform.position, transform.localRotation);
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-25, 25), 80));
           /* */
            yield return new WaitForSeconds(0.1f);
        }

    }




}
