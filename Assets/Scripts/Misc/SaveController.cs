using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string _saveLocation;
    
    void Awake()
    {
        _saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
         LoadGame();
         
    }


    public void SaveGame()
    {
        Debug.Log("Game saved");
        GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().FindCurrentActiveScreen();
            
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().currentScreen,
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().health,
            
            playerHasAxe = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().playerHasAxe,
            playerHasShield = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().playerHasShield,
            playerHasMace = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().playerHasMace,
            
            healFlaskUsesLeft = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().healFlaskUsesLeft,
            attackPointConsumableAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().attackPointConsumableAmount,
            addTurnsConsumableAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().addTurnsConsumableAmount,
            damageOvertimeConsumableAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().damageOvertimeConsumableAmount,
            
            
            items = GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().items,
            icons = GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().icons,
            
            combatEncounters = GameObject.FindGameObjectWithTag("CombatEncounterList").GetComponent<CombatEncounterList>().combatEncounters,
            completedEncounters = GameObject.FindGameObjectWithTag("CombatEncounterList").GetComponent<CombatEncounterList>().completedEncounters,
            
            collectableItems = GameObject.FindGameObjectWithTag("CollectableItemsList").GetComponent<CollectableItemsList>().collectableItems,
            collectedItems = GameObject.FindGameObjectWithTag("CollectableItemsList").GetComponent<CollectableItemsList>().collectedItems,
            
            healRefillStations = GameObject.Find("HealRefillStationList").GetComponent<HealRefillStationList>().healRefillStations,
            healRefillStationsUsesLeft = GameObject.Find("HealRefillStationList").GetComponent<HealRefillStationList>().healRefillStationsUsesLeft,
            
            treePuzzleCompleted = GameObject.FindGameObjectWithTag("Items").GetComponent<ItemInteraction>().treePuzzleCompleted,
            bushPuzzleCompleted = GameObject.FindGameObjectWithTag("Items").GetComponent<ItemInteraction>().bushPuzzleCompleted,
            shovelPuzzleCompleted = GameObject.FindGameObjectWithTag("Items").GetComponent<ItemInteraction>().shovelPuzzleCompleted,
            
        };
        
        File.WriteAllText(_saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        
        
        if (File.Exists(_saveLocation)) 
        {
            Debug.Log("Game loaded");
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_saveLocation));
            
            GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().currentScreen = saveData.playerPosition;
            GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().UpdateScreen();
            
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().health = saveData.playerHealth;
            
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().playerHasAxe = saveData.playerHasAxe;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().playerHasShield = saveData.playerHasShield;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().playerHasMace = saveData.playerHasMace;
            
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().healFlaskUsesLeft = saveData.healFlaskUsesLeft;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().attackPointConsumableAmount = saveData.attackPointConsumableAmount;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().addTurnsConsumableAmount = saveData.addTurnsConsumableAmount;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().damageOvertimeConsumableAmount = saveData.damageOvertimeConsumableAmount;
                
            GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().items = saveData.items;
            GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().icons = saveData.icons;
            
            GameObject.FindGameObjectWithTag("CombatEncounterList").GetComponent<CombatEncounterList>().combatEncounters = saveData.combatEncounters;
            GameObject.FindGameObjectWithTag("CombatEncounterList").GetComponent<CombatEncounterList>().completedEncounters = saveData.completedEncounters;
            
            GameObject.FindGameObjectWithTag("CollectableItemsList").GetComponent<CollectableItemsList>().collectableItems = saveData.collectableItems;
            GameObject.FindGameObjectWithTag("CollectableItemsList").GetComponent<CollectableItemsList>().collectedItems = saveData.collectedItems;
            
            GameObject.Find("HealRefillStationList").GetComponent<HealRefillStationList>().healRefillStations = saveData.healRefillStations;
            GameObject.Find("HealRefillStationList").GetComponent<HealRefillStationList>().healRefillStationsUsesLeft = saveData.healRefillStationsUsesLeft;

            GameObject.FindGameObjectWithTag("Items").GetComponent<ItemInteraction>().treePuzzleCompleted = saveData.treePuzzleCompleted;
            GameObject.FindGameObjectWithTag("Items").GetComponent<ItemInteraction>().bushPuzzleCompleted = saveData.bushPuzzleCompleted;
            GameObject.FindGameObjectWithTag("Items").GetComponent<ItemInteraction>().shovelPuzzleCompleted = saveData.shovelPuzzleCompleted;



        }
        else 
        {
            Debug.Log("Save file created");
            SaveGame();
            
        }
    }

    public void DeleteSave()
    {
        try {
            if (File.Exists(_saveLocation)) {
                File.Delete(_saveLocation);
                Debug.Log("Save file deleted");
            }

            else {
                Debug.Log("No save file found");
            }
        }

        catch (DirectoryNotFoundException)
        {
            Debug.Log("File not found");
        }

    }
}
