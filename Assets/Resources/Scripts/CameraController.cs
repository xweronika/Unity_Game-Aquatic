using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        transform.position = new Vector3(50, 50, -20);
        cam.orthographicSize = 50;
    }

    // Update is called once per frame
    void Update()
    {
        bool right = Input.GetKey(KeyCode.RightArrow);
        bool left = Input.GetKey(KeyCode.LeftArrow);
        bool up = Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.DownArrow);
        bool forward = Input.GetKey(KeyCode.KeypadPlus);
        bool backward = Input.GetKey(KeyCode.KeypadMinus);

        Move(right, left, up, down, forward, backward);
    }

    private void Move(bool right, bool left, bool up, bool down, bool forward, bool backward)
    {
        float moveX = 0;
        float moveY = 0;

        if (right && transform.position.x <= 98)
        {
            moveX += 2; 
        }
        if (left && transform.position.x >= 2)
        {
            moveX -= 2;
        }
        if (up && transform.position.y <= 98)
        {
            moveY += 2;
        }
        if (down && transform.position.y>=2)
        {
            moveY -= 2;
        }
        if (forward && Camera.main.orthographicSize > 1)
        {
            Camera.main.orthographicSize -= 1;
        }
        if (backward && Camera.main.orthographicSize < 50)
        {
            Camera.main.orthographicSize += 1;
        }

        transform.position += new Vector3(moveX, moveY);
    }
}
