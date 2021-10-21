using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bering.Inventories;
using RPG.Stats;

public class StatsEquipment : Equipment, IModifierProvider
{
    public IEnumerable<float> GetAdditiveModifiers(Stat stat)
    {
        foreach (var slot in GetAllPopulatedSlots())
        {
            var item = GetItemInSlot(slot) as IModifierProvider;
            if (item == null) continue;

            foreach (float Modifier in item.GetAdditiveModifiers(stat))
            {
                yield return Modifier;
            }
        }
    }

    public IEnumerable<float> GetPercentageModifiers(Stat stat)
    {
        foreach (var slot in GetAllPopulatedSlots())
        {
            var item = GetItemInSlot(slot) as IModifierProvider;
            if (item == null) continue;

            foreach (float Modifier in item.GetPercentageModifiers(stat))
            {
                yield return Modifier;
            }
        }
    }
}
