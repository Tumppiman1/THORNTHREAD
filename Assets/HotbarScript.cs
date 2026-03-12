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
    }

    
    void Update()
    {
        
    }

    public void UpdateHotbar()
    {
        for (int i = 0; i <= items.Count; i++) 
        {
            Debug.Log(i);
            _inventoryController.GetComponent<Items>().ItemIcon(i);
        }
        
    }
    
    
}
