using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private LayerMask enemyMask;
    public List<GameObject> enemies = new List<GameObject>();
    
    public bool isPlayerTurn = true;
    public bool chooseTarget = false;
    public GameObject target;
    public int playerActionsLeft = 0;
    public int enemyActionsLeft = 0;

    private int index = 0;
    public int attackType = 0;
    
    public bool playerIsBlocking = false;
    
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCombat();
    }
    
    void Update()
    {
        if (chooseTarget) 
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, enemyMask)) 
            {
                if (Input.GetKeyDown(KeyCode.Mouse0)) {
                    target = hit.collider.gameObject;
                    PlayerAttack(attackType);
                    chooseTarget = false;
                    
                }
                
            }
        }
    }

    public void StartCombat()
    {
        GameObject.Find("TestUI").transform.GetChild(0).gameObject.SetActive(true);
        if (isPlayerTurn) {
            _player.GetComponent<PlayerStats>().ResetAttackPoints();
            playerActionsLeft++;
            PlayerTurn();
        }
    }

    void ChangeTurn()
    {
        if (isPlayerTurn) // player turn
        {
            Debug.Log("Player turn");
            playerIsBlocking = false;
            playerActionsLeft++;
            PlayerTurn();
        }

        else if (!isPlayerTurn) // enemy turn 
        {
            Debug.Log("Enemy turn");
            
            if (enemies.Count > 0) {

                foreach (GameObject enemy in enemies) 
                {
                    // Remove block effect from enemies at the start of enemy turn
                    enemy.GetComponent<EnemyStats>().isBlocking = false;
                }
                enemyActionsLeft++;
                EnemyTurn();
            }

            else {
                EndCombat();
            }
        }
    }

    void PlayerTurn()
    {
        if (playerActionsLeft > 0) 
        {
            // Player attack
            
        }

        else {
            isPlayerTurn = false;
            ChangeTurn();
        }
    }

    void EnemyTurn()
    {
        if (enemyActionsLeft > 0) 
        {
            // Enemy attack
            
            // One enemy
            if (enemies.Count == 1) 
            {
                Debug.Log("single");
                if (enemies[0].GetComponent<EnemyStats>().enemyAlive) 
                {
                        // Decide enemy attack option
                        int enemyAttackChance = enemies[0].GetComponent<EnemyStats>().attackChance;
                        int enemyBlockChance = enemies[0].GetComponent<EnemyStats>().blockChance;
                        int enemyHealChance = enemies[0].GetComponent<EnemyStats>().healChance;
                        int enemySpecialChance = enemies[0].GetComponent<EnemyStats>().specialChance;

                        int totalChance = enemyAttackChance + enemyBlockChance + enemyHealChance + enemySpecialChance;
                        // Debug.Log(totalChance);
                        
                        int randomInt = UnityEngine.Random.Range(0, totalChance + 1);

                        if (randomInt >= 0 && randomInt < enemyAttackChance) {
                            // enemy attack
                            // Debug.Log(randomInt);
                            Debug.Log("Enemy attack");
                            
                            if (!playerIsBlocking) {
                                
                                int randomHitChance = UnityEngine.Random.Range(0, 101);
                                
                                if (randomHitChance <= enemies[0].GetComponent<EnemyStats>().hitChance) 
                                {
                                    float enemyDamage = enemies[0].GetComponent<EnemyStats>().enemyDamage;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(enemyDamage);
                                    enemyActionsLeft--;
                                    EnemyTurn();
                                }

                                else {
                                    Debug.Log("Enemy attack missed");
                                    enemyActionsLeft--;
                                    EnemyTurn();
                                }
                            }
                            
                            else 
                            {
                                Debug.Log("Attack blocked");
                                playerIsBlocking = false;
                                enemyActionsLeft--;
                                EnemyTurn();
                            }
                            
                        }
                        
                        else if (randomInt >= enemyAttackChance && randomInt < enemyAttackChance + enemyBlockChance) {
                            // enemy block
                            //Debug.Log(randomInt);
                            Debug.Log("Enemy block");

                            enemies[0].GetComponent<EnemyStats>().isBlocking = true;
                            enemyActionsLeft--;
                            EnemyTurn();
                        }
                        
                        else if (randomInt >= enemyAttackChance + enemyBlockChance && randomInt < enemyAttackChance + enemyBlockChance + enemyHealChance) {
                            // Enemy Heal
                            //Debug.Log(randomInt);
                            Debug.Log("Enemy heal");
                            enemies[0].GetComponent<EnemyStats>().TakeHealing(enemies[0].GetComponent<EnemyStats>().healAmount);
                            enemyActionsLeft--;
                            EnemyTurn();
                        }
                        
                        else if (randomInt >= enemyAttackChance + enemyBlockChance + enemyHealChance && randomInt <= totalChance) {
                            // enemy special
                            //Debug.Log(randomInt);
                            Debug.Log("Enemy special");
                            enemyActionsLeft--;
                            EnemyTurn();
                        }

                        else {
                            //Debug.Log("something");
                            //Debug.Log(randomInt);
                        }
                        
                }

                else 
                {
                    enemies.Clear();
                    EndCombat();
                }
                
            }
            
            // Multiple enemies
            else if (enemies.Count > 1) {
                index = 0;
                Debug.Log("multiple");
                
                foreach (GameObject enemy in enemies.ToList()) 
                {
                    if (!enemy.GetComponent<EnemyStats>().enemyAlive) 
                    {
                        enemies.RemoveAt(index);
                        index = 0;
                    }

                    if (enemy.GetComponent<EnemyStats>().enemyAlive) 
                    {
                        // Decide enemy attack option
                        int enemyAttackChance = enemy.GetComponent<EnemyStats>().attackChance;
                        int enemyBlockChance = enemy.GetComponent<EnemyStats>().blockChance;
                        int enemyHealChance = enemy.GetComponent<EnemyStats>().healChance;
                        int enemySpecialChance = enemy.GetComponent<EnemyStats>().specialChance;

                        int totalChance = enemyAttackChance + enemyBlockChance + enemyHealChance + enemySpecialChance;
                        // Debug.Log(totalChance);
                        
                        int randomInt = UnityEngine.Random.Range(0, totalChance + 1);

                        if (randomInt >= 0 && randomInt < enemyAttackChance) {
                            // enemy attack
                            // Debug.Log(randomInt);
                            Debug.Log("Enemy attack");
                            
                            if (!playerIsBlocking) {
                                
                                int randomHitChance = UnityEngine.Random.Range(0, 101);
                                
                                if (randomHitChance <= enemy.GetComponent<EnemyStats>().hitChance) 
                                {
                                    float enemyDamage = enemy.GetComponent<EnemyStats>().enemyDamage;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(enemyDamage);
                                    
                                }

                                else {
                                    Debug.Log("Enemy attack missed");
                                    
                                }
                            }
                            
                            else 
                            {
                                Debug.Log("Attack blocked");
                                playerIsBlocking = false;
                                
                            }
                            
                        }
                        
                        else if (randomInt >= enemyAttackChance && randomInt < enemyAttackChance + enemyBlockChance) {
                            // enemy block
                            //Debug.Log(randomInt);
                            Debug.Log("Enemy block");

                            enemy.GetComponent<EnemyStats>().isBlocking = true;
                            
                        }
                        
                        else if (randomInt >= enemyAttackChance + enemyBlockChance && randomInt < enemyAttackChance + enemyBlockChance + enemyHealChance) {
                            // Enemy Heal
                            //Debug.Log(randomInt);
                            Debug.Log("Enemy heal");
                            enemy.GetComponent<EnemyStats>().TakeHealing(enemy.GetComponent<EnemyStats>().healAmount);
                            
                        }
                        
                        else if (randomInt >= enemyAttackChance + enemyBlockChance + enemyHealChance && randomInt <= totalChance) {
                            // enemy special
                            //Debug.Log(randomInt);
                            Debug.Log("Enemy special");
                            
                        }

                        else {
                            //Debug.Log("something");
                            //Debug.Log(randomInt);
                        }
                    }
                }
                
                enemyActionsLeft--;
                EnemyTurn();
            }
        }

        else 
        {
            isPlayerTurn = true;
            ChangeTurn();
        }
    }

    void EndCombat()
    {
        // End combat encounter
        
        if (enemies.Count == 0) 
        {
            Debug.Log("EndCombat");
            gameObject.SetActive(false);
            GameObject.Find("TestUI").transform.GetChild(0).gameObject.SetActive(false);
        }
        
        
    }

    private void PlayerAttack(int attackTypeID)
    {
        // Broken sword
        if (attackTypeID == 1) 
        {
            if (target != null && !target.GetComponent<EnemyStats>().isBlocking) 
            {  
                int playerRandomHitChance = UnityEngine.Random.Range(0, 100);

                if (playerRandomHitChance <= _player.GetComponent<PlayerStats>().swordHitChance) 
                {
                    // attack target
                    // Debug.Log("here");
                    target.GetComponent<EnemyStats>().TakeDamage(_player.GetComponent<PlayerStats>().brokenSwordDamage);
                    playerActionsLeft--;
                    target = null;
                    PlayerTurn();
                }

                else {
                    Debug.Log("Player attack missed");
                    playerActionsLeft--;
                    target = null;
                    PlayerTurn();
                }
            }

            else {
                Debug.Log("Enemy blocked attack");
                target.GetComponent<EnemyStats>().isBlocking = false;
                target = null;
                playerActionsLeft--;
                PlayerTurn();
            }
        }
        
        // Axe
        else if (attackTypeID == 2) 
        {
            if (target != null && !target.GetComponent<EnemyStats>().isBlocking) 
            {  
                int playerRandomHitChance = UnityEngine.Random.Range(0, 100);

                if (playerRandomHitChance <= _player.GetComponent<PlayerStats>().axeHitChance) 
                {
                    // attack target
                    // Debug.Log("here");
                    target.GetComponent<EnemyStats>().TakeDamage(_player.GetComponent<PlayerStats>().axeDamage);
                    _player.GetComponent<PlayerStats>().TakeAttackPoints(_player.GetComponent<PlayerStats>().axeApCost);
                    playerActionsLeft--;
                    target = null;
                    PlayerTurn();
                }
                
                else {
                    Debug.Log("Player attack missed");
                    _player.GetComponent<PlayerStats>().TakeAttackPoints(_player.GetComponent<PlayerStats>().axeApCost);
                    playerActionsLeft--;
                    target = null;
                    PlayerTurn();
                }
            }
            
            else {
                Debug.Log("Enemy blocked attack");
                target.GetComponent<EnemyStats>().isBlocking = false;
                _player.GetComponent<PlayerStats>().TakeAttackPoints(_player.GetComponent<PlayerStats>().axeApCost);
                PlayerTurn();
            }
        }
        
    }
    
    public void BrokenSwordAttack()
    {
        attackType = 1;
        
        if (isPlayerTurn) 
        {
                // acquire target
                chooseTarget = true;
        }
    }

    public void AxeAttack()
    {
        attackType = 2;
        if (isPlayerTurn) 
        {
            // acquire target
            chooseTarget = true;
        }
    }

    public void ShieldBlock()
    {
        if (isPlayerTurn) {
            playerIsBlocking = true;
            playerActionsLeft--;
            PlayerTurn();
        }
    }

    public void UseConsumable(int consumableID)
    {
        if (isPlayerTurn) 
        {
            // AP Consumable
            if (consumableID == 1) 
            {
                _player.GetComponent<PlayerStats>().ResetAttackPoints();
                Debug.Log("Used AP consumable");        
            }
            
            // Add +2 Turns Consumable
            else if (consumableID == 2) 
            {
                playerActionsLeft += 2;
            }
            
            playerActionsLeft--;
        }
    }

}
