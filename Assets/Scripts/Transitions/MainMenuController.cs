using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenuController : MonoBehaviour
{

    [SerializeField] private string gameScene;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambientSlider;
    
    private string _saveLocation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        

    }
     private void Start()
    {
        var data = AudioSaveManager.instance.Data;

        masterSlider.value = data.masterVolume;
        musicSlider.value = data.musicVolume;
        sfxSlider.value = data.sfxVolume;
        ambientSlider.value = data.ambVolume;
        
        _saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }
    public void StartGame()
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
        
        
        SceneManager.LoadSceneAsync(gameScene);
       
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(gameScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
