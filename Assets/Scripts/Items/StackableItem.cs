using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An item that can be stacked. If more are added to the Inventory, it 
/// </summary>
public class StackableItem : Item
{
    /// <summary>
    /// The quantity of the StackableItems.
    /// </summary>
    public int Quantity = 1;

    public override void HandlePostUsage()
    {
        Quantity--;

        //Remove the item if it has no uses remaining
        if (Quantity <= 0)
        {
            if (PlayerInventory.HasInstance == true)
                PlayerInventory.Instance.RemoveItem(this);
            Destroy(gameObject);
        }
    }
}
