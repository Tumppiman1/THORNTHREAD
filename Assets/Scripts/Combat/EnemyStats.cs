using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private Material enemyDamaged;
    [SerializeField] private Material enemyNormal;
    [Header("Enemy health")]
    public float enemyHealth;
    public float enemyMaxHealth = 20f;
    public bool enemyAlive = true;
    public int enemyEvadeChance = 100;
    
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

    [Header("Special effects")] 
    public int stunDuration = 0;
    public int damageOvertimeDuration = 0;
    public Transform effectPoint;
   

    public bool isBlocking = false;
    
    void Start()
    {
        enemyHealth = enemyMaxHealth;
        AudioManager.Instance.PlayMusic("Battle");
    }

    
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        Debug.Log("Enemy took: " + damage + " damage");
        StartCoroutine(EnemyDamagedColor());
        // GetComponent<MeshRenderer>().material = enemyDamaged;

        if (enemyHealth <= 0) {
            enemyAlive = false;
            Destroy(gameObject);
            AudioManager.Instance.StopMusic();
            GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().enemies.Remove(gameObject);
            // Invoke(nameof(EnemyDeath), 1f);
        }
        
        // Invoke(nameof(EnemyIdleColor), 0.1f);
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

    IEnumerator EnemyDamagedColor()
    {
        GetComponent<MeshRenderer>().material = enemyDamaged;
        yield return new WaitForSeconds(0.2f);
        GetComponent<MeshRenderer>().material = enemyNormal;
    }
}
