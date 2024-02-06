using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public List<GameObject> hearts;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI levelText;
    public Image expBar;
    public GameObject powerupWindow;
    public List<PowerupButton> powerupButtons;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.monkey.updateHealthEvent += UpdateHealth;
    }

    void UpdateHealth()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (GameManager.Instance.monkey.health - 1 <= i)
            {
                hearts[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            else {
                hearts[i].transform.GetChild(0).gameObject.SetActive(true);
            }

            if (GameManager.Instance.monkey.maxHealth  <= i)
            {
                hearts[i].transform.gameObject.SetActive(false);
            }
            else
            {
                hearts[i].transform.gameObject.SetActive(true);
            }
        }
    }

    public void OpenPowerupWindow()
    {
        powerupWindow.SetActive(true);
        for (int i = 0; i < powerupButtons.Count; i++)
        {
            Powerup.PowerupTypes randomType = (Powerup.PowerupTypes)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Powerup.PowerupTypes)).Length);

            powerupButtons[i].SetupButton(randomType);
        }

        Time.timeScale = 0;
    }
    public void ClosePowerupWindow()
    {
        powerupWindow.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        levelText.text = "Lv: " + GameManager.Instance.level;
        expBar.fillAmount = ((float)GameManager.Instance.exp / (float)GameManager.Instance.expToLevel);

        if (GameManager.Instance.timeOver)
        {
            //Text Timer stuff
            string minutes = Mathf.Floor(GameManager.Instance.timer / 60).ToString("00");
            string seconds = (GameManager.Instance.timer % 60).ToString("00");

            string timerString = $"{minutes}:{seconds}";
            countdownText.text = timerString;
        }
        else
            countdownText.gameObject.SetActive(false);
    }
}
