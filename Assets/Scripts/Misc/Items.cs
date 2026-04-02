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
        if (items.Contains("bucket")) 
        {
            GameObject.Find("Screen_18_Ämpäri").gameObject.SetActive(false);
        }
        
        if (items.Contains("rope")) 
        {
            GameObject.Find("Screen_23d_automatic_combat_Köysi").gameObject.SetActive(false);
        }
        
        if (items.Contains("ropebucket")) 
        {
            GameObject.Find("Screen_18_Ämpäri").gameObject.SetActive(false);
            GameObject.Find("Screen_23d_automatic_combat_Köysi").gameObject.SetActive(false);
        }
        
        if (items.Contains("key1")) 
        {
            GameObject.Find("Screen_18_Ämpäri").gameObject.SetActive(false);
            GameObject.Find("Screen_23d_automatic_combat_Köysi").gameObject.SetActive(false);
        }
        
        if (items.Contains("lantern")) 
        {
            GameObject.Find("Screen_41_Lyhty_placeholder").gameObject.SetActive(false);
        }
        
        if (items.Contains("key2")) 
        {
            GameObject.Find("Screen_35e_Avain1_placeholder").gameObject.SetActive(false);
            GameObject.Find("Screen_41_Lyhty_placeholder").gameObject.SetActive(false);
        }
        
        if (items.Contains("key3")) 
        {
            GameObject.Find("Screen_37c_Avain2_placeholder (1)").gameObject.SetActive(false);
            GameObject.Find("Screen_41_Lyhty_placeholder").gameObject.SetActive(false);
        }
        
        
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
