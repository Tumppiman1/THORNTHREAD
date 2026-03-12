using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public List<string> items = new List<string>();
    public List<RawImage> icons = new List<RawImage>();
    
    private string _kirves;
    private int _kirvesCount = 0;
    [SerializeField] private RawImage kirvesIcon;
    
    private string _key1;
    private int _key1Count;
    
    
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void AddItem(string itemName, int count)
    {
        
        if (itemName == _kirves) 
        {
            
            items.Add(_kirves);
            icons.Add(kirvesIcon);
            _kirvesCount += count;
        }
        
        
        else 
        {
            Debug.Log("Kirves");
            _kirvesCount += count;
        }
        
    }

    public void ItemCount(string item)
    {
        
    }

    public void ItemIcon(int slot)
    {
        if (slot == 0) 
        {
            //slot.GetComponent<Image>().sprite = kirvesIcon.sprite;
            GameObject.Find("HotbarSlots").transform.GetChild(0).GetComponent<RawImage>().texture = icons[slot].texture;

        }
    }
}
