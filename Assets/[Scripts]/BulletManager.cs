using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    [Header("Bullet Properties")]
    [Range(10, 50)]
    public int maxPlayerBullets = 50;
    public int playerBulletCount = 0;
    public int activePlayerBullets;
    [Range(10, 50)]
    public int maxEnemyBullets = 50;
    public int enemyBulletCount = 0;
    public int activeEnemyBullets;

    private BulletFactory factory;
    private Queue<GameObject> playerBulletPool;
    private Queue<GameObject> enemyBulletPool;

    // Start is called before the first frame update
    void Start()
    {
        playerBulletPool = new Queue<GameObject>();
        enemyBulletPool = new Queue<GameObject>();
        factory = GameObject.FindObjectOfType<BulletFactory>();
        BuildBulletPools();
    }

    void BuildBulletPools()
    {
        for (int i = 0; i < maxPlayerBullets; i++)
        {
            playerBulletPool.Enqueue(factory.CreateBullet(BulletType.PLAYER));
        }

        for (int i = 0; i < maxEnemyBullets; i++)
        {
            enemyBulletPool.Enqueue(factory.CreateBullet(BulletType.ENEMY));
        }

        // stats
        playerBulletCount = playerBulletPool.Count;
        enemyBulletCount = enemyBulletPool.Count;
    }


    public GameObject GetBullet(Vector2 position, BulletType type)
    {
        GameObject bullet = null;

        switch (type)
        {
            case BulletType.PLAYER:
            {
                if (playerBulletPool.Count < 1)
                {
                    playerBulletPool.Enqueue(factory.CreateBullet(BulletType.PLAYER));
                }

                bullet = playerBulletPool.Dequeue();
                playerBulletCount = playerBulletPool.Count;
                activePlayerBullets++;
                break;
            }
            case BulletType.ENEMY:
            {
                if (enemyBulletPool.Count < 1)
                {
                    enemyBulletPool.Enqueue(factory.CreateBullet(BulletType.ENEMY));
                }

                bullet = enemyBulletPool.Dequeue();
                enemyBulletCount = enemyBulletPool.Count;
                activeEnemyBullets++;
                break;
            }
        }

        bullet.SetActive(true);
        bullet.transform.position = position;

        return bullet;
    }

    public void ReturnBullet(GameObject bullet, BulletType type)
    {
        bullet.SetActive(false);

        switch (type)
        {
            case BulletType.PLAYER:
                playerBulletPool.Enqueue(bullet);
                playerBulletCount = playerBulletPool.Count;
                activePlayerBullets--;
                break;
            case BulletType.ENEMY:
                enemyBulletPool.Enqueue(bullet);
                enemyBulletCount = enemyBulletPool.Count;
                activeEnemyBullets--;
                break;
        }
    }
}
