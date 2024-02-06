using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public BalloonType balloonType;
    public int health;
    public int dropChance = 50;
    public int expOnDeath = 10;
    public GameObject expDrop;
    public GameObject deathVFXPrefab;
    public SFX onHitSound;
    public float velocity;
    Transform target;
    Rigidbody2D rb;
    public List<Sprite> normalBalloonSprites;
    SpriteRenderer sr;

    public float shootRange = 7f;
    public float spawnRate = 3f;
    float timer;
    public GameObject spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        SetupBalloon();
    }

    private void FixedUpdate()
    {
        BalloonBehaviour();

    }

    //When called it will destroy the gameobject and spawn a VFX
    public void TakeHit()
    {
        health--;
        SetupBalloon();
        if (health <= 0)
        {

            Death();
        }
        else
        {
            AudioManager.Instance.PlaySFX(onHitSound, transform.position);
        }
    }

    private void BalloonBehaviour()
    {
        Vector3 walkDirection = (target.position - transform.position).normalized;

        switch (balloonType)
        {
            case BalloonType.Normal:
                rb.velocity = walkDirection * velocity;
                break;
            case BalloonType.Ranged:
           
                //Only shoot when in range and only move when out of range
                if (Vector2.Distance(target.position, transform.position) < shootRange)
                {
                    rb.velocity = Vector2.zero;
                    timer -= Time.fixedDeltaTime;
                    if (timer <= 0)
                    {
                        timer = spawnRate;
                        Instantiate(spawnedObject, transform.position, Quaternion.LookRotation( Vector3.forward,walkDirection));
                    }
                }
                else
                {
                    rb.velocity = walkDirection * velocity;
                }
                break;
            case BalloonType.Spawner:
                rb.velocity = walkDirection * velocity;
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    timer = spawnRate;
                    Instantiate(spawnedObject, transform.position, Quaternion.identity);
                }
                break;
            case BalloonType.Boss:
                rb.velocity = walkDirection * velocity;
                break;
            case BalloonType.Custom:
                rb.velocity = walkDirection * velocity;
                break;
            default:
                break;
        }
    }

    public void Death()
    {
        Instantiate(deathVFXPrefab, transform.position, transform.rotation);
        int RNG = Random.Range(0, 100);
        if (RNG < dropChance)
        {
            AIManager.Instance.SpawnPowerup(transform.position);
        }
        Instantiate(expDrop, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    void SetupBalloon()
    {
        switch (balloonType)
        {
            case BalloonType.Normal:
                if (normalBalloonSprites.Count > health && health >= 0)
                    sr.sprite = normalBalloonSprites[health];
                break;
            case BalloonType.Ranged:
                break;
            case BalloonType.Spawner:
                break;
            case BalloonType.Boss:
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Monkey monkey = collision.gameObject.GetComponent<Monkey>();
                if (monkey != null)
                {
                    monkey.TakeHit();
                    Death();
                }
            }
        }
    }

    public enum BalloonType
    {
        Normal, Ranged, Spawner, Boss, Custom
    }
}
