using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject currentItemObject; // 현재 들고 있는 아이템의 게임 오브젝트 참조
    public bool isUnlockFishing;


    private void Start()
    {
        isUnlockFishing = false;
    }
    public ItemType GetCurrentItemType()
    {
        if(currentItemObject != null)
        {
            Item item = currentItemObject.GetComponent<Item>();
            if (item != null)
            {
                return item.ItemType; // 아이템 타입 반환
            }
        }
        return ItemType.None;
    }

    public Item GetCurrentItem()
    {
        if (currentItemObject == null)
            return null;

        var item = currentItemObject.GetComponent<Item>();
        if (item == null)
            return null;
        return item;
    }

    public void SetCurrentItem(GameObject itemObject)
    {
        currentItemObject = itemObject;
    }
    public bool HasItem()
    {
        return currentItemObject != null;
    }
    public void DropItem()
    {
        if (currentItemObject != null)
        {
            // 아이템을 플레이어의 위치에 떨어뜨림
            currentItemObject.transform.SetParent(null);
            currentItemObject.transform.position = transform.position + transform.forward; // 플레이어 앞에 배치
            currentItemObject = null; // 인벤토리에서 아이템 제거
        }
    }
    
    public void RemoveItem()
    {
        if (currentItemObject != null)
        {
            Destroy(currentItemObject); // 아이템 오브젝트 제거
            currentItemObject = null; // 인벤토리에서 아이템 제거
        }
    }
}
