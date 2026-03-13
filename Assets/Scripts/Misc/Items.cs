using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public List<string> items = new List<string>();
    public List<RawImage> icons = new List<RawImage>();
    
    private string kirves;
    //public int _kirvesCount = 0;
    [SerializeField] private RawImage kirvesIcon;
    
    private string _key1;
    private int _key1Count;
    
    
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    

    public void ItemCount(string item)
    {
        
    }

    public void ItemIcon(int slot)
    {
        if (true) 
        {
            //slot.GetComponent<Image>().sprite = kirvesIcon.sprite;
            GameObject.Find("HotbarSlots").transform.GetChild(slot).GetComponent<RawImage>().texture = icons[slot].texture;

        }
    }
}
