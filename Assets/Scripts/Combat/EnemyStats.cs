using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Enemy health")]
    public float enemyHealth;
    public float enemyMaxHealth = 20f;
    public bool enemyAlive = true;
    
    [Header("Enemy damage")]
    public float enemyDamage = 10f;
    
    [Header("Enemy heal ability")]
    public float healAmount = 5f;
    
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

    public void TakeHealing(float amountToHeal)
    {
        if (enemyHealth + amountToHeal <= enemyMaxHealth) 
        {
            Debug.Log("enemy heal for: " + amountToHeal);
            enemyHealth += amountToHeal;
        }

        else
        {
            Debug.Log("enemy heal for: " + (enemyMaxHealth - enemyHealth));
            enemyHealth = enemyMaxHealth;
        }
    }

    void EnemyDeath()
    {
        Debug.Log("Enemy dead");
        // Play death animation
        Destroy(gameObject);
    }
}
