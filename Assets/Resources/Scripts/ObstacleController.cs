using System;
using System.Collections.Generic;
using UnityEngine;



public class ObstacleController : MonoBehaviour
{
    public List<GameObject> obstacles;
    public int maxId;
    private System.Random random = new System.Random();
    public GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(prefabs.Length);
        obstacles.Add(CreateNewObstacle(0, 50, 1, 100, 0, true));
        obstacles.Add(CreateNewObstacle(100, 50, 1, 100, 0, true));
        obstacles.Add(CreateNewObstacle(50, 0, 100, 1, 0, true));
        obstacles.Add(CreateNewObstacle(50, 100, 100, 1, 0, true));
        CreateNewObstacle(50, 50, 9.3328f, 9.3328f, 0, false, true);
        

        GameObject frame1 = new GameObject();
        var renderer = frame1.AddComponent<SpriteRenderer>();
        frame1.transform.position = new Vector3(200.5f, 55, 0);
        frame1.transform.localScale = new Vector3(200, 200, 1);
        renderer.sprite = Resources.Load("Square", typeof(Sprite)) as Sprite;
        renderer.sortingLayerName = "frame";
        renderer.color = new Color(0, 0, 0);

        GameObject frame2 = new GameObject();
        renderer = frame2.AddComponent<SpriteRenderer>();
        frame2.transform.position = new Vector3(-100.5f, 55, 0);
        frame2.transform.localScale = new Vector3(200, 200, 1);
        renderer.sprite = Resources.Load("Square", typeof(Sprite)) as Sprite;
        renderer.sortingLayerName = "frame";
        renderer.color = new Color(0, 0, 0);
    }

    public void spawnObstacles()
    {
        obstacles.Add(CreateNewObstacle(random.Next(100), random.Next(100), 3 + random.Next(10), 3 + random.Next(10), Mathf.Deg2Rad*random.Next(180), false));
        obstacles.Add(CreateNewObstacle(random.Next(100), random.Next(100), 3 + random.Next(10), 3 + random.Next(10), Mathf.Deg2Rad * random.Next(180), false));
        obstacles.Add(CreateNewObstacle(random.Next(100), random.Next(100), 3 + random.Next(10), 3 + random.Next(10), Mathf.Deg2Rad * random.Next(180), false));
        obstacles.Add(CreateNewObstacle(random.Next(100), random.Next(100), 3 + random.Next(10), 3 + random.Next(10), Mathf.Deg2Rad * random.Next(180), false));
    }

    public GameObject CreateNewObstacle(float x, float y, float width, float height, float rotation, bool shape, bool bg = false)
    {
        GameObject obstacle;

        if (bg) // background
        {
            obstacle = Instantiate(prefabs[4], new Vector3(x, y, 0), Quaternion.identity);
            obstacle.name = "background";
            obstacle.layer = 0;
            obstacle.transform.position = new Vector3(x, y, 0);
            obstacle.transform.localScale = new Vector3(width, height, 1);
        }
        else if (shape) // wall
        {
            obstacle = new GameObject();
            obstacle.name = "obstacle#" + (maxId + 1);
            obstacle.layer = 8;
            var renderer = obstacle.AddComponent<SpriteRenderer>();
            obstacle.transform.position = new Vector3(x, y, 0);
            obstacle.transform.Rotate(0, 0, rotation * Mathf.Rad2Deg, Space.Self);
            obstacle.transform.localScale = new Vector3(width, height, 1);
            var collider = obstacle.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1,1);
            renderer.sprite = Resources.Load("Square", typeof(Sprite)) as Sprite;
            renderer.sortingLayerName = "frame";
        }
        else // stone obstacle
        {
            obstacle = Instantiate(prefabs[UnityEngine.Random.Range(0, 4)], new Vector3(x, y, 0), Quaternion.identity);
            obstacle.name = "obstacle#" + (maxId + 1);
            obstacle.layer = 8;
            obstacle.transform.position = new Vector3(x, y, 0);
            obstacle.transform.Rotate(0, 0, rotation * Mathf.Rad2Deg, Space.Self);
            obstacle.transform.localScale = new Vector3(width, height, 1);
            obstacle.AddComponent<ObstacleBehaviour>();
            obstacle.GetComponent<SpriteRenderer>().sortingLayerName = "obstacles";

        }



        maxId++;
        return obstacle;
    }



}
