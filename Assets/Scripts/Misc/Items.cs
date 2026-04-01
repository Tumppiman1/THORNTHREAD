using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public List<string> items = new List<string>();
    public List<RawImage> icons = new List<RawImage>();
    public RawImage defaultIcon;
    
    
    
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
            // GameObject.Find("HotbarSlots").transform.GetChild(slot).GetComponent<Button>().colors.normalColor = defaultIcon.colors.normalColor;
            // GameObject.Find("HotbarSlots").transform.GetChild(slot).GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
            GameObject.Find("HotbarSlots").transform.GetChild(slot).GetComponent<RawImage>().color = icons[slot].color;
            GameObject.Find("HotbarSlots").transform.GetChild(slot).GetComponent<RawImage>().texture = icons[slot].texture;
            GameObject.Find("HotbarSlots").transform.GetChild(slot).GetComponent<Button>().interactable = true;

        }
    }

    public void DefaultIcon(int slot)
    {
        GameObject.Find("HotbarSlots").transform.GetChild(slot).GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
        GameObject.Find("HotbarSlots").transform.GetChild(slot).GetComponent<RawImage>().texture = defaultIcon.texture;
        GameObject.Find("HotbarSlots").transform.GetChild(slot).GetComponent<Button>().interactable = false;
    }
    
}
