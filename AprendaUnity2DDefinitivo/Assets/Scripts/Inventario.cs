using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventario : MonoBehaviour
{
    private _GameController _GameController;
    [Header("Slots")]
    public Button[] slot;
    [Header("Icone")]
    public Image[] iconItem;
    [Header("Itens")]
    public TextMeshProUGUI qtdPocao;
    public TextMeshProUGUI qtdMana;
    public TextMeshProUGUI  qtdFlechaA;
    public TextMeshProUGUI qtdFlechaB;
    public TextMeshProUGUI qtdFlechaC;

    [Header("Quantidade de Itens")]
    public int quantidadePocao;
    public int quantidadeMana;
    public int quantidadeFlechaA;
    public int quantidadeFlechaB;
    public int quantidadeFlechaC;

    [Header("Inventario")]

    public List<GameObject> itemInventario;
    public List<GameObject> itensCarregados;


    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;







    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void carregarInventario()
    {

        limparItensCarregados();


            foreach (Button b in slot )
        {

            b.interactable = false;

        }

        foreach (Image i in iconItem)
        {

            i.sprite = null;
            i.gameObject.SetActive(false);
        }

        qtdPocao.text  = " x " + _GameController.qtdPocoes[0].ToString();
        qtdMana.text = " x " + _GameController.qtdPocoes[1].ToString();
        qtdFlechaA.text = " x " + _GameController.qtdFlechas[0].ToString();
        qtdFlechaB.text = " x " + _GameController.qtdFlechas[1].ToString();
        qtdFlechaC.text = " x " + _GameController.qtdFlechas[2].ToString();


        int idSlot = 0;//id do slot
        foreach (GameObject i in itemInventario)
        {
            GameObject temp = Instantiate(i);
            Item itemInfo = temp.GetComponent<Item>();

            itensCarregados.Add(temp);
            slot[idSlot].GetComponent<SlotInventario>().objetoSlot = temp;
            slot[idSlot].interactable = true;
            iconItem[idSlot].sprite = _GameController.imgInventario[itemInfo.idItem];
            iconItem[idSlot].gameObject.SetActive(true);
            idSlot++;


        }


    }

    public void limparItensCarregados()
    {

        foreach (GameObject ic in itensCarregados)
        {
            Destroy(ic);
        }

        itensCarregados.Clear();

    }





}
