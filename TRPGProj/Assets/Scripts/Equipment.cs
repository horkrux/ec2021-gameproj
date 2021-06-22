using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public Equipment(string name, CommonEnums.ItemCategory category, string displayNameText, string descriptionText, int iconId, bool isKey, int itemEffectId) : base(name, category, displayNameText, descriptionText, iconId, isKey, itemEffectId)
    {

    }
}
