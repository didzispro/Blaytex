using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Fide : MonoBehaviour
{
    public GameObject fidePanel;
    public CanvasGroup blackScreen;
    public CanvasGroup[] text;
    public float fadeSpeed = 0.5f;
    public float waitTime = 3f;

    private GameManager gameManager;
   
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void PlayEnding()
    {
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        gameManager.calmMusic.Play();

        yield return new WaitForSecondsRealtime(1f);

        // Fade in black + first text
        yield return StartCoroutine(FadeIn(blackScreen));
        yield return StartCoroutine(FadeIn(text[0]));

        yield return new WaitForSecondsRealtime(waitTime);

        // Loop through remaining texts
        for (int i = 0; i < text.Length; i++)
        {
            yield return StartCoroutine(FadeOut(text[i]));

            if (i + 1 < text.Length)
            {
                yield return new WaitForSecondsRealtime(1f);
                yield return StartCoroutine(FadeIn(text[i + 1]));
                yield return new WaitForSecondsRealtime(waitTime);
            }
        }

        
        
        yield return StartCoroutine(FadeOut(text[text.Length - 1]));
        gameManager.calmMusic.Stop();
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FadeIn(CanvasGroup cg)
    {
        while (cg.alpha < 1)
        {
            cg.alpha += Time.unscaledDeltaTime * fadeSpeed;
            yield return null;
        }

        cg.alpha = 1;
    }

    IEnumerator FadeOut(CanvasGroup cg)
    {
        while (cg.alpha > 0)
        {
            cg.alpha -= Time.unscaledDeltaTime * fadeSpeed;
            yield return null;
        }

        cg.alpha = 0;
    }

    public void DisableAll()
    {
        fidePanel.SetActive(false);

        for (int i = 0; i < text.Length; i++)
        {
            text[i].gameObject.SetActive(false);
        }
    }

    public void EnableAll()
    {
        fidePanel.SetActive(true);

        for (int i = 0; i < text.Length; i++)
        {
            text[i].gameObject.SetActive(true);
        }
    }
}