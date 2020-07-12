using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABossController : MonoBehaviour
{
    public enum rotina
    {
       A,B,C,D
    }


    [Header("Configuracoes")]
    public float speed;
    public rotina currentRotina;
    public Transform[] wayPoints;
    public bool isLookLeft;
    public Transform target;
    public bool isMove;
    //public float jumpForce;
    public bool isGrounded;




    private float waitTime;
    private Rigidbody2D bossRb;
    private Animator bossAnimator;    
    private int h;
    private int idEtapa;
    private float tempTime;


    // Start is called before the first frame update
    void Start()
    {
        bossRb = GetComponent<Rigidbody2D>();
        bossAnimator = GetComponent<Animator>();


        //setup inicial
        idEtapa = 0;
        tempTime = 0;
        waitTime = 3;

    }

    // Update is called once per frame
    void Update()
    {

        switch (currentRotina)
        {
            case rotina.A:
                switch (idEtapa)
                {
                    case 0://espera 3 segundos e define o destino
                        tempTime += Time.deltaTime;
                        if (tempTime>=waitTime)
                        {
                            idEtapa += 1;
                            target = wayPoints[1];
                            h = -1;
                            isMove = true;
                        }


                  break;

                        case 1: //move ate o destino

                        if (transform.position.x <= target.position.x)
                        {
                            idEtapa += 1;
                            tempTime = 0;
                            waitTime = 3;
                            h = 0;
                        }

                        break;




                    case 2:
                        tempTime += Time.deltaTime;
                        if (tempTime >= waitTime)
                        {
                            idEtapa += 1;
                            target = wayPoints[0];
                            h = 1;
                            
                        }
                        break;


                    case 3:
                        if (transform.position.x >= target.position.x)
                        {
                           
                            h = 0;
                            //fim rotina A
                            currentRotina = rotina.B;
                            tempTime = 0;
                            idEtapa = 0;
                            waitTime = 3;

                        }
                        break;

                }

            break;




            case rotina.B:  
            
                switch (idEtapa)
                {
                    case 0://espera 3 segundos e define o destino
                        tempTime += Time.deltaTime;
                        if (tempTime >= waitTime)
                        {
                            idEtapa += 1;
                            target = wayPoints[1];
                            h = -1;
                            isMove = true;
                        }


                        break;

                    case 1: //move ate o destino

                        if (transform.position.x <= target.position.x)
                        {
                            idEtapa += 1;
                            tempTime = 0;
                            waitTime = 3;
                            h = 0;
                        }

                        break;




                    case 2:
                        tempTime += Time.deltaTime;
                        if (tempTime >= waitTime)
                        {
                            idEtapa += 1;
                            target = wayPoints[2];
                            h = 1;

                        }
                        break;


                    case 3:
                        if (transform.position.x >= target.position.x)
                        {

                            h = 0;
                            idEtapa += 1;
                            bossRb.AddForce(new Vector2(0,350));
                            tempTime = 0;
                        }
                        break;


                    case 4:
                        tempTime += Time.deltaTime;
                        if (tempTime >= waitTime)
                        {
                            idEtapa += 1;

                        }

                        break;


                    case 5:
                        tempTime = 0;
                        waitTime = 5;
                        idEtapa += 1;
                        break;


                    case 6:
                        tempTime += Time.deltaTime;
                        if (tempTime >= waitTime)
                        {
                            idEtapa += 1;
                            isMove = false;
                            bossRb.AddForce(new Vector2(150, 350));
                            tempTime = 0;
                            waitTime = 1;
                        }
                        break;


                    case 7:
                        if (isGrounded==true)
                        {
                            target = wayPoints[2];
                            h = -1;
                            idEtapa += 1;
                            isMove = false;
                        }
                        break;


                        case 8:
                        if (transform.position.x <= target.position.x)
                        {

                          
                            //sorteia entre ir para A ou B

                            int rand = Random.Range(0,100);
                            if (rand < 50)
                            {
                                target = wayPoints[0];
                                h = 1;
                                idEtapa = 9;
                            }
                            else
                            {
                                target = wayPoints[1];
                                h = -1;
                                idEtapa = 10;
                            }
                        }

                        break;

                    case 9:  //se for para o ponto A
                        if (transform.position.x >= target.position.x)
                        {

                            
                            
                            idEtapa = 0;
                            tempTime = 0;
                            waitTime = 3;
                            h = 0;
                            currentRotina = rotina.A;
                        }
                        break;

                    case 10: //se for para o ponto B
                        if (transform.position.x <= target.position.x)
                        {

                            h = 0;
                            //currentRotina = rotina.A;
                        }
                        break;


                }

              


                break;

            case rotina.C:

                break;

            case rotina.D:

                break;



        }












        if (h > 0 && isLookLeft==true)
        {
            flip();
        } 
         else if (h < 0 && isLookLeft==false)
        {
            flip();
        }
        if (isMove==true)
        {
           bossRb.velocity = new Vector2(h*speed,bossRb.velocity.y);

        }
       

        bossAnimator.SetInteger("h",h);

    }

   public void flip()
    {
        isLookLeft = !isLookLeft;
        float x= transform.localScale.x * -1;
        transform.localScale = new Vector3(x,transform.localScale.y, transform.localScale.z);

    }

}
