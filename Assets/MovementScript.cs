using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public List<string> requirements = new List<string>();
    private GameObject items;

    public GameObject nextCamera;

    void Start()
    {
        items = GameObject.FindGameObjectWithTag("Items");
        
        if (nextCamera == null) 
        {
            this.gameObject.SetActive(false);
        }
    }

    

    public void Movement()
    {
        if (requirements.Count > 0) 
        {
            /*
            if (items.GetComponent<Items>().items.Contains(requirements[0])) 
            {
                Debug.Log("Requirement conditions met");
                transform.parent.parent.gameObject.SetActive(false);
                nextCamera.SetActive(true);
                
            }
            
            else {
                Debug.Log("Requirement conditions not met");
            }
            */

            foreach (string requirement in requirements) 
            {
                if (items.GetComponent<Items>().items.Contains(requirement)) 
                {
                    Debug.Log("Requirement conditions met");
                    transform.parent.parent.gameObject.SetActive(false);
                    nextCamera.SetActive(true);
                    GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().FindCurrentActiveScreen();
                }

                else {
                    Debug.Log("Requirement conditions not met");
                    break;
                }
                
                
                
            }
            
        }

        else {
            Debug.Log("No Requirements");
            transform.parent.parent.gameObject.SetActive(false);
            nextCamera.SetActive(true);
            GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().FindCurrentActiveScreen();
        }
        
    }
}
