using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DigitalRuby.Tween;

public class ItemPopup : MonoBehaviour
{
    public ItemLotManager itemLotMan;
    private ItemList scrollViewContent;
    public ItemButton buttonPrefab;
    public xbot playerModel;

    // Start is called before the first frame update
    void Start()
    {
        scrollViewContent = GetComponentInChildren<ItemList>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(List<Tuple<int, int>> items)
    {
        scrollViewContent = GetComponentInChildren<ItemList>();

        foreach (Tuple<int, int> itemPair in items)
        {
            int itemId = itemPair.Item1;
            int itemQuantity = itemPair.Item2;
            Item item = itemLotMan.getItem(itemId);

            ItemButton itemButton = Instantiate(buttonPrefab);
            itemButton.GetComponentsInChildren<TextMeshProUGUI>()[0].text = item.DisplayNameText;
            itemButton.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "x" + itemQuantity;
            

            itemButton.transform.SetParent(scrollViewContent.transform);
            itemButton.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 0); //WHYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY
        }

        gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
        gameObject.SetActive(true);

        System.Action<ITween<float>> updateAlphaVal = (t) =>
        {
            gameObject.GetComponent<CanvasGroup>().alpha = t.CurrentValue;
        };

        gameObject.Tween("MovePanelUp", 0.0f, 1.0f, 0.3f, TweenScaleFunctions.Linear, updateAlphaVal);
    }

    public void closeOnClick()
    {
        foreach(ItemButton itemButton in scrollViewContent.GetComponentsInChildren<ItemButton>())
        {
            Destroy(itemButton.gameObject);
        }

        playerModel.GetComponent<Animator>().SetBool("IsPickup", false);

        System.Action<ITween<float>> updateAlphaVal = (t) =>
        {
            gameObject.GetComponent<CanvasGroup>().alpha = t.CurrentValue;
        };

        System.Action<ITween<float>> fadeOutCompleted = (t) =>
        {
            gameObject.SetActive(false);
        };

        gameObject.Tween("MovePanelUp", 1.0f, 0.0f, 0.3f, TweenScaleFunctions.Linear, updateAlphaVal, fadeOutCompleted);

    }
}
