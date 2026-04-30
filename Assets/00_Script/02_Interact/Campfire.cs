using UnityEngine;

public class Campfire : MonoBehaviour , IInteractable
{
    public void Interact(Player player)
    {
        var itemType = player.PlayerInventory.GetCurrentItemType();
        switch(itemType)
        {   
            //========요리===========
            case ItemType.Apple:
                {   
                    break;
                }
            case ItemType.Fish:
                {
                    break;
                }

            //========뗄감 넣기==========
            case ItemType.Plant:
                {
                    GameManager.Instance.IncreaseFireHealth(10.0f);
                    break;
                }
            case ItemType.Wood:
                {
                    GameManager.Instance.IncreaseFireHealth(30.0f);
                    break;
                }
            default:
                {
                    Debug.Log("모닥불에서 할 수 있는 행동이 없습니다");
                    return;
                }

        }

    }

}
