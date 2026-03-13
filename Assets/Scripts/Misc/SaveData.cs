using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveData
{
    public GameObject playerPosition;   // Active camera
    public bool playerHasAxe;           // Player has collected axe
    public bool playerHasShield;        // Player has collected shield
    
    public List<string> items;          // inventory items
    public List<RawImage> icons;        // inventory icons
    
    

}
