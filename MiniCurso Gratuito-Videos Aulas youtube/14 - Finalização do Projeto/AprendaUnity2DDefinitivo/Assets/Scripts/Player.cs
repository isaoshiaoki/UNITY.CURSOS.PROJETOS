using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*
     * 
ID da animacao
0-Idle
1-walk
2-crouch
3-die
animacao para qual a transicao esta sendo direcionada direciona

*/
    private AudioController audioController;
    private _GameController _GameController;
    private Animator playerAnimator;
    private Rigidbody2D playerRb;
    private SpriteRenderer sRender;
    private Fade fade;


    [Header("Configuração do Animator")]
      public int idAnimation;//indica o id da animacao


    [Header("Configuração de Player")]
    
    public float speed;//velocidade de movimento do pesonagem
    public int jumpForce;//forca aplicada para gerar pulo de personagem
    public GameObject balloonAlert;
    

    [Header("Configuração de vida")]
    public int vidaMax;
    public int vidaAtual;     

    [Header("Configuração de Chão")]
    public bool grounded;//indica se o player esta no chao
    public LayerMask whatIsGround;//indica o que é superficie para o teste de grounded
    public Transform groundCheck;//objeto responsavel por detectar se o persobnagem esta sobre uma superficie

    [Header("Configuração de Movimento")]
    public float sentidoMovimentoHorizontal;
    public float sentidoMovimentoVertical;
    public bool lookLeft;//indica se o personagem esta virado para a esquerda

    [Header("Configuração de Ação")]   
    public bool attacking;//indica se o personagem esta executando um  atack
     public int atk;
    public bool naoPodeAtacar;//indica se podemos executar um ataque

    [Header("Configuração de Movimento")]
    public Collider2D standing;//colisor em pe
    public Collider2D crounching;//colisor  agalhado




    [Header("Configuração de Game objetos")]
    //interacao com itens
    public Transform hand;
    public LayerMask interacao;
    public GameObject objetoInteracao;
    private Vector3 direcao = Vector3.right;  //direcao direita


    [Header("Sistema de armas")]
    // sistema de armas

    public int idArma;
    public int idArmaAtual;
    public GameObject[] armas;
    public GameObject[] arcos;
    public GameObject[] staffs;
    public GameObject[] flechaArco;
    public GameObject magiaPrefab;
    public Transform spawnFlecha,spawnMagia;

    // [Header("Materiais")]
    //public Material luz2D;
    // public Material luz2DPadrao;





    // Start is called before the first frame update
    void Start()
    {
        //instancia objetos

        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        //carrega dados iniciais do personagem
        vidaMax = _GameController.vidaMaxima;
        idArma = _GameController.idArma;

        _GameController.manaAtual = _GameController.manaMax;

       sRender = GetComponent<SpriteRenderer>();
        playerRb = GetComponent<Rigidbody2D>();//armazena componente na variavel     
        playerAnimator = GetComponent<Animator>();//associa componente na variavel  

        vidaAtual = vidaMax;

        //desabilita as armas
        foreach (GameObject obj in armas)
        {

            obj.SetActive(false);
        }

        //desabilita arcos
        foreach (GameObject obj in arcos)
        {

            obj.SetActive(false);
        }

        //desabilita staffs
        foreach (GameObject obj in staffs)
        {

            obj.SetActive(false);
        }



        //GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        //GetComponent<SpriteRenderer>().receiveShadows = true;


        trocarArma(idArma);
    }

    private void FixedUpdate()//taxa de atualizacao fixa 0.02
    {
        if (_GameController.currentState != GameState.GAMEPLAY)
        {
            return;
          }
         grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);
        playerRb.velocity = new Vector2(sentidoMovimentoHorizontal * speed, playerRb.velocity.y);


        interagir();
        


        
    }




    // Update is called once per frame
    void Update()
    {

        if (_GameController.currentState == GameState.DIALOGO)
        {
            playerRb.velocity = new Vector2(0,playerRb.velocity.y);
            playerAnimator.SetInteger("idAnimation",0);
            if (Input.GetButtonDown("Acao1"))
            {


                //manda executar uma funcao
               objetoInteracao.SendMessage("falar", SendMessageOptions.DontRequireReceiver);

             
            }






        }


        if (_GameController.currentState != GameState.GAMEPLAY)
        {
            return;
        }



        sentidoMovimentoHorizontal = Input.GetAxisRaw("Horizontal");
        sentidoMovimentoVertical = Input.GetAxisRaw("Vertical");

        if (sentidoMovimentoHorizontal > 0 && lookLeft == true) {
            flip();
        } else if (sentidoMovimentoHorizontal < 0 && lookLeft == false) {

            flip();
        }



        if (sentidoMovimentoVertical < 0)
        {
            idAnimation = 2;

            if (grounded == true)
            {

                sentidoMovimentoHorizontal = 0;

            }


        }
        else if (sentidoMovimentoHorizontal != 0)
        {
            idAnimation = 1;
        }
        else {
            idAnimation = 0; ;
        }



        if (Input.GetButtonDown("Acao1") && sentidoMovimentoVertical >= 0 && attacking == false && objetoInteracao == null && naoPodeAtacar==false) {
            naoPodeAtacar = true;
            playerAnimator.SetTrigger("atack");            

        }

        if (Input.GetButtonDown("Acao1") && sentidoMovimentoVertical >= 0 && attacking == false && objetoInteracao != null)
        {

           
            
             //manda executar uma funcao
            objetoInteracao.SendMessage("interacao", SendMessageOptions.DontRequireReceiver);
        }

        if (Input.GetButtonDown("Jump") && grounded == true && attacking == false)
        {
            playerRb.AddForce(new Vector2(0, jumpForce));
            // crounching.enabled = false;
            //standing.enabled = true;
        }

        
       /////////////////////////codigo temporario////////////////////////////////////////////////////////////////// 
        //controle pelo teclado

        if (Input.GetKeyDown(KeyCode.Alpha1 ) && attacking == false) {

            trocarArma(0);  //usar a espada
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && attacking == false)
        {
            trocarArma(4);  //usar o machado

        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && attacking == false)
        {

            trocarArma(5);  //usar a massa

            print(" Arma 5 ");
        }

        /////////////////////////////////////////////////////////////////////////////////////////// 


        // playerRb.velocity = new Vector2(sentidoMovimentoHorizontal*speed,playerRb.velocity.y);

        if (attacking == true && grounded == true) {
            sentidoMovimentoHorizontal = 0;

        }

        //	ativa colisor para em pe e agachado
        if (sentidoMovimentoVertical < 0 && grounded == true)
        {
            crounching.enabled = true;
            standing.enabled = false;

        }
        else if(sentidoMovimentoVertical >= 0 && grounded == true) {

            crounching.enabled = false;
            standing.enabled = true;
        }
        else if (sentidoMovimentoVertical != 0 && grounded == false)
        {
            crounching.enabled = false;
            standing.enabled = true;

        }



        playerAnimator.SetBool("grounded", grounded);
        playerAnimator.SetInteger("idAnimation", idAnimation);
        playerAnimator.SetFloat("speedY", playerRb.velocity.y);
        playerAnimator.SetFloat("idClasseArma", _GameController.idClasseArma[_GameController.idArmaAtual]);

        if (_GameController.qtdFlechas[_GameController.idFlechaEquipada] > 0) {

            foreach (GameObject f in flechaArco)
            {
                f.SetActive(true);
            }

        }
        else
        {

            foreach (GameObject f in flechaArco)
            {
                f.SetActive(false);
            }

        }



    }


    void LateUpdate()
    {
        if (_GameController.idArma != _GameController.idArmaAtual)
        {

            trocarArma(_GameController.idArma);

        }

        


    }




















    void flip() {

        lookLeft = !lookLeft;//inverte o valor da variavel boleana
        float x = transform.localScale.x;
        x *= -1;//inverte o sinal do scale

        transform.localScale = new Vector3(x,transform.localScale.y, transform.localScale.z);
        //faz do RaycastHit2D do metodo interagir()
        direcao.x = x;
        
    }


    public void atack(int atk) {

        switch (atk) {
       case 0:
       attacking = false;
                armas[2].SetActive(false);
                StartCoroutine("esperarNovoAtaque");
                break;

        case 1:
                audioController.tocarFx(audioController.fxSword,1);
        attacking = true;
        break;
        }      
    }

    public void atackFlecha(int atk)
    {

        switch (atk)
        {
            case 0:
                attacking = false;
                arcos[2].SetActive(false);

                

                break;

            case 1:
                
                attacking = true;               

                break;

           //parametro recebido do animation na configuracao de eventos nos frames
            case 2:
                if (_GameController.qtdFlechas[_GameController.idFlechaEquipada] >0) {

                    audioController.tocarFx(audioController.fxBow, 1);

                    _GameController.qtdFlechas[_GameController.idFlechaEquipada] -= 1;
                    //instancia a flecha
                    GameObject tempPrefab = Instantiate(_GameController.flechaPrefab[_GameController.idFlechaEquipada], spawnFlecha.position, spawnFlecha.localRotation);
                    tempPrefab.transform.localScale = new Vector3(tempPrefab.transform.localScale.x * direcao.x, tempPrefab.transform.localScale.y, tempPrefab.transform.localScale.z);
                    tempPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(_GameController.velocidadeFlecha[_GameController.idFlechaEquipada] * direcao.x, 0);
                    Destroy(tempPrefab, 1);
                   }
                    break;
                

        }
    }

    public void atackStaffs(int atk)
    {

        switch (atk)
        {
            case 0:
                attacking = false;
                staffs[3].SetActive(false);
                break;

            case 1:
                
                attacking = true;
                break;


            //parametro recebido do animation na configuracao de eventos nos frames
            case 2:
                if (_GameController.manaAtual >=1) {
                    audioController.tocarFx(audioController.fxStaff, 1);
                    _GameController.manaAtual -= 1;
                    //instancia a magia
                    GameObject tempPrefab = Instantiate(magiaPrefab, spawnMagia.position, spawnMagia.localRotation);
                    tempPrefab.transform.localScale = new Vector3(tempPrefab.transform.localScale.x * direcao.x, tempPrefab.transform.localScale.y, tempPrefab.transform.localScale.z);
                    tempPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(5 * direcao.x, 0);

                    Destroy(tempPrefab, 1);
                }
                break;


        }
    }













    //verifica se o player esta interagindo com algo
    void interagir()
    {

        Debug.DrawRay(hand.position, direcao * 0.2f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(hand.position, direcao, 0.2f, interacao);


         //qdo estiver interagindo
        if (hit == true)
        {

           print ("Colidiu com :"+hit.collider.gameObject.name);

            objetoInteracao = hit.collider.gameObject;
            balloonAlert.SetActive(true);

        }
        else
        {   //qdo nao estiver interagindo


            objetoInteracao = null;
            balloonAlert.SetActive(false);

        }




    }


    void controleArma(int id)
    {

        foreach (GameObject obj in armas)
        {

            obj.SetActive(false);
        }

        armas[id].SetActive(true);

    }

    void controleArco(int id)
    {

        foreach (GameObject obj in arcos)
        {

            obj.SetActive(false);
        }

        arcos[id].SetActive(true);

    }


    void controleStaff(int id)
    {

        foreach (GameObject obj in staffs)
        {

            obj.SetActive(false);
        }

        staffs[id].SetActive(true);

    }



    void OnTriggerEnter2D(Collider2D col)
    {//ao entrar em colisao     		


        switch (col.gameObject.tag)
        {
            case "coletavel":
                //manda executar uma funcao
                col.SendMessage("coletar", SendMessageOptions.DontRequireReceiver);
                break;


            case "inimigo":
                _GameController.vidaAtual -= 1;
                break;
        }
    }


      public void changeMaterial(Material novoMaterial)
    {

        sRender.material = novoMaterial;
        foreach (GameObject o in armas)
        {

            o.GetComponent<SpriteRenderer>().material = novoMaterial;

        }

        foreach (GameObject o in arcos)
        {

            o.GetComponent<SpriteRenderer>().material = novoMaterial;

        }

        foreach (GameObject o in flechaArco)
        {

            o.GetComponent<SpriteRenderer>().material = novoMaterial;

        }





        foreach (GameObject o in staffs)
        {

            o.GetComponent<SpriteRenderer>().material = novoMaterial;

        }




    }


    public void trocarArma(int id)
    {
         _GameController.idArma = id;
        switch (_GameController.idClasseArma[id]) {


            case 0: //martelos,espadas,machados
        
        armas[0].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas1[id];
        ArmaInfo tempInfoArma = armas[0].GetComponent<ArmaInfo>();

        tempInfoArma = armas[0].GetComponent<ArmaInfo>();
        tempInfoArma.danoMin = _GameController.danoMinArma[idArma];
        tempInfoArma.danoMax = _GameController.danoMaxArma[idArma];
        tempInfoArma.tipoDano = _GameController.tipoDanoArma[idArma];



        armas[1].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas2[id];

        tempInfoArma = armas[1].GetComponent<ArmaInfo>(); 
        tempInfoArma.danoMin = _GameController.danoMinArma[idArma];
        tempInfoArma.danoMax = _GameController.danoMaxArma[idArma];
        tempInfoArma.tipoDano = _GameController.tipoDanoArma[idArma];


        armas[2].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas3[id];
        tempInfoArma = armas[2].GetComponent<ArmaInfo>();
        tempInfoArma.danoMin = _GameController.danoMinArma[idArma];
        tempInfoArma.danoMax = _GameController.danoMaxArma[idArma];
        tempInfoArma.tipoDano = _GameController.tipoDanoArma[idArma];

         break;

            case 1:
                arcos[0].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas1[id];
                arcos[1].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas2[id];
                arcos[2].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas3[id];

                break;

            case 2:
                staffs[0].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas1[id];
                staffs[1].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas2[id];
                staffs[2].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas3[id];
                staffs[3].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas4[id];
                break;




        } //fim switch

  
        _GameController.idArmaAtual = idArma;

    }


   IEnumerator esperarNovoAtaque()
    {
        yield return new WaitForSeconds(0.2f);
        naoPodeAtacar = false;
    }




}
