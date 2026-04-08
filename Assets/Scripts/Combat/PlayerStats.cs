using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject collectAxeButton;
    [SerializeField] private GameObject collectShieldButton;
    [SerializeField] private GameObject collectMaceButton;
    
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject brokenSwordButton;
    [SerializeField] private GameObject axeButton;
    [SerializeField] private GameObject shieldButton;
    [SerializeField] private GameObject maceButton;
    [SerializeField] private GameObject inspectEnemyButton;

    [SerializeField] private GameObject healFlaskButton;
    
    [SerializeField] public GameObject addAttackPointsConsumableButton;
    [SerializeField] public GameObject addTurnsConsumableButton;
    [SerializeField] private GameObject damageOverTimeConsumableButton;
    
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
    public int shieldApCost = 1;
    //public int blockAmount = 1;
    
    // Mace
    [Header("Mace stats")]
    public bool playerHasMace = true;
    public float maceDamage = 10f;
    public int maceApCost = 2;
    public int maceHitChance = 100;
    
    // Inspect enemy
    public int inspectEnemyApCost = 1;
    
    
    // Consumables
    [Header("Consumables")]
    [Header("Health flask")]
    public int healFlaskConsumableID = 0;
    public int healFlaskUsesLeft = 2;
    public float healFlaskHealAmount = 10f;
    
    [Header("AP Consumable")]
    public int attackPointConsumableID = 1;
    public bool attackPointConsumable = false;
    public int attackPointConsumableAmount = 1;
    
    [Header("Add turns")]
    public int addTurnsConsumableID = 2;
    public bool addTurnsConsumable = false;
    public int addTurnsConsumableAmount = 1;
    
    [Header("Damage overtime consumable")]
    public int damageOvertimeConsumableID = 3;
    public bool damageOvertimeConsumable = false;
    public int damageOvertimeConsumableAmount = 0;
    public float damageOvertimeConsumableDamage = 4f;
    public int damageOvertimeConsumableDuration = 4;
    
    
    
    
    void Start()
    {
        // health = maxHealth;


        if (health == 0) {
            health = maxHealth;
        }

        else {
            
        }

        //GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().enabled = true;
        
        
        healthText.text = "Health: " + health;
        attackPointText.text = "AP: " + attackPointCount;

        addAttackPointsConsumableButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = attackPointConsumableAmount.ToString();
        addTurnsConsumableButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = addTurnsConsumableAmount.ToString();
        
    }

    void Update()
    {
        
        // Deactivate axe if not collected or not enough AP to use it
        if (playerHasAxe && attackPointCount >= 3) 
        {
            axeButton.SetActive(true);
            collectAxeButton.gameObject.SetActive(false);
        }

        else {
            axeButton.SetActive(false);
        }
    
        // Deactivate shield if not collected
        if (playerHasShield && attackPointCount >= 1) 
        {
            shieldButton.SetActive(true);
            collectShieldButton.gameObject.SetActive(false);
        }

        else {
            shieldButton.SetActive(false);
        }
        
        // Deactivate mace
        if (playerHasMace && attackPointCount >= 5) {
            maceButton.SetActive(true);
            collectMaceButton.gameObject.SetActive(false);
        }

        else {
            maceButton.SetActive(false);
        }
        
        // Deactive inspect
        if (attackPointCount >= 1) 
        {
            inspectEnemyButton.SetActive(true);
        }

        else {
            inspectEnemyButton.SetActive(false);
        }
        
        
        
        if (attackPointConsumableAmount.ToString() != addAttackPointsConsumableButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text) 
        {
            addAttackPointsConsumableButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = attackPointConsumableAmount.ToString();
        }

        if (addTurnsConsumableAmount.ToString() != addTurnsConsumableButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text) 
        {
            addTurnsConsumableButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = addTurnsConsumableAmount.ToString();
        }

        if (healFlaskUsesLeft.ToString() != healFlaskButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text) 
        {
            healFlaskButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = healFlaskUsesLeft.ToString();
        }

        if (damageOvertimeConsumableAmount.ToString() != damageOverTimeConsumableButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text) 
        {
            damageOverTimeConsumableButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = damageOvertimeConsumableAmount.ToString();
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

    IEnumerator PlayDamageSoundDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySFX("Player_Damage");
    }

    IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlaySFX("Player Death");
    }
    public void TakeDamage(float damage)
    {
        health -= damage;

        healthText.text = "Health: " + health;

        Debug.Log("Player took: " + damage + " damage");

        if (health <= 0)
        {
            Debug.Log("Player dead");

            StartCoroutine(HandleDeath());

            deathScreen.SetActive(true);
        }
        else
        {
            StartCoroutine(PlayDamageSoundDelayed(0.55f));
        }
    }
 
    public void UseHealFlask()
    {
        GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().UseHealthFlask();
    }

    public void Heal(float heal)
    {
        if (health + heal <= maxHealth) 
        {
            health += heal;
            healthText.text = "Health: " + health;
        }

        else {
            health = maxHealth;
            healthText.text = "Health: " + health;
        }
        
    }

    public void RefillHealFlask()
    {
        if (healFlaskUsesLeft < 2) 
        {
            healFlaskUsesLeft = 2;
        }

        else 
        {
            Debug.Log("Heal flask already full");    
        }
    }

    public void BrokenSwordAttack()
    {
        if (GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().isPlayerTurn) 
        {
            GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().BrokenSwordAttack();

            // AudioManager.Instance.PlaySFX("Sword_Equip");
        }


    }

    public void CollectAxe()
    {
        collectAxeButton.gameObject.SetActive(false);
        playerHasAxe = true;
    }
    
    public void AxeAttack()
    {
        
        if (attackPointCount - axeApCost >= 0 && GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().isPlayerTurn) 
        {
            GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().AxeAttack();
            
        }
    }
    
    public void CollectShield()
    {
        collectShieldButton.gameObject.SetActive(false);
        playerHasShield = true;
    }

    public void ShieldBlock()
    {
        if (attackPointCount - shieldApCost >= 0 && GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().isPlayerTurn) {
            GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().ShieldBlock();
        }
    }
    
    public void CollectMace()
    {
        collectMaceButton.gameObject.SetActive(false);
        playerHasMace = true;
    }

    public void StunMaceAttack()
    {
        if (attackPointCount - maceApCost >= 0 && GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().isPlayerTurn) {

            GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().StunMaceAttack();
        }
    }

    public void InspectEnemy()
    {
        if (attackPointCount - inspectEnemyApCost >= 0 && GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().isPlayerTurn) 
        {
            GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().InspectEnemy();
        }
    }

    public void AttackPointConsumable()
    {
        
        if (attackPointConsumableAmount > 0 && GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().isPlayerTurn) {
            GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().UseConsumable(attackPointConsumableID);
        }
        else {
            Debug.Log("No AP consumables left");
        }
    }

    public void CollectAttackPointConsumable()
    {
        attackPointConsumable = true;
        attackPointConsumableAmount++;
        
        /*
        if (!attackPointConsumable) 
        {
            attackPointConsumable = true;
            attackPointConsumableAmount++;
        }

        else {
            Debug.Log("Maximum amount of AP consumables");
        }
        */
    }

    public void AddTurnsConsumable()
    {
        if (addTurnsConsumableAmount > 0 && GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().isPlayerTurn) {
            GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().UseConsumable(addTurnsConsumableID);
        }
        else {
            Debug.Log("No add 2 turns consumables left");
        }

    }

    public void CollectAddTurnsConsumable()
    {
        addTurnsConsumable = true;
        addTurnsConsumableAmount++;
        
        /*
        if (!addTurnsConsumable) {
            addTurnsConsumable = true;
            addTurnsConsumableAmount++;
        }
        else {
            Debug.Log("Maximum amount of add turns consumables");
        }
        */
    }

    public void DamageOvertimeConsumable()
    {
        if (damageOvertimeConsumableAmount > 0 && GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().isPlayerTurn) 
        {
            GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().DamageOvertimeConsumableAttack();
        }
    }

    public void CollectDamageOvertimeConsumable()
    {
        damageOvertimeConsumableAmount++;
    }

    public void ReloadLastCheckPoint()
    {
        SceneManager.LoadScene("Main Scene");
        // GameObject.FindGameObjectWithTag("UI").transform.GetChild(0).gameObject.SetActive(false);
        // GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).gameObject.SetActive(true);
        
        
        //GameObject.FindGameObjectWithTag("SaveController").GetComponent<SaveController>().LoadGame();
        
        
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void DisableActionButtons()
    {
        brokenSwordButton.GetComponent<Button>().interactable = false;
        axeButton.GetComponent<Button>().interactable = false;
        shieldButton.GetComponent<Button>().interactable = false;
        maceButton.GetComponent<Button>().interactable = false;
        inspectEnemyButton.GetComponent<Button>().interactable = false;
        
        healFlaskButton.GetComponent<Button>().interactable = false;
        addAttackPointsConsumableButton.GetComponent<Button>().interactable = false;
        addTurnsConsumableButton.GetComponent<Button>().interactable = false;
        damageOverTimeConsumableButton.GetComponent<Button>().interactable = false;
    }

    public void EnableActionButtons()
    {
        brokenSwordButton.GetComponent<Button>().interactable = true;
        axeButton.GetComponent<Button>().interactable = true;
        shieldButton.GetComponent<Button>().interactable = true;
        maceButton.GetComponent<Button>().interactable = true;
        inspectEnemyButton.GetComponent<Button>().interactable = true;
        
        healFlaskButton.GetComponent<Button>().interactable = true;
        addAttackPointsConsumableButton.GetComponent<Button>().interactable = true;
        addTurnsConsumableButton.GetComponent<Button>().interactable = true;
        damageOverTimeConsumableButton.GetComponent<Button>().interactable = true;
    }
    

    
}
