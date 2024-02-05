using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public BalloonType balloonType;
    public int health;
    public int dropChance = 50;
    public GameObject deathVFXPrefab;
    public float velocity;
    Transform target;
    Rigidbody2D rb;
    public List<Sprite> normalBalloonSprites;
    SpriteRenderer sr;

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
        Vector3 walkDirection = (target.position - transform.position).normalized;
        rb.velocity = walkDirection * velocity;
    }

    //When called it will destroy the gameobject and spawn a VFX
    public void TakeHit()
    {
        health--;
        SetupBalloon();

        if (health < 0)
        {

            Death();
        }
    }

    public void Death()
    {
        Instantiate(deathVFXPrefab, transform.position, transform.rotation);
        int RNG = Random.Range(0, 100);
        if (RNG > dropChance)
        {
            AIManager.Instance.SpawnPowerup(transform.position);
        }

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
        Normal, Ranged, Spawner, Boss
    }
}
