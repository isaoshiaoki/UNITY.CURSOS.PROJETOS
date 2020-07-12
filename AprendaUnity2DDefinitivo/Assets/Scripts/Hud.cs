using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;     //textMeshPro

public class Hud : MonoBehaviour
{
    
    private Player playerScript;
    private _GameController _GameController;
    [Header("Configuração de Vida")]
    public Image[] hpBar;



    [Header("Configuração de Flecha")]
    public Image iconeFlecha;

    [Header("Configuração de Sprites")]
    public Sprite full;
    public Sprite half;
    

    [Header("Configuração de Manas")]
     public Image[] mpBar;
    public Sprite mpFull;
    public Sprite mpHalf;

    [Header("Painel de Manas")]
    public GameObject painelMana;
    public GameObject painelFlechas;
    public TextMeshProUGUI qtdFlechas;

    [Header("Painel de HP")]
    public GameObject boxMp;
    public GameObject boxHp;
    public TextMeshProUGUI qtdHpBoxTxt;
    public TextMeshProUGUI qtdMpBoxTxt;
    public RectTransform boxA;
    public RectTransform boxB;
    public Vector2 posicaoA;
    public Vector2 posicaoB;



    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType(typeof(Player)) as Player;
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;


        painelMana.SetActive(false);
        painelFlechas.SetActive(false);
        boxMp.SetActive(false);
        boxHp.SetActive(false);


        if (_GameController.idClasse[_GameController.idPersonagem]==2)
        {

            painelMana.SetActive(true);
          
        }
        else if (_GameController.idClasse[_GameController.idPersonagem] == 1)
        {
            iconeFlecha.sprite = _GameController.iconeFlecha[_GameController.idFlechaEquipada];
            painelFlechas.SetActive(true);

        }

        posicaoA = boxA.anchoredPosition;
        posicaoB = boxB.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {

       controleBarraVida();
        posicaoCaixaPocoes();
        

        if (painelMana.activeSelf==true)
        {

         controleBarraMana();
            
            }
            else if (painelFlechas.activeSelf==true)
        {
            //pressionar botao "q" que e para esquerda
            if (Input.GetButtonDown("BtnLeft")) {

                if (_GameController.idFlechaEquipada==0) {

                    _GameController.idFlechaEquipada = _GameController.iconeFlecha.Length - 1;
                }
                else
                {
                    _GameController.idFlechaEquipada -= 1;

                }



            }
            //pressionar botao "e" que e para direita
            else if (Input.GetButtonDown("BtnRight")) 
            {

                if (_GameController.idFlechaEquipada == _GameController.iconeFlecha.Length - 1)
                {

                    _GameController.idFlechaEquipada = 0;
                }
                else
                {
                    _GameController.idFlechaEquipada += 1;

                }



            }

            iconeFlecha.sprite = _GameController.iconeFlecha[_GameController.idFlechaEquipada];
            qtdFlechas.text =" X " + _GameController.qtdFlechas[_GameController.idFlechaEquipada].ToString();


            qtdHpBoxTxt.text = _GameController.qtdPocoes[0].ToString();
            qtdMpBoxTxt.text = _GameController.qtdPocoes[1].ToString(); 


        }




        
    }







