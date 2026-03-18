using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public List<string> requirements = new List<string>();
    public List<GameObject> combatRequirements = new List<GameObject>();
    
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
        
        
            if (requirements.Count > 0) {
                // Fade to black, instant transition to next camera


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

                foreach (string requirement in requirements) {
                    if (items.GetComponent<Items>().items.Contains(requirement)) {
                        Debug.Log("Requirement conditions met");
                        GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>().StartFade();
                        Invoke(nameof(NextCamera), 1f);

                        /*
                        transform.parent.parent.gameObject.SetActive(false);
                        nextCamera.SetActive(true);
                        GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().FindCurrentActiveScreen();
                        */
                    }

                    else {
                        Debug.Log("Requirement conditions not met");
                        break;
                    }



                }

            }
            
            else if (combatRequirements.Count > 0) 
            {
                foreach (GameObject combatEncounter in combatRequirements) 
                {
                    if (combatEncounter == null) 
                    {
                        Debug.Log("Requirement conditions met");
                        GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>().StartFade();
                        Invoke(nameof(NextCamera), 1f);
                    }
                    
                    else {
                        Debug.Log("Requirement conditions not met");
                        break;
                    }
                }
            }

            else {
                Debug.Log("No Requirements");
                GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>().StartFade();
                Invoke(nameof(NextCamera), 1f);

                /*
                transform.parent.parent.gameObject.SetActive(false);
                nextCamera.SetActive(true);
                GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().FindCurrentActiveScreen();
                */

            }

    }

    void NextCamera()
    {
        transform.parent.parent.gameObject.SetActive(false);
        nextCamera.SetActive(true);
        //nextCamera.GetComponent<CinemachineCamera>().ForceCameraPosition(nextCamera.transform.position);
        GameObject.FindGameObjectWithTag("ScreenList").GetComponent<ScreenListScript>().FindCurrentActiveScreen();
    }
}
