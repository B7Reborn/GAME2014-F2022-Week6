using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public Boundaries horizontalBoundary;
    public Boundaries verticalBoundary;
    public Boundaries horizontalSpeedRange;
    public float horizontalSpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        var RandomXPos = Random.Range(horizontalBoundary.min, horizontalBoundary.max);
        var RandomYPos = Random.Range(verticalBoundary.min, verticalBoundary.max);
        horizontalSpeed = Random.Range(horizontalSpeedRange.min, horizontalSpeedRange.max);
        transform.position = new Vector3(RandomXPos, RandomYPos, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalLength = horizontalBoundary.max - horizontalBoundary.min;
        transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, horizontalLength) - horizontalBoundary.max, transform.position.y, transform.position.z);
    }
}
