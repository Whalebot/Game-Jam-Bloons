using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public List<GameObject> hearts;
    public TextMeshProUGUI countdownText;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.monkey.takeDamageEvent += UpdateHealth;
    }

    void UpdateHealth()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (GameManager.Instance.monkey.health < i)
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {



        string minutes = Mathf.Floor(GameManager.Instance.timer / 60).ToString("00");
        string seconds = (GameManager.Instance.timer % 60).ToString("00");

        string timerString = $"{minutes}:{seconds}";
        countdownText.text = timerString;
    }
}
