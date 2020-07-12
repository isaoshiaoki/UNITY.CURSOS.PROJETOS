using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;     //textMeshPro
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
public enum GameState{
    
      PAUSE,
      GAMEPLAY,
      ITENS,
      DIALOGO,
      FIMDIALOGO,
      LOADGAME
    }

public class _GameController : MonoBehaviour {


    // private Fade fade;
    private AudioController audioController;
    private Player playerScript;
    private Inventario inventario;
    private Hud hud;

    [Header("Estado De Maquina")]
    public GameState currentState;

    [Header("Informacoes Do Player")]
    public int idPersonagem;
    public int idPersonagemAtual;
    public int idArma;
    public int idArmaAtual;
    public int vidaMaxima;
    public int vidaAtual;
    public int manaMax;
    public int manaAtual;
    public int idFlechaEquipada;
    public int[] qtdPocoes; //0 =pocao de cura   ,1 =pocao de mana   


    [Header("Banco de DADOS de Personagens")]
    public string[] nomePersonagem;
    public Texture[] spriteSheetName;
    public int[] idClasse;
    public GameObject[] armaInicial;
    public int idArmaInicial;
    public ItemModelo[] armaInicialPersonagem;



    [Header("Banco de DADOS de Armas")]
    //classe:0   Machado,espadas,martelo
    //classe:1   Arcos
    //classe:2  staffs
   
    public List<string> nomeArma;    
    public List<Sprite> imgInventario;
    public List<int> custoArma;
    public List<int> idClasseArma;
    public List<Sprite> spriteArmas1;
    public List<Sprite> spriteArmas2;
    public List<Sprite> spriteArmas3;
    public List<Sprite> spriteArmas4;
    public List<int> danoMinArma;
    public List<int> danoMaxArma;
    public List<int> tipoDanoArma;
     
    

    
    public List<int> aprimoramentoArma;

    [Header("Flechas")]

    public Sprite[] iconeFlecha;//0 flecha comum-1 flecha de prata -2 flecha de ouros
    public int[] qtdFlechas;
    public Sprite[] imgFlecha; //0 flecha comum-1 flecha de prata -2 flecha de ouros
    public float[] velocidadeFlecha;
    public GameObject[] flechaPrefab;


    [Header("Danos")]
    public string[] tiposDano;
	public GameObject[] fxDano;

    [Header("Fx Itens")]
    public GameObject fxMorte;
    public TextMeshProUGUI goldTxt;

    [Header("Quantidade coletada")]  
    public int gold;//armazena a qtd de ouro q coletamos

    [Header("Paineis")]
    public GameObject painelPause;
    public GameObject painelItens;
    public GameObject painelItemInfo;


    [Header("Primeiro elemento de cada painel")]
    public Button firstPainelPause;
    public Button firstPainelItens;
    public Button firstPainelItemInfo;

    [Header("Materiais")]
    public Material luz2D;
    public Material luz2DPadrao;

    [Header("Idioma")]

    public int idioma;
    public string[] idiomaFolder;

    [Header("Controle de Missão")]
  //  public bool missao1Aceita;  // indica que aceitei a missao
    public bool missao1;  // indica que a missão foi concluida
   private List<string> itensInventario;
    

    // Use this for initialization
    void Start () {

        

        //manter o objeto depois de mudar de cena
        DontDestroyOnLoad(this.gameObject);
        playerScript = FindObjectOfType(typeof(Player)) as Player;
        inventario = FindObjectOfType(typeof(Inventario)) as Inventario;
        hud = FindObjectOfType(typeof(Hud)) as Hud;
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;

        painelPause.SetActive(false);
        painelItens.SetActive(false);
        painelItemInfo.SetActive(false);

        load(PlayerPrefs.GetString("slot"));


    }
	
	// Update is called once per frame
	void Update () {

        if (currentState==GameState.GAMEPLAY) {


            if (playerScript == null) { playerScript = FindObjectOfType(typeof(Player)) as Player; }


            //parametro ("N0") define se havera casas decimais
            string pontos = gold.ToString("N0");

            //substitue a virgula do padrao americano para ponto e ficar no padrao brasileiro
            goldTxt.text = pontos.Replace(",", ".");

            //validarArma();


            if (Input.GetButtonDown("Cancel") && currentState != GameState.ITENS) {


                audioController.tocarFx(audioController.fxClick, 1);
                pauseGame();
            }


        }


    }


