using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : Item, IInteractable, IPickupable
{
    public void Interact(Player player)
    {
        //수리 또는 제작
    }

    public void Pickup(Player player)
    {
        if (player.PlayerInventory.HasItem()) return; 
        player.PlayerInventory.SetCurrentItem(gameObject);
        gameObject.transform.SetParent(player.transform); 
        gameObject.transform.localPosition = Vector3.up; 
    }
}
