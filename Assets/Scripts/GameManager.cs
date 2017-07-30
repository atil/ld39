using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public FpsController FpsController;
    public Kamyon Kamyon;
    public Death Death;
    public BatterySpawner BatterySpawner;
    public Ui Ui;
    public Fire Fire;

    private bool _isGameEnded;

    public void EndGame(bool isWon)
    {
        if (_isGameEnded)
        {
            return;
        }
        _isGameEnded = true;

        FpsController.InputEnabled = false;
        FpsController.MouseLookEnabled = false;
        FpsController.MoveEnabled = false;
        BatterySpawner.enabled = false;
        Death.enabled = false;
        Kamyon.enabled = false;
        Fire.enabled = false;

        if (isWon)
        {
            Ui.Win();
        }
        else
        {
            FpsController.ForceVelocity((Death.transform.forward * 0.75f + Vector3.up * 1.5f) * 40f);
            Ui.GameOver();
        }
    }


    public void OnReplayClicked()
    {
        SceneManager.LoadScene("Game");
    }
}
