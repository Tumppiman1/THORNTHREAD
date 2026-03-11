using System;
using System.Collections.Generic;
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

    public int index = 0;
    
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
                    BrokenSwordAttack();
                    chooseTarget = false;
                    
                }
                
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    public void StartCombat()
    {
        if (isPlayerTurn) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().attackPointCount =
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxAttackPointCount;
            playerActionsLeft++;
            PlayerTurn();
        }
    }

    void ChangeTurn()
    {
        if (isPlayerTurn) // player turn
        {
            playerActionsLeft++;
            PlayerTurn();
        }

        else if (!isPlayerTurn) // enemy turn 
        {
            if (enemies.Count > 0) {
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
            
            if (enemies.Count == 1) {

                if (enemies[0].GetComponent<EnemyStats>().enemyAlive) 
                {
                    float enemyDamage = enemies[0].GetComponent<EnemyStats>().enemyDamage;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(enemyDamage);
                
                    enemyActionsLeft--;
                    EnemyTurn();
                }

                else {
                    EndCombat();
                }
                
            }
            
            // Multiple enemies
            else if (enemies.Count > 1) {
                index = 0;


                foreach (GameObject enemy in enemies) 
                {
                    if (!enemy.GetComponent<EnemyStats>().enemyAlive) 
                    {
                        enemies.RemoveAt(index);
                        index = 0;
                    }
                    
                    float enemyDamage = enemy.GetComponent<EnemyStats>().enemyDamage;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(enemyDamage);

                    index++;
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
            gameObject.SetActive(false);    
        }
    }
    
    public void BrokenSwordAttack()
    {
        if (isPlayerTurn) 
        {
            if (target != null) 
            {  
                // attack target
                // Debug.Log("here");
                target.GetComponent<EnemyStats>().TakeDamage(_player.GetComponent<PlayerStats>().brokenSwordDamage);
                playerActionsLeft--;
                target = null;
                PlayerTurn();
            }

            else {
                // acquire target
                chooseTarget = true;
            }
            
        }
    }

    public void AxeAttack()
    {
        if (isPlayerTurn) 
        {
            playerActionsLeft--;
        }
    }

    public void ShieldBlock()
    {
        if (isPlayerTurn) 
        {
            playerActionsLeft--;
        }
    }

}
