using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Linq;

public class LoadArms : MonoBehaviour
{
    private _GameController _GameController;



    [Header("Arquivo de armas")]
    public string nomeArquivoXml; //nome do arquivo Xml que sera feito a leitura

    [Header("Listagem de armas")]
    public List<string> nomeArma;  //armazena o nome da arma para exibir no inventario e na loja
    public List<string> nomeIconeArma; //nome dp icone no arquivo SpriteSheet de icone das armas
    public List<Sprite> iconeArma;   //icone ixibido no inventario e loja
    public List<string> categoriaArma;  //martelo,machado,maca,espada
    public List<int> idClasseArma;

    [Header("Dano das armas")]

    public List<int> danoMinArma;
    public List<int> danoMaxArma;
    public List<int> tipoDanoArma;

    [Header("Listagem de sprites das armas")]
    public List<Sprite> spriteArmas1;
    public List<Sprite> spriteArmas2;
    public List<Sprite> spriteArmas3;
    public List<Sprite> spriteArmas4;

    /*..........TEMPORARIOS..............*/
    [Header("Banco de sprites das armas")]
    public List<Sprite> bancoDeSpritesArma; //Armazena todos os sprites de todas as armas de forma temporaria
    public Sprite[] spriteSheetIconesArmas;
    public Sprite[] espadas;
    public Sprite[] machados;
    public Sprite[] arcos;
    public Sprite[] macas;
    public Sprite[] martelos;
    public Sprite[] staffs;
    private Dictionary<string, Sprite> spriteSheetArmas;

    [Header("Texturas das armas")]
    public Texture ssIcones;
    public Texture ssEspadas;
    public Texture ssMachados;
    public Texture ssArcos;
    public Texture ssMacas;
    public Texture ssMartelo;
    public Texture ssStaffs;


    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;

        loadData();


    }

    //funcao responsavel pela leitura do arquivo xml   e carregamento das imagens
    void loadData()
    {
        spriteSheetIconesArmas = Resources.LoadAll<Sprite>(ssIcones.name);

        espadas = Resources.LoadAll<Sprite>(ssEspadas.name);
        machados = Resources.LoadAll<Sprite>(ssMachados.name);
        arcos = Resources.LoadAll<Sprite>(ssArcos.name);
        macas = Resources.LoadAll<Sprite>(ssMacas.name);
        martelos = Resources.LoadAll<Sprite>(ssMartelo.name);
        staffs = Resources.LoadAll<Sprite>(ssStaffs.name);

        foreach (Sprite s in espadas)
        {
            bancoDeSpritesArma.Add(s);
        }
        foreach (Sprite s in machados)
        {
            bancoDeSpritesArma.Add(s);
        }
        foreach (Sprite s in arcos)
        {
            bancoDeSpritesArma.Add(s);
        }

        foreach (Sprite s in macas)
        {
            bancoDeSpritesArma.Add(s);
        }
        foreach (Sprite s in martelos)
        {
            bancoDeSpritesArma.Add(s);
        }
        foreach (Sprite s in staffs)
        {
            bancoDeSpritesArma.Add(s);
        }

        spriteSheetArmas = bancoDeSpritesArma.ToDictionary(x=>x.name,x=>x);

        //leitura do xml
        TextAsset xmlData = (TextAsset)Resources.Load(_GameController.idiomaFolder[_GameController.idioma] + "/" + nomeArquivoXml);
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlData.text);
        foreach (XmlNode atributo in xmlDocument["Armas"].ChildNodes)
        {
            string att = atributo.Attributes["atributo"].Value;

            foreach (XmlNode a in atributo["armas"].ChildNodes)
            {
                switch (att)
                {
                    case "nome":
                        nomeArma.Add(a.InnerText);
                        break;

                    case "icone":
                        nomeIconeArma.Add(a.InnerText);

                        for (int i=0;i < spriteSheetIconesArmas.Length;i++ )
                        {

                            if (spriteSheetIconesArmas[i].name== a.InnerText)
                            {
                               iconeArma.Add(spriteSheetIconesArmas[i]);
                                //reduz o loop
                                break;
                            }

                        }


                        break;

                    case "categoria":
                        categoriaArma.Add(a.InnerText);
                        if (a.InnerText=="Staff")
                        {
                            idClasseArma.Add(2);
                        } else if (a.InnerText=="Arco")
                        {
                            idClasseArma.Add(1);
                        }
                        else
                        {
                            idClasseArma.Add(0);
                        }

                        break;

                    case "danoMin":
                        danoMinArma.Add(int.Parse(a.InnerText));
                        break;
                    case "danoMax":
                        danoMaxArma.Add(int.Parse(a.InnerText));
                        break;
                    case "tipoDano":
                        tipoDanoArma.Add(int.Parse(a.InnerText));
                        break;

                }




            }
        }



        for(int i= 0; i< iconeArma.Count ; i++)
        {
             spriteArmas1.Add(spriteSheetArmas[nomeIconeArma[i] + "0"] );
             spriteArmas2.Add(spriteSheetArmas[nomeIconeArma[i] + "1"]);
             spriteArmas3.Add(spriteSheetArmas[nomeIconeArma[i] + "2"]);

            if (categoriaArma[i]!="Staff")
            {
                spriteArmas4.Add(null);
            }
            else
            {
                spriteArmas4.Add(spriteSheetArmas[nomeIconeArma[i] + "3"]);
            }


        }
        atualizarGameController();

            }




   public void atualizarGameController()
    {
        _GameController.nomeArma = nomeArma;
        _GameController.idClasseArma = idClasseArma;

        _GameController.danoMinArma = danoMinArma;
        _GameController.danoMaxArma = danoMaxArma;
        _GameController.tipoDanoArma = tipoDanoArma;
        _GameController.imgInventario = iconeArma;

        _GameController.spriteArmas1 = spriteArmas1;
        _GameController.spriteArmas2 = spriteArmas2;
        _GameController.spriteArmas3 = spriteArmas3;
        _GameController.spriteArmas4 = spriteArmas4;
    }

}