   public  void controleBarraMana()   {     

        float percMana = (float)_GameController.manaAtual / (float)_GameController.manaMax;//calcula o pecentual de mana
                                                                                           //representa 100% de mana
        if (Input.GetButtonDown("ItemB") && percMana<1)
        {
            _GameController.usarPocao(1); //pocao de mana
        }


        foreach (Image img in mpBar)
        {

            img.enabled = true;
            img.sprite = mpFull;

        }

        if (percMana == 1)
        {

        }
        else if (percMana >= 0.9f)
        {
            mpBar[4].sprite = mpHalf;


        }
        else if (percMana >= 0.8f)
        {
            mpBar[4].enabled = false;


        }
        else if (percMana >= 0.7f)
        {
            mpBar[4].enabled = false;
            mpBar[3].sprite = mpHalf;


        }
        else if (percMana >= 0.6f)
        {
            mpBar[4].enabled = false;
            mpBar[3].enabled = false;


        }
        else if (percMana >= 0.5f)
        {
            mpBar[4].enabled = false;
            mpBar[3].enabled = false;

            mpBar[2].sprite = mpHalf;
        }
        else if (percMana >= 0.4f)
        {
            mpBar[4].enabled = false;
            mpBar[3].enabled = false;
            mpBar[2].enabled = false;



        }
        else if (percMana >= 0.3f)
        {
            mpBar[4].enabled = false;
            mpBar[3].enabled = false;
            mpBar[2].enabled = false;


            mpBar[1].sprite = mpHalf;
        }
        else if (percMana >= 0.2f)
        {
            mpBar[4].enabled = false;
            mpBar[3].enabled = false;
            mpBar[2].enabled = false;
            mpBar[1].enabled = false;




        }
        else if (percMana >= 0.01f)
        {
            mpBar[4].enabled = false;
            mpBar[3].enabled = false;
            mpBar[2].enabled = false;
            mpBar[1].enabled = false;


            mpBar[0].sprite = mpHalf;

        }

        else if (percMana >= 0)
        {
            mpBar[4].enabled = false;
            mpBar[3].enabled = false;
            mpBar[2].enabled = false;
            mpBar[1].enabled = false;
            mpBar[0].enabled = false;

        }

        if (_GameController.qtdPocoes[1] > 0) {
            boxMp.SetActive(true);
        }
        else
        {
            boxMp.SetActive(false);

        }








    }


   public void  posicaoCaixaPocoes()
    {

        if (_GameController.qtdPocoes[0] > 0 )
        {
            boxHp.GetComponent<RectTransform>().anchoredPosition = posicaoA;
            boxMp.GetComponent<RectTransform>().anchoredPosition = posicaoB;



        }
        else
        {
            boxHp.GetComponent<RectTransform>().anchoredPosition = posicaoB;
            boxMp.GetComponent<RectTransform>().anchoredPosition = posicaoA;


        }



    }






  public  void controleBarraVida() {          



        float percVida = (float)_GameController.vidaAtual / (float)_GameController.vidaMaxima;//calcula o pecentual de vida

        if (Input.GetButtonDown("ItemA") && percVida<1)
        {
            _GameController.usarPocao(0); //pocao de mana
        }



        //representa 100% de vida
        foreach (Image img in hpBar ) {

            img.enabled = true;
            img.sprite = full;

        }

        if (percVida == 1)
        {     

        }
        else if (percVida >= 0.9f)
        {
            hpBar[4].sprite = half;


        }
        else if (percVida >= 0.8f)
        {
            hpBar[4].enabled = false;


        }
        else if (percVida >= 0.7f)
        {
            hpBar[4].enabled = false;
            hpBar[3].sprite = half;


        }
        else if (percVida >= 0.6f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;

        
        }
        else if (percVida >= 0.5f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            
            hpBar[2].sprite = half;
        }
        else if (percVida >= 0.4f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;

            

        }
        else if (percVida >= 0.3f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            

            hpBar[1].sprite = half;
        }
        else if (percVida >= 0.2f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            hpBar[1].enabled = false;



           
        }
        else if (percVida >= 0.01f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            hpBar[1].enabled = false;
           

            hpBar[0].sprite = half;

        }

        else if (percVida >= 0)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            hpBar[1].enabled = false;


            hpBar[0].enabled = false;

        }
        if (_GameController.qtdPocoes[0] > 0)
        {
            boxHp.SetActive(true);
        }
        else
        {
            boxHp.SetActive(false);

        }


    }



    public void verificarHudPersonagem()

    {
        painelMana.SetActive(false);
        painelFlechas.SetActive(false);

        if (_GameController.idClasse[_GameController.idPersonagem] == 2)
        {

            painelMana.SetActive(true);

        }
        else if (_GameController.idClasse[_GameController.idPersonagem] == 1)
        {
            iconeFlecha.sprite = _GameController.iconeFlecha[_GameController.idFlechaEquipada];
            painelFlechas.SetActive(true);

        }

    }



}
