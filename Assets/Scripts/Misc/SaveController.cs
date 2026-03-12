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
        GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().FindCurrentActiveScreen();
            
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().currentScreen
        };
        
        File.WriteAllText(_saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(_saveLocation)) 
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_saveLocation));
            
            GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().currentScreen = saveData.playerPosition;
        }
        else 
        {
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
