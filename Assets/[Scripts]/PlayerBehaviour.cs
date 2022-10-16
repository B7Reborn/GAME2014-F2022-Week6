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
    public bool usingMobileInput = false;

    public ScoreManager scoreManager;

    [Header("Bullet Properties")] 
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float fireRate = 0.2f;
    public Transform bulletParent;


    private Camera camera;

    void Start()
    {
        camera = Camera.main;

        usingMobileInput = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;

        scoreManager = FindObjectOfType<ScoreManager>();

        InvokeRepeating("FireBullets", 0.0f, fireRate);
    }


    // Update is called once per frame
    void Update()
    {
        if (usingMobileInput)
        {
            MobileInput(); 
        }
        else
        {
            ConventionalInput();
        }

        Move();

        if(Input.GetKeyDown(KeyCode.K))
        {
            scoreManager.AddPoints(10);
        }
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

    private void FireBullets()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity, bulletParent);
    }
}
