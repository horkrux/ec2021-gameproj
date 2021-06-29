using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;

public class InventoryUI : MonoBehaviour
{
    public GameObject testest;
    public GameObject panel;
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

        panel.GetComponent<CanvasGroup>().alpha = 0.0f;

        System.Action<ITween<float>> updateAlphaVal = (t) =>
        {
            panel.GetComponent<CanvasGroup>().alpha = t.CurrentValue;
        };

        testest.Tween("FadeInPanel", 0.0f, 1.0f, 0.2f, TweenScaleFunctions.Linear, updateAlphaVal);

        testest.transform.position = new Vector3(testest.transform.position.x, testest.transform.position.y - 100, testest.transform.position.z);
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
        //testest.transform.position = new Vector3(testest.transform.position.x, testest.transform.position.y + 100, testest.transform.position.z);
        System.Action<ITween<float>> updateAlphaVal = (t) =>
        {
            panel.GetComponent<CanvasGroup>().alpha = t.CurrentValue;
        };

        System.Action<ITween<float>> fadeOutCompleted = (t) =>
        {
            gameObject.SetActive(false);
        };

        testest.Tween("FadeOutPanel", 1.0f, 0.0f, 0.2f, TweenScaleFunctions.Linear, updateAlphaVal, fadeOutCompleted);
        
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
