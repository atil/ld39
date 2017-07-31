using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EndGameReason
{
    None, KamyonWall, PlayerWall, KamyonMinion, Win
}

public class GameManager : MonoBehaviour
{
    public FpsController FpsController;
    public Kamyon Kamyon;
    public Death Death;
    public BatterySpawner BatterySpawner;
    public MinionSpawner MinionSpawner;
    public Ui Ui;
    public Fire Fire;

    public AnimationCurve Silence;

    private bool _isGameEnded;

    public void EndGame(EndGameReason reason)
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
        MinionSpawner.DeactivateMinions();

        Ui.GameOver(reason);
        if (reason == EndGameReason.KamyonMinion)
        {
            FpsController.ForceVelocity((Death.transform.forward * 0.75f + Vector3.up * 1.5f) * 40f);
        }
        StartCoroutine(WaitAndDisablePlayer());
        StartCoroutine(SilenceAllAudio());
    }

    private IEnumerator SilenceAllAudio()
    {
        var allAudio = FindObjectsOfType<AudioSource>();
        var initials = allAudio.Select(x => x.volume).ToList();

        const float duration = 1f;
        var timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;

            for (var i = 0; i < allAudio.Length; i++)
            {
                allAudio[i].volume = Mathf.Lerp(0f, initials[i], Silence.Evaluate(timer / duration));
            }

            yield return null;
        }

    }

    private IEnumerator WaitAndDisablePlayer()
    {
        yield return new WaitForSeconds(1f);
        FpsController.enabled = false;
    }

    public void OnReplayClicked()
    {
        SceneManager.LoadScene("Game");
    }
}
