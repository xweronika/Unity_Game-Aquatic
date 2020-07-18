using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "fish")
        {
            if (collision.gameObject.GetComponent<FishBehaviour>().enabled)
            {
                collision.gameObject.GetComponent<FishBehaviour>().enabled = false;
            }
            Vector2 dir = new Vector2(collision.contacts[0].point.x, collision.contacts[0].point.y) - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) ;
            dir = dir.normalized/10;
            collision.gameObject.GetComponent<Rigidbody2D>().position += dir;
        }
       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "fish")
        {
            collision.gameObject.GetComponent<FishBehaviour>().enabled = true;
        }
    }
}
