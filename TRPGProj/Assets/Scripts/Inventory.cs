using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private Dictionary<int, int> _items;

    public Dictionary<int, int> Items
    {
        get { return _items; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _items = new Dictionary<int, int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(int itemId, int quantity)
    {
        if (_items.ContainsKey(itemId))
        {
            _items[itemId] += quantity;
        }
        else
        {
            _items.Add(itemId, quantity);
        }
    }

    public void RemoveItem(int itemId)
    {
        if (_items.ContainsKey(itemId))
        {
            _items[itemId]--;

            if (_items[itemId] <= 0)
                _items.Remove(itemId);
        }
    }

    public bool HasItem(int itemId)
    {
        return _items.ContainsKey(itemId);
    }
}
