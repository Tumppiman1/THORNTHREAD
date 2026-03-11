using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    
    public bool isPlayerTurn = true;
    public int playerActionsLeft = 0;
    public int enemyActionsLeft = 0;
    
    void Start()
    {
        StartCombat();
    }
    
    void Update()
    {
        
    }

    public void StartCombat()
    {
        if (isPlayerTurn) 
        {
            playerActionsLeft++;
            PlayerTurn();
        }
    }

    void ChangeTurn()
    {
        if (isPlayerTurn) // player turn
        {
            PlayerTurn();
        }

        else if (!isPlayerTurn) // enemy turn 
        {
            EnemyTurn();
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
        }

        else 
        {
            isPlayerTurn = true;
        }
    }

    void EndCombat()
    {
        // End combat encounter
    }
}
