using System;
using UnityEngine;

public class HealRefillStation : MonoBehaviour
{
    public int refillsLeft;

    private void Start()
    {
        
    }

    public void Refill()
    {
        if (refillsLeft > 0 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().healFlaskUsesLeft < 2) 
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().RefillHealFlask();
            refillsLeft--;

            if (refillsLeft == 0) 
            {   
                transform.parent.parent.gameObject.SetActive(false);
                GameObject.Find("HealRefillStationList").GetComponent<HealRefillStationList>().EmptyHealRefillStations(transform.parent.parent.gameObject);
            }
        }
        
        
    }
}
