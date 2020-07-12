using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIA : MonoBehaviour
{
    private GameController _GameController;
    private Rigidbody2D slimeRb;
    private Animator slimeAnimator;
    private int h;


    public float speed;
    public float timeToWalk;
    public GameObject hitBox;
    public bool isLookLeft;
    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        slimeRb = GetComponent<Rigidbody2D>();
        slimeAnimator = GetComponent<Animator>();
        StartCoroutine("slimeWalk");

    }

    // Update is called once per frame
    void Update()
    {
        //controle de flip
        if (h > 0 && isLookLeft == true)
        {
            flip();
        }
        else if (h < 0 && isLookLeft == false)
        {
            flip();
        }


        slimeRb.velocity = new Vector2(h*speed,slimeRb.velocity.y);

        if (h!=0)
        {
            slimeAnimator.SetBool("isWalk",true);
        }
        else
        {
            slimeAnimator.SetBool("isWalk", false);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="HitBox")
        {
            h = 0;
            StopCoroutine("slimeWalk");
            Destroy(hitBox);

            _GameController.playSFX(_GameController.sfxEnemyDead, 0.2f);
            slimeAnimator.SetTrigger("dead");

        }


    }



   void Ondead()
    {
        Destroy(this.gameObject);
    }


    IEnumerator slimeWalk()
    {
        int rand = Random.Range(0,100);
        if (rand <33)
        {
            h = -1;
        }
         else if (rand < 66)
        {
            h = 0;
        }
        else if(rand < 100)
        {
            h = 1;
        }



        yield return new WaitForSeconds(timeToWalk);
        StartCoroutine("slimeWalk");
    }

    public void flip()
    {
        isLookLeft = !isLookLeft;

        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

}
