using System.Collections.Generic;
using UnityEngine;

public class ScreenListScript : MonoBehaviour
{
    public List<GameObject> screenList = new List<GameObject>();
    public int currentScreen = 0;
    void Start()
    {
        
        /*
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
        }*/

        
        screenList[currentScreen].gameObject.SetActive(true);
        
        
        Debug.Log("Screen list loaded");
        
    }

    public void UpdateScreen()
    {
        screenList[currentScreen].gameObject.SetActive(true);
    }

    /*
    public void FindCurrentActiveScreen()
    {
        foreach (GameObject screen in screenList) 
        {
            if (screen.activeInHierarchy) 
            {
                screen
            }
        }
    }*/
    
    public void FindCurrentActiveScreen()
    {
        foreach (GameObject screen in screenList) 
        {
            if (screen.activeInHierarchy) 
            {
                currentScreen =  screen.transform.GetSiblingIndex();
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
