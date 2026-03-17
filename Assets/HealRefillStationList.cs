using System.Collections.Generic;
using UnityEngine;

public class HealRefillStationList : MonoBehaviour
{
    public List<GameObject> healRefillStations = new List<GameObject>();
    public List<GameObject> emptyHealRefillStations = new List<GameObject>();
    
    void Start()
    {
        DeactivateEmptyStations();
    }

    public void EmptyHealRefillStations(GameObject healRefillStation)
    {
        if (healRefillStations.Contains(healRefillStation)) 
        {
            healRefillStations.Remove(healRefillStation);
            emptyHealRefillStations.Add(healRefillStation);
            DeactivateEmptyStations();
        }
    }

    public void DeactivateEmptyStations()
    {
        if (emptyHealRefillStations.Count > 0) 
        {
            foreach (GameObject station in emptyHealRefillStations) 
            {
                station.gameObject.SetActive(false);    
            }
        }
    }
    
}
