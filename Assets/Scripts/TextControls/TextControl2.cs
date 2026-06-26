using System.Collections;
using TMPro;
using UnityEngine;

public class TextControl2 : MonoBehaviour
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
    private PlayerController2 playerController2;
    private GameManager2 gameManager2;
    private PauseManager pauseManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        musicFigthing.Stop();

        playerController2 = FindObjectOfType<PlayerController2>();
        gameManager2 = FindObjectOfType<GameManager2>();
        pauseManager = FindObjectOfType<PauseManager>();

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

    public void Player1Timers()
    {
        StartCoroutine(Player1Timer());
    }

    public void Player2Timers()
    {
        StartCoroutine(Player2Timer());
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

        playerController2.isJumping2 = true;
        gameManager2.canAttack = true;

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
            
            roundText.text = playerController2.ConvertToSpriteText("ROUND " + round);
            yield return new WaitForSecondsRealtime(0.3f);

            roundText.gameObject.SetActive(false);
            roundText.text = playerController2.ConvertToSpriteText("ROUND " + round);
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

        playerController2.isJumping2 = true;
        gameManager2.canAttack = true;

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

    IEnumerator Player1Timer()
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

    IEnumerator Player2Timer()
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
