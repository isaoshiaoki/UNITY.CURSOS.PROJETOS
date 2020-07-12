using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D playerRb;
    public Transform groundCheck;
    public Transform hitBox;
    public GameObject hitBoxPrefab;

    private Animator playerAnimator;
    public float forcaPulo;
    private bool isGrounded;
    public float velocidadeMovimento;
    public bool isWalk;

    public bool isLookLeft;

    private bool isAtack;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // atualizado a cada frame
    void Update(){
        float velocidadeY = playerRb.velocity.y;

        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            isWalk = true;


            if (horizontal > 0 && isLookLeft == true) {
                flip();
            }
            else if (horizontal < 0 && isLookLeft == false) {
                flip();
            }
        }
        else {
            isWalk = false;
        }


        //comando de movimento
       // playerRb.velocity = new Vector2(horizontal*velocidadeMovimento,velocidadeY);

//atacar econdiçoes para atacar
        if (Input.GetButtonDown("Fire1") && isGrounded==true && isAtack==false) {
            isAtack = true;
            playerRb.velocity=new Vector2(0,0);
            playerAnimator.SetTrigger("atack");
        }


        //para pular
        if (Input.GetButtonDown("Jump") && isGrounded==true && isAtack == false) {
            playerRb.AddForce(new Vector2(0,forcaPulo));
    }

        if (isAtack==false) {
            playerRb.velocity = new Vector2(horizontal * velocidadeMovimento, velocidadeY);
        }
        playerAnimator.SetBool("isGrounded",isGrounded);
        playerAnimator.SetFloat("speedY", velocidadeY);
        playerAnimator.SetBool("walk", isWalk);


    }
    //atualizado mais rapido
    void FixedUpdate()
    {
        //cria um circulo de contato entre o personagem e o chao
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,002f);
    }

    //muda de direcao
    void flip() {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x;
        x *= -1;


        transform.localScale = new Vector3(x,transform.localScale.y, transform.localScale.z);
    }

    public void OnAtackEnded() {
        isAtack = false;
    }

    public void OnHitBox()
    {

       GameObject hit= Instantiate(hitBoxPrefab,hitBox.position,hitBox.localRotation);
        //Debug.LogError("pause");

        Destroy(hit.gameObject,0.03f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "inimigo":
                playerAnimator.SetTrigger("hit");
                print("colidiu com personagem");
                break;

        }
        
            }








        }








