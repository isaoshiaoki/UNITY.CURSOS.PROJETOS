using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollision : MonoBehaviour
{
    [Header("Layer")]
    public LayerMask destruirLayer;

    // Start is called before the first frame update
    void Start()
    {
       



        }

    // Update is called once per frame
    void Update()
    {
          //Debug.DrawRay(hand.position, direcao * 0.2f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, destruirLayer);


        if (hit == true)
        {
            Destroy(this.gameObject);
        }
    }
}
