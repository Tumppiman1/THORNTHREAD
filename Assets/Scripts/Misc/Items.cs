using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public List<string> items = new List<string>();
    public List<RawImage> icons = new List<RawImage>();
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ItemIcon(int slot)
    {
        if (true) 
        {
            // Sets icons for hotbar slots from icons list
            GameObject.Find("HotbarSlots").transform.GetChild(slot).GetComponent<RawImage>().texture = icons[slot].texture;

        }
    }
    
}
