using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatIA : MonoBehaviour
{
    private GameController _GameController;
    private bool isFollow;
    public float speed;
    public bool isLookLeft;


    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;


    }

    // Update is called once per frame
    void Update()
    {
        if (isFollow==true)
        {
            transform.position = Vector3.MoveTowards(transform.position,_GameController.playerTransform.position,speed * Time.deltaTime);
        }



        if (transform.position.x < _GameController.playerTransform.position.x && isLookLeft==true)
        {
            flip();
        }
        else if (transform.position.x > _GameController.playerTransform.position.x && isLookLeft == false)
        {
            flip();
        }




    }

    private void OnBecameVisible()
    {
        isFollow = true;
    }

    public void flip()
    {
        isLookLeft = !isLookLeft;

        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

}
