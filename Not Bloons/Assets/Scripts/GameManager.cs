
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Almost every game project includes some kind of Game Manager class that handles the overall flow of the game, namely loading and transitioning through scenes.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int level = 1;
    public int exp = 0;
    public int expToLevel = 100;

    public SFX levelUpSound;

    public Monkey monkey;
    public bool timeOver;
    public bool gameOver;
    public bool won;

    public float timer = 300f;
    public float timePassed = 300f;
    float startTimer;
    public int wave = 1;
    public SFX waveChangeSFX;
    public SFX winSFX;
    public SFX loseSFX;

    public event Action<Vector3> waveSpawn;
    public event Action gameOverEvent;
    public event Action gameWinEvent;
    public List<Wave> waves;



    private void Awake()
    {
        Instance = this;
        startTimer = timer;
        Time.timeScale = 1.0f;
    }
    private void Start()
    {
        monkey.deathEvent += GameLoss;
    }
    void GameWin()
    {
        won = true;
        AudioManager.Instance.PlaySFX(winSFX);
        Time.timeScale = 0;
        gameWinEvent?.Invoke();
    }
    void GameLoss()
    {
        gameOver = true;
        AudioManager.Instance.PlaySFX(loseSFX);
        Time.timeScale = 0;
        gameOverEvent?.Invoke();
    }
    public Wave CurrentWave()
    {
        if (wave > waves.Count)
        {
            return waves[waves.Count - 1];
        }
        return waves[wave - 1];
    }

    public int Exp
    {
        get { return exp; }
        set
        {
            if (value > expToLevel)
            {
                //Calculate EXP overflow
                exp = value - expToLevel;
                //Level up
                level++;
                //Update new exp to level up
                expToLevel = (int)(100 * Mathf.Pow(1.5f, level - 1));
                AudioManager.Instance.PlaySFX(levelUpSound);


                monkey.Health++;

                UIManager.Instance.OpenPowerupWindow();
            }
            else
            {
                exp = value;
            }
        }
    }

    /// <summary>
    /// This GameManager will check for input to restart the scene
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Return))
        {
            RestartTheGame();
        }
        if (timer <= 0 && !won)
        {
            if (AIManager.Instance.allBalloons.Count == 0)
            {
                GameWin();
            }
            timeOver = true;
            return;
        }
        timePassed = Time.timeSinceLevelLoad;
        timer -= Time.deltaTime;

        CheckWave();
    }

    private void CheckWave()
    {
        //If 30 seconds have passed start wave 2
        if (timePassed > 30 * wave)
        {
            AudioManager.Instance.PlaySFX(waveChangeSFX);
            wave++;
        }

    }

    public void SpawnWave(Vector3 spawnPos)
    {
        int currentWave = wave - 1;
        if (wave > waves.Count)
        {
            currentWave = waves.Count - 1;
        }
        int RNG = UnityEngine.Random.Range(0, waves[currentWave].enemyTypes.Count);
        Instantiate(waves[currentWave].enemyTypes[RNG], spawnPos, Quaternion.identity);
    }

    public void SpawnFromWave(Vector3 spawnPos)
    {
        waveSpawn?.Invoke(spawnPos);
    }

    /// <summary>
    /// Reloads the current scene.
    /// </summary>
    public void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

[System.Serializable]
public class Wave
{
    public List<GameObject> enemyTypes;
    public float spawnRate;
}