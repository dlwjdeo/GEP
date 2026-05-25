using UnityEngine;

public class Comparer
{   
    public Comparer(Player player)
    {
        _player = player;
    }
    private Player _player;

   public string CheckAllInteractable(Item item, GameObject targetObj)
    {
        ItemType itemType = item != null ? item.ItemType : ItemType.None;

        if (targetObj == null)
        {
            if (checkPlant(itemType))
                return "Plant";

            return string.Empty;
        }

        if (checkCook(item, targetObj))
            return "Cook";
        if (checkFishing(itemType, targetObj))
            return "Fishing";
        if (checkBurning(itemType, targetObj))
            return "Burning";
        if (checkLogging(itemType, targetObj))
            return "Logging";
        if (checkRepair(itemType, targetObj))
            return "Repair";
        if (checkCraft(itemType, targetObj))
            return "Craft";
        return string.Empty;
    }

    private bool checkPlant(ItemType itemType)
    {
        return itemType == ItemType.Apple;
    }

    private bool checkFishing(ItemType itemType, GameObject targetObj)
    {   
        if(_player != null)
        {
            if (_player.PlayerInventory.isUnlockFishing == false)
                return false;
        }
        return itemType == ItemType.None && targetObj.TryGetComponent<Pond>(out _);
    }

    private bool checkCook(Item item, GameObject targetObj)
    {
        return item is ICookable && targetObj.TryGetComponent<Campfire>(out _);
    }

    private bool checkRepair(ItemType itemType, GameObject targetObj)
    {
        bool canRepairMaterial =
            itemType == ItemType.Iron ||
            itemType == ItemType.Wood ||
            itemType == ItemType.Plant;

        return canRepairMaterial && targetObj.TryGetComponent<Rocket>(out _);
    }

    private bool checkCraft(ItemType itemType, GameObject targetObj)
    {

        if (!targetObj.TryGetComponent<Item>(out Item targetItem))
            return false;

        return GameManager.Instance.CraftManager.IsCraft(itemType, targetItem.ItemType);
    }

    private bool checkLogging(ItemType itemType, GameObject targetObj)
    {
        return itemType == ItemType.None && targetObj.TryGetComponent<Tree>(out _);
    }

    private bool checkBurning(ItemType itemType, GameObject targetObj)
    {
        return ((itemType == ItemType.Wood) || (itemType==ItemType.Plant)) && targetObj.TryGetComponent<Campfire>(out _);
    }
}