    public void validarArma()
    {
        /*
        if (idClasseArma[idArma] != idClasse[idPersonagem])
        {
            idArma = idArmaInicial;
            playerScript.trocarArma(idArma);
        }
          */




        if (idClasseArma[idArma] != idClasse[idPersonagem])
        {
            idArma = idArmaInicial;
           playerScript.trocarArma(idArma);
        }
         
        


    }


    public void pauseGame() {

       bool  pauseState= painelPause.activeSelf;
        pauseState = !pauseState;
        painelPause.SetActive (pauseState);

        

        switch (pauseState)

        {
            case true:

               // Time.timeScale = 0;
                changeState(GameState.PAUSE);
                firstPainelPause.Select();
                break;

            case false:
               
                changeState(GameState.GAMEPLAY);
              // Time.timeScale = 1;
                break;

                

        }
    }




    public void changeState(GameState newState)
    {

        currentState = newState;
       switch (newState)
        {
            case GameState.GAMEPLAY: 
            Time.timeScale = 1;
                break;

            case GameState.PAUSE:

                Time.timeScale = 0;
                break;

            case GameState.ITENS:

                Time.timeScale = 0;
                break;

            case GameState.FIMDIALOGO:

                StartCoroutine("fimConversa");
                break;


        }




    }


    public void btnItensDown()
    {
        
        painelPause.SetActive(false);
        painelItens.SetActive(true);
        firstPainelItens.Select();
        inventario.carregarInventario();
        changeState(GameState.ITENS);
    }



    public void fecharPainel()
    {
        painelItens.SetActive(false);        
        painelPause.SetActive(true);
        painelItemInfo.SetActive(false);
        firstPainelPause.Select();
        inventario.limparItensCarregados();
        changeState(GameState.PAUSE);
    }

  /*
   *public void fecharPainelItemInfo()
    {
        painelItens.SetActive(true);
        painelPause.SetActive(true);
        painelItemInfo.SetActive(false);
        firstPainelPause.Select();
        
        changeState(GameState.PAUSE); 
    }
   */
    public void usarItemArma(int idArma)
    {
        playerScript.trocarArma(idArma);
    }


    public void openItemInfo()
    {
        painelItemInfo.SetActive(true); 
    }

    public void voltarGameplay()
    {
        painelItens.SetActive(false);
        painelPause.SetActive(false);
        painelItemInfo.SetActive(false);
        changeState(GameState.GAMEPLAY);
    }





    public void excluirItem(int idSlot)
    {
        inventario.itemInventario.RemoveAt(idSlot);
        inventario.carregarInventario();
        painelItemInfo.SetActive(false);
        firstPainelItens.Select();
    }


      public void aprimorarArma(int idArma)
    {
        int ap = aprimoramentoArma[idArma];

        if (ap < 10) {
            ap += 1;

            aprimoramentoArma[idArma] = ap;
        }

    }




    public void swap(int idSlot )
    {
        GameObject t1 = inventario.itemInventario[0];
        GameObject t2 = inventario.itemInventario[idSlot];

        inventario.itemInventario[0] = t2;
        inventario.itemInventario[idSlot] = t1;

        voltarGameplay();

    }



    public void coletarArma(GameObject objetoColetado) {

        

        inventario.itemInventario.Add(objetoColetado);

    }


    public void usarPocao(int idPocao) {

           

        if (qtdPocoes[idPocao] > 0) { 

           qtdPocoes[idPocao] -=1;  
            
        switch (idPocao) {

            case 0: //pocao cura
                    vidaAtual += 3;
                    if (vidaAtual > vidaMaxima)
                    {
                        vidaAtual = vidaMaxima;
                    }



                break;

            case 1:  //pocao mana
                     //programar o uso da pocao
                    manaAtual += 3;//recupera 3 de mana

                break;
          }


        }


    }

    IEnumerator fimConversa()
    {
        yield return new WaitForEndOfFrame();
        changeState(GameState.GAMEPLAY);
    }



