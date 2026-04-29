using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : Item, IInteractable, IPickupable
{
    public void Interact(Player player)
    {
        if (player.PlayerInventory.GetCurrentItemType() == ItemType.None)  //만약 맨손이라면 수리
        {
            //수리
            return;
        }
        //여기서부턴 제작

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
