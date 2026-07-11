using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public Enemy enemy;
    public Enemy1 enemy1;
    public Enemy2 enemy2;

    public Transform playerSpawn;
    public Transform enemySpawn;

    private TextControl textControl;
    private TextControl1 textControl1;
    public GameObject[] settingsCanvas;
    public bool canAttack = false;

    bool roundEnded = false;

    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject lostCanvas;

    [Header("Audio")]

    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip uiSound;
    public AudioSource calmMusic;

    [Header("Panels")]

    [SerializeField] private GameObject playerTagPanel;
    
    [SerializeField] private GameObject multiplayerPanel;
    [SerializeField] private GameObject multiplayerPanel1;
    [SerializeField] private GameObject pausemenuPanel;
    [SerializeField] private GameObject betPanel;
    [SerializeField] private GameObject playerTagPanel1;

    



    [Header("GameObjects")]
    [SerializeField] private GameObject mainmenu;

    [SerializeField] private GameObject cubes;
    [SerializeField] private GameObject cubes1;
    [SerializeField] private GameObject gameObject1;
    [SerializeField] private GameObject gameObject2;
   

    public bool canStart = false;

    private AudioSource audioSource;

    private PlayerController playerController;
    private Fide fide;
    private Enemy enemyAi;
    [SerializeField] private AudioSettings audioSettings;

    int round = 1;
    int playerWins = 0;
    int enemyWins = 0;

    bool gameEnded = false;
    private PauseManager pauseManager;

    void Awake()
    {
       audioSource = GetComponent<AudioSource>();

        CheckNull();
        
    }

    void Start()
    {
        
        textControl = FindObjectOfType<TextControl>();
        textControl1 = FindObjectOfType<TextControl1>();
        playerController = FindObjectOfType<PlayerController>();
        enemyAi = FindObjectOfType<Enemy>();
        pauseManager = FindObjectOfType<PauseManager>();
        
        if (SceneManager.GetActiveScene().name == "MainGame2")
        {
            canStart = true;
        }
        if (SceneManager.GetActiveScene().name == "MainGame3")
        {
            canStart = true;
        }

        enemy1 = FindObjectOfType<Enemy1>();
        enemy1 = FindObjectOfType<Enemy1>();
        fide = FindObjectOfType<Fide>();

        if (fide != null)
        {
            fide.DisableAll();
        }
    }

    void CheckNull()
    {
        if (calmMusic != null)
        {
            calmMusic.Stop();
        }

        if (playerTagPanel1 != null)
        {
            playerTagPanel1.SetActive(false);
        }

        if (betPanel != null)
        {
            betPanel.SetActive(false);
        }

        if (cubes1!= null)
        {
            cubes1.SetActive(false);
        }

        if (cubes != null)
        {
            cubes.SetActive(false);
        }

        if (gameObject2 != null)
        {
            gameObject2.SetActive(false);
        }

        if (gameObject1 != null)
        {
            gameObject1.SetActive(false);
        }

        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }

        if (lostCanvas != null)
        {
            lostCanvas.SetActive(false);
        }

        for (int i = 0; i < settingsCanvas.Length; i++)
        {
            settingsCanvas[i].SetActive(false);
        }

        if (playerTagPanel != null)
        {
            playerTagPanel.SetActive(false);
        }

        if (multiplayerPanel != null)
        {
            multiplayerPanel.SetActive(false);
        }

        if (multiplayerPanel1 != null)
        {
            multiplayerPanel1.SetActive(false);
        }

        if (mainmenu != null)
        {
            mainmenu.SetActive(true);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void Settings()
    {
        settingsCanvas[0].SetActive(true);
        mainmenu.SetActive(false);

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void Settings1()
    {
        settingsCanvas[0].SetActive(true);
        pausemenuPanel.SetActive(false);
        pauseManager.canPressEsc = true;

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void Backbutton()
    {
        settingsCanvas[0].SetActive(false);
        mainmenu.SetActive(true);

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void Backbutton1()
    {
        settingsCanvas[1].SetActive(false);
        settingsCanvas[0].SetActive(true);

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void Backbutton2()
    {
        settingsCanvas[2].SetActive(false);
        settingsCanvas[0].SetActive(true);

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void Backbutton3()
    {
        settingsCanvas[3].SetActive(false);
        settingsCanvas[0].SetActive(true);

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void Backbutton4()
    {
        settingsCanvas[4].SetActive(false);
        settingsCanvas[0].SetActive(true);

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void HowToPlayScene()
    {
        settingsCanvas[3].SetActive(true);
        settingsCanvas[0].SetActive(false);

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void ControlsScene()
    {
        settingsCanvas[2].SetActive(true);
        settingsCanvas[0].SetActive(false);

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void PlayerTag()
    {
        gameObject2.SetActive(true);
        playerTagPanel.SetActive(true);
        mainmenu.SetActive(false);
        multiplayerPanel.SetActive(false);
        cubes.SetActive(false);

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void PlayerTag1()
    {
        gameObject2.SetActive(false);
        cubes1.SetActive(false);
        cubes.SetActive(false);
        playerTagPanel1.SetActive(false);
        mainmenu.SetActive(false);
        multiplayerPanel1.SetActive(false);
        gameObject1.SetActive(true);
        betPanel.SetActive(true);

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void Multiplayer()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        gameObject2.SetActive(false);
        multiplayerPanel.SetActive(true);
        cubes.SetActive(true);
        playerTagPanel.SetActive(false);
        gameObject1.SetActive(false);
        betPanel.SetActive(false);
    }

    public void Multiplayer1()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        gameObject1.SetActive(false);
        multiplayerPanel1.SetActive(true);
        cubes1.SetActive(true);
        playerTagPanel1.SetActive(false);
        gameObject2.SetActive(false);
        betPanel.SetActive(false);
    }

    public void LocalMultiplayerScene()
    {
        settingsCanvas[4].SetActive(true);
        settingsCanvas[0].SetActive(false);

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void MainScene1()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        SceneManager.LoadScene("MainGame 1");
    }

    public void MainScene2()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        SceneManager.LoadScene("MainGame 2");
    }

    public void MainScene3()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        SceneManager.LoadScene("MainGame 3");
    }

    public void MainSceneEnemy()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        SceneManager.LoadScene("MainGameEnemy");
    }

    public void MainSceneEnemy2()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        SceneManager.LoadScene("MainGameEnemy2");
    }

    public void MainSceneEnemy3()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        SceneManager.LoadScene("MainGameEnemy3");
    }

    public void ContinueButton()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        betPanel.SetActive(true);
        gameObject2.SetActive(true);
        playerTagPanel.SetActive(false);
        gameObject1.SetActive(false);
    }

    public void Restart()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        SceneManager.LoadScene("MainMenu");
    }

    public void BackButton5()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        playerTagPanel.SetActive(false);
        gameObject2.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void BackButton6()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        settingsCanvas[0].SetActive(false);
        pauseManager.canPressEsc = false;
        pausemenuPanel.SetActive(true);
    }

    public void BackButton7()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        playerTagPanel.SetActive(true);
        betPanel.SetActive(false);
        gameObject1.SetActive(false);
        gameObject2.SetActive(true);
    }


    public void Continue()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        SceneManager.LoadScene("MainGame2");
    }

    public void Continue2()
    {
        audioSource.PlayOneShot(uiSound, 1.0f);

        SceneManager.LoadScene("MainGame3");
    }

    public void VolumeScene()
    {
        settingsCanvas[1].SetActive(true);
        settingsCanvas[0].SetActive(false);

        audioSource.PlayOneShot(uiSound, 1.0f);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitRoutine());
    }

    IEnumerator QuitRoutine()
    {
        audioSource.PlayOneShot(uiSound);

        yield return new WaitForSecondsRealtime(0.2f);

        Application.Quit();
    }

    public void StartNextRound()
    {
        StartCoroutine(RoundRoutine());
    }

    IEnumerator RoundRoutine()
    {
        yield return StartCoroutine(CoolDown());

        round++;

        if (textControl != null)
        {
            textControl.PlayRoundIntro(round);
        }

        if (textControl1 != null)
        {
            textControl1.PlayRoundIntro(round);
        }

        

        yield return new WaitForSecondsRealtime(2f);

        ResetCharacters();
    }

    void ResetCharacters()
    {
        if (playerController != null)
        {
            playerController.isJumping = false;
        }
        roundEnded = false;
        canAttack = false;

        if (player != null && playerSpawn != null)
        {
            player.transform.position = playerSpawn.position;
            player.ResetPlayer();
        }

        if (enemy != null && enemySpawn != null)
        {
            enemy.transform.position = enemySpawn.position;
            enemy.ResetEnemy();
        }

        if (enemy1 != null && playerSpawn != null)
        {
            enemy1.transform.position = playerSpawn.position;
            enemy1.ResetEnemy();
        }

        if (enemy2 != null && enemySpawn != null)
        {
            enemy2.transform.position = enemySpawn.position;
            enemy2.ResetEnemy();
        }
    }

    public void OnRoundEnd(RoundResult result)
    {
        if (gameEnded || roundEnded) return;

        roundEnded = true;

        if (result == RoundResult.Player)
        {
            playerWins++;
            textControl.PlayerTimers();
            
        }
        else if (result == RoundResult.Enemy)
        {
            enemyWins++;
            textControl.EnemyTimers();
        }
        else
        {
            Debug.Log("Draw round!");
        }

        if (result == RoundResult.Enemy1)
        {
            playerWins++;
            textControl1.Enemy1Timers();
        }
        else if (result == RoundResult.Enemy2)
        {
            enemyWins++;
            textControl1.Enemy2Timers();
        }
        else
        {
            Debug.Log("Draw round!");
        }

        if (playerWins >= 2)
        {
            GameOver();
            return;
        }

        if (enemyWins >= 2)
        {
            GameOver1();
            return;
        }

        StartNextRound();
    }

    void GameOver()
    {
        StartCoroutine(CoolDown1());
    }

    void GameOver1()
    {
        StartCoroutine(CoolDown2());
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0.0f;
    }

    IEnumerator CoolDown1()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 0.0f;

        if (fide != null)
        {
            fide.EnableAll();
        }

        if (fide != null)
        {
            fide.PlayEnding();
        }

        if (winCanvas != null)
        {
            audioSource.PlayOneShot(winSound, 1.0f);
            winCanvas.SetActive(true);
        }
        
        
        gameEnded = true;
    }

    IEnumerator CoolDown2()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 0.0f;

        lostCanvas.SetActive(true);
        gameEnded = true;

        audioSource.PlayOneShot(loseSound, 1.0f);
    }

    public enum RoundResult
    {
        Player, Enemy, Enemy1, Enemy2
    }
}
