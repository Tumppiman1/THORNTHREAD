using UnityEngine;

public class StartCombatEncounter : MonoBehaviour
{
    public void ActivateCombatEncounter()
    {
        if (GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>() != null) 
        {
            GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().enabled = true;
        }

        else {
            Debug.Log("No CombatEncounter found");
        }
    }

    
}
