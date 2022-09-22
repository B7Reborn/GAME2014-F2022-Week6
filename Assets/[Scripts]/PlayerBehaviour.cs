using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 2.0f;
    public Boundaries boundaries;
    public float verticalPosition;
    public float horizontalSpeed = 2.0f;

    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        //ConventionalInput();
        MobileInput();
        Move();
    }

    public void ConventionalInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        transform.position += new Vector3(x, verticalPosition, 0.0f);
    }

    public void MobileInput()
    {
        foreach (var touch in Input.touches)
        {
            var destination = camera.ScreenToWorldPoint(touch.position);
            transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime * horizontalSpeed);
        }
    }

    public void Move()
    {


        float clampedPosition = Mathf.Clamp(transform.position.x, boundaries.min, boundaries.max);
        transform.position = new Vector2(clampedPosition, verticalPosition);

    }
}
