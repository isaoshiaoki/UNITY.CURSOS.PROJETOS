using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBola : MonoBehaviour
{
    public float velocidade = 2.5f;
    public int moedas = 0;
    public GameObject efeitoCoin;
    public AudioClip coinSom;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));
        }
        print(moedas);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Moeda"))
        {

            Instantiate(efeitoCoin,new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z),Quaternion.identity);
            AudioController.inst.PlayAudio(coinSom);
            moedas++;
            Destroy(collision.gameObject);
        }




    }



}
