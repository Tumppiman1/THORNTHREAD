using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // [Header("Enemy health")]
    public float enemyHealth;
    public float enemyMaxHealth = 20f;
    public bool enemyAlive = true;
    
    //[Header("Enemy damage")]
    public float enemyDamage = 10f;
    
    

    //[Header("Combat stats")]
    public float hitChance = 70f;
    public float attackChance;
    public float blockChance = 20f;
    public float healChance = 10f;
    public float specialChance = 5f;
    
    void Start()
    {
        enemyHealth = enemyMaxHealth;
    }

    
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        Debug.Log("Enemy took: " + damage + " damage");

        if (enemyHealth <= 0) {
            enemyAlive = false;
            // Destroy(gameObject);
            Invoke(nameof(EnemyDeath), 1f);
        }
    }

    void EnemyDeath()
    {
        // Play death animation
        Destroy(gameObject);
    }
}
