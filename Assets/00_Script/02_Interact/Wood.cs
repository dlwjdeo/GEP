using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wood : Item, IInteractable, IPickupable
{
    public void Interact(Player player)
    {   
        

        var CM = GameManager.Instance.CraftManager;
        CM.Craft(player.PlayerInventory.GetCurrentItem(), this, player);

    }

    public void Pickup(Player player)
    {
        if (player.PlayerInventory.HasItem()) return;
        player.PlayerInventory.SetCurrentItem(gameObject);
        gameObject.transform.SetParent(player.transform);
        gameObject.transform.localPosition = Vector3.up;
    }
}
