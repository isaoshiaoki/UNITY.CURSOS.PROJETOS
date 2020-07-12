using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PainelItemInfo : MonoBehaviour
{
    private _GameController _GameController;
    private int idArma;
    private int aprimoramento;

    [Header("Id Slots")]
    public int idSlot;

    [Header("GameObjets")]
    public GameObject objetoSlot;
    public GameObject[] aprimoramentos;

    [Header("HUD")]
    public Image imgItem;
    public TMP_Text nomeItem;
    public TMP_Text danoArma;

    [Header("Botoes")]
    public Button btnAprimorar;
    public Button btnEquipar;
    public Button btnExcluir;

    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void carregarInfoItem() {

        Item itemInfo = objetoSlot.GetComponent<Item>();
        idArma = itemInfo.idItem;
        imgItem.sprite = _GameController.imgInventario[idArma];
        nomeItem.text = _GameController.nomeArma[idArma];
        string tipoDano = _GameController.tiposDano[_GameController.tipoDanoArma[idArma]];

        


        int danoMin = _GameController.danoMinArma[idArma] + aprimoramento;
        int danoMax = _GameController.danoMaxArma[idArma] + aprimoramento;
        danoArma.text = " Dano: " + danoMin.ToString() + "-" + danoMax.ToString() + " / " + tipoDano;

        
        carregarAprimoramento();

        if (idSlot==0) {
            btnEquipar.interactable = false;
            btnExcluir.interactable = false;
        }
        else
        {
            int idClasseArma = _GameController.idClasseArma[idArma];
            int idClassePersonagem = _GameController.idClasse[_GameController.idPersonagem];

            if (idClasseArma== idClassePersonagem)
            {
               btnEquipar.interactable = true;
            }
            else {
                btnEquipar.interactable = false;
            }



            btnExcluir.interactable = true;

        }



    }


         public void  botaoAprimorar()
    {
        _GameController.aprimorarArma(idArma);
        carregarAprimoramento();

    }



    public void botaoEquipar()
    {
        objetoSlot.SendMessage("usarItem", SendMessageOptions.DontRequireReceiver);
        _GameController.swap(idSlot);

    }




    public void botaoExcluir()
    {

        _GameController.excluirItem(idSlot);
    }



    public void carregarAprimoramento() {

         aprimoramento = _GameController.aprimoramentoArma[idArma];

        if (aprimoramento >= 10) {
            btnAprimorar.interactable = false;
        }
        else
        {
            btnAprimorar.interactable = true;
        }


        foreach (GameObject a in aprimoramentos)
        {

            a.SetActive(false);
        }
        for (int i = 0; i < aprimoramento; i++)
        {

            aprimoramentos[i].SetActive(true);

        }
    }













}
