using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartButton : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => PressButton());
    }

    // Update is called once per frame
    void PressButton()
    {

        SceneManager.LoadScene(1);
    }
}