    public string textoFormatado(string frase)
    {
        //cor=nomeCor <color=#corCorrespondente>
        //   fimCor </corlor>

        string temp = frase;
         temp=frase.Replace("cor=yellow", "<color=#ffff00ff>");
        //  temp = frase.Replace("cor=red", "<color=#ff0000ff>");
        //temp = frase.Replace("negrito", "<b>");
        //temp = frase.Replace("fim", "</b>");
        //temp = frase.Replace("cor=red", "<color=#ff0000ff>");

        temp = temp.Replace("end", "</color>");
        temp = temp.Replace("fimTag", "</color>");

        return temp;
    }





    //salva arquivo
    public void save()
    {

        string nomeArquivo = PlayerPrefs.GetString("slot");

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + nomeArquivo);

        PlayerData data = new PlayerData();
        data.idioma = idioma;
        data.idPersonagem = idPersonagem;
        data.gold = gold;
        data.idArma = idArma;
        data.idFlechaEquipada= idFlechaEquipada;
        data.qtdFlechas=qtdFlechas;
        data.qtdPocoes = qtdPocoes;
        data.aprimoramentoArma = aprimoramentoArma;


        //verifica o iventario
        
        if (itensInventario.Count !=0)
        {
           itensInventario.Clear();
        }

        foreach (GameObject i in inventario.itemInventario)
        {
            itensInventario.Add(i.name);
        }


        data.itensInvetario = itensInventario;
        bf.Serialize(file, data);
        file.Close();
    }


    //carrega arquivo
    public void load(string slot)
    {
        if (File.Exists(Application.persistentDataPath + "/" + slot))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + slot , FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            idioma = data.idioma;
            gold= data.gold;
            idPersonagem = data.idPersonagem;
            idFlechaEquipada = data.idFlechaEquipada;
            qtdFlechas = data.qtdFlechas;
            itensInventario = data.itensInvetario;
            idArma=data.idArma  ;
            idArmaAtual = data.idArma;
            //idArmaInicial = data.idArma;
            aprimoramentoArma = data.aprimoramentoArma;

            inventario.itemInventario.Clear();  
            foreach (string i in itensInventario) {
                inventario.itemInventario.Add(Resources.Load<GameObject>("Armas/" + i));
            }
            file.Close();

            inventario.itemInventario.Add(armaInicial[idPersonagem]);
            GameObject tempArma = Instantiate(armaInicial[idPersonagem]);
            inventario.itensCarregados.Add(tempArma);
            idArmaInicial = tempArma.GetComponent<Item>().idItem;
           vidaAtual = vidaMaxima;
            manaAtual = manaMax;

            hud.verificarHudPersonagem();
           // hud.controleBarraVida();
           //hud.controleBarraHud();
            changeState(GameState.GAMEPLAY);

            string nomeCena = "cena1";
            audioController.trocarMusica(audioController.musicaFase1,nomeCena,true);

        }
        else
        {
            newGame();
        }
       
    }



    void newGame()
    {

        //define os valores iniciais do jogo

        gold = 0; 
        //recupera do PlayerPrefs que veio do titulo
        idPersonagem = PlayerPrefs.GetInt("idPersonagem");
        idArma = armaInicialPersonagem[idPersonagem].idArma;
        idArmaAtual = armaInicialPersonagem[idPersonagem].idArma;
        idFlechaEquipada = 0;
        qtdFlechas[0] = 25;
        qtdFlechas[1] = 0;
        qtdFlechas[2] = 0;


        qtdPocoes[0] = 3;
        qtdPocoes[1] = 3;





        //inventario.itemInventario.Add(armaInicial[idPersonagem]);
        //  GameObject tempArma = Instantiate(armaInicial[idPersonagem]);
        //  inventario.itensCarregados.Add(tempArma);
        //  idArmaInicial = tempArma.GetComponent<Item>().idItem;
        //vidaAtual = vidaMaxima;
        //manaAtual = manaMax;

        save();
        load(PlayerPrefs.GetString("slot"));
    }

    public void click()
    {
        audioController.tocarFx(audioController.fxClick, 1);
    }
}
[Serializable]

class PlayerData
{
    public int idioma;
    public int gold;
    public int idPersonagem;
    public int idArma;
    public int idArmaAtual;
    public int idFlechaEquipada;
    public int[] qtdFlechas;
    public int[] qtdPocoes;
   
    public List<string> itensInvetario;
    public List<int> aprimoramentoArma;
   

}