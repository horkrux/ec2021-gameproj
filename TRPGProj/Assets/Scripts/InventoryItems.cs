using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class InventoryItems : MonoBehaviour
{
    public ItemButton prefab;
    public ItemLotManager itemLotMan;
    List<ItemButton> items = new List<ItemButton>();
    public bool changed = false;
    private int _selectedItemId = -1;
    public int SelectedItemId
    {
        get { return _selectedItemId; }
    }

    public bool enableUse = false;
    public bool enableRemove = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Populate(Dictionary<int, int> inventoryItems)
    {
        int counter = 0;
        bool firstSet = false;
        int firstItemId = -1;
        //UnityEngine.Events.UnityAction[] shits = { delegate { ItemOnClick(0); }, delegate { ItemOnClick(1); } };

        foreach (int itemId in inventoryItems.Keys)
        {
            int buttonId = counter;
            int buttonItemId = itemId;
            ItemButton itemButton = Instantiate(prefab, transform);

            if (!firstSet)
            {
                firstItemId = itemId;
                firstSet = true;
            }
            //Button shit = itemButton.gameObject.GetComponent<Button>();
            //TextMeshProUGUI tmpshit = shit.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            //tmpshit.SetText("all dirty");
            //itemButton.gameObject.GetComponent<Button>().gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText(itemLotMan.getItem(itemId).DisplayNameText);
            itemButton.gameObject.GetComponent<Button>().gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].SetText(itemLotMan.getItem(itemId).DisplayNameText);
            itemButton.gameObject.GetComponent<Button>().gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1].SetText("x" + inventoryItems[itemId]);
            itemButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ItemOnClick(buttonId, buttonItemId); });
            items.Add(itemButton);
            counter++;

        }

        if (counter > 0)
        {
            items[0].GetComponent<Button>().Select();
            ItemOnClick(0, firstItemId);

        }
        else
        {
            enableUse = false;
            enableRemove = false;
        }
            

        changed = true;
    }

    

    public void ClearList()
    {
        foreach (ItemButton item in items)
        {
            Destroy(item.gameObject);
        }

        items.Clear();
        //foreach (ItemButton item in gameObject.GetComponentsInChildren<ItemButton>())
        //{
        //    Destroy(item);
        //}
    }

    public void ItemOnClick(int buttonId, int itemId)
    {
        _selectedItemId = itemId;
        changed = true;

        CommonEnums.ItemCategory cat = itemLotMan.getItem(itemId).Category;

        enableRemove = true;

        if (cat == CommonEnums.ItemCategory.Consumable)
        {
            enableUse = true;
            
        }
        else if (cat == CommonEnums.ItemCategory.Key)
        {
            enableRemove = false;
        }

        
    }
}
