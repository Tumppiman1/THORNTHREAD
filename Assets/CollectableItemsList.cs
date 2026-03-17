using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemsList : MonoBehaviour
{
    public List<GameObject> collectableItems = new List<GameObject>();
    public List<GameObject> collectedItems = new List<GameObject>();

    private void Start()
    {
        LoadCollectableItems();
    }

    public void ItemCollected(GameObject item)
    {
        if (collectableItems.Contains(item)) 
        {
            collectableItems.Remove(item);
            collectedItems.Add(item);
            LoadCollectableItems();
        }
    }

    public void LoadCollectableItems()
    {
        if (collectableItems.Count > 0 | collectedItems.Count > 0) 
        {
            foreach (GameObject item in collectedItems) 
            {
                Destroy(item);
            }
        }
    }
}
