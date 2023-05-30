using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class PlayerHealth : MonoBehaviour
{

    public int startingHealth = 3;  // the starting number of hearts
    public int currentHealth;       // the current number of hearts
    public SpriteRenderer[] hearts; // an array of SpriteRenderer components representing the hearts
    public Sprite fullHeart;        // the sprite used for a full heart
    public Sprite emptyHeart;       // the sprite used for an empty heart
    public bool isAlive = true;     // a boolean to track whether the player is alive or not

    public float invincibilityLength = 1.0f;
    public float invincibilityCounter;

    public Renderer playerRenderer;
    private float flashCount;
    public float flashLength = 0.1f;

    public AudioSource DeathAudio;
    public AudioSource HitAudio;

    void Start()
    {
        currentHealth = startingHealth;
    }

    void Update()
    {
        // loop through the hearts and update their sprite based on the current health
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                if(hearts != null)
                    hearts[i].sprite = fullHeart;
            }
            else
            {
                if(hearts != null)
                    hearts[i].sprite = emptyHeart;
            }
        }

        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
            flashCount -= Time.deltaTime;
            if(flashCount <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCount = flashLength;
            }
            if(invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if(invincibilityCounter <= 0)
        {
            currentHealth -= damageAmount;
            // clamp the health value to prevent it from going negative
            currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);
            // check if the player is out of health
            if (currentHealth <= 0)
            {
                isAlive = false;
                // handle player death here
                OnDeath?.Invoke(this, new OnDeathEventArgs(this));
                DeathAudio.Play();
                SceneManager.LoadScene("DeathMenu");
            }
            else
            {
                HitAudio.Play();
                invincibilityCounter = invincibilityLength;

                //playerRenderer.enabled = false;

                flashCount = flashLength;
            }

            
        }
        
    }

    public void AddHealth(int healthToAdd)
    {
        currentHealth += healthToAdd;

        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);
    }


    public void Invincibility(int invincibilityToAdd)
    {
        invincibilityCounter += invincibilityToAdd;
    }

    public class OnDeathEventArgs
    {
        public PlayerHealth DeadPlayer;
        public OnDeathEventArgs(PlayerHealth deadPlayer)
        {
            DeadPlayer = deadPlayer;
        }
    }

    public static event System.EventHandler<OnDeathEventArgs> OnDeath;
}

