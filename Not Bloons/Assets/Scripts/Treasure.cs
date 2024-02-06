using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Monkey monkey = collision.GetComponent<Monkey>();
            if (monkey != null)
            {
                UIManager.Instance.OpenPowerupWindow();

                Destroy(gameObject);
            }
        }
    }
}
