using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    public Slider KamyonHpSlider;
    public GameObject Crosshair;

    public GameObject GameOverText;
    public GameObject LoadingScreen;
    public GameObject ReplayButton;

    public void GameOver()
    {
        KamyonHpSlider.gameObject.SetActive(false);
        Crosshair.SetActive(false);
        GameOverText.SetActive(true);
        ReplayButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnClickReplay()
    {
        if (LoadingScreen != null)
        {
            LoadingScreen.SetActive(true);
        }
    }
}
