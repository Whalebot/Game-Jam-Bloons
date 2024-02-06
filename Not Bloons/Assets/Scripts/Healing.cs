using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Monkey monkey = collision.GetComponent<Monkey>();
            if (monkey != null)
            {
                GameManager.Instance.monkey.Health++;

                Destroy(gameObject);
            }
        }
    }
}
