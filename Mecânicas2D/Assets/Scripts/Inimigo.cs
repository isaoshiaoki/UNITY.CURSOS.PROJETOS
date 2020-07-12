using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{

     public float velocidade;
    public GameObject hitPrefab;
   
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = new Vector2(velocidade,0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "hitBox":
                GameObject temp = Instantiate(hitPrefab,transform.position,transform.localRotation);
                Destroy(temp.gameObject,0.5f);
                Destroy(this.gameObject);

                break;

        }

    }

















}
