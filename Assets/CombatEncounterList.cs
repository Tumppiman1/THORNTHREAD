using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatEncounterList : MonoBehaviour
{
    public List<GameObject> combatEncounters = new List<GameObject>();
    public List<GameObject> completedEncounters = new List<GameObject>();

    void Start()
    {
        LoadCombatEncounters();
    }

    public void RemoveCompletedCombatEncounters()
    {
        foreach (GameObject combatEncounter in combatEncounters.ToList()) 
        {
            if (combatEncounter.GetComponent<CombatManager>().combatEncounterCompleted == true) 
            {
                if (combatEncounters.Contains(combatEncounter)) 
                {
                    completedEncounters.Add(combatEncounter);
                    combatEncounters.Remove(combatEncounter);
                }
            }
        }
    }

    public void LoadCombatEncounters()
    {
        if (completedEncounters.Count > 0) 
        {
            foreach (GameObject combatEncounter in completedEncounters.ToList()) 
            {
                if (!combatEncounters.Contains(combatEncounter)) 
                {
                    Destroy(combatEncounter.gameObject);
                }
            }
        }
    }
    
    
}
