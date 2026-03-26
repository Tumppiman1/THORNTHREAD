using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask itemMask;
    private GameObject items;
    
    public string activeItem;
    
    
    void Start()
    {
        items = GameObject.FindGameObjectWithTag("Items");
    }

    public void UseItem(int slot)
    {
        if (items.GetComponent<Items>().items[slot] != null) 
        {
            activeItem = items.GetComponent<Items>().items[slot];
            Debug.Log(activeItem);
        }

        else {
            activeItem = null;
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
