using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
   public Consumable(string name, CommonEnums.ItemCategory category, string displayNameText, string descriptionText, int iconId, bool isKey, int itemEffectId) : base(name, category, displayNameText, descriptionText, iconId, isKey, itemEffectId)
   {

   }

    public void Use(Character chr)
    {
        //hardcode effects for the time being
        if (_itemEffectId == 0)
        {
            chr.AddHp(10);
        }
        else if (_itemEffectId == 1)
        {
            chr.AddMana(10);
        }
    }
}
