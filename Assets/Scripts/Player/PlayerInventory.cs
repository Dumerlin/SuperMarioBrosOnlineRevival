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

    public delegate void OnCurrencyAdded(Constants.CurrencyTypes currencyType, uint currencyAdded);
    public event OnCurrencyAdded CurrencyAddedEvent = null;

    public delegate void OnCurrencySubtracted(Constants.CurrencyTypes currencyType, uint currencySubtracted);
    public event OnCurrencySubtracted CurrencySubtractedEvent = null;

    public delegate void OnItemAdded(Item itemAdded);
    public event OnItemAdded ItemAddedEvent = null;

    public delegate void OnItemRemoved(Item itemRemoved);
    public event OnItemRemoved ItemRemovedEvent = null;

    #endregion

    #region Special Instance Methods

    public static PlayerInventory Instance { get { return instance; } }

    public static bool HasInstance { get { return (instance != null); } }

    private static PlayerInventory instance = null;

    #endregion

    /// <summary>
    /// The currency the Player has.
    /// </summary>
    private readonly Dictionary<Constants.CurrencyTypes, uint> Currencies = new Dictionary<Constants.CurrencyTypes, uint>();

    /// <summary>
    /// The Player's items.
    /// </summary>
    //Kimimaru - NOTE: May require a different data structure depending on how we want to handle items
    public readonly List<Item> Items = new List<Item>(Constants.INV_START_SIZE);

    public readonly List<Item> KeyItems = new List<Item>(Constants.KEY_INV_MAX_SIZE);

    private void Awake()
    {
        //Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
        //Destroy other instances
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }

        CurrencyAddedEvent = null;
        CurrencySubtractedEvent = null;
        ItemAddedEvent = null;
        ItemRemovedEvent = null;
    }

    public void AddCurrency(Constants.CurrencyTypes currencyType, uint currencyAdded)
    {
        if (HasCurrency(currencyType) == false)
        {
            Currencies.Add(currencyType, 0);
        }
        
        Currencies[currencyType] = Util.Clamp(Currencies[currencyType] + currencyAdded, Constants.MIN_CURRENCY, Constants.MAX_CURRENCY);

        if (CurrencyAddedEvent != null)
            CurrencyAddedEvent(currencyType, currencyAdded);
    }

    public void SubtractCurrency(Constants.CurrencyTypes currencyType, uint currencySubtracted)
    {
        if (HasCurrency(currencyType) == false)
        {
            Currencies.Add(currencyType, 0);
        }

        Currencies[currencyType] = Util.Clamp(Currencies[currencyType] - currencySubtracted, Constants.MIN_CURRENCY, Constants.MAX_CURRENCY);

        if (CurrencySubtractedEvent != null)
            CurrencySubtractedEvent(currencyType, currencySubtracted);
    }

    private bool HasCurrency(Constants.CurrencyTypes currencyType)
    {
        return Currencies.ContainsKey(currencyType);
    }

    /// <summary>
    /// Tells if the Player has enough currency of a specific type for something.
    /// </summary>
    /// <param name="currencyType"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool HasEnoughCurrency(Constants.CurrencyTypes currencyType, uint amount)
    {
        if (HasCurrency(currencyType) == false)
        {
            if (amount == 0) return true;
            return false;
        }

        return (Currencies[currencyType] >= amount);
    }

    public void AddItem(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Attempting to add a null item to the inventory!");
            return;
        }

        int itemCount = item.ItemType == Constants.ItemTypes.KeyItem ? KeyItems.Count +1 : Items.Count + 1;
        int itemCapacity = item.ItemType == Constants.ItemTypes.KeyItem ? KeyItems.Capacity : Items.Capacity;
        
        if (itemCount > itemCapacity)
        {
            Debug.LogWarning("The item cannot be added because the inventory is full!");
            return;
        }

        //Add key items into the key item inventory
        if (item.ItemType == Constants.ItemTypes.KeyItem)
        {
            KeyItems.Add(item);
        }
        else
        {
            switch (item.ItemType)
            {
                //If the item is stackable, try to find the item in the inventory and add its uses count to the existing one
                case Constants.ItemTypes.Stackable:
                    StackableItem stackableItem = (StackableItem)item;
                    StackableItem stackableInvItem = FindItem(stackableItem.Name, stackableItem.ItemType) as StackableItem;

                    //The item is found in the inventory as a StackableItem, so add its uses
                    if (stackableInvItem != null)
                    {
                        stackableInvItem.Quantity += stackableItem.Quantity;
                    }
                    //The item wasn't found, so add the item to a new slot in the inventory
                    else
                        goto default;
                    break;
                default:
                    Items.Add(item);
                    break;
            }
        }

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

        //Remove key items from the key item inventory
        if (item.ItemType == Constants.ItemTypes.KeyItem)
        {
            KeyItems.Remove(item);
        }
        else
        {
            Items.Remove(item);
        }

        if (ItemRemovedEvent != null)
            ItemRemovedEvent(item);
    }

    public void RemoveItemAt(int index)
    {
        if (index >= 0 && index < Items.Count)
        {
            //Kimimaru - NOTE: Refactor this as we can actually use List.RemoveAt() for performance.
            //Make that the default remove method and use List.IndexOf() to find the index in the reference overload
            RemoveItem(Items[index]);
        }
        else
        {
            Debug.LogError("Index " + index + " is not within the Item count of " + Items.Count);
        }
    }

    /// <summary>
    /// Finds the first instance of an item by name.
    /// </summary>
    /// <param name="itemName">The name of the item.</param>
    /// <param name="itemType">The type of item.</param>
    /// <returns></returns>
    public Item FindItem(string itemName, Constants.ItemTypes itemType)
    {
        if (itemType == Constants.ItemTypes.KeyItem)
            return KeyItems.Find((item) => item.name == itemName);

        return Items.Find((item) => item.Name == itemName);
    }

    /// <summary>
    /// Finds all instances of an item by name.
    /// </summary>
    /// <param name="itemName">The name of the item.</param>
    /// <param name="itemType">The type of item.</param>
    /// <returns></returns>
    public List<Item> FindAllItems(string itemName, Constants.ItemTypes itemType)
    {
        if (itemType == Constants.ItemTypes.KeyItem)
            return KeyItems.FindAll((item) => item.name == itemName);

        return Items.FindAll((item) => item.Name == itemName);
    }
}
