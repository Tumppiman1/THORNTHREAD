using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask itemMask;
    private GameObject items;
    
    public string activeItem = null;

    public RawImage ropeBucket;
    
    [Header("Puzzles")]
    public GameObject treePuzzle;
    public bool treePuzzleCompleted = false;
    
    public bool wellPuzzleCompleted = false;
    
    public bool bridgePuzzleCompleted = false;
    
    public GameObject bushPuzzle;
    public bool bushPuzzleCompleted = false;
    
    public GameObject shovelPuzzle;
    public bool shovelPuzzleCompleted = false;
    
    
    public bool casteLock1pened = false;
    public bool casteLock2pened = false;
    public bool castleLockPuzzleCompleted = false;

    public RawImage key1Icon;
    public RawImage key2Icon;
    public RawImage key3Icon;
    
    void Start()
    {
        activeItem = null;
        items = GameObject.FindGameObjectWithTag("Items");

        if (treePuzzleCompleted) 
        {
            treePuzzle.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("TreePuzzle").transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        }

        if (bushPuzzleCompleted) 
        {
            bushPuzzle.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("BushPuzzle").transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        }

        if (shovelPuzzleCompleted) 
        {
            shovelPuzzle.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }

        
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
                    Debug.Log(activeItem + " and " + itemToCombine + " combined");

                    if (GetComponent<Items>().items.Contains(activeItem) && GetComponent<Items>().items.Contains(itemToCombine)) 
                    {
                        int activeItemIndex = GetComponent<Items>().items.IndexOf(activeItem);
                        
                        GetComponent<Items>().icons.RemoveAt(activeItemIndex);
                        GetComponent<Items>().items.Remove(activeItem);
                        
                        int itemToCombineIndex = GetComponent<Items>().items.IndexOf(itemToCombine);
                        
                        GetComponent<Items>().icons.RemoveAt(itemToCombineIndex);
                        GetComponent<Items>().items.Remove(itemToCombine);
                        
                        GetComponent<Items>().items.Add("RopeBucket");
                        GetComponent<Items>().icons.Add(ropeBucket);
                    }
                    
                    GameObject.Find("HotbarSlots").GetComponent<HotbarScript>().UpdateHotbar();
                }
                
                else if (activeItem == "bucket" && itemToCombine == "rope") 
                {
                    Debug.Log(activeItem + " and " + itemToCombine + " combined");

                    if (GetComponent<Items>().items.Contains(activeItem) && GetComponent<Items>().items.Contains(itemToCombine)) 
                    {
                        int activeItemIndex = GetComponent<Items>().items.IndexOf(activeItem);
                        
                        GetComponent<Items>().icons.RemoveAt(activeItemIndex);
                        GetComponent<Items>().items.Remove(activeItem);
                        
                        int itemToCombineIndex = GetComponent<Items>().items.IndexOf(itemToCombine);
                        
                        GetComponent<Items>().icons.RemoveAt(itemToCombineIndex);
                        GetComponent<Items>().items.Remove(itemToCombine);
                        
                        GetComponent<Items>().items.Add("ropebucket");
                        GetComponent<Items>().icons.Add(ropeBucket);
                    }
                    
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
                            treePuzzleCompleted = true;

                        }
                    }

                    if (hit.collider.gameObject.layer == 16) 
                    {
                        if (activeItem == "lantern" && hit.collider.gameObject.layer == 16) 
                        {
                            hit.collider.gameObject.SetActive(false);
                            hit.collider.gameObject.transform.parent.transform.GetChild(1).gameObject.SetActive(true);
                            
                            GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().items.Add("key2");
                            GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().icons.Add(key1Icon);
                            
                            bushPuzzleCompleted = true;
                        }
                    }
                    
                    if (hit.collider.gameObject.layer == 17) 
                    {
                        if (activeItem == "shovel" && hit.collider.gameObject.layer == 17) 
                        {
                            hit.collider.gameObject.SetActive(false);
                            //hit.collider.gameObject.transform.parent.transform.GetChild(1).gameObject.SetActive(true);
                            shovelPuzzleCompleted = true;
                        }
                    }

                    if (hit.collider.gameObject.layer == 18) 
                    {
                        if (activeItem == "ropebucket" && hit.collider.gameObject.layer == 18) 
                        {
                            if (!GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().items.Contains("key1")) 
                            {
                                GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().items.Add("key1");
                                GameObject.FindGameObjectWithTag("Items").GetComponent<Items>().icons.Add(key1Icon);
                                
                                GameObject.Find("HotbarSlots").GetComponent<HotbarScript>().UpdateHotbar();
                                wellPuzzleCompleted = true;
                                Debug.Log("Well puzzle completed");
                            }
                        }
                    }

                    if (hit.collider.gameObject.layer == 19) 
                    {
                        if (activeItem == "key1" && hit.collider.gameObject.layer == 19) 
                        {
                            // Open bridge lock
                            bridgePuzzleCompleted = true;
                        }
                    }
                    
                    if (hit.collider.gameObject.layer == 20) 
                    {
                        if (activeItem == "key2" && hit.collider.gameObject.layer == 20) 
                        {
                            // Open castle lock #1
                            
                        }
                        
                        if (!castleLockPuzzleCompleted && casteLock1pened && casteLock2pened) 
                        {
                            castleLockPuzzleCompleted = true;
                        }
                    }

                    if (hit.collider.gameObject.layer == 21) 
                    {
                        if (activeItem == "key3" && hit.collider.gameObject.layer == 21) 
                        {
                            // Open castle lock #2
                        }

                        if (!castleLockPuzzleCompleted && casteLock1pened && casteLock2pened) 
                        {
                            castleLockPuzzleCompleted = true;
                        }
                            
                            
                    }
                    
                    
                    
                    activeItem = null;
                    
                }
                
                
            }
        }
    }
}
