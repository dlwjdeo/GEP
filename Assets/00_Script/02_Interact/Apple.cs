using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Item, IInteractable, IPickupable, IEatable
{
    public void Interact(Player player)
    {
        //사과를 심는 로직 구현
        Debug.Log("사과를 심었습니다!");
    }

    public void Eat(Player player)
    {
        // 사과를 먹는 로직 구현
        Debug.Log("사과를 먹었습니다!");
        player.PlayerInventory.RemoveItem(); // 아이템 제거
    }

    public void Pickup(Player player)
    {
        if(player.PlayerInventory.HasItem()) return; // 이미 아이템이 있는 경우 픽업 불가
        player.PlayerInventory.SetCurrentItem(gameObject);
        gameObject.transform.SetParent(player.transform); // 플레이어의 자식으로 설정하여 함께 이동하도록 함
        gameObject.transform.localPosition = Vector3.up; // 플레이어의 위치에 아이템
    }
}
