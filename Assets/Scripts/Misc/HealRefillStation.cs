using System;
using UnityEngine;

public class HealRefillStation : MonoBehaviour
{
    public int refillsLeft;
    
    public void Refill()
    {
        if (refillsLeft > 0 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().healFlaskUsesLeft < 2) 
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().RefillHealFlask();
            refillsLeft--;
            GameObject.Find("HealRefillStationList").GetComponent<HealRefillStationList>().HealStationUses(transform.parent.transform.parent.gameObject);
            

            if (refillsLeft == 0) 
            {   
                transform.parent.parent.gameObject.SetActive(false);
            }
        }
        
    }
}
