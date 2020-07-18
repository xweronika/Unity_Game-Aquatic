using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Vector3 mousePosition;
    private float maxspeed = 15;
    private float maxForce = 10;
    private float forceMultiplier = 20;
    public float size = 2.5f;
    public GameObject controller;
    public List<GameObject> fishList;
    private Rigidbody2D rb2d;
    public int score;
    System.Random rng;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        score = 0;
        StartCoroutine(shrink());
        controller.GetComponent<UI>().setHighscore(PlayerPrefs.GetInt("highscore", 0));
        rng = new System.Random();
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
    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        rb2d.AddForce(Seek(mousePosition)*forceMultiplier);
        rb2d.rotation = Mathf.Rad2Deg * Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x);
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
    private Vector2 LimitToMaxforce(Vector2 v)
    {
        if (v.magnitude > maxForce)
        {
            v.Normalize();
            v *= maxForce;
        }
        return v;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (fishList.Contains(collision.gameObject) && collision.gameObject.GetComponent<FishBehaviour>().size * 1.5 < size)
        {
            fishList.Remove(collision.gameObject);
            if (size <= 12)
            {
                size += collision.gameObject.GetComponent<FishBehaviour>().size / 5;
            }
            gameObject.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(size, size);
            //Destroy(collision.gameObject);
            score += (int)collision.gameObject.GetComponent<FishBehaviour>().size*100;
            collision.gameObject.GetComponent<FishBehaviour>().kill();
            controller.GetComponent<UI>().setScore(score);
        }
    }

    public void kill()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;

        StartCoroutine(fade());
 
    }
    private IEnumerator fade()
    {
        for (int i = 0; i < 10; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, gameObject.GetComponent<SpriteRenderer>().color.a - 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        size = 2.5f;
        transform.position = new Vector3(rng.Next(60)+20, rng.Next(60) + 20, 0);
        bool check = false;
        do
        {
            check = false;
            foreach (GameObject g in controller.GetComponent<ObstacleController>().obstacles)
            {
                if (gameObject.GetComponent<SpriteRenderer>().bounds.Intersects(g.GetComponent<SpriteRenderer>().bounds))
                {
                    check = true;
                }
            }
            if (check)
            {
                gameObject.transform.position = new Vector3(rng.Next(60) + 20, rng.Next(60) + 20, 0);
            }


        } while (check);
        rb2d.velocity = new Vector2();
        gameObject.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(size, size);
        if(score> PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            controller.GetComponent<UI>().setHighscore(PlayerPrefs.GetInt("highscore", 0));
        }
        score = 0;
        controller.GetComponent<UI>().setScore(0);

        for (int i = 0; i < 10; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, gameObject.GetComponent<SpriteRenderer>().color.a + 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

}
