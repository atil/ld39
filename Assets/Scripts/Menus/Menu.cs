using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Image Overlay;
    public AnimationCurve OverlayAnim;

    public Text Plstext;

    public void OnPlsClicked()
    {
        Application.OpenURL("http://listenonrepeat.com/watch/?v=JLMPA4xPFpg");
        Plstext.text = "thank you !";
    }

    public void OnPlayClicked()
    {
        StartCoroutine(WaitAndAdvance());
    }

    IEnumerator WaitAndAdvance()
    {
        yield return StartCoroutine(Ui.OverlayAppearCoroutine(Overlay, OverlayAnim));
        SceneManager.LoadScene("Game");
    }

}
