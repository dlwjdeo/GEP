using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected ItemType itemType;
    public ItemType ItemType => itemType;
    protected Player player; // 상호작용하는 플레이어 참조 ex) 상호작용 시 플레이어의 인벤토리에 아이템 추가 등
    public void BindPlayer(Player player)
    {
        this.player = player;
    }
}
