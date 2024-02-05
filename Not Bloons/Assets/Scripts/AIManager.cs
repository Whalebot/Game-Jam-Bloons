using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance;
    public List<GameObject> powerupDrops;

    public List<GameObject> waveType1, waveType2, waveType3, bossWave;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    public void SpawnPowerup(Vector3 spawnPosition) {
        int RNG = Random.Range(0, powerupDrops.Count);
        Instantiate(powerupDrops[RNG], spawnPosition, Quaternion.identity);
    }
}
