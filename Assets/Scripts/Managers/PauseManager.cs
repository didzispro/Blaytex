using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;

    public bool ifPausedMenu = false;
    public bool canPressEsc = false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip uiSound;

    public bool isPaused = false;
    [SerializeField] private AudioSettings audioSettings;
    private TextControl textControl;
    private TextControl1 textControl1;
    private TextControl2 textControl2;
    public AudioSource musicPausemenu;
    

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {   musicPausemenu.Stop();
        textControl = FindObjectOfType<TextControl>();
        textControl1 = FindObjectOfType<TextControl1>();
        textControl2 = FindObjectOfType<TextControl2>();
        pauseCanvas.SetActive(false);
    }

    void Update()
    {
        if (canPressEsc) return;
         
        if (Input.GetKeyDown(KeyCode.Escape) && ifPausedMenu)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
       
    }

    // Call this to pause the game
    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseCanvas.SetActive(true);
        audioSource.PlayOneShot(uiSound, 1.0f);
        musicPausemenu.Play();

        PauseGameTextControl();
    }

    void PauseGameTextControl()
    {
        if (textControl != null)
        {
            textControl.musicFigthing.Stop();
        }

        if (textControl1 != null)
        {
            textControl1.musicFigthing.Stop();
        }

        if (textControl2 != null)
        {
            textControl2.musicFigthing.Stop();
        }
    }

    // Call this to unpause the game
    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseCanvas.SetActive(false);
        audioSource.PlayOneShot(uiSound, 1.0f);

        ResumeTextControl();
        
        musicPausemenu.Stop();
    }

    void ResumeTextControl()
    {
        if (textControl != null)
        {
            textControl.musicFigthing.Play();
        }

        if (textControl1 != null)
        {
            textControl1.musicFigthing.Play();
        }

        if (textControl2 != null)
        {
            textControl2.musicFigthing.Play();
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        audioSource.PlayOneShot(uiSound, 1.0f);
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        audioSource.PlayOneShot(uiSound, 1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void QuitGame()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);
        Application.Quit();
        Debug.Log("Game Quit"); 
    }
}
