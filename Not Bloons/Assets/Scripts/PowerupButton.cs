using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PowerupButton : Powerup
{
    Button button;
    public TextMeshProUGUI powerupName;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => PressButton());
    }

    void PressButton() {
        GivePowerup();
        UIManager.Instance.ClosePowerupWindow();
    }

    public void SetupButton(PowerupTypes newType)
    {
        powerupType = newType;
        powerupName.text = "" + powerupType;
    }
}
