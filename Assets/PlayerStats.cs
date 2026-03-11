using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject brokenSwordButton;
    [SerializeField] private GameObject axeButton;
    [SerializeField] private GameObject shieldButton;
    
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackPointText;
    public float health;
    public float maxHealth = 100f;
    
    public float brokenSwordDamage = 5f;
    public float axeDamage = 20f;

    public int attackPointCount = 4;
    public int maxAttackPointCount = 4;

    public int axeApCost = 2;
    
    
    
    void Start()
    {
        health = maxHealth;
        
        
        healthText.text = "Health: " + health;
        attackPointText.text = "AP: " + attackPointCount;
    }

    void Update()
    {
        if (attackPointCount <= 0) 
        {
            axeButton.SetActive(false);
        }

        else {
            axeButton.SetActive(true);
        }
        
        
    }

    public void TakeAttackPoints(int apCost)
    {
        attackPointCount -= apCost;
        attackPointText.text = "AP: " + attackPointCount;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthText.text = "Health: " + health;
        Debug.Log("Player took: " + damage + " damage");
        
        if (health <= 0) {
            Debug.Log("Player dead");
        }
    }

    public void Heal(float heal)
    {
        if (health + heal <= maxHealth) 
        {
            health += heal;
        }

        else {
            health = maxHealth;
        }
        
    }

    public void BrokenSwordAttack()
    {
        GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().BrokenSwordAttack();
        // GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().
        
    }

    public void AxeAttack()
    {
        if (attackPointCount - axeApCost >= 0) 
        {
            GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().AxeAttack();
            TakeAttackPoints(axeApCost);
        }
    }

    public void ShieldBlock()
    {
        GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().ShieldBlock();
    }
    

    
}
