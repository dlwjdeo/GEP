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
                    var item = player.PlayerInventory.GetCurrentItem() as Apple;
                    if (item == null)
                        return;

                    var cookedApplePrefab = item.Cooked();                                      
                    var cookedAppleInstance = GameObject.Instantiate(cookedApplePrefab);

                    player.PlayerInventory.RemoveItem();
                    cookedAppleInstance.GetComponent<CookedApple>().Pickup(player);


                    break;
                }

            case ItemType.Fish:
                {
                    var item = player.PlayerInventory.GetCurrentItem() as Fish;
                    if (item == null)
                        return;

                    var cookedFishPrefab = item.Cooked();
                    var cookedFishInstance = GameObject.Instantiate(cookedFishPrefab);

                    player.PlayerInventory.RemoveItem();
                    cookedFishInstance.GetComponent<CookedFish>().Pickup(player);

                    break;
                }

            //========뗄감 넣기==========
            case ItemType.Plant:
                {
                    player.PlayerInventory.RemoveItem();
                    GameManager.Instance.IncreaseFireHealth(10.0f);
                    break;
                }
            case ItemType.Wood:
                {
                    player.PlayerInventory.RemoveItem();
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
