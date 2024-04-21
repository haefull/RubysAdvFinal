using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    
    public int maxHealth = 5;
    public int score = 0; 
    
    public GameObject projectilePrefab;
    public GameObject healthPickUpEffect;
    public GameObject takeDamageEffect;
    public GameObject scoreText;
    
    public AudioClip throwSound;
    public AudioClip hitSound;
    
    public int health { get { return currentHealth; }}
    int currentHealth;
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    
    AudioSource audioSource;

    bool gameover = false;
    bool gamelost = false;

    bool game_start = false;
    float game_time = 0.0f;
    float max_game_time = 60.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game_start && !gameover)
        {
            game_time += Time.deltaTime;
            UITimer.instance.UpdateTimer(game_time);

            if (game_time >= max_game_time)
            {
                gameover = true;
                gamelost = true;
                UIGameOver.instance.LoseGame();
            }
        }

        if (gameover)
        {
            horizontal = 0;
            vertical = 0;
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (gamelost)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.C) && game_start)
        {
            Launch();
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                    game_start = true;
                }
            }
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            
            PlaySound(hitSound);
            PlayEffects(takeDamageEffect);
        }
        else
        {
            PlayEffects(healthPickUpEffect);
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (currentHealth <= 0)
        {
            gameover = true;
            gamelost = true;
            UIGameOver.instance.LoseGame();
        }
        
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    // This function is to increment the player score
     public void ChangeScore(int amount)
    {
        score += amount;
        
        // UIHealthBar.instance.SetValue(score);
        UIPlayerScore.instance.SetValue(score);

        if (score == 2)
        {
            gameover = true;
            UIGameOver.instance.WinGame();
        }

    }


    // This function will instantiate and play an effect 
    void PlayEffects(GameObject fx)
    {
        GameObject fxObject = Instantiate(fx, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

    }
    
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        
        PlaySound(throwSound);
    } 
    
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
