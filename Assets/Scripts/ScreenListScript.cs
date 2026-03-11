using System.Collections.Generic;
using UnityEngine;

public class ScreenListScript : MonoBehaviour
{
    public List<GameObject> screenList = new List<GameObject>();
    public GameObject currentScreen;
    void Start()
    {

        if (currentScreen == null) 
        {
            currentScreen = screenList[0];
            screenList[0].gameObject.SetActive(true);
        }
        
        else {
            DeactivateScreens();

            if (screenList.Contains(currentScreen)) {
                currentScreen.gameObject.SetActive(true);
            }
        }
        
    }

    public void FindCurrentActiveScreen()
    {
        foreach (GameObject screen in screenList) 
        {
            if (screen.activeInHierarchy) 
            {
                currentScreen = screen;
            }
        }
    }

    public void DeactivateScreens()
    {
        foreach (GameObject screen in screenList) 
        {
            if (screen.activeInHierarchy) 
            {
                screen.SetActive(false);
            }
        }
    }
    
}
