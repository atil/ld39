using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    public Slider KamyonHpSlider;
    public GameObject Crosshair;

    public GameObject GameOverText;
    public GameObject WinText;
    public GameObject LoadingScreen;
    public GameObject ReplayButton;

    
    public Text ReasonText;
    public Image GameOverOverlay;
    public Image WinOverlay;
    public AnimationCurve OverlayAppear;

    public void GameOver(EndGameReason reason)
    {
        KamyonHpSlider.gameObject.SetActive(false);
        Crosshair.SetActive(false);
        ReplayButton.SetActive(true);
        ReasonText.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        if (reason == EndGameReason.Win)
        {
            WinText.SetActive(true);
        }
        else
        {
            GameOverText.SetActive(true);
        }

        if (reason != EndGameReason.Win)
        {
            StartCoroutine(OverlayAppearCoroutine(GameOverOverlay));
        }
        else
        {
            StartCoroutine(OverlayAppearCoroutine(WinOverlay));
        }

        switch (reason)
        {
            case EndGameReason.None:
                break;
            case EndGameReason.KamyonWall:
                ReasonText.text = "TRUCK IS SWALLOWED BY THE STORM";
                break;
            case EndGameReason.PlayerWall:
                ReasonText.text = "YOU ARE LOST IN THE STORM";
                break;
            case EndGameReason.KamyonMinion:
                ReasonText.text = "TRUCK IS BLOWN AWAY";
                break;
            case EndGameReason.Win:
                break;
            default:
                throw new ArgumentOutOfRangeException("reason", reason, null);
        }
    }

    private IEnumerator OverlayAppearCoroutine(Image img)
    {
        const float duration = 1f;
        var timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            var c = img.color;
            c.a = OverlayAppear.Evaluate(timer / duration);
            img.color = c;
            yield return null;
        }
    }

    public void OnClickReplay()
    {
        if (LoadingScreen != null)
        {
            LoadingScreen.SetActive(true);
        }
    }
}
