using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    public GameObject fishController;
    public int type;
    public float size;
    public GameObject player;
    public List<GameObject> fishList;
    private Rigidbody2D rb2d;
    private Vector2 cohesion;
    private Vector2 alignment;
    private Vector2 separation;
    private Vector2 avoidance;

    /************** parametry boidow********************/
    private float maxspeed = 15;
    private float maxForce = 10;
    private float separationDist = 15.0f;
    private float seeingDistance = 15;
    private Vector4 forceMultipliers = new Vector4(10, 0, 3, 5);


    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cohesion = new Vector2();
        separation = new Vector2();
        avoidance = new Vector2();
        fishList = fishController.GetComponent<FishController>().fishInTheSea;
        StartCoroutine(shrink());
    }
    private IEnumerator shrink()
    {
        while (true)
        {
            if (size > 5)
            {
                size -= 0.001f * (size * size);
                gameObject.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(size, size);
            }
            yield return new WaitForSeconds(0.2f);
        }


    }
    public void calculateVelocities()
    {
        cohesion = Cohesion();
        separation = Separate();
        avoidance = AvoidStuff();

    }

    public void updateVelocities()
    {
        rb2d.AddForce(separation * forceMultipliers.x);
        rb2d.AddForce(cohesion * forceMultipliers.z);
        rb2d.AddForce(avoidance * forceMultipliers.w);
        rb2d.rotation = Mathf.Rad2Deg * Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x);
    }

    private Vector2 Separate()
    {
        Vector2 steer = new Vector2(0, 0);
        int tooClose = 0;

        foreach (GameObject g in fishList)
        {
            if(g.GetComponent<FishBehaviour>().size > size*1.5)
            {
                float dist = Vector2.Distance(rb2d.position, g.GetComponent<Rigidbody2D>().position);
                if (g != gameObject && dist- g.GetComponent<FishBehaviour>().size < 2 * separationDist)
                {
                    Vector2 diff = rb2d.position - g.GetComponent<Rigidbody2D>().position;
                    diff.Normalize();
                    diff /= dist;
                    steer += diff;
                    tooClose++;
                }
            }

        }
        if (player.GetComponent<PlayerBehaviour>().size > size * 1.5)
        {
            float dist = Vector2.Distance(rb2d.position, player.GetComponent<Rigidbody2D>().position);
            if (dist- player.GetComponent<PlayerBehaviour>().size < 2 * separationDist)
            {
                Vector2 diff = rb2d.position - player.GetComponent<Rigidbody2D>().position;
                diff.Normalize();
                diff /= dist;
                steer += diff;
                tooClose++;
            }
        }
        if (tooClose > 0)
        {
            steer = steer / tooClose;
        }
        if (steer.magnitude > 0)
        {
            steer.Normalize();
            steer *= maxspeed;
            steer -= rb2d.velocity;
            steer = LimitToMaxforce(steer);
        }
        return steer; 
    }

    private Vector2 Cohesion()
    {
        Vector2 sum = new Vector2(0, 0);
        Vector2 steer = new Vector2(0, 0);
        float minDistance =200;
        Vector2 target = new Vector2();
        foreach (GameObject g in fishList)
        {
            if (g.GetComponent<FishBehaviour>().size*1.5 < size)
            {
                if (g != gameObject && Vector2.Distance(rb2d.position, g.GetComponent<Rigidbody2D>().position) < minDistance)
                {
                    target = g.GetComponent<Rigidbody2D>().position;
                    minDistance = Vector2.Distance(rb2d.position, g.GetComponent<Rigidbody2D>().position);
                }

            }

        }
        if (player.GetComponent<PlayerBehaviour>().size * 1.5 < size)
        {
            if (player != gameObject && Vector2.Distance(rb2d.position, player.GetComponent<Rigidbody2D>().position) < minDistance)
            {
                target = player.GetComponent<Rigidbody2D>().position;
                minDistance = Vector2.Distance(rb2d.position, player.GetComponent<Rigidbody2D>().position);
            }

        }
        if (minDistance < 200)
        {
            return Seek(target);
        }
        else
        {
            return new Vector2();
        }
        
       

    }

    private Vector2 Seek(Vector2 sum)
    {
        Vector2 desired = sum - rb2d.position;
        desired.Normalize();
        desired *= maxspeed;
        Vector2 steer = desired - rb2d.velocity;
        steer = LimitToMaxforce(steer);
        return steer;
    }

    private Vector2 AvoidStuff()
    {
        Vector2 steer = new Vector2(0, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rb2d.velocity, seeingDistance);
        if (hit.collider != null && hit.collider.gameObject.layer!=2)
        {
            steer = hit.normal;
            steer *= maxForce/2;
        }
        hit = Physics2D.Raycast(transform.position, rb2d.velocity, seeingDistance/2);
        if (hit.collider != null&& hit.collider.gameObject.layer!=2)
        {
            steer = hit.normal;
            steer *= maxForce;
        }
        return steer;
    }

    private Vector2 LimitToMaxforce(Vector2 v)
    {
        if (v.magnitude > maxForce)
        {
            v.Normalize();
            v*= maxForce;
        }
        return v;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (fishList.Contains(collision.gameObject)&&collision.gameObject.GetComponent<FishBehaviour>().size*1.5 < size)
        {
            fishList.Remove(collision.gameObject);
            if (size <= 12)
            {
                size += collision.gameObject.GetComponent<FishBehaviour>().size / 5;
            }
            gameObject.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(size, size);
            //Destroy(collision.gameObject);
            
            collision.gameObject.GetComponent<FishBehaviour>().kill();
        
        }
        else if(collision.gameObject == player&& collision.gameObject.GetComponent<PlayerBehaviour>().size * 1.5 < size)
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().kill();
        }
    }
    public void kill()
    {
        Destroy(gameObject, 1.2f);
        StopAllCoroutines();
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        StartCoroutine(fade());
    }
    private IEnumerator fade()
    {
        for (int i = 0; i < 10; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, gameObject.GetComponent<SpriteRenderer>().color.a - 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator fadeIn()
    {
        for(int i = 0; i<10; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, gameObject.GetComponent<SpriteRenderer>().color.a + 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
