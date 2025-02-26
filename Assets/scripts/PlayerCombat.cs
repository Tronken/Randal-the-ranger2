using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint; // The position from where the attack originates
    public float attackRange = 0.5f; // Radius of the attack area
    public LayerMask enemyLayers; // The layer of enemies that can be attacked

    public int attackDamage = 1; // The damage dealt to enemies

    void Update()
    {
        // Check for attack input (Space key)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Detect enemies within attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Get the `Enemy_Damage` component and apply damage
            Enemy_Damage enemyDamage = enemy.GetComponent<Enemy_Damage>();
            if (enemyDamage != null)
            {
                enemyDamage.EnemyTakeDamage(attackDamage); // Call the damage function
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        // Draw the attack range in the editor for visualization
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}