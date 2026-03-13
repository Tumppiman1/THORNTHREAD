using System;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject brokenSwordButton;
    [SerializeField] private GameObject axeButton;
    [SerializeField] private GameObject shieldButton;
    
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackPointText;
    [Header("Player health")]
    public float health;
    public float maxHealth = 100f;
    
    // AP system
    [Header("Attack point system")]
    public int attackPointCount = 4;
    public int maxAttackPointCount = 4;
    
    // Broken sword
    [Header("Broken sword stats")]
    public float brokenSwordDamage = 5f;
    public int swordHitChance = 70;
    
    
    // Axe
    [Header("Axe stats")]
    public bool playerHasAxe = true;
    public float axeDamage = 20f;
    public int axeApCost = 2;
    public int axeHitChance = 100;
    
    // Shield
    [Header("Shield stats")]
    public bool playerHasShield = true;
    //public int blockAmount = 1;
    
    
    // Consumables
    [Header("Consumables")]
    public int healFlaskConsumableID = 0;
    public int attackPointConsumableID = 1;
    public int addTurnsConsumableID = 2;
    
    
    
    
    void Start()
    {
        health = maxHealth;
        
        
        healthText.text = "Health: " + health;
        attackPointText.text = "AP: " + attackPointCount;
    }

    void Update()
    {
        
        // Deactivate axe if not collected or not enough AP to use it
        if (!playerHasAxe && attackPointCount <= 0) 
        {
            axeButton.SetActive(false);
        }

        else {
            axeButton.SetActive(true);
        }
    
        // Deactivate shield if not collected
        if (!playerHasShield) 
        {
            shieldButton.SetActive(false);
        }

        else {
            shieldButton.SetActive(true);
        }
        
        
    }

    public void TakeAttackPoints(int apCost)
    {
        attackPointCount -= apCost;
        attackPointText.text = "AP: " + attackPointCount;
    }

    public void AddAttackPoints(int apToAdd)
    {
        attackPointCount += apToAdd;
        attackPointText.text = "AP: " + attackPointCount;
    }

    public void ResetAttackPoints()
    {
        attackPointCount = maxAttackPointCount;
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
            
        }
    }

    public void ShieldBlock()
    {
        GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().ShieldBlock();
    }

    public void AttackPointConsumable()
    {
        GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().UseConsumable(attackPointConsumableID);
    }

    public void AddTurnsConsumable()
    {
        GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().UseConsumable(addTurnsConsumableID);
    }

    
}
