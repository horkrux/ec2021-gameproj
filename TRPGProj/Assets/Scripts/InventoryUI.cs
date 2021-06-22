using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public InventoryItems items;
    public PlayerCharacter player;
    private Inventory inventory;
    public Text itemDesc;
    public ItemLotManager itemLotMan;

    public Button closeButton;
    public  Button useButton;
    public Button removeButton;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        if (inventory == null)
            inventory = player.GetComponent<Inventory>();

        items.Populate(inventory.Items);
    }

    void OnDisable()
    {
        items.ClearList();
    }

    // Update is called once per frame
    void Update()
    {
        if (items.changed && inventory?.Items.Count > 0)
        {
            itemDesc.text = itemLotMan.getItem(items.SelectedItemId).DescriptionText;
            items.changed = false;
        }

        if (items.enableUse && !useButton.enabled)
            useButton.enabled = true;
        else if (!items.enableUse && useButton.enabled)
            useButton.enabled = false;

        if (items.enableRemove && !removeButton.enabled)
        {
            removeButton.enabled = true;
        }
        else if (!items.enableRemove && removeButton.enabled)
        {
            removeButton.enabled = false;
        }
        
    }

    public void CloseOnClick()
    {
        gameObject.SetActive(false);
        //items.ClearList();
    }

    public void UseItemOnClick()
    {
        ((Consumable)itemLotMan.getItem(items.SelectedItemId)).Use(player);
        inventory.RemoveItem(items.SelectedItemId);
        items.ClearList();
        PopulateInventoryItems();
    }

    public void ThrowAwayItemOnClick()
    {
        inventory.RemoveItem(items.SelectedItemId);
        items.ClearList();
        PopulateInventoryItems();
    }



    public void PopulateInventoryItems()
    {
        items.Populate(inventory.Items);
    }
}
