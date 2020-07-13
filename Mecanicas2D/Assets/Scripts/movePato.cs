using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePato : MonoBehaviour
{
    public float vel = 2.5f;
    public int moedas = 0;
    public GameObject efeitoCoin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {

            transform.Translate(new Vector2(vel * Time.deltaTime,0));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector2(-vel * Time.deltaTime, 0));

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("moeda"))
        {
            Instantiate(efeitoCoin,new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z),Quaternion.identity);
            moedas++;
            Destroy(collision.gameObject);
        }
    }











}
