using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum enemyState
{

    PARADO,
    ALERTA,
    PATRULHA,
    ATACK ,
    RECUAR
}
public class Goblin : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rBody;
    private Vector3 direcao = Vector3.right;
    private Player playerScript;
    private bool attacking;
    private SpriteRenderer sRender;
    private _GameController _GameController;



    [Header("Estado De Maquina")]
    public enemyState currentEnemyState;
    public enemyState stateInicial;


    [Header("GameObjets")]
    public GameObject alert;

    [Header("Variaveis")]
    public float distanciaMudarRota;
    public bool lookLeft;
    public float tempoEsperaIdle;
    public float distanciaVerPersonagem;
    public float distanciaAtaque;
    public float distanciaSairAlerta;
    public float tempoRecuo;

    [Header("Sistema de armas")]
    public int idClasse;
    public int idArma;
    //public int idArmaInicial;
    //public GameObject[] armaInicial;
    // public int idArmaAtual; 

    public GameObject[] armas;    
    public GameObject[] arcos;
    public GameObject[] staffs;
    public GameObject[] flechaArco;


    //  [Header("Banco de DADOS de Personagens")]





    [Header("Velocidades")]
    public float velocidadeBase;
    public float velocidade;

    [Header("LayerMask")]
    public LayerMask layerPersonagem;
    public LayerMask layerObstaculos;

    [Header("Variaveis")]

    public bool ambienteEscuro;//qdo colocado em ambiente sensivel a luz
    public bool emAlertaHit;


    // Start is called before the first frame update
    void Start()
    {

        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        playerScript = FindObjectOfType(typeof(Player)) as Player;
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRender = GetComponent<SpriteRenderer>();


        if (lookLeft==true)
        {
            flip();
        }

        changeState(stateInicial);
        trocarArma(idArma);

        if (ambienteEscuro==true)
        {
            changeMaterial(_GameController.luz2D);


        }
        else
        {
            changeMaterial(_GameController.luz2DPadrao);
        }




    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemyState != enemyState.ATACK && currentEnemyState != enemyState.RECUAR) {

            Debug.DrawRay(transform.position, direcao * distanciaVerPersonagem, Color.red);
            RaycastHit2D hitPersonagem = Physics2D.Raycast(transform.position, direcao, distanciaVerPersonagem, layerPersonagem);

            if (hitPersonagem == true)
            {
                changeState(enemyState.ALERTA);
            }

        }


        if (currentEnemyState==enemyState.PATRULHA )
        {
         // Debug.DrawRay(transform.position, direcao * distanciaMudarRota, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direcao,  distanciaMudarRota, layerObstaculos);


        if (hit==true)
        {
                //StartCoroutine ("idle");
                changeState(enemyState.PARADO);
        }

         }

        if (currentEnemyState == enemyState.RECUAR)
        {
            // Debug.DrawRay(transform.position, direcao * distanciaMudarRota, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direcao, distanciaMudarRota, layerObstaculos);


            if (hit == true)
            {
                flip();
            }

        }









        if (currentEnemyState==enemyState.ALERTA)
        {
            float dist = Vector3.Distance(transform.position,playerScript.transform.position);

            if (dist <=distanciaAtaque)
            {
                changeState(enemyState.ATACK);
            }  else if (dist >= distanciaSairAlerta && emAlertaHit==false)
            {
                changeState(enemyState.PARADO);
            }
              
        }      

        if (currentEnemyState != enemyState.ALERTA)
        {
            alert.SetActive(false); 
        }

        rBody.velocity = new Vector2(velocidade, rBody.velocity.y); 
        if (velocidade == 0) {animator.SetInteger("idAnimation", 0);
        }else if (velocidade != 0) { animator.SetInteger("idAnimation", 1); }
        animator.SetFloat("idClasse", idClasse);

    }

    void flip()
    {

        lookLeft = !lookLeft;//inverte o valor da variavel boleana
        float x = transform.localScale.x;
        x *= -1;//inverte o sinal do scale

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        //faz do RaycastHit2D do metodo interagir()
        direcao.x = x;
        velocidadeBase *= -1;
        float vAtual = velocidade * -1;
        velocidade = vAtual;
    }


         IEnumerator idle()
    {

        //changeState(enemyState.PARADO);
        yield return new WaitForSeconds(tempoEsperaIdle);
          flip();
        changeState(enemyState.PATRULHA);
       

    }

    IEnumerator recuar()
    {
        yield return new WaitForSeconds(tempoRecuo);
        flip();
        changeState(enemyState.ALERTA);

    }


        void changeState(enemyState newState)
    {
        currentEnemyState = newState;
        switch (newState)
        {
            case enemyState.PARADO:
                velocidade = 0;
                StartCoroutine("idle");

                break;

            case enemyState.PATRULHA:
                velocidade = velocidadeBase;
                break;

            case enemyState.ALERTA:
                velocidade = 0;
                alert.SetActive(true);
                break;

            case enemyState.ATACK:
                animator.SetTrigger("atack");
                break;

            case enemyState.RECUAR:
                flip();
                velocidade = velocidadeBase * 2;
                StartCoroutine("recuar");
                break;

        }



    }

    public void atack(int atk)
    {

        switch (atk)
        {
            case 0:
                attacking = false;
                armas[2].SetActive(false);
                changeState(enemyState.RECUAR);
                break;

            case 1:
                attacking = true;
                break;
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

      public void tomeiHit()
    {     
        StartCoroutine("hitAlerta");
        emAlertaHit = true;
        changeState(enemyState.ALERTA);

    }

    IEnumerator hitAlerta()
    {
        yield return new WaitForSeconds(1);
        emAlertaHit = false;
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
        
        switch (id)
        {


            case 0: //martelos,espadas,machados

                armas[0].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas1[idArma];
                ArmaInfo tempInfoArma = armas[0].GetComponent<ArmaInfo>();

                tempInfoArma = armas[0].GetComponent<ArmaInfo>();
                tempInfoArma.danoMin = _GameController.danoMinArma[idArma];
                tempInfoArma.danoMax = _GameController.danoMaxArma[idArma];
                tempInfoArma.tipoDano = _GameController.tipoDanoArma[idArma];



                armas[1].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas2[idArma];

                tempInfoArma = armas[1].GetComponent<ArmaInfo>();
                tempInfoArma.danoMin = _GameController.danoMinArma[idArma];
                tempInfoArma.danoMax = _GameController.danoMaxArma[idArma];
                tempInfoArma.tipoDano = _GameController.tipoDanoArma[idArma];


                armas[2].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas3[idArma];
                tempInfoArma = armas[2].GetComponent<ArmaInfo>();
                tempInfoArma.danoMin = _GameController.danoMinArma[idArma];
                tempInfoArma.danoMax = _GameController.danoMaxArma[idArma];
                tempInfoArma.tipoDano = _GameController.tipoDanoArma[idArma];

                break;

            case 1:
                arcos[0].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas1[idArma];
                arcos[1].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas2[idArma];
                arcos[2].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas3[idArma];

                break;

            case 2:
                staffs[0].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas1[idArma];
                staffs[1].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas2[idArma];
                staffs[2].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas3[idArma];
                staffs[3].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArmas4[idArma];
                break;




        } //fim switch


      

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
                /*
                if (_GameController.qtdFlechas[_GameController.idFlechaEquipada] > 0)
                {

                    _GameController.qtdFlechas[_GameController.idFlechaEquipada] -= 1;
                    //instancia a flecha
                    GameObject tempPrefab = Instantiate(_GameController.flechaPrefab[_GameController.idFlechaEquipada], spawnFlecha.position, spawnFlecha.localRotation);
                    tempPrefab.transform.localScale = new Vector3(tempPrefab.transform.localScale.x * direcao.x, tempPrefab.transform.localScale.y, tempPrefab.transform.localScale.z);
                    tempPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(_GameController.velocidadeFlecha[_GameController.idFlechaEquipada] * direcao.x, 0);
                    Destroy(tempPrefab, 1);
                }
                 */
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
             /*
                if (_GameController.manaAtual >= 1)
                {

                    _GameController.manaAtual -= 1;
                    instancia a magia
                    GameObject tempPrefab = Instantiate(magiaPrefab, spawnMagia.position, spawnMagia.localRotation);
                    tempPrefab.transform.localScale = new Vector3(tempPrefab.transform.localScale.x * direcao.x, tempPrefab.transform.localScale.y, tempPrefab.transform.localScale.z);
                    tempPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(5 * direcao.x, 0);

                    Destroy(tempPrefab, 1);
                }

                */
                break;


        }
    }







}
