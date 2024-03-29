using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public SFX powerupSFX;
    void DoMagnetStuff() {
        ExpDrop[] expDrops = FindObjectsOfType<ExpDrop>();
        foreach (var item in expDrops)
        {
            item.StartMagnet();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Monkey monkey = collision.GetComponent<Monkey>();
            if (monkey != null)
            {
                DoMagnetStuff();
                   AudioManager.Instance.PlaySFX(powerupSFX);
                Destroy(gameObject);
            }
        }
    }
}
