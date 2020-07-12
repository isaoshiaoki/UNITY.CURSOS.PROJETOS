using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator playerAnimator;
    private Rigidbody2D playerRb;
    private bool isGrounded;


    public float jumpForce;
    public float speed;
    public bool isLookLeft;
    public Transform groundCheck;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        

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

        if (Input.GetButtonDown("Fire1") )
        {
            playerAnimator.SetTrigger("atack");
        }




        playerRb.velocity = new Vector2(h * speed,speedY);

        playerAnimator.SetInteger("h",(int)h);
        playerAnimator.SetBool("isGrounded",isGrounded);
        playerAnimator.SetFloat("speedY", speedY);

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

}
