using UnityEngine;

public class CollectItemScript : MonoBehaviour
{
    public int amount = 1;
    
    public void AddItem(string itemName)
    {
        Debug.Log(itemName);
        GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().AddItem(itemName, amount);
    }
}
