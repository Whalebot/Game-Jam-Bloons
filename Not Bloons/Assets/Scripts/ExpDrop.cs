using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDrop : MonoBehaviour
{
    public int expGained;
    public SFX pickupSFX;
    bool isMagneting = false;
    private void Update()
    {
        if (isMagneting)
        {
            transform.Translate((GameManager.Instance.monkey.transform.position - transform.position).normalized * Time.deltaTime * 5f);
        }
    }
    public void StartMagnet()
    {
        isMagneting = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Monkey monkey = collision.GetComponent<Monkey>();
            if (monkey != null)
            {
                GameManager.Instance.Exp += expGained;
                AudioManager.Instance.PlaySFX(pickupSFX);
                Destroy(gameObject);
            }
        }
    }
}
