using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    /*
    public int vida;    
    public bool estaVoando;
    public string nome;

    public float tempo;
    public float distancia;
    public float velocidade;
    */
    public GameObject LastCheckpoint;
    public Text textLives;
    public Text textRings;
    public int lives;
    public int rings;
public float forcaPulo;
public float velocidadeMaxima;
    public bool isGrounded;//esta no chao
    public bool canFly;
    public bool inWater;


    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<CapsuleCollider2D>() ;
        textLives.text = lives.ToString();
        textRings.text = rings.ToString();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
       float movimento = Input.GetAxis("Horizontal");
       
        rigidBody.velocity = new Vector2(movimento* velocidadeMaxima, rigidBody.velocity.y);


        /* */
        //se o movimento for para esquerda no sentido contrario da visao do player  faz flip
        if (movimento < 0)
        {
//COMPONENTE FLIP   
            GetComponent<SpriteRenderer>().flipX=true;
//se o movimento for para direita no sentido do da visao do player nao faz flip
        } else if (movimento > 0) {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //pega o componente animator se o movimento for diferente de zero o plsyer esta andando

        if (movimento > 0 || movimento < 0) {

            //coloca true para o checbox do animator
            GetComponent<Animator>().SetBool("walking",true);


        }
        else {
            //coloca false para o checbox do animator
            GetComponent<Animator>().SetBool("walking", false);
        }

        /////////////////////////////////////////////////////////////////////
        //print("andando");



        if (!inWater)
        {

            //aperta o space para pular

            if (Input.GetKeyDown(KeyCode.Space))
            {



                if (isGrounded == true)
                {

                    rigidBody.AddForce(new Vector2(0, forcaPulo));
                    GetComponent<AudioSource>().Play();
                    canFly = false;
                }
                else
                {
                    canFly = true;
                }




            }

            if (canFly == true && Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Animator>().SetBool("flying", true);
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, -0.5f);
            }
            else
            {
                GetComponent<Animator>().SetBool("flying", false);
            }





            //se esta no chao pode pular seta o animator em true
            if (isGrounded == true)
            {

                GetComponent<Animator>().SetBool("jumping", false);
            }
            else
            {
                //se nao esta no chao pode nao pode pular seta o animator em false

                GetComponent<Animator>().SetBool("jumping", true);
            }

        }// fim condicao if (!inWater)


        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rigidBody.AddForce(new Vector2(0, 6f * Time.deltaTime), ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                rigidBody.AddForce(new Vector2(0, -6f * Time.deltaTime), ForceMode2D.Impulse);
            }

            rigidBody.AddForce(new Vector2(0, 10f * Time.deltaTime), ForceMode2D.Impulse);
        }

        GetComponent<Animator>().SetBool("swimming", inWater);


        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GetComponent<Animator>().SetTrigger("hammer");


            Collider2D[] colliders = new Collider2D[3];
            transform.Find("HammerArea").gameObject.GetComponent<Collider2D>()
                .OverlapCollider(new ContactFilter2D(), colliders);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != null && colliders[i].gameObject.CompareTag("Monstros"))
                {
                    Destroy(colliders[i].gameObject);
                }
            }
        }












    }

//chamado qdo player colide com outro objeto
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Plataformas")) {    

            //coloca true para o isGrounded.significa q esta colidindo com o chao. a variavel  isGrounded
            //sera true para ser usada posteriormente
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Trampolim"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);

            print("Trampolim");

        }

        if (collision.gameObject.CompareTag("Monstros"))
        {
            lives--;
            textLives.text = lives.ToString();
            if (lives == 0)
            {
                transform.position = LastCheckpoint.transform.position;
            }


        }








        //print(" COLIDIU " + collision.gameObject.tag);


    }


    //chamado qdo player para de colidir com outro objeto
    private void OnCollisionExit2D(Collision2D collision)
    {
        //print(" EXIT " + collision.gameObject.tag);

        //coloca false para o isGrounded.significa q nao esta colidindo com o chao
        isGrounded = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Water"))
        {
            //coloca true para o inWater.significa q esta contato com a agua. a variavel  inWater
            //sera true para ser usada posteriormente
            inWater = true;

        }
        

        if (collision.gameObject.CompareTag("Moedas"))
        {
            Destroy(collision.gameObject);
            rings++;
            textRings.text = rings.ToString();
        }



        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            LastCheckpoint = collision.gameObject;
        }








    }

    private void OnTriggerExit2D(Collider2D collision)
    {



        if (collision.gameObject.CompareTag("Water"))
        {




            //coloca false para o inWater.significa q nao em esta contato com a agua. a variavel  inWater
            //sera false para ser usada posteriormente
            inWater = false;



        }




    }

}
