using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public float velocity;
    public float lifetime = 5;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime() {
        //Do this first
        yield return new WaitForSeconds(lifetime);
        //After lifetime seconds do this
        
        yield return new WaitForSecondsRealtime(lifetime);
     
        Destroy(gameObject);
    
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Balloon balloon = collision.GetComponent<Balloon>();

        if (balloon != null)
        {
            balloon.TakeHit();
            Destroy(gameObject);
        }
    }
}
