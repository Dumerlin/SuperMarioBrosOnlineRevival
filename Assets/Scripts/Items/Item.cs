using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class for an Item.
/// </summary>
public class Item : MonoBehaviour
{
    public delegate void ItemUsed(Item item);
    public event ItemUsed ItemUsedEvent = null;

    /// <summary>
    /// The name of the item.
    /// </summary>
    public string Name = string.Empty;

    [Tooltip("The general type of item. Stackable: 2+ of the item takes up only one slot. KeyItem: Gets put in KeyItem list.")]
    public Constants.ItemTypes ItemType = Constants.ItemTypes.Standard;

    [Tooltip("Tells whether the item is usable in battle. If not, it won't show up in the battle item menu")]
    public bool UsedInBattle = true;

    /*Kimimaru - Only fill out one of these. There's no support for having a single item heal you and damage an enemy yet.*/

    public HealingData HealingInfo;
    public DamageData DamageInfo;

    private void OnDestroy()
    {
        ItemUsedEvent = null;
    }

    public void UseItem(PlayerStats usedOn)
    {
        //Kimimaru - Placeholder; this will need to be fully implemented later

        usedOn.HealHP(HealingInfo.HP);
        usedOn.HealFP(HealingInfo.FP);

        usedOn.TakeDamage(DamageInfo.Damage, DamageInfo.Element);

        if (ItemUsedEvent != null)
        {
            ItemUsedEvent(this);
        }

        HandlePostUsage();
    }

    /// <summary>
    /// Handles what happens to the item after its used.
    /// The base behavior is to remove it from the inventory and destroy it.
    /// </summary>
    public virtual void HandlePostUsage()
    {
        if (PlayerInventory.HasInstance == true)
            PlayerInventory.Instance.RemoveItem(this);
        Destroy(gameObject);
    }

    [Serializable]
    public struct HealingData
    {
        public int HP;
        public int FP;
    }

    [Serializable]
    public struct DamageData
    {
        public int Damage;
        public Constants.DamageElements Element;
    }
}
