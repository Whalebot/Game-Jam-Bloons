using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public float spawnRate = 3f;
    float timer;
    public GameObject spawnedObject;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = spawnRate;
            Instantiate(spawnedObject, transform.position, Quaternion.identity);
        }
    }
}
