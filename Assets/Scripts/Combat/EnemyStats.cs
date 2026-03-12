using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Enemy health")]
    public float enemyHealth;
    public float enemyMaxHealth = 20f;
    public bool enemyAlive = true;
    
    [Header("Enemy damage")]
    public float enemyDamage = 10f;
    
    [Header("Combat stats")]
    public int hitChance = 70;
    public int attackChance = 60;
    public int blockChance = 30;
    public int healChance = 5;
    public int specialChance = 5;

    public bool isBlocking = false;
    
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
        Debug.Log("Enemy dead");
        // Play death animation
        Destroy(gameObject);
    }
}
