using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour, IInteractable
{
    public void Interact(Player player)
    {
        var itemType = player.PlayerInventory.GetCurrentItemType();
        if (itemType == ItemType.Plant || itemType == ItemType.Wood || itemType == ItemType.Iron)
        {
            player.SetRepairDuration(2.0f); // 수리하는 데 걸리는 시간 설정
            player.stateMachine.ChangeState(player.stateMachine.repairState); // 플레이어의 상태를 수리 상태로 변경
        }
    }

    public void Repair(Player player)
    {
        var itemType = player.PlayerInventory.GetCurrentItemType();
        var manager = GameManager.Instance;
        switch (itemType)
        {
            case ItemType.Plant:
                {   
                    
                    manager.IncreaseRocketHealth(10.0f);
                    break;
                }
            case ItemType.Wood:
                {
                    manager.IncreaseRocketHealth(50.0f);
                    break;
                }
            case ItemType.Iron:
                {
                    manager.IncreaseRocketHealth(200.0f);
                    break;
                }
            default:
                {
                    Debug.Log("로켓에서 할 수 있는 행동이 없습니다");
                    return;
                }
        }

        player.PlayerInventory.RemoveItem();
    }

}
