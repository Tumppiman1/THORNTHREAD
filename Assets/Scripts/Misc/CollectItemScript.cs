using UnityEngine;
using UnityEngine.UI;

public class CollectItemScript : MonoBehaviour
{
    public string itemName;
    public int amount = 1;
    public RawImage itemIcon;
    
    
    public void AddItem()
    {
        
        //GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().AddItem(itemName, amount);
        
        // Add item to inventory list if it doesnt already exist
        if (!GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().items.Contains(itemName)) 
        {
            Debug.Log("Added to inventory: " + itemName);
            GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().items.Add(itemName);
            GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().icons.Add(itemIcon);
            GameObject.Find("InventoryStuff").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<HotbarScript>().UpdateHotbar();
            
        }
        else {
            Debug.Log(itemName + " already exists");
        }
    }
}
