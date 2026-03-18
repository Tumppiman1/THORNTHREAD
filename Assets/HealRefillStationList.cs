using System.Collections.Generic;
using UnityEngine;

public class HealRefillStationList : MonoBehaviour
{
    public List<GameObject> healRefillStations = new List<GameObject>();
    public List<int> healRefillStationsUsesLeft = new List<int>();
    
    //public List<GameObject> emptyHealRefillStations = new List<GameObject>();
    
    void Start()
    {
        DeactivateEmptyStations();
    }

    public void EmptyHealRefillStations(GameObject healRefillStation)
    {
        if (healRefillStations.Contains(healRefillStation)) 
        {
            healRefillStations.Remove(healRefillStation);
            //emptyHealRefillStations.Add(healRefillStation);
            DeactivateEmptyStations();
        }
    }
    
    public void DeactivateEmptyStations()
    {
        foreach (int station in healRefillStationsUsesLeft) 
        {
            if (station == 0) 
            {
                GameObject emptyStation = healRefillStations[station];
                emptyStation.SetActive(false);
            }    
        }
    }

    public void HealStationUses(GameObject healRefillStation)
    {
        
        
        foreach (GameObject station in healRefillStations) 
        {
            Debug.Log(healRefillStations.IndexOf(station));

            if (healRefillStations[healRefillStations.IndexOf(station)].gameObject == healRefillStation) 
            {
                healRefillStationsUsesLeft[healRefillStations.IndexOf(station)] = healRefillStation.transform.GetChild(0).transform.GetChild(0).GetComponent<HealRefillStation>().refillsLeft;
            }
        }
    }
    
    /*
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
    */
    
}
