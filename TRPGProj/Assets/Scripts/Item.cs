using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string _internalName;
    private CommonEnums.ItemCategory _category;
    protected string _displayNameText;
    private string _descriptionText;
    private int _displayNameTextId;
    private int _descriptionTextId;
    private int _iconId;
    private bool _isKey;
    protected int _itemEffectId;

    public string DisplayNameText
    {
        get { return _displayNameText; }
    }

    public string DescriptionText
    {
        get { return _descriptionText; }
    }

    public CommonEnums.ItemCategory Category
    {
        get { return _category; }
    }
    

    public Item(string name, CommonEnums.ItemCategory category, string displayNameText, string descriptionText, int iconId, bool isKey, int itemEffectId)
    {
        _internalName = name;
        _category = category;
        _displayNameText = displayNameText;
        _descriptionText = descriptionText;
        _iconId = iconId;
        _isKey = isKey;
        _itemEffectId = itemEffectId;
    }

    
}
