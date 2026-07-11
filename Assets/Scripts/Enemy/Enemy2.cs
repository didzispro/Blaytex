using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    [Space(5)]
    [Header("Assign in Inspector")]
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform attackPoint;

    bool isDead = false;
    private bool isrolling = false;

    private GameManager gameManager;

    [Header("Health")]
    [SerializeField] private int enemyHealth = 100;
    private int currentHealth;

    [Header("Combat")]
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float knockBack = 5.0f;
    [Space(10)]
    [SerializeField] private float deathSeconds;
    [Space(10)]
    [SerializeField] private float moveSpeed;
    [Space(5)]
    [SerializeField] private float jumpForce = 1.0f;

    [SerializeField] private TextMeshProUGUI text;

    [Header("Audio")]
    [SerializeField] private AudioClip punchSound;
    [SerializeField] private AudioClip jumpSound;

    private AudioSource audioSource;

    private float distance;

    public bool canAttack = true;
    public bool isAttacking = false;
    public bool canAttack1 = true;
    public bool canJump = true;
    private bool isGround = false;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 0.7f;
    [SerializeField] private float attackRange = 0.5f;

    private float jumpTimer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color originalColor;
    private Animator animator;
    private PauseManager pauseManager;
    private TextControl1 textControl1;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyHealth;
        originalColor = sr.color;

        UpdateHealthUI();

        jumpTimer = Random.Range(1.5f, 3f);

        gameManager = FindObjectOfType<GameManager>();
        pauseManager = FindObjectOfType<PauseManager>();
        textControl1 = FindObjectOfType<TextControl1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        FollowMovement();
        AttackStuff();
        Rolling();
        FaceRotation();
        JumpChance();
    }

    void FollowMovement()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        bool IsRunning = Mathf.Abs(rb.velocity.x) > 0.1f;
        animator.SetBool("IsRunning", IsRunning);

        if (distance > 2.5f && !isAttacking)
        {
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); 
        }
    }

    void AttackStuff()
    {
        if (distance < 2.5f && canAttack)
        {
            float attackVertical = 40f;
            float attackHorizontal = 60f;
            

            float roll = Random.Range(0, 100f);

            if (distance < 2.5f && roll < attackVertical)
            {
                Debug.Log("VERTICAL BRANCH");
                StartCoroutine(AttackWindup());
            }
            else if (distance < 2.5f && roll < attackHorizontal)
            {
                Debug.Log("Horizontal BRANCH");
                StartCoroutine(AttackWindupHorizontal());
            }
        }
    }

    void Rolling()
    {
        if (distance > 1.5f && !canAttack && !isrolling) 
        {
            float rollLeft = 40f;

            float roll = Random.Range(0, 100f);

            if (distance > 1.5f && roll < rollLeft)
            {
                RollLeft();
            }

            StartCoroutine(TimeRoll());
        }

        if (distance < 1.5f && canAttack && !isrolling)
        {
            float rollRight = 45f;

            float roll = Random.Range(0, 100f);

            if (distance < 1.5f && roll < rollRight)
            {
                RollRight();
            }

            StartCoroutine(TimeRoll());
        }
    }

    void FaceRotation()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void JumpChance()
    {
        jumpTimer -= Time.deltaTime;

        if (jumpTimer <= 0f && canJump && isGround)
        {
            float backChance = 45f;
            float forwardChance = 40f;


            float roll = Random.Range(0f, 100f);

            if (distance < 3f)
            {
                if (roll < backChance)
                {
                    BackWardsJump();
                }
            }
            else if (distance < 3f)
            {
                if (roll < forwardChance)
                {
                    ForwardsJump();
                }
            }

            StartCoroutine(JumpCoolDown());
            jumpTimer = Random.Range(1.5f, 3f);
        }   
    }

    void RollLeft()
    {
        animator.SetTrigger("Roll");

        float dir = Mathf.Sign(transform.localScale.x);
        rb.velocity = new Vector2(dir * 7.0f, rb.velocity.y);
    }

    void RollRight()
    {
        animator.SetTrigger("Roll");

        float dir = -Mathf.Sign(transform.localScale.x);
        rb.velocity = new Vector2(dir * -7.0f, rb.velocity.y);
    }

    IEnumerator TimeRoll()
    {
        isrolling = true;
        yield return new WaitForSeconds(1.0f);
        isrolling = false;
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
        if (isDead) return;

        textControl1.KoTimers();

        isDead = true;

        // Die Animation.
        animator.SetTrigger("IsDead");

        StopAllCoroutines();
        
        rb.bodyType = RigidbodyType2D.Kinematic;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // Disable the enemy.
        GetComponent<Collider2D>().enabled = false;
        pauseManager.ifPausedMenu = false;
        gameManager.OnRoundEnd(GameManager.RoundResult.Enemy1);
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

    IEnumerator AttackWindup()
    {
        canAttack1 = true;

        isAttacking = true;
        canAttack = false;

        animator.SetTrigger("Attack");

        if (punchSound != null)
        {
            audioSource.PlayOneShot(punchSound, 1.0f);
        }

        yield return new WaitForSeconds(0.2f);

        Collider2D[] hitplayer = Physics2D.OverlapCircleAll
        (
            attackPoint.position,
            attackRange,
            playerLayer
        );

        foreach (Collider2D player in hitplayer)
        {
            player.GetComponent<Enemy1>().TakeDamage(attackDamage);

            float dir = transform.localScale.x;

            player.GetComponent<Enemy1>().Knockback(dir);
        }

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        canAttack = true;
    }


    IEnumerator AttackWindupHorizontal()
    {
        canAttack1 = true;

        isAttacking = true;
        canAttack = false;

        animator.SetTrigger("AttackHorizontal");

        if (punchSound != null)
        {   
            audioSource.PlayOneShot(punchSound, 1.0f);
        }

        yield return new WaitForSeconds(0.2f);

        Collider2D[] hitplayer = Physics2D.OverlapCircleAll
        (
            attackPoint.position,
            attackRange,
            playerLayer
        );

        foreach (Collider2D player in hitplayer)
        {
            player.GetComponent<Enemy1>().TakeDamage(attackDamage);

            float dir = transform.localScale.x;

            player.GetComponent<Enemy1>().Knockback(dir);
        }

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        canAttack = true;
    }

    public void StopAttackWindup()
    {
        StopCoroutine(AttackWindup());
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);  
    }

    public void Knockback(float dir)
    {
        rb.velocity = new Vector2(dir * knockBack, rb.velocity.y);
    }

    void BackWardsJump()
    {
        Vector2 jumpDir = new Vector2(-transform.localScale.x, 1f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound, 1.0f);
        }
        
        rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
    }

    void ForwardsJump()
    {
        Vector2 jumpDir = new Vector2(transform.localScale.x, 1f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound, 1.0f);
        }
        
        rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
    }

    IEnumerator JumpCoolDown()
    {
        canJump = false;
        yield return new WaitForSeconds(2.0f);
        canJump = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
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

        foreach (char c in input)
        {
            int index = chars.IndexOf(c);

            if (index >= 0)
            {
                result += $"<sprite index={index}> ";
            }
        }

        return result;
    }
    public void ResetEnemy()
    {

        isDead = false;

        currentHealth = enemyHealth;

        UpdateHealthUI();

        StopAllCoroutines();

        canAttack = true;
        isAttacking = false;
        canJump = true;
        isGround = false;

        jumpTimer = Random.Range(1.5f, 3f);

        // reset position should be handled by GameManager too

        gameObject.SetActive(true);

        // physics reset
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.constraints = RigidbodyConstraints2D.None;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.simulated = true;

        // reset animator (IMPORTANT)
        animator.Rebind();
        animator.Update(0f);
        animator.ResetTrigger("IsDead");

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        this.enabled = true;

        // re-enable script + collider
        GetComponent<Collider2D>().enabled = true;
        
    }
}
