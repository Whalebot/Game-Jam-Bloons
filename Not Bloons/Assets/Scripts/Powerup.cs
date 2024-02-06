using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupTypes powerupType;
    public SFX powerupSFX;
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
                GivePowerup();

                Destroy(gameObject);
            }
        }
    }

    public enum PowerupTypes
    {
        FireRate, DartsFired, DartSpread, DartSpacing, MovementSpeed, Health, DartSize
    }
    public void GivePowerup()
    {
        AudioManager.Instance.PlaySFX(powerupSFX);
        switch (powerupType)
        {
            case PowerupTypes.FireRate:
                GameManager.Instance.monkey.dartRateOfFire += 0.5f;
                break;
            case PowerupTypes.DartsFired:
                GameManager.Instance.monkey.dartsPerShot++;
                break;
            case PowerupTypes.DartSpread:
                GameManager.Instance.monkey.dartSpreadAngle += 5;
                break;
            case PowerupTypes.DartSpacing:
                GameManager.Instance.monkey.dartSpawnSpacing += 0.1f;
                break;
            case PowerupTypes.MovementSpeed:
                GameManager.Instance.monkey.velocity += 0.5F;
                break;
            case PowerupTypes.Health:
                GameManager.Instance.monkey.maxHealth++;
                GameManager.Instance.monkey.Health++;
                break;
            case PowerupTypes.DartSize:
                GameManager.Instance.monkey.sizeModifier += 0.5f;
                break;
            default:
                break;
        }
    }
}
