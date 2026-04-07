using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool combatEncounterCompleted = false;
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
    [SerializeField] GameObject SlashVFX;
    [SerializeField] GameObject MissVFX;

    private bool _coroutineActive = false;
    
    // Animations
    [Header("Animations")] 
    [SerializeField] private GameObject _miekka;
    [SerializeField] private GameObject _kirves;
    [SerializeField] private GameObject _nuija;
    
    [SerializeField] private GameObject _miekkaKäsi;
    [SerializeField] private GameObject _kirvesKäsi;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _player.GetComponent<PlayerStats>().DisableActionButtons();
        StartCombat();
        
        //_miekkaKäsi = GameObject.FindGameObjectWithTag("Hands").transform.GetChild(0).gameObject;
        //_kirvesKäsi = GameObject.FindGameObjectWithTag("Hands").transform.GetChild(1).gameObject;
        
        //_kirves = _kirves.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject;
        //_nuija = _kirves.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject;
    }
    
    void Update()
    {
        if (chooseTarget) 
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, enemyMask)) 
            {
                if (Input.GetKeyDown(KeyCode.Mouse0)) 
                {
                    if (target == null) {
                        Debug.Log("here");
                        chooseTarget = false;
                        target = hit.collider.gameObject;

                        //chooseTarget = false;
                        PlayerAttack(attackType);
                    }


                }
                
            }
        }
    }

    public void StartCombat()
    {
        transform.parent.GetChild(0).gameObject.SetActive(false);
        
        GameObject.Find("TestUI").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("TestUI").transform.GetChild(0).gameObject.SetActive(true);
        _player.GetComponent<PlayerStats>().ResetAttackPoints();
        
        if (isPlayerTurn) {
            //_player.GetComponent<PlayerStats>().ResetAttackPoints();
            playerActionsLeft++;
            StartCoroutine(PlayerFirstTurnText());
            //Invoke(nameof(PlayerTurn), 3f);
            
        }

        else if (!isPlayerTurn) {
            
            enemyActionsLeft++;
            //Invoke(nameof(EnemyTurn), 1f);
            StartCoroutine(EnemyFirstTurnText());
            //Invoke(nameof(EnemyTurn), 3f);
        }
    }

    void ChangeTurn()
    {
        if (isPlayerTurn) // player turn
        {
            target = null;
            playerActionsLeft = 0;
            _player.GetComponent<PlayerStats>().EnableActionButtons();
            Debug.Log("Player turn");
            playerIsBlocking = false;
            playerActionsLeft = 1;

            if (!_coroutineActive) 
            {
                StartCoroutine(PlayerTurnText());
            }

            //Invoke(nameof(PlayerTurn), 2f);
            //PlayerTurn();
        }

        else if (!isPlayerTurn) // enemy turn 
        {
            target = null;
            enemyActionsLeft = 0;
            _player.GetComponent<PlayerStats>().DisableActionButtons();
            
            if (enemies.Count >= 1) 
            {   
                enemyActionsLeft = 1;
                
                foreach (GameObject enemy in enemies) 
                {
                    // Remove block effect from enemies at the start of enemy turn
                    enemy.GetComponent<EnemyStats>().isBlocking = false;
                }

                if (!_coroutineActive) 
                {
                    StartCoroutine(EnemyTurnText());
                }

                //Invoke(nameof(EnemyTurn), 2f);
                Debug.Log("Enemy turn");
                // EnemyTurn();
            }

            else 
            {
                Debug.Log("End of combat");
                EndCombat();
            }
        }
    }

    void PlayerTurn()
    {
        if (playerActionsLeft > 0) 
        {
            // Player attack
            target = null;

        }

        else {
            // Damage overtime effect
            isPlayerTurn = false;
            target = null;
            //_player.GetComponent<PlayerStats>().DisableActionButtons();
            foreach (GameObject enemy in enemies.ToList()) 
            {
                if (enemy != null) 
                {
                    if (enemy.GetComponent<EnemyStats>().enemyAlive && enemy.GetComponent<EnemyStats>().damageOvertimeDuration > 0) 
                    {
                        Debug.Log("Enemy took: " + _player.GetComponent<PlayerStats>().damageOvertimeConsumableDamage + " damage overtime");
                        enemy.GetComponent<EnemyStats>().TakeDamage(_player.GetComponent<PlayerStats>().damageOvertimeConsumableDamage);
                        enemy.GetComponent<EnemyStats>().damageOvertimeDuration--;
                    }
                }
            }
            
            //isPlayerTurn = false;
            
            
            ChangeTurn();
        }
    }

    void EnemyTurn()
    {
        if (enemyActionsLeft == 1) 
        {
            // Enemy attack
            
            // Single enemy
            if (enemies.Count == 1) 
            {
                Debug.Log("single");
                if (enemies[0].GetComponent<EnemyStats>().enemyAlive && enemies[0].GetComponent<EnemyStats>().stunDuration <= 0) 
                {
                        // Decide enemy attack option
                        int enemyAttackChance = enemies[0].GetComponent<EnemyStats>().attackChance;
                        int enemyBlockChance = enemies[0].GetComponent<EnemyStats>().blockChance;
                        int enemyHealChance = enemies[0].GetComponent<EnemyStats>().healChance;
                        int enemySpecialChance = enemies[0].GetComponent<EnemyStats>().specialChance;

                        int totalChance = enemyAttackChance + enemyBlockChance + enemyHealChance;
                        // Debug.Log(totalChance);
                        
                        int randomInt = UnityEngine.Random.Range(0, totalChance - 1);
                        Debug.Log(randomInt);

                        if (randomInt >= 0 && randomInt < enemyAttackChance) 
                        {
                            // enemy attack
                            // Debug.Log(randomInt);
                            Debug.Log("Enemy attack");
                            
                            if (!playerIsBlocking) {
                                
                                int randomHitChance = UnityEngine.Random.Range(0, 101);
                                
                                if (randomHitChance <= enemies[0].GetComponent<EnemyStats>().hitChance) 
                                {
                                    float enemyDamage = enemies[0].GetComponent<EnemyStats>().enemyDamage;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(enemyDamage);
                                    StartCoroutine(PlayerDamageText("-" + enemyDamage));
                                    AudioManager.Instance.PlaySFX("Skeleton hit");
                                    
                                enemyActionsLeft--;
                                    EnemyTurn();
                                }

                                else {
                                    Debug.Log("Enemy attack missed");
                                    AudioManager.Instance.PlaySFX("Skeleton Miss");
                                    enemyActionsLeft--;
                                    EnemyTurn();
                                }
                            }
                            
                            else 
                            {
                                Debug.Log("Attack blocked");
                                playerIsBlocking = false;
                            AudioManager.Instance.PlaySFX("Shield_Block3");
                                enemyActionsLeft--;
                                EnemyTurn();
                            }
                            
                        }
                        
                        else if (randomInt >= enemyAttackChance && randomInt < enemyAttackChance + enemyBlockChance) {
                            // enemy block
                            //Debug.Log(randomInt);
                            Debug.Log("Enemy block");

                            enemies[0].GetComponent<EnemyStats>().isBlocking = true;
                        AudioManager.Instance.PlaySFX("Shield_Block2");
                            enemyActionsLeft--;
                            EnemyTurn();
                        }
                        
                        else if (randomInt >= enemyAttackChance + enemyBlockChance && randomInt < totalChance) 
                        {
                            // Enemy Heal
                            //Debug.Log(randomInt);

                            if (enemies[0].GetComponent<EnemyStats>().enemyHealth < enemies[0].GetComponent<EnemyStats>().enemyMaxHealth / 2) {
                                Debug.Log("Enemy heal");
                                AudioManager.Instance.PlaySFX("Heal Bot Drink");
                                enemies[0].GetComponent<EnemyStats>().TakeHealing(enemies[0].GetComponent<EnemyStats>().healAmount);
                                enemyActionsLeft--;
                                EnemyTurn();
                            }

                            else
                            {
                                Debug.Log("Enemy attack");
                            
                                if (!playerIsBlocking) {
                                
                                    int randomHitChance = UnityEngine.Random.Range(0, 101);
                                
                                    if (randomHitChance <= enemies[0].GetComponent<EnemyStats>().hitChance) 
                                    {
                                        float enemyDamage = enemies[0].GetComponent<EnemyStats>().enemyDamage;
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(enemyDamage);
                                        StartCoroutine(PlayerDamageText("-" + enemyDamage));
                                        AudioManager.Instance.PlaySFX("Skeleton hit");
                                        enemyActionsLeft--;
                                        EnemyTurn();
                                    }

                                    else {
                                        Debug.Log("Enemy attack missed");
                                        AudioManager.Instance.PlaySFX("Skeleton Miss");
                                        enemyActionsLeft--;
                                        EnemyTurn();
                                    }
                                }
                            
                                else 
                                {
                                    Debug.Log("Attack blocked");
                                    playerIsBlocking = false;
                                    AudioManager.Instance.PlaySFX("Shield_Block3");
                                    enemyActionsLeft--;
                                    EnemyTurn();
                                }
                            }
                        }
                        
                        /*
                        else if (randomInt >= enemyAttackChance + enemyBlockChance + enemyHealChance && randomInt <= totalChance) {
                            // enemy special
                            //Debug.Log(randomInt);
                            Debug.Log("Enemy special");
                            enemyActionsLeft--;
                            EnemyTurn();
                        }
                        */

                        else {
                            Debug.Log("something");
                            //Debug.Log(randomInt);
                        }
                        
                }
                
                else if (enemies[0].GetComponent<EnemyStats>().enemyAlive && enemies[0].GetComponent<EnemyStats>().stunDuration > 0) 
                {
                    Debug.Log("Enemy is stunned");
                    enemies[0].GetComponent<EnemyStats>().stunDuration--;
                    enemyActionsLeft--;
                    EnemyTurn();
                }

                else 
                {
                    Debug.Log("No enemies");
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

                    if (enemy.GetComponent<EnemyStats>().enemyAlive && enemy.GetComponent<EnemyStats>().stunDuration <= 0) 
                    {
                        // Decide enemy attack option
                        int enemyAttackChance = enemy.GetComponent<EnemyStats>().attackChance;
                        int enemyBlockChance = enemy.GetComponent<EnemyStats>().blockChance;
                        int enemyHealChance = enemy.GetComponent<EnemyStats>().healChance;
                        //int enemySpecialChance = enemy.GetComponent<EnemyStats>().specialChance;

                        int totalChance = enemyAttackChance + enemyBlockChance + enemyHealChance;
                        // Debug.Log(totalChance);
                        
                        int randomInt = UnityEngine.Random.Range(0, totalChance);

                        if (randomInt >= 0 && randomInt < enemyAttackChance) {
                            // enemy attack
                            // Debug.Log(randomInt);
                            Debug.Log("Enemy attack");
                            
                            if (!playerIsBlocking) {
                                
                                int randomHitChance = UnityEngine.Random.Range(0, 101);
                                
                                if (randomHitChance <= enemy.GetComponent<EnemyStats>().hitChance) 
                                {
                                    float enemyDamage = enemy.GetComponent<EnemyStats>().enemyDamage;
                                    AudioManager.Instance.PlaySFX("Skeleton hit");
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(enemyDamage);
                                    StartCoroutine(PlayerDamageText("-" + enemyDamage));
                                    
                                }

                                else {
                                    Debug.Log("Enemy attack missed");
                                    AudioManager.Instance.PlaySFX("Skeleton Miss");
                                }
                            }
                            
                            else 
                            {
                                Debug.Log("Attack blocked");
                                AudioManager.Instance.PlaySFX("Shield_Block3");
                                playerIsBlocking = false;
                                
                            }
                            
                        }
                        
                        else if (randomInt >= enemyAttackChance && randomInt < enemyAttackChance + enemyBlockChance) {
                            // enemy block
                            //Debug.Log(randomInt);
                            Debug.Log("Enemy block");
                            AudioManager.Instance.PlaySFX("Shield_Block2");
                            enemy.GetComponent<EnemyStats>().isBlocking = true;
                            
                        }
                        
                        else if (randomInt >= enemyAttackChance + enemyBlockChance && randomInt < enemyAttackChance + enemyBlockChance + enemyHealChance) {
                            // Enemy Heal
                            //Debug.Log(randomInt);
                            Debug.Log("Enemy heal");
                            AudioManager.Instance.PlaySFX("Heal Bot Drink");
                            enemy.GetComponent<EnemyStats>().TakeHealing(enemy.GetComponent<EnemyStats>().healAmount);
                            
                        }                                          

                        else {
                            //Debug.Log("something");
                            //Debug.Log(randomInt);
                        }
                    }
                    
                    else if (enemy.GetComponent<EnemyStats>().enemyAlive && enemy.GetComponent<EnemyStats>().stunDuration > 0) 
                    {
                        Debug.Log("Enemy is stunned");
                        enemy.GetComponent<EnemyStats>().stunDuration--;
                        
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
            GameObject.Find("CombatText").transform.GetChild(2).gameObject.SetActive(false);
            GameObject.Find("CombatText").transform.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("CombatText").transform.GetChild(4).gameObject.SetActive(false);
            GameObject.Find("CombatText").transform.GetChild(5).gameObject.SetActive(false);
            GameObject.Find("CombatText").transform.GetChild(6).gameObject.SetActive(false);
            TooltipSystem.Hide();
            GameObject.Find("TestUI").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("TestUI").transform.GetChild(1).gameObject.SetActive(true);
            combatEncounterCompleted = true;
            GameObject.FindGameObjectWithTag("CombatEncounterList").GetComponent<CombatEncounterList>().RemoveCompletedCombatEncounters();
            
            // GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().currentScreen.transform.GetChild(0).gameObject.SetActive(true);
            transform.parent.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject);
        }
        
        
    }

    public void PlayerIdle()
    {
        _kirvesKäsi.gameObject.SetActive(false);
        _miekkaKäsi.gameObject.SetActive(true);
    }

    private void PlayerAttack(int attackTypeID)
    {
        Debug.Log("Player attack");
        // _player.GetComponent<PlayerStats>().DisableActionButtons();
        
        // Broken sword
        if (attackTypeID == 1) 
        {
            if (target != null && !target.GetComponent<EnemyStats>().isBlocking) 
            {  
                int playerRandomHitChance = UnityEngine.Random.Range(0, target.GetComponent<EnemyStats>().enemyEvadeChance);

                if (playerRandomHitChance <= _player.GetComponent<PlayerStats>().swordHitChance) 
                {
                    // attack target
                    // Debug.Log("here");
                    Instantiate(SlashVFX, target.GetComponent<EnemyStats>().effectPoint.position, Quaternion.identity);
                    target.GetComponent<EnemyStats>().TakeDamage(_player.GetComponent<PlayerStats>().brokenSwordDamage);
                    target = null;
                    playerActionsLeft--;
                    AudioManager.Instance.PlaySFX("Sword_Hit");
                    Debug.Log("Slash");
                    //GameObject.FindGameObjectWithTag("Hands").transform.GetChild(1).gameObject.SetActive(false);
                    //GameObject.FindGameObjectWithTag("Hands").transform.GetChild(0).gameObject.SetActive(true);
                    //GameObject.FindGameObjectWithTag("Hands").GetComponent<Animator>().SetTrigger("AxeAttack");
                    
                    _kirvesKäsi.gameObject.SetActive(false);
                    _miekkaKäsi.gameObject.SetActive(true);
                    _miekkaKäsi.GetComponent<Animator>().SetTrigger("AxeAttack");
                    


                    PlayerTurn();
                }

                else {
                    Debug.Log("Player attack missed");
                    Instantiate(MissVFX, target.GetComponent<EnemyStats>().effectPoint.position, Quaternion.identity);
                    target = null;
                    playerActionsLeft--;
                    AudioManager.Instance.PlaySFX("Sword_Miss");
                    
                    PlayerTurn();
                }
            }

            else {
                Debug.Log("Enemy blocked attack");
                Instantiate(MissVFX, target.GetComponent<EnemyStats>().effectPoint.position, Quaternion.identity);
                target.GetComponent<EnemyStats>().isBlocking = false;
                target = null;
                playerActionsLeft--;
                AudioManager.Instance.PlaySFX("Sword_Miss2");
                PlayerTurn();
            }
        }
        
        // Axe
        else if (attackTypeID == 2) 
        {
            if (target != null && !target.GetComponent<EnemyStats>().isBlocking) 
            {  
                int playerRandomHitChance = UnityEngine.Random.Range(0, target.GetComponent<EnemyStats>().enemyEvadeChance);

                if (playerRandomHitChance <= _player.GetComponent<PlayerStats>().axeHitChance) 
                {
                    // attack target
                    // Debug.Log("here");
                    target.GetComponent<EnemyStats>().TakeDamage(_player.GetComponent<PlayerStats>().axeDamage);
                    _player.GetComponent<PlayerStats>().TakeAttackPoints(_player.GetComponent<PlayerStats>().axeApCost);
                    StartCoroutine(PlayerAPText("-" + _player.GetComponent<PlayerStats>().axeApCost.ToString()));
                    playerActionsLeft--;
                    target = null;
                    AudioManager.Instance.PlaySFX("Axe_Hit");
                    //GameObject.FindGameObjectWithTag("Hands").transform.GetChild(0).gameObject.SetActive(false);
                    //GameObject.FindGameObjectWithTag("Hands").transform.GetChild(1).gameObject.SetActive(true);
                    //GameObject.FindGameObjectWithTag("Kirves").gameObject.SetActive(true);
                    //GameObject.FindGameObjectWithTag("Hands").GetComponent<Animator>().SetTrigger("AxeAttack");
                    
                    _miekkaKäsi.gameObject.SetActive(false);
                    _kirvesKäsi.gameObject.SetActive(true);
                    
                    _kirvesKäsi.GetComponent<Animator>().SetTrigger("AxeAttack");
                    
                    PlayerTurn();
                }
                
                else {
                    Debug.Log("Player attack missed");
                    _player.GetComponent<PlayerStats>().TakeAttackPoints(_player.GetComponent<PlayerStats>().axeApCost);
                    StartCoroutine(PlayerAPText("-" + _player.GetComponent<PlayerStats>().axeApCost.ToString()));
                    playerActionsLeft--;
                    target = null;
                    AudioManager.Instance.PlaySFX("Sword_Miss3");
                    PlayerTurn();
                }
            }
            
            else {
                Debug.Log("Enemy blocked attack");
                target.GetComponent<EnemyStats>().isBlocking = false;
                _player.GetComponent<PlayerStats>().TakeAttackPoints(_player.GetComponent<PlayerStats>().axeApCost);
                StartCoroutine(PlayerAPText("-" + _player.GetComponent<PlayerStats>().axeApCost.ToString()));
                playerActionsLeft--;
                PlayerTurn();
            }
        }
        
        
        // Mace attack
        else if (attackTypeID == 3) 
        {
            // Check if enemy is blocking
            if (target != null && !target.GetComponent<EnemyStats>().isBlocking) 
            {  
                // Hit chance calculation
                int playerRandomHitChance = UnityEngine.Random.Range(0, target.GetComponent<EnemyStats>().enemyEvadeChance);

                if (playerRandomHitChance <= _player.GetComponent<PlayerStats>().maceHitChance) 
                {
                    // attack target
                    // Debug.Log("here");
                    target.GetComponent<EnemyStats>().TakeDamage(_player.GetComponent<PlayerStats>().maceDamage);
                    target.GetComponent<EnemyStats>().stunDuration = 2;
                    _player.GetComponent<PlayerStats>().TakeAttackPoints(_player.GetComponent<PlayerStats>().maceApCost);
                    StartCoroutine(PlayerAPText("-" + _player.GetComponent<PlayerStats>().maceApCost.ToString()));
                    playerActionsLeft--;
                    AudioManager.Instance.PlaySFX("Axe_Hit");
                    target = null;
                    
                    //GameObject.FindGameObjectWithTag("Hands").transform.GetChild(0).gameObject.SetActive(false);
                    //GameObject.FindGameObjectWithTag("Hands").transform.GetChild(1).gameObject.SetActive(true);
                    //GameObject.FindGameObjectWithTag("Nuija").gameObject.SetActive(true);
                    //GameObject.FindGameObjectWithTag("Hands").GetComponent<Animator>().SetTrigger("AxeAttack");

                    _miekkaKäsi.gameObject.SetActive(false);
                    _kirvesKäsi.gameObject.SetActive(true);
                    _kirvesKäsi.GetComponent<Animator>().SetTrigger("AxeAttack");
                    
                    PlayerTurn();
                }
                
                else {
                    Debug.Log("Player attack missed");
                    _player.GetComponent<PlayerStats>().TakeAttackPoints(_player.GetComponent<PlayerStats>().maceApCost);
                    StartCoroutine(PlayerAPText("-" + _player.GetComponent<PlayerStats>().maceApCost.ToString()));
                    playerActionsLeft--;
                    AudioManager.Instance.PlaySFX("Sword_Miss2");
                    target = null;
                    PlayerTurn();
                }
            }
            
            else {
                Debug.Log("Enemy blocked attack");
                target.GetComponent<EnemyStats>().isBlocking = false;
                _player.GetComponent<PlayerStats>().TakeAttackPoints(_player.GetComponent<PlayerStats>().maceApCost);
                playerActionsLeft--;
                PlayerTurn();
            }
        }
        
        else if (attackTypeID == 4) 
        {
            // Check if enemy isnt null
            if (target != null) 
            {  
                    // attack target
                    // Debug.Log("here");
                    // target.GetComponent<EnemyStats>().TakeDamage(_player.GetComponent<PlayerStats>().maceDamage);
                    target.GetComponent<EnemyStats>().damageOvertimeDuration = _player.GetComponent<PlayerStats>().damageOvertimeConsumableDuration;
                    _player.GetComponent<PlayerStats>().damageOvertimeConsumableAmount--;
                    
                    playerActionsLeft--;
                    target = null;
                    PlayerTurn();
            }
        }
        
        // Inspect enemy
        else if (attackTypeID == 5) 
        {
            // Check if enemy isnt null
            if (target != null) 
            {  
                // Inspect target
                StartCoroutine(InspectEnemyText("Enemy HP: " + target.GetComponent<EnemyStats>().enemyHealth + "/" + target.GetComponent<EnemyStats>().enemyMaxHealth));
                _player.GetComponent<PlayerStats>().TakeAttackPoints(_player.GetComponent<PlayerStats>().inspectEnemyApCost);
                playerActionsLeft--;
                target = null;
                //PlayerTurn();
                Invoke(nameof(PlayerTurn), 2f);
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
            _player.GetComponent<PlayerStats>().TakeAttackPoints(_player.GetComponent<PlayerStats>().shieldApCost);
            AudioManager.Instance.PlaySFX("Shied_Block");
            PlayerTurn();
        }
    }

    public void StunMaceAttack()
    {
        attackType = 3;
        
        if (isPlayerTurn) 
        {
            // acquire target
            chooseTarget = true;
        }
    }

    public void InspectEnemy()
    {
        attackType = 5;
        
        if (isPlayerTurn) 
        {
            // acquire target
            chooseTarget = true;
        }
    }

    public void DamageOvertimeConsumableAttack()
    {
        // Damage overtime consumable

        attackType = 4;
        
        if (isPlayerTurn) 
        {
            // acquire target
            chooseTarget = true;
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
                _player.GetComponent<PlayerStats>().attackPointConsumableAmount -= 1;
                _player.GetComponent<PlayerStats>().attackPointConsumable = false;
                AudioManager.Instance.PlaySFX("OneTimePot");
                Debug.Log("Used AP consumable");
                
            }
            
            // Add +2 Turns Consumable
            else if (consumableID == 2) 
            {
                _player.GetComponent<PlayerStats>().addTurnsConsumableAmount -= 1;
                _player.GetComponent<PlayerStats>().addTurnsConsumable = false;
                playerActionsLeft += 2;
                AudioManager.Instance.PlaySFX("OneTimePot");
                Debug.Log("Used add 2 turns consumable");
            }
            
            
            
            playerActionsLeft--;
        }
    }

    public void UseHealthFlask()
    {
        if (isPlayerTurn) 
        {
            if (_player.GetComponent<PlayerStats>().healFlaskUsesLeft > 0) 
            {
                _player.GetComponent<PlayerStats>().Heal(_player.GetComponent<PlayerStats>().healFlaskHealAmount);
                _player.GetComponent<PlayerStats>().healFlaskUsesLeft--;
                playerActionsLeft--;
                AudioManager.Instance.PlaySFX("Heal Bot Drink");
                PlayerTurn();
                Debug.Log("Used heal flask");
            }

            else {
                Debug.Log("No heal flask uses left");
            }
        }
            
    }

    IEnumerator PlayerTurnText()
    {
        _coroutineActive = true;
        GameObject.Find("CombatText").transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObject.Find("CombatText").transform.GetChild(1).gameObject.SetActive(false);
        _coroutineActive = false;
        PlayerTurn();
    }
    
    IEnumerator EnemyTurnText()
    {
        _coroutineActive = true;
        GameObject.Find("CombatText").transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObject.Find("CombatText").transform.GetChild(0).gameObject.SetActive(false);
        _coroutineActive = false;
        EnemyTurn();
    }
    
    IEnumerator PlayerFirstTurnText()
    {
        GameObject.Find("CombatText").transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObject.Find("CombatText").transform.GetChild(3).gameObject.SetActive(false);
        PlayerTurn();
    }
    
    IEnumerator EnemyFirstTurnText()
    {
        GameObject.Find("CombatText").transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObject.Find("CombatText").transform.GetChild(2).gameObject.SetActive(false);
        EnemyTurn();
    }
    
    IEnumerator PlayerDamageText(string text)
    {
        GameObject.Find("CombatText").transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = text;
        GameObject.Find("CombatText").transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObject.Find("CombatText").transform.GetChild(4).gameObject.SetActive(false);
    }
    
    IEnumerator PlayerAPText(string text)
    {
        GameObject.Find("CombatText").transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>().text = text;
        GameObject.Find("CombatText").transform.GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObject.Find("CombatText").transform.GetChild(5).gameObject.SetActive(false);
    }
    
    IEnumerator InspectEnemyText(string text)
    {
        GameObject.Find("CombatText").transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>().text = text;
        GameObject.Find("CombatText").transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObject.Find("CombatText").transform.GetChild(6).gameObject.SetActive(false);
    }

}
