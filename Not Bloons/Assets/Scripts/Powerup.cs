using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupTypes powerupType;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Monkey monkey = collision.GetComponent<Monkey>();
            if (monkey != null)
            {
                //Double Attack speed
                Random.Range(0, 2);
                switch (powerupType)
                {
                    case PowerupTypes.AttackSpeed:
                        monkey.spawnRate *= 0.5f;
                        break;
                    case PowerupTypes.DartsFired:
                        monkey.dartsPerShot ++;
                        break;
                    case PowerupTypes.Spread:
                        monkey.dartSpreadAngle +=5;
                        break;
                    default:
                        break;
                }
     
                Destroy(gameObject);
            }
        }
    }

    public enum PowerupTypes
    {
        AttackSpeed, DartsFired, Spread
    }
}
