using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask itemMask;
    private GameObject items;
    
    public string activeItem = null;
    
    
    void Start()
    {
        activeItem = null;
        items = GameObject.FindGameObjectWithTag("Items");
    }

    public void UseItem(int slot)
    {
        if (string.IsNullOrEmpty(activeItem)) 
        {
            if (items.GetComponent<Items>().items[slot] != null) {
                activeItem = items.GetComponent<Items>().items[slot];
                Debug.Log(activeItem);
            }

            else {
                activeItem = null;
            }
        }

        else {
            Debug.Log("here");

            
            if (items.GetComponent<Items>().items[slot] != null) 
            {
                Debug.Log(items.GetComponent<Items>().items[slot]);
                
                string itemToCombine = items.GetComponent<Items>().items[slot];

                if (activeItem == "rope" && itemToCombine == "bucket") 
                {
                    Debug.Log("items combined");
                    int activeItemIndex = GetComponent<Items>().items.IndexOf(activeItem);
                    
                    GetComponent<Items>().icons.RemoveAt(activeItemIndex);
                    GetComponent<Items>().items.RemoveAt(activeItemIndex);
                    
                    GameObject.Find("HotbarSlots").GetComponent<HotbarScript>().UpdateHotbar();
                }

                else 
                {
                    activeItem = itemToCombine;
                }
            }
        }
    }
    
    
    void Update()
    {
        if (activeItem != null) 
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, itemMask)) 
            {
                if (Input.GetKeyDown(KeyCode.Mouse0)) 
                {
                    // hit = hit.collider.gameObject;
                    if (hit.collider.gameObject.layer == 15) 
                    {   
                        // Debug.Log("Tree" + hit.collider.gameObject.name);

                        if (activeItem == "kirves" && hit.collider.gameObject.layer == 15) 
                        {
                            // Debug.Log(activeItem + hit.collider.gameObject.layer);
                            hit.collider.gameObject.SetActive(false);
                            hit.collider.gameObject.transform.parent.transform.GetChild(1).gameObject.SetActive(true);
                            
                        }
                    }
                    
                    
                    activeItem = null;
                    
                }
                
                
            }
        }
    }
}
