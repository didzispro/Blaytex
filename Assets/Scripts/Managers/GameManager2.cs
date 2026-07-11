using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public PlayerController2 player1;
    public Multiplayer player2;

    public Transform playerSpawn;
    public Transform enemySpawn;

    private TextControl2 textControl2;
    public SpriteRenderer player1Sprite;
    public SpriteRenderer player2Sprite;
    public bool canAttack = false;

    bool roundEnded = false;

    [SerializeField] private GameObject player1WinCanvas;
    [SerializeField] private GameObject player2WinCanvas;

    [SerializeField] private AudioClip winSound;

    [SerializeField] private TMP_Text topNameDisplay;
    [SerializeField] private TMP_Text topNameDisplay2;

    public bool canStart = false;

    private AudioSource audioSource;
    private PlayerController2 playerController2;
    private Multiplayer multiplayer;
    [SerializeField] private AudioSettings audioSettings;
    [SerializeField] private AudioClip uiSound;

    int round = 1;
    int playerWins = 0;
    int enemyWins = 0;

    bool gameEnded = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        player1WinCanvas.SetActive(false);
        player2WinCanvas.SetActive(false);
    }

    void Start()
    {
        TopNameDisplay();
        PlayersColor();

        textControl2 = FindObjectOfType<TextControl2>();
        playerController2 = FindObjectOfType<PlayerController2>();
        multiplayer = FindObjectOfType<Multiplayer>();

        if (SceneManager.GetActiveScene().name == "MainGame 1")
        {
            canStart = true;
        }

        if (SceneManager.GetActiveScene().name == "MainGame 2")
        {
            canStart = true;
        }

        if (SceneManager.GetActiveScene().name == "MainGame 3")
        {
            canStart = true;
        }
    }

    void TopNameDisplay()
    {
        if (topNameDisplay != null)
        {
            topNameDisplay.text = PlayerPrefs.GetString("Player1Name", "Player1");
        }
        if (topNameDisplay2 != null)
        {
            topNameDisplay2.text = PlayerPrefs.GetString("Player2Name", "Player2");
        }
    }

    void PlayersColor()
    {
        string player1Color = PlayerPrefs.GetString("Player1Color", "None");
        string player2Color = PlayerPrefs.GetString("Player2Color", "None");

        player1Sprite.color = Color.white;
        player2Sprite.color = Color.white;

        if (player1Color == "Red")
        {
            player1Sprite.color = Color.red;
        }

        if (player2Color == "Red")
        {
            player2Sprite.color = Color.red;
        }

        if (player1Color == "White")
        {
            player1Sprite.color = Color.white;
        }

        if (player2Color == "White")
        {
            player2Sprite.color = Color.white;
        }

        if (player1Color == "Orange")
        {
            player1Sprite.color = new Color(1f, 0.5f, 0f); // RGB for orange!
        }

        if (player2Color == "Orange")
        {
            player2Sprite.color = new Color(1f, 0.5f, 0f); // RGB for orange!
        }

        if (player1Color == "Blue")
        {
            player1Sprite.color = Color.blue;
        }

        if (player2Color == "Blue")
        {
            player2Sprite.color = Color.blue;
        }

        if (player1Color == "Green")
        {
            player1Sprite.color = Color.green;
        }

        if (player2Color == "Green")
        {
            player2Sprite.color = Color.green;
        }

        if (player1Color == "Black")
        {
            player1Sprite.color = Color.black;
        }

        if (player2Color == "Black")
        {
            player2Sprite.color = Color.black;
        }

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

        textControl2.PlayRoundIntro(round);

        yield return new WaitForSecondsRealtime(2f);

        ResetCharacters();
    }

    void ResetCharacters()
    {
        roundEnded = false;
        playerController2.isJumping2 = false;
        canAttack = false;
        
        player1.transform.position = playerSpawn.position;
        player2.transform.position = enemySpawn.position;

        player1.ResetPlayer();
        player2.ResetPlayer();
    }

    public void OnRoundEnd(RoundResult result)
    {
        if (gameEnded || roundEnded) return;

        roundEnded = true;

        if (result == RoundResult.Player)
        {
            playerWins++;
            textControl2.Player2Timers();
        }
        else if (result == RoundResult.Player2)
        {
            enemyWins++;
            textControl2.Player1Timers();
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

        player1WinCanvas.SetActive(true);
        gameEnded = true;

        audioSource.PlayOneShot(winSound, 1.0f);
    }

    IEnumerator CoolDown2()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 0.0f;

        player2WinCanvas.SetActive(true);
        gameEnded = true;

        audioSource.PlayOneShot(winSound, 1.0f);
    }

    public enum RoundResult
    {
        Player, Player2
    }
}
