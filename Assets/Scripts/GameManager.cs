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

    public void GameOver()
    {
        FpsController.InputEnabled = false;
        FpsController.MouseLookEnabled = false;
        FpsController.MoveEnabled = false;
        FpsController.ForceVelocity((Death.transform.forward * 0.5f + Vector3.up * 2) * 40f);
        Ui.GameOver();
        BatterySpawner.enabled = false;
        Death.enabled = false;
        Kamyon.enabled = false;
    }

    public void OnReplayClicked()
    {
        SceneManager.LoadScene("Game");
    }
}
