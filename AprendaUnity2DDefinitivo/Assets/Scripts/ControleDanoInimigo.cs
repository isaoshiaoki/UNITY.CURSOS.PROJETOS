using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;     //textMeshPro
public class ControleDanoInimigo : MonoBehaviour {


    private _GameController _GameController;
    private Player playerScript;
    private Animator animator;
    private SpriteRenderer sRender;
    private bool died;  //indica se esta morto


    [Header("Configuração de knockback")]

    //knockback
    public GameObject knockForcePrefab;//forca de repulsão
    public Transform knockPosition;//ponto de origem
    public float knockX;//valor padrao do position x   definido no painel do unity
    private float kx;//valor temporario
    public bool olhandoEsquerda;
    public bool playerEsquerda;
    public bool playerDireita;
    public bool getHit; //indica se tomou hit-dano

    [Header("Configuração de vida")]
    public int vidaInimigo;
    public int vidaAtual;
    public GameObject barrasVida;//objeto contendo todas as vidas
    public Transform hpBar;//objeto indicador da quantidade de vida
    public Color[] characterColor;//controle da cor do personagem
    private float percVida;//controla o percentual de vida
    public GameObject danoTxtPrefab;//objeto que ira exibir o dano tomado

    [Header("Configuração de Resistencia/Fraqueza")]
    public float[] ajusteDano;// sistema de resistencia / fraquesa contra determinado tipo de dano

   
    [Header("Configuração de Chão")]
    public Transform groundCheck;//objeto responsavel por detectar se o persobnagem esta sobre uma superficie
    public LayerMask whatIsGround;//indica o que é superficie para o teste de grounded

    [Header("Configuração de Loot")]
    public GameObject loots;

   



    private void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        playerScript = FindObjectOfType(typeof(Player)) as Player;
        sRender = GetComponent<SpriteRenderer>();  
        sRender.color = characterColor[0];
        animator = GetComponent<Animator>();

        //desativa barra de vida ao iniciar game
        barrasVida.SetActive(false);
        vidaAtual = vidaInimigo;
         //barra inicia cheia
        hpBar.localScale = new Vector3(1,1,1);


