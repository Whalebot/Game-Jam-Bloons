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
    public List<GameObject> bossSummons;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        SetupBalloon();
        AIManager.Instance.allBalloons.Add(this);
        timer = spawnRate;
    }
    private void OnDisable()
    {
        AIManager.Instance.allBalloons.Remove(this);
    }
    private void FixedUpdate()
    {
        if (transform.rotation != Quaternion.identity)
            transform.rotation = Quaternion.identity;
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
                        Instantiate(spawnedObject, transform.position, Quaternion.LookRotation(Vector3.forward, walkDirection));
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
                int dartsPerShot = 10;
                int dartSpreadAngle = 36;
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    timer = spawnRate;
                    for (int i = 0; i < dartsPerShot; i++)
                    {
                        Vector3 dartRotation = transform.rotation.eulerAngles;
                        float spawnOffset = 2;

                        if (dartsPerShot % 2 == 1)
                        {
                            float offset = i * dartSpreadAngle - (int)(dartsPerShot / 2f) * dartSpreadAngle;
                            dartRotation.z -= offset;
                        }
                        else
                        {
                            float offset = i * dartSpreadAngle - (int)(dartsPerShot / 2f) * dartSpreadAngle + 0.5f * dartSpreadAngle;

                            dartRotation.z -= offset;
                        }
                        int rng = Random.Range(0, bossSummons.Count);
                        Instantiate(bossSummons[rng], transform.position + Quaternion.Euler(dartRotation) * transform.right * spawnOffset, Quaternion.Euler(dartRotation));
                    }
                }

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
                if (normalBalloonSprites.Count > health && health > 0)
                    sr.sprite = normalBalloonSprites[health - 1];
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
                    if (balloonType != BalloonType.Boss)
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
