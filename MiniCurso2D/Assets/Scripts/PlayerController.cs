using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController _GameController;
    private Animator playerAnimator;
    private Rigidbody2D playerRb;
    private bool isGrounded;
    private bool isAtack;
    private SpriteRenderer playerSr;


    public float jumpForce;
    public float speed;
    public bool isLookLeft;
    public Transform groundCheck;
    public GameObject hitBoxPrefab;
    public Transform mao;
    public Color hitColor;
    public Color noHitColor;
    public int maxHP;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSr = GetComponent<SpriteRenderer>();
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        _GameController.playerTransform = this.transform;

    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetBool("isGrounded", isGrounded);

        if (_GameController.currentState !=gameState.GAMEPLAY)
        {
            playerRb.velocity = new Vector2(0,playerRb.velocity.y);
            playerAnimator.SetInteger("h",0);
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");

        if (isAtack==true && isGrounded==true)
        {
            h = 0;
        }

       //controle de flip
        if (h>0 && isLookLeft==true)
        {
            flip();
        } else if (h < 0 && isLookLeft == false)
        {
            flip();
        }

         float speedY = playerRb.velocity.y;

        if (Input.GetButtonDown("Jump") && isGrounded==true)
        {
            _GameController.playSFX(_GameController.sfxJump,0.5f);
            playerRb.AddForce(new Vector2(0,jumpForce));
        }

        if (Input.GetButtonDown("Fire1") && isAtack==false)
        {
            isAtack = true;
            _GameController.playSFX(_GameController.sfxAtack, 0.5f);
            playerAnimator.SetTrigger("atack");
        }




        playerRb.velocity = new Vector2(h * speed,speedY);

        playerAnimator.SetInteger("h",(int)h);
        playerAnimator.SetBool("isGrounded",isGrounded);
        playerAnimator.SetFloat("speedY", speedY);
        playerAnimator.SetBool("isAtack", isAtack);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,0.02f);  
    }



    public void flip()
    {
        isLookLeft = !isLookLeft;

        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x,transform.localScale.y, transform.localScale.z);
    }


    public void onEndAtack()
    {
        isAtack = false;
    }

        public void hitBoxAtack()
    {
        GameObject hitBoxTemp = Instantiate(hitBoxPrefab,mao.position,transform.localRotation);

        Destroy(hitBoxTemp,0.2f);

    }


  void footStep()
    {
        _GameController.playSFX(_GameController.sfxStep[Random.Range(0, _GameController.sfxStep.Length)],0.5f);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="coletavel")
        {
            _GameController.playSFX(_GameController.sfxCoin,0.5f);
            _GameController.getCoin();
            Destroy(collision.gameObject);
        }
          else if (collision.gameObject.tag == "Damage")
        {
            _GameController.getHit();
            if (_GameController.vida > 0) {

                StartCoroutine("damageController");

            }
                else if (collision.gameObject.tag == "Abismo")
            {
                _GameController.playSFX(_GameController.sfxDamage,0.05f);
                _GameController.vida = 0;
                _GameController.heartController();
                _GameController.painelGameOver.SetActive(true);
                _GameController.currentState = gameState.GAMEOVER;
                _GameController.trocarMusica(musicaFase.GAMEOVER);
            }
            else if (collision.gameObject.tag == "Flag")
            {
                _GameController.theEnd();
            }




 }

    }

       IEnumerator damageController()
    {
        _GameController.playSFX(_GameController.sfxDamage,0.05f);
        maxHP -= 1;
        if (maxHP<=0)
        {
            print("Game Over");
        }



        this.gameObject.layer = LayerMask.NameToLayer("Invencivel");
        playerSr.color = hitColor;
        yield return new WaitForSeconds(0.03f);
        playerSr.color = noHitColor;

        for (int i=0;i<5;i++)
        {
            playerSr.enabled = false;
            yield return new WaitForSeconds(0.02f);
            playerSr.enabled = true;
            yield return new WaitForSeconds(0.02f);

        }
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        playerSr.color = Color.white;
    }



}