        if (olhandoEsquerda == true)
        {

            float x = transform.localScale.x;
            x *= -1;
             transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);             
            barrasVida.transform.localScale = new Vector3(x, barrasVida.transform.localScale.y, barrasVida.transform.localScale.z);

        }



    }

    void Update()
    {

        //public float knockX;//valor padrao do position x   definido no painel do unity
        // verifica se o player esta a esquerda ou a doreita do inimigo
        //posicao do player 
        float xPlayer = playerScript.transform.position.x;
        if (xPlayer < transform.position.x)
        {

            playerEsquerda = true;
            playerDireita = false;
            //kx = knockX;
            //		print ("Player esta a esquerda ");

        }
        else if (xPlayer > transform.position.x)
        {
            playerDireita = true;
            playerEsquerda = false;
            //kx = knockX * -1;
            //print ("Player esta a direita ");

        }

        if (olhandoEsquerda == true && playerEsquerda == true)
        {

              kx = knockX;

        }
        else if (olhandoEsquerda == false && playerEsquerda == true)
        {

            // kx = knockX * -1;
             kx = knockX;

        }
        else if (olhandoEsquerda == true && playerEsquerda == false)
        {

           kx = knockX * -1;

        }

        else if (olhandoEsquerda == false && playerEsquerda == false)
        {

            kx = knockX * -1;
          //  kx = knockX;
        }


        knockPosition.localPosition = new Vector3(kx, knockPosition.localPosition.y, 0);
        //passa para o animator o grounded true
        animator.SetBool("grounded", true);
    }





    //entra em contato com o colisor
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (died == true) {return;}    //encerra o comando


        //pega a tag do elemento de colisão
        switch (collision.gameObject.tag)
            { 

         
               //qdo entar em colisao com elemento da tag arma
                //se for uma arma executara os comandos dentro do seu case - case "arma":
                case "arma":

               //depois de entra no case arma faz oura verificacao se nao tomou tomou hit
                if (getHit == false)
                {
                     //QDO TOMAR HIT a variavel recebe true
                    getHit = true;
                    //mostra a barra de vida qdo tomar hit
                    barrasVida.SetActive(true);
                    //pega o componente da tag arma para acessar seus atributos
                    ArmaInfo infoArma = collision.gameObject.GetComponent<ArmaInfo>();

                    animator.SetTrigger("hit");

                    float danoArma = Random.Range(infoArma.danoMin, infoArma.danoMax);// dano entre 1 e 5
                    int tipoDano = infoArma.tipoDano; //  acessa o tipo de dano   

            //calculo para o dano
            float danoTomado = danoArma + (danoArma * (ajusteDano[tipoDano] / 100));
            vidaAtual -= Mathf.RoundToInt(danoTomado);//	reduz da vida a qtd de dano tomado

          percVida = (float)vidaAtual / (float)vidaInimigo;
           if (percVida<0) { percVida = 0; }
          //ataualiza a barra de vidas
          hpBar.localScale = new Vector3(percVida, 1, 1);

          if (vidaAtual <= 0)

            {
                        died = true;

                        //vai para animacao de morte
                        animator.SetInteger("idAnimation", 3);
                        //  Destroy(this.gameObject,2);
                        StartCoroutine("loot");
                    }


                     //instancia o efeito  TextMesh  e o lugar onde ele inicia
                      GameObject danoTemp = Instantiate(danoTxtPrefab, transform.position, transform.localRotation);   
                       
                                        
                    //    .GetComponentInChildren esse comando e por causa que esta dentro de outro GameObject
                    //pegar o objet filho
                    danoTemp.GetComponentInChildren<TextMeshPro>().text = Mathf.RoundToInt(danoTomado).ToString();
                    // coloca o componente na layer Hud
                    danoTemp.GetComponentInChildren<MeshRenderer>().sortingLayerName = "Hud";

                     //intancia animacao tipo de dano:normal fogo ou agua
                    GameObject fxTemp = Instantiate(_GameController.fxDano[tipoDano], transform.position, transform.localRotation);
                    Destroy(fxTemp, 1);



                    int forcaX = 50;

                    if (playerEsquerda == false)//player esta a direita
                    {
                        forcaX *= -1;
                    }
                    //adiciona forca
                    danoTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaX, 230)); 
                    Destroy(danoTemp, 0.5f);





                    //instancia o efeito  knockForcePrefab dos prefabs.coloca o efeito em cena
                    // knockForcePrefab foi arrastado para o painel do unity
                    GameObject knockTemp = Instantiate(knockForcePrefab, knockPosition.position, knockPosition.localRotation);
            
                    
                    
                    //0.02 taxa de atualizacao de frame
            Destroy(knockTemp, 0.02f);

                    //qdo tomar dano chama a coroutine para piscar .enquanto estiver piscando nao recebe dano
                      StartCoroutine("invuneravel");

                    this.gameObject.SendMessage("tomeiHit",SendMessageOptions.DontRequireReceiver);


                }//fim do if  (getHit == false)

                break;//fim do case arma













        } //fim do switch

    }//fim do trigger


    void flip()
    {
        olhandoEsquerda = !olhandoEsquerda;//inverte o valor da variavel bolena

        float x = transform.localScale.x;
        x *= -1;//inverte o sinal do scale x
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        barrasVida.transform.localScale = new Vector3(x, barrasVida.transform.localScale.y, barrasVida.transform.localScale.z);


    }


    IEnumerator loot()
    {
        yield return new WaitForSeconds(1);
        //coloca em cena animacao de morte
        GameObject fxMorte = Instantiate(_GameController.fxMorte, groundCheck.position, transform.localRotation);
        yield return new WaitForSeconds(0.5f);
        //desabilita o player
        sRender.enabled = false;

        //controle de loot  .
        int qtdMoedas = Random.Range(1, 5);
        for (int l = 0; l < qtdMoedas; l++)
        {
            GameObject lootTemp = Instantiate(loots, transform.position, transform.localRotation);
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-25, 25), 80));
            yield return new WaitForSeconds(0.1f);
        }





        yield return new WaitForSeconds(0.7f);
        Destroy(fxMorte);
        Destroy(this.gameObject);
    }








    //enquanto estiver piscando nao recebera dano
    //criando efeito de piscar
    IEnumerator invuneravel()
    {
        //    recebe a transparencia
        sRender.color = characterColor[1];
        yield return new WaitForSeconds(0.2f);
        //    recebe a cor
        sRender.color = characterColor[0];
        yield return new WaitForSeconds(0.2f);

        sRender.color = characterColor[1];
        yield return new WaitForSeconds(0.2f);

        sRender.color = characterColor[0];
        yield return new WaitForSeconds(0.2f);


        sRender.color = characterColor[1];
        yield return new WaitForSeconds(0.2f);


        sRender.color = characterColor[0];

        getHit = false;
         //qdo o hit terminar a barra sera desativada
        barrasVida.SetActive(false);

    }





}
