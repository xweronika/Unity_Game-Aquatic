using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FishController : MonoBehaviour
{
    public List<GameObject> fishInTheSea;
    public int maxId;

    public GameObject prefab;
    public GameObject prefabY;
    private System.Random rng;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rng = new System.Random();
        fishInTheSea = new List<GameObject>();

        gameObject.GetComponent<ObstacleController>().spawnObstacles();
        player = Instantiate(prefabY, new Vector3(rng.Next(80) + 10, rng.Next(80) + 10, 0), Quaternion.identity);
        bool check = false;
        do
        {
            check = false;
            foreach (GameObject g in gameObject.GetComponent<ObstacleController>().obstacles)
            {
                if (player.GetComponent<SpriteRenderer>().bounds.Intersects(g.GetComponent<SpriteRenderer>().bounds))
                {
                    check = true;
                }
            }
            if (check)
            {
                player.transform.position = new Vector3(rng.Next(80) + 10, rng.Next(80) + 10, 0);
            }


        } while (check);
        player.GetComponent<PlayerBehaviour>().fishList = fishInTheSea;
        player.GetComponent<PlayerBehaviour>().controller = gameObject;
        player.GetComponent<SpriteRenderer>().sortingLayerName = "player";
        maxId = 0;
        for(int i = 0; i<50; i++)
        {
            CreateNewFish(rng.Next(80)+10, rng.Next(80)+10, new Vector2(rng.Next(3)-1, rng.Next(3)-1), 1);
        }


    }

    void Update()
    {
        foreach(GameObject g in fishInTheSea)
        {
            g.GetComponent<FishBehaviour>().calculateVelocities();
        }
        foreach (GameObject g in fishInTheSea)
        {
            g.GetComponent<FishBehaviour>().updateVelocities();
        }
        if (fishInTheSea.Count < 50)
        {
            CreateNewFish(rng.Next(80) + 10, rng.Next(80) + 10, new Vector2(rng.Next(3) - 1, rng.Next(3) - 1), 1);
        }
    }

    void CreateNewFish(float x, float y, Vector2 velocity, float rotation)
    {
        GameObject fish;
        fish = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        fish.name = "fish#" + (maxId + 1);
        fishInTheSea.Add(fish);
        fish.GetComponent<FishBehaviour>().type = 1;
        fish.GetComponent<FishBehaviour>().size = 1 + (float)rng.NextDouble();
        fish.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(fish.GetComponent<FishBehaviour>().size, fish.GetComponent<FishBehaviour>().size);

        fish.layer = 2;
        var renderer = fish.GetComponent<SpriteRenderer>();
        var rb2d = fish.GetComponent<Rigidbody2D>();
        var behaviour = fish.GetComponent<FishBehaviour>();
        var collider = fish.GetComponent<CircleCollider2D>();
        behaviour.fishController = gameObject;
        behaviour.player = player;
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2d.velocity = velocity;
        rb2d.gravityScale = 0;
        rb2d.rotation =rotation*Mathf.Rad2Deg;
        fish.tag = "fish";
        renderer.sortingOrder = maxId;
        renderer.sortingLayerName = "targets";
        bool check = false;
        do
        {
            check = false;
            foreach (GameObject g in gameObject.GetComponent<ObstacleController>().obstacles)
            {
                if (renderer.bounds.Intersects(g.GetComponent<SpriteRenderer>().bounds))
                {
                    check = true;
                }
            }
            if (check)
            {
                fish.transform.position = new Vector3(rng.Next(80) + 10, rng.Next(80) + 10, 0);
            }

                
        } while (check);
        maxId++;
        StartCoroutine(fish.GetComponent<FishBehaviour>().fadeIn());

        
    }


}
