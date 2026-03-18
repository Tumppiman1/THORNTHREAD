using UnityEngine;

public class SavePointScript : MonoBehaviour
{
    
    void Start()
    {
        GameObject.FindGameObjectWithTag("SaveController").GetComponent<SaveController>().SaveGame();
    }

    
}
