using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Multiplayer : MonoBehaviour
{
    [Space(5)]
    [Header("Assign in Inspector")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject enemy;
    private PlayerController2 playerController2;
    private Enemy enemyAI;

    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("Combat")]
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private int attackDamage = 10;
    [Space(10)]
    [SerializeField] private float jumpForce = 5.0f;

    [SerializeField] private TextMeshProUGUI text;

    [Header("Audio")]
    [SerializeField] private AudioClip punchSound;
    [SerializeField] private AudioClip jumpSound;

    private AudioSource audioSource;

    public float knockBack = 5.0f;

    private bool isKnocked;
    public bool isAttack;
   
    public bool isJumping;
    private bool isRolling;
    private bool isRolling1 = true;

    private float moveHorizontal;
    private GameManager2 gameManager2;

    private Animator animator;
    private SpriteRenderer sr;
    private Color originalColor;
    private PauseManager pauseManager;
    private TextControl2 textControl2;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Start()
    {
        currentHealth = maxHealth;
        originalColor = sr.color;

        gameManager2 = FindObjectOfType<GameManager2>();
        enemyAI = FindObjectOfType<Enemy>();
        playerController2 = FindObjectOfType<PlayerController2>();
        pauseManager = FindObjectOfType<PauseManager>();
        textControl2 = FindObjectOfType<TextControl2>();

        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        AttackStuff();
        Rolling();
        Jump();
        FaceRotation();
    }

    void Movement()
    {
        moveHorizontal = 0.0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -1.0f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = 1.0f;
        }
    }

    void AttackStuff()
    {
        if (Input.GetKeyDown(KeyCode.L) && gameManager2.canAttack)
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.K) && gameManager2.canAttack)
        {
            
            AttackHorizontal();
        }
    }

    void Rolling()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.DownArrow) && gameManager2.canAttack && isRolling1 && gameManager2.canStart)
        {
            AnimationStartsLeft();
        }

        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.DownArrow) && gameManager2.canAttack && isRolling1 && gameManager2.canStart)
        {
            AnimationStartsRight();
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            audioSource.PlayOneShot(jumpSound, 2.0f);
            isJumping = false;
            
        }
    }

    void FaceRotation()
    {
        Vector3 scale = transform.localScale;

        if (moveHorizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else if (moveHorizontal < 0)
        {
            scale.x = -Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    void AnimationStartsLeft()
    {
        isRolling = true;
        isRolling1 = false;
        animator.SetTrigger("Roll");

        float dir = -Mathf.Sign(transform.localScale.x);
        rb.velocity = new Vector2(dir * -7.0f, rb.velocity.y);

        StartCoroutine(RollTimer());
        StartCoroutine(RollTimer1());
    }

    void AnimationStartsRight()
    {
        isRolling = true;
        isRolling1 = false;
        animator.SetTrigger("Roll");

        float dir = Mathf.Sign(transform.localScale.x);

        rb.velocity = new Vector2(dir * 7.0f, rb.velocity.y);

        StartCoroutine(RollTimer());
        StartCoroutine(RollTimer1());
    }

    IEnumerator RollTimer()
    {
        yield return new WaitForSeconds(0.7f);
        isRolling = false;
    }

    IEnumerator RollTimer1()
    {
        yield return new WaitForSeconds(0.7f);
        isRolling1 = true;
    }

    void FixedUpdate()
    {
        if (isKnocked || isRolling) return;

        bool IsRunning = moveHorizontal != 0;

        animator.SetBool("IsRunning", IsRunning);

        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
    }

    void Attack()
    {
        if (isAttack) return;

        isAttack = true;

        // Play attack animation.
        animator.SetTrigger("Attack"); 

        if (punchSound != null)
        {
            audioSource.PlayOneShot(punchSound, 1.0f);
        }

        StartCoroutine(HitStop());

        // Detect player in range of attack.
        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Damage them.
        foreach (Collider2D player in hitplayer)
        {
            player.GetComponent<PlayerController2>().TakeDamage(attackDamage);

            float dir = transform.localScale.x;

            player.GetComponent<PlayerController2>().Knockback(dir);
        }
        
        StartCoroutine(CoolDown());
    }

    void AttackHorizontal()
    {
        if (isAttack) return;

        isAttack = true;

        // Play attack animation.
        animator.SetTrigger("AttackHorizontal"); 

        if (punchSound != null)
        {
            audioSource.PlayOneShot(punchSound, 1.0f);
        }   

        StartCoroutine(HitStop());

        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Damage them.
        foreach (Collider2D player in hitplayer)
        {
            player.GetComponent<PlayerController2>().TakeDamage(attackDamage);

            float dir = transform.localScale.x;

            player.GetComponent<PlayerController2>().Knockback(dir);
        }
        StartCoroutine(CoolDown1());
    }

    IEnumerator HitStop()
    {
        yield return new WaitForSeconds(0.2f);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);  
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        StartCoroutine(DamageEffect());

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameManager2 == null) return;
        if (this.enabled == false) return;

        textControl2.KoTimers();

        animator.SetTrigger("IsDead");

        pauseManager.ifPausedMenu = false;

        rb.velocity = Vector2.zero;

        gameManager2.OnRoundEnd(GameManager2.RoundResult.Player2);

        // Disable the player.
        this.enabled = false;
    }

    IEnumerator DamageEffect()
    {
        float t = 0.0f;

        Color originalColor = sr.color;
        Color flashColor;

        // If the sprite is already Red, flash to White. Otherwise, flash to Red.
        if (originalColor == Color.red)
        {
            flashColor = Color.white;
        }
        else
        {
            flashColor = Color.red;
        }
        
        while (t < 1.0f)
        {
            sr.color = Color.Lerp(originalColor, flashColor, t);
            t += Time.unscaledDeltaTime * 10.0f;
            yield return null;
        }

        t = 0.0f;

        while (t < 1.0f)
        {
            sr.color = Color.Lerp(flashColor, originalColor, t);
            t += Time.unscaledDeltaTime * 10.0f;
            yield return null;
        }

        sr.color = originalColor;
    }

    public void Knockback(float dir)
    {
        isKnocked = true;
        rb.velocity = new Vector2(dir * knockBack, rb.velocity.y);

        StartCoroutine(KnockbackTimer());
    }

    IEnumerator KnockbackTimer()
    {
        yield return new WaitForSeconds(0.4f);
        isKnocked = false;
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.7f);
        isAttack = false;
    }

    IEnumerator CoolDown1()
    {
        yield return new WaitForSeconds(0.7f);
        isAttack = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            isJumping = true;
        }
    }

    void UpdateHealthUI()
    {
        text.text = ConvertToSpriteText(currentHealth.ToString());
    }

    private string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public string ConvertToSpriteText(string input)
    {
        string result = "";

        foreach (char c in input.ToUpper())
        {
            if (c == ' ')
            {
                // Forces TextMeshPro to create a physical gap of 20 pixels
                result += "<space=20>"; 
                continue;
            }
            int index = chars.IndexOf(c);

            if (index >= 0)
            {
                result += $"<sprite index={index}><space=25>";
            }
        }

        return result;
    }

    public void ResetPlayer()
    {
        currentHealth = maxHealth;

        UpdateHealthUI();

        gameObject.SetActive(true);

        rb.velocity = Vector2.zero;

        animator.Rebind();
        animator.Update(0f);
        animator.ResetTrigger("IsDead");
        rb.angularVelocity = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        this.enabled = true;
    }
}
