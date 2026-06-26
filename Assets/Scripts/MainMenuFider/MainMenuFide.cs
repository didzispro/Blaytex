using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFide : MonoBehaviour
{

    public CanvasGroup blackScreen;
    public GameObject panel;
    public float fadeSpeed = 1f;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while (blackScreen.alpha > 0)
        {
            blackScreen.alpha -= Time.unscaledDeltaTime * fadeSpeed;
            yield return null;
        }

        blackScreen.alpha = 0;

        blackScreen.gameObject.SetActive(false);
        panel.SetActive(false);
    }
}
