using System.Collections;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    // Public variables for customization in the Inspector
    public int maxHealth = 3; // Maximum health of the enemy
    private int currentHealth; // Tracks the current health of the enemy
    public MonoBehaviour EnemyScase; // Reference to an enemy behavior script
    private Collider2D enemyCollider; // Collider component of the enemy
    public float stunDuration = 1f; // How long the enemy stays stunned
    public float knockBackForce = 10f; // Force applied during knock-back
    public float knockBackDuration = 0.2f; // How long the knock-back effect lasts
    public float blinkDuration = 0.1f; // Duration of each red blink
    public int blinkTimes = 3; // Number of times the enemy blinks red after taking damage

    // Private components
    private Rigidbody2D rb; // Rigidbody2D for physics interactions
    private SpriteRenderer spriteRenderer; // SpriteRenderer for visual effects
    private Color originalColor; // Stores the original color of the sprite

    private void Start()
    {
        // Initialize current health to maximum health
        currentHealth = maxHealth;

        // Get required components from the GameObject
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure the SpriteRenderer exists and cache its original color
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer component missing on enemy.");
        }
    }

    // Method to handle taking damage
    public void EnemyTakeDamage(int damage)
    {
        currentHealth -= damage; // Subtract damage from current health

        if (currentHealth <= 0)
        {
            Die(); // Call Die() if health drops to 0 or below
        }
        else
        {
            // Trigger visual and movement effects
            StartCoroutine(BlinkRed());
            StartCoroutine(KnockBackAndStun());
        }
    }

    // Handles enemy death logic
    void Die()
    {
        this.enabled = false; // Disable this script
        if (EnemyScase != null)
        {
            EnemyScase.enabled = false; // Disable the behavior script
        }
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false; // Disable the enemy's collider
        }
        StartCoroutine(TurnRedAndDisableSprite(1f)); // Trigger death visual effect
    }

    // Coroutine for knock-back and stunning the enemy
    IEnumerator KnockBackAndStun()
    {
        if (rb != null)
        {
            // Calculate direction to push the enemy away from the player
            Vector2 knockBackDirection = (transform.position - GameObject.FindWithTag("Player").transform.position).normalized;

            // Apply knock-back force
            rb.AddForce(knockBackDirection * knockBackForce, ForceMode2D.Impulse);

            // Wait for the knock-back duration to finish
            yield return new WaitForSeconds(knockBackDuration);

            // Stop enemy movement after knock-back
            rb.velocity = Vector2.zero;
        }

        // Stun the enemy temporarily by disabling its behavior
        if (EnemyScase != null)
        {
            EnemyScase.GetComponent<EnemyScase>().isStunned = true;
            yield return new WaitForSeconds(stunDuration); // Wait for stun duration
            EnemyScase.GetComponent<EnemyScase>().isStunned = false;
        }
    }

    // Coroutine to create a blinking red visual effect
    IEnumerator BlinkRed()
    {
        for (int i = 0; i < blinkTimes; i++)
        {
            spriteRenderer.color = Color.red; // Change sprite color to red
            yield return new WaitForSeconds(blinkDuration); // Wait for blink duration
            spriteRenderer.color = originalColor; // Restore original color
            yield return new WaitForSeconds(blinkDuration); // Wait before next blink
        }
    }

    // Coroutine to change the sprite color to red and hide it after a delay
    IEnumerator TurnRedAndDisableSprite(float delay)
    {
        spriteRenderer.color = Color.red; // Change color to red
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        spriteRenderer.enabled = false; // Hide the sprite
    }

    // Damage dealt to the player when they collide with the enemy
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.CompareTag("Player"))
        {
            // Deal damage to the player's health component
            var playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("Player does not have a Health component.");
            }
        }
    }
}