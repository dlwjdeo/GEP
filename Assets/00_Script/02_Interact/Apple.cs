using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Item, IInteractable, IPickupable, IEatable, ICookable
{
    [SerializeField] private float fillAmount = 15f; // 체력 회복량
    [SerializeField] private float eatDuration = 2f; // 먹는 데 걸리는 시간
    [SerializeField] private float cookDuration = 1f; // 조리하는 데 걸리는 시간

    [SerializeField] private GameObject cookedApple;
    [SerializeField] private GameObject TreePrefab;

    public float FillAmount => fillAmount;
    public float CookDuration => cookDuration;

    public void Interact(Player player)
    {
        //사과를 심는 로직 구현
        Debug.Log("사과를 심었습니다!");
    }

    public void Eat(Player player)
    {
        // 사과를 먹는 로직 구현
        Debug.Log("사과 먹기 실행!");
        player.SetEatingDuration(eatDuration); // 플레이어의 먹는 시간 설정
        player.stateMachine.ChangeState(player.stateMachine.eatingState); // 플레이어의 상태를 먹는 상태로 변경
    }

    public void Pickup(Player player)
    {
        if(player.PlayerInventory.HasItem()) return; // 이미 아이템이 있는 경우 픽업 불가
        gameObject.transform.SetParent(player.transform); // 플레이어의 자식으로 설정하여 함께 이동하도록 함
        gameObject.transform.localPosition = Vector3.up; // 플레이어의 위치에 아이템
        player.PlayerInventory.SetCurrentItem(gameObject);
    }

    public GameObject Cooked()
    {
        if (cookedApple == null)
            return null;
    

        return cookedApple;
    }

    public void Plant(Player player)
    {
        if(TreePrefab == null)
            return;
        if (player.stateMachine.currentState == player.stateMachine.eatingState)
            return;
        GameObject.Instantiate(TreePrefab, player.transform.position + player.transform.forward, Quaternion.identity);
        player.PlayerInventory.RemoveItem();
    }
}
