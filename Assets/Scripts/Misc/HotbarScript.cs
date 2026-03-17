using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarScript : MonoBehaviour
{
    public List<string> items;
    private GameObject _inventoryController;
    
    void Start()
    {
        items = GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().items;
        _inventoryController = GameObject.FindGameObjectWithTag("Items");
        
        UpdateHotbar();
    }

    
    void Update()
    {
        
    }

    public void UpdateHotbar()
    {
        // Updates hotbar icons when new items are added to items list
        if (items.Count > 0) {
            for (int i = 0; i < items.Count; i++) 
            {
                //Debug.Log(i);
                
                // Gets item icon from inventory controller
                _inventoryController.GetComponent<Items>().ItemIcon(i);
            }
        }

    }
    
    
}
