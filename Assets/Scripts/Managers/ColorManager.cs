using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ColorManager : MonoBehaviour
{

    [SerializeField] private AudioClip uiSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SelectRed()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        PlayerPrefs.SetString("Player1Color", "Red");
        PlayerPrefs.Save();
    }

    public void Select2Red()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        PlayerPrefs.SetString("Player2Color", "Red");
        PlayerPrefs.Save();
    }

    public void SelectWhite()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        PlayerPrefs.SetString("Player1Color", "White");
        PlayerPrefs.Save();
    }

    public void Select2White()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        PlayerPrefs.SetString("Player2Color", "White");
        PlayerPrefs.Save();
    }

    public void SelectOrange()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        PlayerPrefs.SetString("Player1Color", "Orange");
        PlayerPrefs.Save();
    }

    public void Select2Orange()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        PlayerPrefs.SetString("Player2Color", "Orange");
        PlayerPrefs.Save();
    }

    public void SelectBlue()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        PlayerPrefs.SetString("Player1Color", "Blue");
        PlayerPrefs.Save();
    }

    public void Select2Blue()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        PlayerPrefs.SetString("Player2Color", "Blue");
        PlayerPrefs.Save();
    }

    public void SelectGreen()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        PlayerPrefs.SetString("Player1Color", "Green");
        PlayerPrefs.Save();
    }

    public void Select2Green()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        PlayerPrefs.SetString("Player2Color", "Green");
        PlayerPrefs.Save();
    }

    public void SelectBlack()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        PlayerPrefs.SetString("Player1Color", "Black");
        PlayerPrefs.Save();
    }

    public void Select2Black()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        PlayerPrefs.SetString("Player2Color", "Black");
        PlayerPrefs.Save();
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Start()
    {
        PlayerPrefs.DeleteKey("Player1Color");
        PlayerPrefs.DeleteKey("Player2Color");
    }

    

}
