using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public Boundaries horizontalBoundary;
    public Boundaries verticalBoundary;
    public Boundaries screenBounds;
    public float horizontalSpeed = 0.0f;
    public float verticalSpeed = 0.0f;
    public Color randomColor;

    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public float fireRate = 0.2f;

    private BulletManager bulletManager;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
        InvokeRepeating("FireBullets", 0.0f, fireRate);
        ResetEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    public void Move()
    {
        var horizontalLength = horizontalBoundary.max - horizontalBoundary.min;
        transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, horizontalLength) - horizontalBoundary.max, 
                                         transform.position.y - (verticalSpeed * Time.deltaTime), 
                                         transform.position.z);

    }

    public void CheckBounds()
    {
        if (transform.position.y < screenBounds.min)
        {
            ResetEnemy();
        }
    }

    public void ResetEnemy()
    {
        var RandomXPos = UnityEngine.Random.Range(horizontalBoundary.min, horizontalBoundary.max);
        var RandomYPos = UnityEngine.Random.Range(verticalBoundary.min, verticalBoundary.max);
        horizontalSpeed = UnityEngine.Random.Range(1.0f, 6.0f);
        verticalSpeed = UnityEngine.Random.Range(1.0f, 3.0f);
        transform.position = new Vector3(RandomXPos, RandomYPos, 0.0f);

        List<Color> colorList = new List<Color> { Color.red, Color.yellow, Color.white, Color.magenta, Color.blue, Color.white };

        randomColor = colorList[UnityEngine.Random.Range(0, 6)];
        spriteRenderer.material.SetColor("_Color", randomColor);
    }

    private void FireBullets()
    {
        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.ENEMY);
    }
}
