
using UnityEngine;

public class Plant : Item, IPickupable,IInteractable
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
        if (player.PlayerInventory.HasItem()) return; // 이미 아이템이 있는 경우 픽업 불가
        player.PlayerInventory.SetCurrentItem(gameObject);
        gameObject.transform.SetParent(player.transform); // 플레이어의 자식으로 설정하여 함께 이동하도록 함
        gameObject.transform.localPosition = Vector3.up; // 플레이어의 위치에 아이템
    }
}
