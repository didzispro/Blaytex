using System.Collections;
using TMPro;
using UnityEngine;

public class TextControl1 : MonoBehaviour
{
    [Space(5)]
    [Header("Texts")]
    [Space(5)]
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private TextMeshProUGUI roundText;

    [Header("Audio")]
    [Space(5)]
    [SerializeField] private AudioClip roundSound;
    [SerializeField] private AudioClip Sound1;
    [SerializeField] private AudioClip Sound2;
    [SerializeField] private AudioClip Sound3;
    [SerializeField] private AudioClip goSound;
    [SerializeField] private AudioClip koSound;
    [Space(10)]
    public AudioSource musicFigthing;
    [Space(10)]
   
    private AudioSource audioSource;

    private PlayerController playerController;
    private GameManager gameManager;
    private PauseManager pauseManager;
    private Enemy1 enemy1;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        musicFigthing.Stop();
        
        playerController = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        pauseManager = FindObjectOfType<PauseManager>();
        enemy1 = FindObjectOfType<Enemy1>();

        roundText.gameObject.SetActive(false);

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].gameObject.SetActive(false); 
        }

        Time.timeScale = 0.0f;
        StartCoroutine(FlikerTimer());

    }

    public void PlayRoundIntro(int round)
    {
        StartCoroutine(FlikerTimer1(round));
    }

    public void KoTimers()
    {
        StartCoroutine(KoTimer());
    }

    public void Enemy1Timers()
    {
        StartCoroutine(Enemy1Timer());
    }

    public void Enemy2Timers()
    {
        StartCoroutine(Enemy2Timer());
    }

    IEnumerator FlikerTimer()
    {
        musicFigthing.Stop();

        for (int i = 0; i < 5; i++)
        {
            texts[0].gameObject.SetActive(true);

            if (i == 0)
            {
                audioSource.PlayOneShot(roundSound, 1.0f);
            }
            
            yield return new WaitForSecondsRealtime(0.3f);

            texts[0].gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(0.3f);
        }

        texts[1].gameObject.SetActive(true);
        audioSource.PlayOneShot(Sound1, 1.0f);
        yield return new WaitForSecondsRealtime(0.5f);
        texts[1].gameObject.SetActive(false);

        texts[2].gameObject.SetActive(true);
        audioSource.PlayOneShot(Sound2, 1.0f);
        yield return new WaitForSecondsRealtime(0.5f);
        texts[2].gameObject.SetActive(false);    

        texts[3].gameObject.SetActive(true);
        audioSource.PlayOneShot(Sound3, 1.0f);
        yield return new WaitForSecondsRealtime(0.5f);
        texts[3].gameObject.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            texts[4].gameObject.SetActive(true);

            if (i == 0)
            {
                audioSource.PlayOneShot(goSound, 1.0f);
            }
            
            yield return new WaitForSecondsRealtime(0.2f);

            texts[4].gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(0.2f);
        }

        Time.timeScale = 1.0f;

        if (playerController != null)
        {
            playerController.isJumping = true;
        }

        
        gameManager.canAttack = true;

        pauseManager.ifPausedMenu = true;

        musicFigthing.Play();
    }

    IEnumerator FlikerTimer1(int round)
    {
        musicFigthing.Stop();

        yield return new WaitForSecondsRealtime(2.0f);

        
        for (int i = 0; i < 5; i++)
        {
            roundText.gameObject.SetActive(true);

            if (i == 0)
            {
                audioSource.PlayOneShot(roundSound, 1.0f);
            }
            
            roundText.text = enemy1.ConvertToSpriteText("ROUND " + round);
            yield return new WaitForSecondsRealtime(0.3f);

            roundText.gameObject.SetActive(false);
            roundText.text = enemy1.ConvertToSpriteText("ROUND " + round);
            yield return new WaitForSecondsRealtime(0.3f);
        }

        texts[6].gameObject.SetActive(true);
        audioSource.PlayOneShot(Sound1, 1.0f);
        yield return new WaitForSecondsRealtime(0.5f);
        texts[6].gameObject.SetActive(false);

        texts[7].gameObject.SetActive(true);
        audioSource.PlayOneShot(Sound2, 1.0f);
        yield return new WaitForSecondsRealtime(0.5f);
        texts[7].gameObject.SetActive(false);

        texts[8].gameObject.SetActive(true);
        audioSource.PlayOneShot(Sound3, 1.0f);
        yield return new WaitForSecondsRealtime(0.5f);
        texts[8].gameObject.SetActive(false);
        

        for (int i = 0; i < 5; i++)
        {
            texts[9].gameObject.SetActive(true);

            if (i == 0)
            {
                audioSource.PlayOneShot(goSound, 1.0f);
            }
            
            yield return new WaitForSecondsRealtime(0.2f);

            texts[9].gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(0.2f);
            
        }

        Time.timeScale = 1.0f;

        if (playerController != null)
        {
            playerController.isJumping = true;
        }

        
        gameManager.canAttack = true;
        pauseManager.ifPausedMenu = true;

        musicFigthing.Play();
    }

    IEnumerator KoTimer()
    {
        musicFigthing.Stop();

        for (int i = 0; i < 5; i++)
        {
            texts[10].gameObject.SetActive(true);

            if (i == 0)
            {
                audioSource.PlayOneShot(koSound, 1.0f);
            }
            
            yield return new WaitForSecondsRealtime(0.3f);

            texts[10].gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }

    IEnumerator Enemy1Timer()
    {
        musicFigthing.Stop();

        for (int i = 0; i < 5; i++)
        {
            texts[11].gameObject.SetActive(true);

            if (i == 0)
            {
                audioSource.PlayOneShot(koSound, 1.0f);
            }
            
            yield return new WaitForSecondsRealtime(0.3f);

            texts[11].gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }

    IEnumerator Enemy2Timer()
    {
        musicFigthing.Stop();

        for (int i = 0; i < 5; i++)
        {
            texts[12].gameObject.SetActive(true);

            if (i == 0)
            {
                audioSource.PlayOneShot(koSound, 1.0f);
            }
            
            yield return new WaitForSecondsRealtime(0.3f);

            texts[12].gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }
    
}
