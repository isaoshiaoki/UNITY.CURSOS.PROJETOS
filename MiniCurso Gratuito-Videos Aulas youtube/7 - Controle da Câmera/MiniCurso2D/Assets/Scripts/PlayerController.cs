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

    public float jumpForce;
    public float speed;
    public bool isLookLeft;
    public Transform groundCheck;
    public GameObject hitBoxPrefab;
    public Transform mao;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        _GameController.playerTransform = this.transform;

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (isAtack==true && isGrounded==true)
        {
            h = 0;
        }


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
            playerRb.AddForce(new Vector2(0,jumpForce));
        }

        if (Input.GetButtonDown("Fire1") && isAtack==false)
        {
            isAtack = true;
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

}
