using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveData
{
    public GameObject playerPosition;           // Active camera
    public float playerHealth;                  // Player health
    
    public bool playerHasAxe;                   // Player has collected axe
    public bool playerHasShield;                // Player has collected shield

    public int attackPointConsumableAmount;     // Amount of AP consumables left
    public int addTurnsConsumableAmount;        // Amount of +2 turns consumables left
    
    
    public List<string> items;                  // inventory items
    public List<RawImage> icons;                // inventory icons
    
    public List<GameObject> combatEncounters;
    public List<GameObject> completedEncounters;

}
