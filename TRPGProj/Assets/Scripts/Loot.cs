using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public int itemLotId;
    public ItemLotManager itemLotMan;
    public ItemPopup itemPopup;
    public Texture2D mouse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pickUp(Inventory inventory)
    {
        List<Tuple<int, int>> items = itemLotMan.getItemLotItems(itemLotId);

        foreach (Tuple<int, int> item in items)
        {
            inventory.AddItem(item.Item1, item.Item2);
        }

        itemPopup.Show(items);

        gameObject.SetActive(false);
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(mouse, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
