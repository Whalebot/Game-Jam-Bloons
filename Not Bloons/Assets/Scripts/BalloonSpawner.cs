using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public float spawnRate = 3f;
    float timer;
    public GameObject spawnedObject;
    public bool levelSpawner;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (!levelSpawner)
        {
            if (timer <= 0)
            {
                timer = spawnRate;
                Instantiate(spawnedObject, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (timer <= 0)
            {
                timer = GameManager.Instance.CurrentWave().spawnRate;
                GameManager.Instance.SpawnWave(transform.position);
            }
        }
    }
}
