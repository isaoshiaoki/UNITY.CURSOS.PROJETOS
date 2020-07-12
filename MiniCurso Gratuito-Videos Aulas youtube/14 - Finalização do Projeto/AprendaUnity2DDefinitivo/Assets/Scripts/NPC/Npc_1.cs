using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;     //textMeshPro
using System.Xml;
using UnityEngine.UI;


public class Npc_1 : MonoBehaviour
{
    private bool dialogoOn;
    private bool respondendoPergunta;
    private _GameController _GameController;

    public string nomeArquivoNpcXml;
    public GameObject canvasNPC;
    public GameObject painelResposta;
    public Button btnA;
    public TMP_Text TextobtnA;
    public TMP_Text TextobtnB;

    public TMP_Text caixaTexto;
    public List<string> fala0;
    public List<string> fala1;
    public List<string> fala2;
    public List<string> fala3;
    public List<string> fala4;//fala ao concluir missão
    public List<string> fala5;

    public List<string> respostaFala0;

    public int idFala;
    public int idDialogo;

    public List<string> linhasDialogo;

   // public TMP_Text textoTemporario;


    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        loadDialogoData();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (_GameController.currentState==GameState.DIALOGO && Input.GetButtonDown("Acao1"))
        {
            interacao();
        }
        */
    }
    public void interacao()
    {

        if (_GameController.currentState==GameState.GAMEPLAY)
        {
            _GameController.changeState(GameState.DIALOGO);
            idFala = 0;
            //verifica se a missão foi concluida
           if(idDialogo==3 && _GameController.missao1==true){
                idDialogo = 4;//dialogo de missão concluida
            }
            prepararDialogo();
            dialogo();               
            canvasNPC.SetActive(true);
            //textoTemporario.text = "<color=#800000>Este e um texto de teste</color>";
            dialogoOn=true;

            
        }
        



    }


    public void dialogo()
    {
        if (idFala < linhasDialogo.Count ) {
            caixaTexto.text = linhasDialogo[idFala];

            if (idDialogo==0 && idFala==2)
            {
                TextobtnA.text = respostaFala0[0];
                TextobtnB.text = respostaFala0[1];

                painelResposta.SetActive(true);
                btnA.Select();
                respondendoPergunta = true;
            }


        }
        else
        {
            switch (idDialogo)
            {
                case 0:
                   
                    break;

                case 1:

                    idDialogo = 3;

                    break;

                case 2:

                    idDialogo = 0;

                    break;
                case 4:

                    idDialogo = 5;

                    break;

               
            }




            canvasNPC.SetActive(false);
            dialogoOn = false;


            _GameController.changeState(GameState.FIMDIALOGO);
        }
    }

    public void btnRespostaA()
    {
        idDialogo = 1;
        prepararDialogo();
        idFala = 0;
        respondendoPergunta = false;
        painelResposta.SetActive(false);
        dialogo();
    }


    public void btnRespostaB()
    {
        idDialogo = 2;
        prepararDialogo();
        idFala = 0;
        respondendoPergunta = false;
        painelResposta.SetActive(false);
        dialogo();
    }

    public void prepararDialogo()
    {

        linhasDialogo.Clear();

        switch (idDialogo)
        {
            case 0:
                foreach (string s in fala0 )                    
                    { linhasDialogo.Add(s); }
                break;

            case 1:
                foreach (string s in fala1)
                { linhasDialogo.Add(s); }
                break;

            case 2:
                foreach (string s in fala2)
                { linhasDialogo.Add(s); }
                break;

            case 3:
                foreach (string s in fala3)
                { linhasDialogo.Add(s); }
                break;
            case 4:
                foreach (string s in fala4)
                { linhasDialogo.Add(s); }
                break;

            case 5:
                foreach (string s in fala5)
                { linhasDialogo.Add(s); }
                break;

        }
    }

   //vai ler xml NPC
    void loadDialogoData()
    {

        TextAsset xmlData = (TextAsset)Resources.Load(_GameController.idiomaFolder[_GameController.idioma] + "/" + nomeArquivoNpcXml);
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlData.text);
        foreach (XmlNode dialogo in xmlDocument["dialogos"].ChildNodes)
        {
            string dialogoName = dialogo.Attributes["name"].Value;

            foreach (XmlNode f in dialogo["falas"].ChildNodes)
            {
                switch (dialogoName)
                {

                    case "fala0":
                        //fala0.Add(f.InnerText);
                        fala0.Add(_GameController.textoFormatado(f.InnerText));
                        break;

                    case "fala1":
                       //fala1.Add(f.InnerText);
                        fala1.Add(_GameController.textoFormatado(f.InnerText));

                        break;

                    case "fala2":
                        //fala2.Add(f.InnerText);
                        fala2.Add(_GameController.textoFormatado(f.InnerText));
                        break;

                     case "fala3":
                        //fala3.Add(f.InnerText);
                        fala3.Add(_GameController.textoFormatado(f.InnerText));
                        break;

                    case "fala4":
                       // fala4.Add(f.InnerText);
                        fala4.Add(_GameController.textoFormatado(f.InnerText));
                        break;
                    case "fala5":
                        //fala5.Add(f.InnerText);
                        fala5.Add(_GameController.textoFormatado(f.InnerText));
                        break;

                    case "resposta0":
                        //respostaFala0.Add(f.InnerText);
                        respostaFala0.Add(_GameController.textoFormatado(f.InnerText));
                        break;

                }

                
            }
        }

    }


    public void falar()
    {

        if (dialogoOn == true && respondendoPergunta == false)
        {
            idFala += 1;
            dialogo();
        }
    }

}
