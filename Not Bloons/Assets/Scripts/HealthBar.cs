using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    Balloon balloon;
    int startHealth;
    public Image healthBar;
    private void Start()
    {
        balloon = GetComponent<Balloon>();
        startHealth = balloon.health;
    }
    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = ((float)balloon.health / (float)startHealth);
    }
}
