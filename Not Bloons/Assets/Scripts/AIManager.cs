using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance;
    public List<GameObject> powerupDrops;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
    }

    public void SpawnPowerup(Vector3 spawnPosition)
    {
        int RNG = Random.Range(0, powerupDrops.Count);
        Instantiate(powerupDrops[RNG], spawnPosition, Quaternion.identity);
    }
}
