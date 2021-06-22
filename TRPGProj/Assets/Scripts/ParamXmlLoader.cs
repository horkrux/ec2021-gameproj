using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System;

public class ParamXmlLoader : MonoBehaviour
{
    public ItemLotManager itemLotMan;

    // Start is called before the first frame update
    void Start()
    {
        LoadItemParams();
        LoadItemLotParams();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadItemParams()
    {
        XDocument doc = XDocument.Load("Assets/Params/ItemParam.txt");
        XDocument itemDescDoc = XDocument.Load("Assets/Text/ItemDesc.txt");

        XElement root = doc.Root;
        XElement rootDesc = itemDescDoc.Root;

        Dictionary<int, Tuple<string, string>> itemTextDict = new Dictionary<int, Tuple<string, string>>();

        foreach (XElement itemDesc in rootDesc.Elements())
        {
            itemTextDict.Add(int.Parse(itemDesc.Attribute("id").Value), new Tuple<string, string>(itemDesc.Element("name").Value, itemDesc.Element("desc").Value));
        }
        
        foreach (XElement item in root.Elements())
        {
            int itemId = int.Parse(item.Attribute("id").Value);
            string internalName = item.Attribute("internalName").Value;
            //int displayNameTextId = int.Parse(item.Attribute("displayNameTextId").Value);
            //int descriptionTextId = int.Parse(item.Attribute("descriptionTextId").Value);
            int itemEffectId = int.Parse(item.Attribute("itemEffectId").Value);
            int iconId = int.Parse(item.Attribute("iconId").Value);
            bool isConsumable = Convert.ToBoolean(int.Parse(item.Attribute("isConsumable").Value));
            bool isKey = Convert.ToBoolean(int.Parse(item.Attribute("isKey").Value));


            if (isConsumable)
            {
                Consumable consumableItem = new Consumable(internalName, CommonEnums.ItemCategory.Consumable, itemTextDict[itemId].Item1, itemTextDict[itemId].Item2, iconId, isKey, itemEffectId);

                itemLotMan.AddItem(itemId, consumableItem);
            }
            else
            {
                Item thing = new Item(internalName, isKey ? CommonEnums.ItemCategory.Key : CommonEnums.ItemCategory.Default, itemTextDict[itemId].Item1, itemTextDict[itemId].Item2, iconId, isKey, itemEffectId); ;

                itemLotMan.AddItem(itemId, thing);
            }
        }
    }

    private void LoadItemLotParams()
    {
        XDocument doc = XDocument.Load("Assets/Params/ItemLotParam.txt");

        XElement root = doc.Root;

        foreach (XElement itemLot in root.Elements())
        {
            int itemLotId = int.Parse(itemLot.Attribute("id").Value);
            List<Tuple<int, int>> itemLotList = new List<Tuple<int, int>>();

            foreach (XElement item in itemLot.Elements())
            {
                CommonEnums.ItemCategory type = (CommonEnums.ItemCategory)int.Parse(item.Attribute("category").Value);
                int itemId = int.Parse(item.Attribute("itemId").Value);
                int quantity = int.Parse(item.Attribute("quantity").Value);
                
                
                itemLotList.Add(new Tuple<int, int>(itemId, quantity));


                //if (item.Attribute())
                //Instantiate
                //itemLotMan.AddItemLot((int)item.Attribute("id"), item.Attribute("")
            }
            itemLotMan.AddItemLot(itemLotId, itemLotList);
            //XAttribute name = itemLot.Attribute("internalName");
        }

        
    }
}
