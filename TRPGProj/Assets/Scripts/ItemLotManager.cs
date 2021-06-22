using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLotManager : MonoBehaviour
{
    Dictionary<int, Item> itemList = new Dictionary<int, Item>();
    Dictionary<int, List<Tuple<int, int>>> itemLots = new Dictionary<int, List<Tuple<int, int>>>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(int id, Item item)
    {
        itemList.Add(id, item);
    }

    public void AddItemLot(int id, List<Tuple<int, int>> itemLot)
    {
        itemLots.Add(id, itemLot);
    }

    public List<Tuple<int, int>> getItemLotItems(int itemLotId)
    {
        if (itemLots.ContainsKey(itemLotId))
            return itemLots[itemLotId];
        else
            return new List<Tuple<int, int>>();
    }

    public Item getItem(int itemId)
    {
        if (itemList.ContainsKey(itemId))
            return itemList[itemId];
        else
            return null;
    }
}
