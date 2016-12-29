using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player inventory.
/// </summary>
[DisallowMultipleComponent]
public class PlayerInventory : MonoBehaviour
{
    #region Events

    public delegate void OnCoinsAdded(int coinsAdded);
    public event OnCoinsAdded CoinsAddedEvent = null;

    public delegate void OnCoinsSubtracted(int coinsSubtracted);
    public event OnCoinsSubtracted CoinsSubtractedEvent = null;

    public delegate void OnItemAdded(Item itemAdded);
    public event OnItemAdded ItemAddedEvent = null;

    public delegate void OnItemRemoved(Item itemRemoved);
    public event OnItemRemoved ItemRemovedEvent = null;

    #endregion

    /// <summary>
    /// The number of coins the Player has.
    /// </summary>
    public int Coins = 0;//{ get; private set; }

    /// <summary>
    /// The Player's items.
    /// </summary>
    //NOTE: May require a different data structure depending on how we want to handle items
    public List<Item> Items = new List<Item>(Constants.INV_START_SIZE);

    private void Awake()
    {
        
    }

    private void OnDestroy()
    {
        CoinsAddedEvent = null;
        CoinsSubtractedEvent = null;
        ItemAddedEvent = null;
        ItemRemovedEvent = null;
    }

    public void AddCoins(int coins)
    {
        Coins = Mathf.Clamp(Coins + coins, Constants.MIN_COINS, Constants.MAX_COINS);

        if (CoinsAddedEvent != null)
            CoinsAddedEvent(coins);
    }

    public void SubtractCoins(int coins)
    {
        Coins = Mathf.Clamp(Coins - coins, Constants.MIN_COINS, Constants.MAX_COINS);

        if (CoinsSubtractedEvent != null)
            CoinsSubtractedEvent(coins);
    }

    public void AddItem(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Attempting to add a null item to the inventory!");
            return;
        }

        int itemCount = Items.Count + 1;
        if (itemCount > Items.Capacity)
        {
            Debug.LogWarning("The item cannot be added because the inventory is full!");
            return;
        }

        Items.Add(item);

        if (ItemAddedEvent != null)
            ItemAddedEvent(item);
    }

    public void RemoveItem(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Attempting to remove a null item from the inventory!");
            return;
        }

        Items.Remove(item);

        if (ItemRemovedEvent != null)
            ItemRemovedEvent(item);
    }

    public void RemoveItemAt(int index)
    {
        if (index >= 0 && index < Items.Count)
        {
            RemoveItem(Items[index]);
        }
        else
        {
            Debug.LogError("Index " + index + " is not within the Item count of " + Items.Count);
        }
    }
}
