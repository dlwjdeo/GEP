using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Item,IPickupable
{   
    public bool isFilled = false; //false는 비었음 true는 채워짐

    [SerializeField] private Material filledMaterial; // 양동이가 채워졌을 때의 재질
    [SerializeField] private Material emptyMaterial; // 양동이가 비었을 때의 재질

    public void Pickup(Player player)
    {
        if (player.PlayerInventory.HasItem()) return; // 이미 아이템이 있는 경우 픽업 불가
        player.PlayerInventory.SetCurrentItem(gameObject);
        gameObject.transform.SetParent(player.transform); // 플레이어의 자식으로 설정하여 함께 이동하도록 함
        gameObject.transform.localPosition = Vector3.up; // 플레이어의 위치에 아이템
    }

    public void Fill()
    {
        isFilled = true;
        GetComponent<Renderer>().material = filledMaterial; // 양동이가 채워졌을 때의 재질로 변경
    }

    public void Empty()
    {
        isFilled = false;
        GetComponent<Renderer>().material = emptyMaterial; // 양동이가 비었을 때의 재질로 변경
    }
}
