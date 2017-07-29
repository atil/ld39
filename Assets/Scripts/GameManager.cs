using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
        FpsController.ForceVelocity(Vector3.zero);
        Ui.GameOver();
        BatterySpawner.enabled = false;
        Death.enabled = false;
        Kamyon.enabled = false;

    }
}
